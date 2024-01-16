using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace SarastyaTech
{
    public partial class EditUserForm : Form
    {
        private const string ConnectionString = "Host=localhost;Username=postgres;Password=SMKbisa1234;Database=sarastya";
        private int userId;

        public EditUserForm(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            InitializeUI();
            LoadUserData();
        }

        private void InitializeUI()
        {
            // Add UI components for editing user

            // Add a label for username
            Label lblUsername = new Label();
            lblUsername.Text = "Username:";
            lblUsername.Width = 50;
            lblUsername.Location = new System.Drawing.Point(50, 50);
            this.Controls.Add(lblUsername);

            // Add a textbox for editing username
            TextBox txtUsername = new TextBox();
            txtUsername.Location = new System.Drawing.Point(150, 50);
            this.Controls.Add(txtUsername);

            // Add a button for saving changes
            Button btnSaveChanges = new Button();
            btnSaveChanges.Text = "Save Changes";
            btnSaveChanges.Location = new System.Drawing.Point(150, 80);
            btnSaveChanges.Click += (s, ev) => SaveChanges(txtUsername.Text);
            this.Controls.Add(btnSaveChanges);
        }

        private void LoadUserData()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM users WHERE user_id = @userId";
                    command.Parameters.AddWithValue("@userId", userId);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Load user data into the form controls
                            ((TextBox)this.Controls[1]).Text = reader["username"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("User not found.");
                            this.Close();
                        }
                    }
                }
            }
        }

        private void SaveChanges(string newUsername)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "UPDATE users SET username = @newUsername WHERE user_id = @userId";
                    command.Parameters.AddWithValue("@newUsername", newUsername);
                    command.Parameters.AddWithValue("@userId", userId);

                    command.ExecuteNonQuery();

                    MessageBox.Show("Changes saved successfully.");
                    this.Close(); // Close the EditUserForm after saving changes
                }
            }
        }
    }
}
