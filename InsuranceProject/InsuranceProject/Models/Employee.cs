namespace InsuranceDay1.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; } //LoginID
        public string? Password { get; set; }
        public bool IsActive { get; set; }
    }
}
