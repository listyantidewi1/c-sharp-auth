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

            // Add a label for nama
            Label lblNama = new Label();
            lblNama.Text = "Nama:";
            lblNama.Width = 50;
            lblNama.Location = new System.Drawing.Point(50, 80);
            this.Controls.Add(lblNama);

            // Add a textbox for editing username
            TextBox txtNama = new TextBox();
            txtNama.Location = new System.Drawing.Point(150, 80);
            this.Controls.Add(txtNama);

            // Add a label for nama
            Label lblKelas = new Label();
            lblKelas.Text = "Kelas:";
            lblKelas.Width = 50;
            lblKelas.Location = new System.Drawing.Point(50, 110);
            this.Controls.Add(lblKelas);

            // Add a textbox for editing username
            TextBox txtKelas = new TextBox();
            txtKelas.Location = new System.Drawing.Point(150, 110);
            this.Controls.Add(txtKelas);

            // Add a label for nama
            Label lblProgli = new Label();
            lblProgli.Text = "Progli:";
            lblProgli.Width = 50;
            lblProgli.Location = new System.Drawing.Point(50, 140);
            this.Controls.Add(lblProgli);

            // Add a textbox for editing username
            TextBox txtProgli = new TextBox();
            txtProgli.Location = new System.Drawing.Point(150, 140);
            this.Controls.Add(txtProgli);

            // Add a button for saving changes
            Button btnSaveChanges = new Button();
            btnSaveChanges.Text = "Save Changes";
            btnSaveChanges.Location = new System.Drawing.Point(150, 170);
            btnSaveChanges.Click += (s, ev) => SaveChanges(txtUsername.Text, txtNama.Text, txtKelas.Text, txtProgli.Text);
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
                            ((TextBox)this.Controls[3]).Text = reader["nama"].ToString();
                            ((TextBox)this.Controls[5]).Text = reader["kelas"].ToString();
                            ((TextBox)this.Controls[7]).Text = reader["progli"].ToString();
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

        private void SaveChanges(string newUsername, string newNama, string newKelas, string newProgli)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "UPDATE users SET username = @newUsername, nama = @newNama, kelas = @newKelas, progli = @newProgli WHERE user_id = @userId";
                    command.Parameters.AddWithValue("@newUsername", newUsername);
                    command.Parameters.AddWithValue("@newNama", newNama);
                    command.Parameters.AddWithValue("@newKelas", newKelas);
                    command.Parameters.AddWithValue("@newProgli", newProgli);
                    command.Parameters.AddWithValue("@userId", userId);

                    command.ExecuteNonQuery();

                    MessageBox.Show("Changes saved successfully.");
                    this.Close(); // Close the EditUserForm after saving changes
                }
            }
        }

        private void EditUserForm_Load(object sender, EventArgs e)
        {

        }
    }
}
