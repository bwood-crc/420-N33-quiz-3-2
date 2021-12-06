using System;
using System.Data.SqlClient;
using System.Windows;

namespace Quiz3_2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateRows()) // Make sure the minimum and max size of the database field is respected.
            {
                InsertStore(); // Do the insert.
                ClearErrors(); // Clear the errors.
                ClearTextBoxes(); // Clear the text boxes.
            }
        }

        private void ClearTextBoxes()
        {
            txtAddress.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtId.Text = string.Empty;
            txtName.Text = string.Empty;
            txtState.Text = string.Empty;
            txtZip.Text = string.Empty;
        }

        private void ClearErrors()
        {
            lblErrId.Visibility = lblErrName.Visibility = lblErrAddress.Visibility =
                lblErrCity.Visibility = lblErrState.Visibility = lblErrZip.Visibility = Visibility.Hidden;
        }

        private Boolean ValidateRows()
        {
            ClearErrors();

            Boolean isValidated = true;

            if (txtId.Text.Length > 4 || txtId.Text.Length == 0)
            {
                lblErrId.Visibility = Visibility.Visible;
                isValidated = false;
            }

            if (txtAddress.Text.Length > 40 || txtAddress.Text.Length == 0)
            {
                lblErrAddress.Visibility = Visibility.Visible;
                isValidated = false;
            }

            if (txtName.Text.Length > 40 || txtName.Text.Length == 0)
            {
                lblErrName.Visibility = Visibility.Visible;
                isValidated = false;
            }

            if (txtCity.Text.Length > 20 || txtCity.Text.Length == 0)
            {
                lblErrCity.Visibility = Visibility.Visible;
                isValidated = false;
            }

            if (txtState.Text.Length > 2 || txtState.Text.Length == 0)
            {
                lblErrState.Visibility = Visibility.Visible;
                isValidated = false;
            }

            if (txtZip.Text.Length > 5 || txtZip.Text.Length == 0)
            {
                lblErrZip.Visibility = Visibility.Visible;
                isValidated = false;
            }

            return isValidated;


        }

        private void InsertStore()
        {
            SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=pubs;Integrated Security=True;");

            string query = "INSERT INTO stores (stor_id, stor_name, stor_address, city, state, zip)" +
                " VALUES (@id, @name, @address, @city, @state, @zip)";
            SqlCommand cmd = new SqlCommand(query, con);

            //Pass values to Parameters
            cmd.Parameters.AddWithValue("@id", txtId.Text);
            cmd.Parameters.AddWithValue("@name", txtName.Text);
            cmd.Parameters.AddWithValue("@address", txtAddress.Text);
            cmd.Parameters.AddWithValue("@city", txtCity.Text);
            cmd.Parameters.AddWithValue("@state", txtState.Text);
            cmd.Parameters.AddWithValue("@zip", txtZip.Text);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Store Added!", "YAY");
                Console.WriteLine("Records Inserted Successfully");
            }
            catch (SqlException e)
            {
                MessageBox.Show("ERROR", "Oops", MessageBoxButton.OK, MessageBoxImage.Error);
                MessageBox.Show(e.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
