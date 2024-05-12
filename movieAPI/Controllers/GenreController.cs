namespace movieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly MovieDbContext _context;

        public GenreController(MovieDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetGenres()
        {
            var category = await _context.Genres.ToListAsync();
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> PostCategories(Genre dto)
        {
            var category = new Genre { Name = dto.Name };

            await _context.Genres.AddAsync(category);
            await _context.SaveChangesAsync();
            return Ok(category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategories(int id, [FromBody] Genre name)
        {
            var genre = await _context.Genres.SingleOrDefaultAsync(g => g.Id == id);
            if (genre == null)
                return NotFound("no category was found with id:" + id);
           name.Name = genre.Name;
            await _context.SaveChangesAsync();
            return Ok(genre);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletCategories(int id)
        {
            var genre = await _context.Genres.SingleOrDefaultAsync(g => g.Id == id);
            if (genre == null)
                return NotFound("no category was found with id:" + id);
            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
            return Ok(genre);
        }


    }
}
