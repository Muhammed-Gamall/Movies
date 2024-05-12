﻿namespace movieAPI.DTOs
{
    public class GetDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public int Year { get; set; }

        public double Rate { get; set; }

        public byte[]? Poster { get; set; }

       public byte GenreId { get; set; }

       public string GenreName { get; set; }
    }
}
