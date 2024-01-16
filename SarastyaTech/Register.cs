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

            // Add textboxes
            TextBox txtUsername = new TextBox();
            txtUsername.Location = new System.Drawing.Point(150, 50);
            this.Controls.Add(txtUsername);

            TextBox txtPassword = new TextBox();
            txtPassword.Location = new System.Drawing.Point(150, 80);
            txtPassword.PasswordChar = '*'; // Mask the password
            this.Controls.Add(txtPassword);

            // Add a button for registration
            Button btnRegister = new Button();
            btnRegister.Text = "Register";
            btnRegister.Location = new System.Drawing.Point(150, 110);
            btnRegister.Click += (s, ev) => RegisterUser(txtUsername.Text, txtPassword.Text);
            this.Controls.Add(btnRegister);
        }

        private void RegisterUser(string username, string password)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "INSERT INTO users (username, password) VALUES (@username, @password)";
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password); // Hash the password in a real application

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
