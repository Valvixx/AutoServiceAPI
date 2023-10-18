namespace AutoServiceAPI.Models
{
    public class Car
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int ReleaseYear { get; set; }
        public string VIN { get; set; }


        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(Brand) ||
                string.IsNullOrWhiteSpace(Model) ||
                string.IsNullOrWhiteSpace(VIN) ||
                ReleaseYear <= 0) { return false; }
            return true;
        } 
    }
}