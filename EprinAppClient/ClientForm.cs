using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace EprinAppClient
{
    public partial class ClientForm : Form
    {
        private readonly TcpClient _tcpClient;
        private readonly NetworkStream _stream;
        public ClientForm()
        {
            InitializeComponent();
            _tcpClient = new TcpClient();
            _tcpClient.Connect("127.0.0.1", 12345);
            _stream = _tcpClient.GetStream();
            LoadPeople();
        }
        private void LoadPeople()
        {
            var response = SendRequest("GET");
            var people = JsonSerializer.Deserialize<Person[]>(response);

            peopleListBox.Items.Clear();
            if (people != null)
            {
                foreach (var person in people)
                {
                    peopleListBox.Items.Add(person);
                }
            }
        }

        private string SendRequest(string request)
        {
            try
            {
                var buffer = Encoding.UTF8.GetBytes(request);
                _stream.Write(buffer, 0, buffer.Length);


                buffer = new byte[4096];
                var bytesRead = _stream.Read(buffer, 0, buffer.Length);
                return Encoding.UTF8.GetString(buffer, 0, bytesRead);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error communicating with the server: {ex.Message}");
                return "ERROR";
            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _stream?.Close();
            _tcpClient?.Close();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            var firstName = firstNameTextBox.Text;
            var lastName = lastNameTextBox.Text;

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                MessageBox.Show("Please fill in both textboxes.");
                return;
            }

            var response = SendRequest($"ADD|{firstName}|{lastName}");
            if (response != "ERROR")
            {
                LoadPeople();
                firstNameTextBox.Clear();
                lastNameTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Failed to add person");
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            if (peopleListBox.SelectedItem is not Person selectedPerson)
            {
                MessageBox.Show("Please select a row to update");
                return;
            }

            var firstName = firstNameTextBox.Text;
            var lastName = lastNameTextBox.Text;

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            {
                MessageBox.Show("Please fill first name and last name textboxes.");
                return;
            }

            var response = SendRequest($"UPDATE|{selectedPerson.Id}|{firstName}|{lastName}");
            if (response == "OK")
            {
                LoadPeople();
                firstNameTextBox.Clear();
                lastNameTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Failed to update person");
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (peopleListBox.SelectedItem is not Person selectedPerson)
            {
                MessageBox.Show("Please select a row to delete");
                return;
            }

            var response = SendRequest($"DELETE|{selectedPerson.Id}");
            if (response == "OK")
            {
                LoadPeople();
            }
            else
            {
                MessageBox.Show("Failed to delete person");
            }
        }
    }
}
