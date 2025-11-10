<<<<<<< HEAD
namespace CustomerManagementSystem.Models
{
    /// <summary>
    /// Interface for manageable entities that can be updated and validated
    /// </summary>
    public interface IManageable
    {
        void UpdateDetails(string firstName, string lastName, string email);
        bool Validate();
        string GetInfo();
    }
}
=======
namespace CustomerManagementSystem.Models
{
    /// <summary>
    /// Interface for manageable entities that can be updated and validated
    /// </summary>
    public interface IManageable
    {
        void UpdateDetails(string firstName, string lastName, string email);
        bool Validate();
        string GetInfo();
    }
}
>>>>>>> 951c9e8ce6fb53e87b8fa3c11efbf189d41d40cd
