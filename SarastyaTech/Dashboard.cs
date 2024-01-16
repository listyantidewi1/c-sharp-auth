using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace SarastyaTech
{
    public partial class Dashboard : Form
    {
        private const string ConnectionString = "Host=localhost;Username=postgres;Password=SMKbisa1234;Database=sarastya";

        public Dashboard()
        {
            InitializeComponent();
            InitializeUI();
            LoadUsers();
        }

        private void InitializeUI()
        {
            // Add UI components for the dashboard

            // Add a DataGridView to display the list of users
            DataGridView dgvUsers = new DataGridView();
            dgvUsers.Location = new System.Drawing.Point(50, 50);
            dgvUsers.Size = new System.Drawing.Size(300, 200);
            this.Controls.Add(dgvUsers);

            // Add a button for editing selected user
            Button btnEditUser = new Button();
            btnEditUser.Text = "Edit User";
            btnEditUser.Location = new System.Drawing.Point(50, 270);
            btnEditUser.Click += (s, ev) => EditSelectedUser(dgvUsers);
            this.Controls.Add(btnEditUser);
        }

        private void LoadUsers()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM users";

                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        ((DataGridView)this.Controls[0]).DataSource = dataTable;
                    }
                }
            }
        }

        private void EditSelectedUser(DataGridView dgvUsers)
        {
            // Check if a user is selected in the DataGridView
            if (dgvUsers.SelectedRows.Count > 0)
            {
                // Get the selected user's data (example: user_id is assumed to be in the first column)
                int userId = Convert.ToInt32(dgvUsers.SelectedRows[0].Cells[0].Value);

                // Open the EditUserForm passing the userId for editing
                EditUserForm editUserForm = new EditUserForm(userId);
                editUserForm.ShowDialog();

                // Reload users after editing
                LoadUsers();
            }
            else
            {
                MessageBox.Show("Please select a user to edit.");
            }
        }
    }
}
