namespace CustomerManagementSystem
{
    public interface IManageable
    {
        void UpdateDetails();
        void DeleteRecord();
        bool Validate();
        string GetInfo();
    }
}
