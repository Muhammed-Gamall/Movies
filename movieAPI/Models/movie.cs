namespace movieAPI.Models
{
    public class movie
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public int Year { get; set; }

        public double Rate { get; set; }

        public byte[]? Poster { get; set; }

        public byte GenreId { get; set; }

        public Genre Genre { get; set; }

    }
}
