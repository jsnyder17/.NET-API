namespace APIThingOrWhateverIdk.Models
{
    public class ItemModel
    {
        public Int64 Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Type { get; set; } = String.Empty;
        public Double Price { get; set; }
        public Int64 Quantity { get; set; }
        public DateTime InsertDate { get; set; }
    }
}
