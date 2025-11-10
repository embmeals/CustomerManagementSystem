using CustomerManagementSystem.Data;
using CustomerManagementSystem.Models;

namespace CustomerManagementSystem
{
    public delegate void CustomerEventHandler(Customer customer);

    public class CustomerManager
    {
        private readonly CustomerCollection<Customer> _customers;
        public event CustomerEventHandler? CustomerAdded;

       public event CustomerEventHandler? CustomerUpdated;
       public event CustomerEventHandler? CustomerDeleted;

        public CustomerManager(CustomerCollection<Customer> customers)
        {
            _customers = customers;
        }

        public bool AddCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            if (!customer.Validate())
                throw new ArgumentException("Customer data is invalid.");

            if (_customers.Any(c => c == customer))
            {
                return false; // Duplicate customer
            }

            _customers.Add(customer);

            CustomerAdded?.Invoke(customer);

            return true;
        }

        public void UpdateCustomer(int index, string firstName, string lastName, string email)
        {
            if (index < 0 || index >= _customers.Count)
                throw new IndexOutOfRangeException("Invalid customer index.");

            var customer = _customers[index];
            customer.UpdateDetails(firstName, lastName, email);

            if (!customer.Validate())
                throw new ArgumentException("Updated customer data is invalid.");

            CustomerUpdated?.Invoke(customer);
        }

        public void DeleteCustomer(int index)
        {
            if (index < 0 || index >= _customers.Count)
                throw new IndexOutOfRangeException("Invalid customer index.");

            var customer = _customers[index];
            _customers.RemoveAt(index);

            CustomerDeleted?.Invoke(customer);
        }

        public Customer GetCustomer(int index)
        {
            return _customers[index];
        }

        public List<Customer> GetAllCustomers()
        {
            return _customers.GetAll();
        }

        public int CustomerCount => _customers.Count;
    }
}
