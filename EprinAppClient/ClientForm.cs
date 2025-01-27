using System.Data;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using EprinAppLibrary;

namespace EprinAppClient
{
    public partial class ClientForm : Form
    {
        private TcpClient _tcpClient;
        private NetworkStream _stream;
        public ClientForm()
        {
            InitializeComponent();
            ToggleControls(false);
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
                CleanTextBox();
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
                CleanTextBox();
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
                CleanTextBox();
            }
            else
            {
                MessageBox.Show("Failed to delete person");
            }
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            try
            {
                string ipAddress = ipTextBox.Text;
                int port = int.Parse(portTextBox.Text);

                _tcpClient = new TcpClient();
                _tcpClient.Connect(ipAddress, port);
                _stream = _tcpClient.GetStream();

                ToggleControls(true);
                MessageBox.Show($"Connected to server {ipAddress}:{port}.");
                LoadPeople();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error connecting to server: {ex.Message}");
            }
        }

        private void ToggleControls(bool enabled)
        {
            deleteButton.Enabled = enabled;
            addButton.Enabled = enabled;
            updateButton.Enabled = enabled;
            firstNameTextBox.Enabled = enabled;
            lastNameTextBox.Enabled = enabled;
            connectButton.Enabled = !enabled;
            discButton.Enabled = enabled;
        }

        private void discButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (_tcpClient != null)
                {
                    _stream?.Close();
                    _tcpClient.Close();
                }
                ToggleControls(false);
                peopleListBox.Items.Clear();
                MessageBox.Show("Disconnected from server!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error disconnecting: {ex.Message}");
            }
        }

        private void peopleListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (peopleListBox.SelectedItem is Person selectedPerson)
            {
                firstNameTextBox.Text = selectedPerson.FirstName;
                lastNameTextBox.Text = selectedPerson.LastName;
            }
        }

        private void CleanTextBox()
        {
            firstNameTextBox.Clear();
            lastNameTextBox.Clear();
        }
    }
}
