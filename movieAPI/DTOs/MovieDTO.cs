namespace movieAPI.DTOs
{
    public class MovieDTO
    {
        public string Title { get; set; }

        public int Year { get; set; }

        public double Rate { get; set; }

        public IFormFile? Poster { get; set; }

        public byte GenreId { get; set; }
    }
}
