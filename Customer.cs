namespace CustomerManagementSystem.Models
{
    /// <summary>
    /// Represents a customer with basic information
    /// </summary>
    public class Customer : IManageable
    {
        private static int _nextId = 1;

        public int Id { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string FullName => $"{FirstName} {LastName}";

        public Customer(string firstName, string lastName, string email)
        {
            Id = _nextId++;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public void UpdateDetails(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public bool Validate()
        {
            return !string.IsNullOrWhiteSpace(FirstName) &&
                   !string.IsNullOrWhiteSpace(LastName) &&
                   !string.IsNullOrWhiteSpace(Email) &&
                   Email.Contains("@");
        }

        public string GetInfo()
        {
            return $"ID: {Id}, Name: {FullName}, Email: {Email}";
        }

        public override string ToString()
        {
            return GetInfo();
        }

        public static bool operator ==(Customer? c1, Customer? c2)
        {
            if (ReferenceEquals(c1, c2)) return true;
            if (c1 is null || c2 is null) return false;
            return c1.Email.Equals(c2.Email, StringComparison.OrdinalIgnoreCase);
        }

        public static bool operator !=(Customer? c1, Customer? c2)
        {
            return !(c1 == c2);
        }

        public override bool Equals(object? obj)
        {
            return obj is Customer customer && this == customer;
        }

        public override int GetHashCode()
        {
            return Email.ToLower().GetHashCode();
        }
    }
}
