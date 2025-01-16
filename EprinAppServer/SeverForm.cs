namespace EprinAppServer
{
    public partial class SeverForm : Form
    {
        public SeverForm()
        {
            InitializeComponent();

            const int port = 12345;
            const string dataFilePath = "people.json";

            var server = new Server(port, dataFilePath);
            server.Start();
        }
    }
}
