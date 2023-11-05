namespace InsuranceDay1.Models
{
    public class Documents
    {
        public int Id { get; set; }
        public string DocumentType { get; set; } 
        //Document Upload Attribute
        public string DocumentInformation { get; set; } //If upload option is not available
        public Customer Customer { get; set; }
    }
}
