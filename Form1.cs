using CustomerManagementSystem.Data;
using CustomerManagementSystem.Models;

namespace CustomerManagementSystem
{
    public partial class Form1 : Form
    {
        private readonly CustomerCollection<Customer> collection;
        private readonly CustomerManager manager;

        public Form1()
        {
            InitializeComponent();

            collection = new CustomerCollection<Customer>();
            manager = new CustomerManager(collection);

            manager.CustomerAdded += OnCustomerAdded;

            RefreshCustomerList();
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtFirstName.Text))
                {
                    ShowStatus("Please enter a first name.", false);
                    txtFirstName.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtLastName.Text))
                {
                    ShowStatus("Please enter a last name.", false);
                    txtLastName.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    ShowStatus("Please enter an email address.", false);
                    txtEmail.Focus();
                    return;
                }

                var newCustomer = new Customer(
                    txtFirstName.Text.Trim(),
                    txtLastName.Text.Trim(),
                    txtEmail.Text.Trim()
                );

                manager.AddCustomer(newCustomer);

                ClearInputFields();

                RefreshCustomerList();
            }
            catch (ArgumentException ex)
            {
                ShowStatus($"Error: {ex.Message}", false);
            }
            catch (Exception ex)
            {
                ShowStatus($"Unexpected error: {ex.Message}", false);
            }
        }

        private void btnUpdateCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvCustomers.SelectedIndices.Count == 0)
                {
                    ShowStatus("Please select a customer to update.", false);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                    string.IsNullOrWhiteSpace(txtLastName.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    ShowStatus("Please fill in all fields.", false);
                    return;
                }

                int selectedIndex = lvCustomers.SelectedIndices[0];

                manager.UpdateCustomer(
                    selectedIndex,
                    txtFirstName.Text.Trim(),
                    txtLastName.Text.Trim(),
                    txtEmail.Text.Trim()
                );

                ClearInputFields();

                RefreshCustomerList();
            }
            catch (ArgumentException ex)
            {
                ShowStatus($"Error: {ex.Message}", false);
            }
            catch (Exception ex)
            {
                ShowStatus($"Unexpected error: {ex.Message}", false);
            }
        }

        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvCustomers.SelectedIndices.Count == 0)
                {
                    ShowStatus("Please select a customer to delete.", false);
                    return;
                }

                int selectedIndex = lvCustomers.SelectedIndices[0];

                var customer = manager.GetCustomer(selectedIndex);

                var result = MessageBox.Show(
                    $"Are you sure you want to delete {customer.FullName}?",
                    "Confirm Deletion",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    collection.RemoveAt(selectedIndex);

                    ClearInputFields();

                    RefreshCustomerList();
                }
            }
            catch (Exception ex)
            {
                ShowStatus($"Error: {ex.Message}", false);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OnCustomerAdded(Customer customer)
        {
            ShowStatus($"Customer added successfully: {customer.FullName}", true);
        }

        private void RefreshCustomerList()
        {
            lvCustomers.Items.Clear();

            var customers = manager.GetAllCustomers();

            foreach (var customer in customers)
            {
                var item = new ListViewItem(customer.CustomerID.ToString());
                item.SubItems.Add(customer.FullName);
                item.SubItems.Add(customer.Email);
                lvCustomers.Items.Add(item);
            }

            if (customers.Count == 0)
            {
                ShowStatus("No customers in the system.", true);
            }
            else
            {
                ShowStatus($"Total customers: {customers.Count}", true);
            }
        }

        private void ClearInputFields()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtEmail.Clear();
            txtFirstName.Focus();
            lvCustomers.SelectedIndices.Clear();
        }

        private void ShowStatus(string message, bool isSuccess)
        {
            lblStatus.Text = message;
            lblStatus.ForeColor = isSuccess ? System.Drawing.Color.Green : System.Drawing.Color.Red;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            lvCustomers.SelectedIndexChanged += (s, args) =>
            {
                if (lvCustomers.SelectedIndices.Count > 0)
                {
                    int selectedIndex = lvCustomers.SelectedIndices[0];
                    var customer = manager[selectedIndex];

                    txtFirstName.Text = customer.FirstName;
                    txtLastName.Text = customer.LastName;
                    txtEmail.Text = customer.Email;
                }
            };
        }
    }
}
