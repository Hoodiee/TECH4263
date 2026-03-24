namespace EquipmentClient.Models
{
    public class Equipment
    {
        private static int _nextId = 1;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
        public string Location { get; set; }

        public Equipment(string name, string category, string status, string location)
        {
            Id = _nextId++;
            Name = name;
            Category = category;
            Status = status;
            Location = location;
        }


    }
}
