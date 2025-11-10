using CustomerManagementSystem.Data;
using CustomerManagementSystem.Models;

namespace CustomerManagementSystem
{
    public delegate void CustomerAddedHandler(Customer customer);

    public class CustomerManager
    {
        private readonly CustomerCollection<Customer> customers;
        public event CustomerAddedHandler? CustomerAdded;

        public CustomerManager(CustomerCollection<Customer> customerCollection)
        {
            customers = customerCollection;
        }

        public void AddCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            if (!customer.Validate())
                throw new ArgumentException("Customer data is invalid.");

            if (customers.Any(c => c == customer))
            {
                throw new InvalidOperationException("A customer with this email already exists.");
            }

            customers.Add(customer);

            CustomerAdded?.Invoke(customer);
        }

        public void UpdateCustomer(int index, string firstName, string lastName, string email)
        {
            if (index < 0 || index >= customers.Count)
                throw new IndexOutOfRangeException("Invalid customer index.");

            var customer = customers[index];
            customer.FirstName = firstName;
            customer.LastName = lastName;
            customer.Email = email;
            customer.UpdateDetails();

            if (!customer.Validate())
                throw new ArgumentException("Updated customer data is invalid.");
        }

        public void DeleteCustomer(int index)
        {
            if (index < 0 || index >= customers.Count)
                throw new IndexOutOfRangeException("Invalid customer index.");

            var customer = customers[index];
            customer.DeleteRecord();
            customers.RemoveAt(index);
        }

        public Customer this[int index]
        {
            get { return customers[index]; }
        }

        public Customer GetCustomer(int index)
        {
            return customers[index];
        }

        public List<Customer> GetAllCustomers()
        {
            return customers.GetAll();
        }

        public int CustomerCount => customers.Count;
    }
}
