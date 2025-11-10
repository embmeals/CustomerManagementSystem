using CustomerManagementSystem.Data;
using CustomerManagementSystem.Models;

namespace CustomerManagementSystem
{
    public partial class Form1 : Form
    {
        private readonly CustomerCollection<Customer> _customerCollection;
        private readonly CustomerManager _customerManager;

        public Form1()
        {
            InitializeComponent();

            _customerCollection = new CustomerCollection<Customer>();
            _customerManager = new CustomerManager(_customerCollection);

            _customerManager.CustomerAdded += OnCustomerAdded;
            _customerManager.CustomerUpdated += OnCustomerUpdated;
            _customerManager.CustomerDeleted += OnCustomerDeleted;

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

                var customer = new Customer(
                    txtFirstName.Text.Trim(),
                    txtLastName.Text.Trim(),
                    txtEmail.Text.Trim()
                );

                bool added = _customerManager.AddCustomer(customer);

                if (!added)
                {
                    ShowStatus("A customer with this email already exists!", false);
                    return;
                }

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

                _customerManager.UpdateCustomer(
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

                var customer = _customerManager.GetCustomer(selectedIndex);

                var result = MessageBox.Show(
                    $"Are you sure you want to delete {customer.FullName}?",
                    "Confirm Deletion",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    _customerManager.DeleteCustomer(selectedIndex);

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

        private void OnCustomerUpdated(Customer customer)
        {
            ShowStatus($"Customer updated successfully: {customer.FullName}", true);
        }

        private void OnCustomerDeleted(Customer customer)
        {
            ShowStatus($"Customer deleted successfully: {customer.FullName}", true);
        }

        private void RefreshCustomerList()
        {
            lvCustomers.Items.Clear();

            var customers = _customerManager.GetAllCustomers();

            foreach (var customer in customers)
            {
                var item = new ListViewItem(customer.Id.ToString());
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
                    var customer = _customerManager.GetCustomer(selectedIndex);

                    txtFirstName.Text = customer.FirstName;
                    txtLastName.Text = customer.LastName;
                    txtEmail.Text = customer.Email;
                }
            };
        }
    }
}
