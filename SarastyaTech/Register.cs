using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace SarastyaTech
{
    public partial class Register : Form
    {
        private const string ConnectionString = "Host=localhost;Username=postgres;Password=SMKbisa1234;Database=sarastya";

        public Register()
        {
            InitializeComponent();
            InitializeUI();
        }

        private void InitializeUI()
        {
            // Add labels
            Label lblUsername = new Label();
            lblUsername.Text = "Username:";
            lblUsername.Location = new System.Drawing.Point(50, 50);
            this.Controls.Add(lblUsername);

            Label lblPassword = new Label();
            lblPassword.Text = "Password:";
            lblPassword.Location = new System.Drawing.Point(50, 80);
            this.Controls.Add(lblPassword);

            Label lblNama = new Label();
            lblNama.Text = "Nama: ";
            lblNama.Location = new System.Drawing.Point(50, 110);
            this.Controls.Add(lblNama);

            Label lblKelas = new Label();
            lblKelas.Text = "Kelas: ";
            lblKelas.Location = new System.Drawing.Point(50, 140);
            this.Controls.Add(lblKelas);

            Label lblProgli = new Label();
            lblProgli.Text = "Progli: ";
            lblProgli.Location = new System.Drawing.Point(50, 170);
            this.Controls.Add(lblProgli);

            // Add textboxes
            TextBox txtUsername = new TextBox();
            txtUsername.Location = new System.Drawing.Point(150, 50);
            this.Controls.Add(txtUsername);

            TextBox txtPassword = new TextBox();
            txtPassword.Location = new System.Drawing.Point(150, 80);
            txtPassword.PasswordChar = '*'; // Mask the password
            this.Controls.Add(txtPassword);

            TextBox txtNama = new TextBox();
            txtNama.Location = new System.Drawing.Point(150, 110);
            this.Controls.Add(txtNama);

            TextBox txtKelas = new TextBox();
            txtKelas.Location = new System.Drawing.Point(150, 140);
            this.Controls.Add(txtKelas);

            TextBox txtProgli = new TextBox();
            txtProgli.Location = new System.Drawing.Point(150, 170);
            this.Controls.Add(txtProgli);

            // Add a button for registration
            Button btnRegister = new Button();
            btnRegister.Text = "Register";
            btnRegister.Location = new System.Drawing.Point(150, 200);
            btnRegister.Click += (s, ev) => RegisterUser(txtUsername.Text, txtPassword.Text, txtNama.Text, txtKelas.Text, txtProgli.Text);
            this.Controls.Add(btnRegister);
        }

        private void RegisterUser(string username, string password, string nama, string kelas, string progli)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "INSERT INTO users (username, password, nama, kelas, progli) VALUES (@username, @password, @nama, @kelas, @progli)";
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password); // Hash the password in a real application
                    command.Parameters.AddWithValue("@nama", nama);
                    command.Parameters.AddWithValue("@kelas", kelas);
                    command.Parameters.AddWithValue("@progli", progli);

                    command.ExecuteNonQuery();

                    MessageBox.Show("Registration successful!");

                    // Redirect to the Login form after successful registration
                    this.Hide(); // Hide the current form
                    Login loginForm = new Login();
                    loginForm.Show();
                }
            }
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }
    }
    
    /*
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Register());
        }
    }
    */
}
