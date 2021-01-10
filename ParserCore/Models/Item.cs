namespace ParserCore.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Count { get; set; }
        public int SecondSubId { get; set; }
        public bool SoldOut { get; set; }
        public string ImageSrc { get; set; }
    }
}
