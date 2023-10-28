namespace AutoService.Models
{
    public class Customer
    {
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }

        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(Name) ||
                string.IsNullOrWhiteSpace(Adress) ||
                string.IsNullOrWhiteSpace(Phone))
            { return false; }
            return true;
        }
    }
}
