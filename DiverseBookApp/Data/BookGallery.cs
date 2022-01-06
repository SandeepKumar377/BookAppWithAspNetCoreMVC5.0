namespace DiverseBookApp.Data
{
    public class BookGallery
    {
        public int Id { get; set; }
        public int BookId { get; set; }

        public string Name { get; set; }
        public string Url { get; set; }
        public Books Books { get; set; }
    }
}
