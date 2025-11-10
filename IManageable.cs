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
