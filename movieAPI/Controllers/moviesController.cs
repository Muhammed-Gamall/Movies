
using AutoMapper;
using movieAPI.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace movieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class moviesController : ControllerBase
    {

        private readonly IMapper _mapper;

        private readonly MovieDbContext _context;

        private new List<string> _allwedExtentions = new List<string> { ".jpg , .png" };
        // private long maxSize = 1048576;
        public moviesController(MovieDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        //[HttpGet]
        //public async Task<IActionResult> GetMovies()
        //{
        //    var film = await _context.movies
        //        .Include(m => m.Genre)
        //        .Select(m => new GetDTO
        //        {
        //            Id = m.Id,
        //            Title = m.Title,
        //            Rate = m.Rate,
        //            Year = m.Year,
        //            GenreId = m.GenreId,
        //            GenreName = m.Genre.Name,
        //            Poster = m.Poster
        //        })
        //        .ToListAsync();
        //    return Ok(film);
        //}
        // After mapping ->>

        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            var film = await _context.movies.Include(m => m.Genre)
                .ToListAsync();
            var x=_mapper.Map<List<GetDTO>>(film);
            
            return Ok(x);
        }

        //[HttpGet("Genreid")]
        //public async Task<IActionResult> GetMovieByGenreID(byte Genreid)
        //{
        //    var film = await _context.movies.Where(g => g.GenreId == Genreid).Include(m => m.Genre)

        //        .Select(m => new GetDTO
        //        {
        //            Id = m.Id,
        //            Title = m.Title,
        //            Rate = m.Rate,
        //            Year = m.Year,
        //            GenreId = m.GenreId,
        //            GenreName = m.Genre.Name,
        //            Poster = m.Poster
        //        })
        //        .ToListAsync();

        //    if (film == null)
        //        return NotFound("no movies under this category");

        //    return Ok(film);
        //}
        //
        //after Mapping ->>

       

        [HttpGet("Genreid")]
        public async Task<IActionResult> GetMovieByGenreID(byte Genreid)
        {
            var film = await _context.movies.Where(g => g.GenreId == Genreid).Include(m => m.Genre)
                     .ToListAsync();
            if (film == null)
                return NotFound("no movies under this category");

            var x=_mapper.Map<List<GetDTO>>(film);

            return Ok(x);
        }


        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetMovieByID(int id)
        //{
        //    var film = await _context.movies.Include(m => m.Genre).SingleOrDefaultAsync(m => m.Id == id);

        //    if (film == null)
        //        return NotFound("no movies with this id");

        //    var dto = new GetDTO
        //    {
        //        Id = film.Id,
        //        Title = film.Title,
        //        Rate = film.Rate,
        //        Year = film.Year,
        //        GenreId = film.GenreId,
        //        GenreName = film.Genre.Name,
        //        Poster = film.Poster
        //    };

        //    return Ok(dto);
        //}
        //after Mapping ->>


        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieByID(int id)
        {
            var film = await _context.movies.Include(m => m.Genre).SingleOrDefaultAsync(m => m.Id == id);

            if (film == null)
                return NotFound("no movies with this id");

            var x = _mapper.Map<List<GetDTO>>(film);
            return Ok(x);
        }


        //[HttpPost]
        //public async Task<IActionResult> CreateMovie([FromForm] MovieDTO dto)
        //{
        //    if (dto.Poster == null)
        //        return BadRequest("the poster is required");

        //    //if (!_allwedExtentions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
        //    //    return BadRequest("Only .jpg or .png");

        //    if (dto.Poster.Length > 1048576)
        //        return BadRequest("max allawed size for the poster is 1Mb");

        //    var isValidGenre = await _context.Genres.AnyAsync(g => g.Id == dto.GenreId);
        //    if (!isValidGenre)
        //        return BadRequest("Invalid GenreId");

        //    using var dataStream = new MemoryStream();
        //    await dto.Poster.CopyToAsync(dataStream);

        //    var film = new movie
        //    {
        //        Title = dto.Title,
        //        Year = dto.Year,
        //        Rate = dto.Rate,
        //        GenreId = dto.GenreId,
        //        Poster = dataStream.ToArray()
        //    };

        //    await _context.AddAsync(film);

        //    await _context.SaveChangesAsync();
        //    return Ok(film);
        //}
        //after Mapping ->>


        [HttpPost]
        public async Task<IActionResult> CreateMovie([FromForm] MovieDTO dto)
        {
            if (dto.Poster == null)
                return BadRequest("the poster is required");

            //if (!_allwedExtentions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
            //    return BadRequest("Only .jpg or .png");

            if (dto.Poster.Length > 1048576)
                return BadRequest("max allawed size for the poster is 1Mb");

            var isValidGenre = await _context.Genres.AnyAsync(g => g.Id == dto.GenreId);
            if (!isValidGenre)
                return BadRequest("Invalid GenreId");

            using var dataStream = new MemoryStream();
            await dto.Poster.CopyToAsync(dataStream);

            var film = _mapper.Map<movie>(dto);

            film.Poster = dataStream.ToArray();

            await _context.AddAsync(film);
            await _context.SaveChangesAsync();
            return Ok(film);
        }

        //after Mapping ->>

        [HttpPut ("{id}")]
        public async Task<IActionResult> UpdateMovie(int id,[FromForm] MovieDTO dto)
        {
            var film = await _context.movies.Include(m => m.Genre).SingleOrDefaultAsync(m => m.Id == id);

            if (film == null)
                return NotFound("there's no movie with id:" + id);

            var isValidGenre = await _context.Genres.AnyAsync(g => g.Id == dto.GenreId);
            if (!isValidGenre)
                return BadRequest("Invalid GenreId");

            if(dto.Poster != null)
            {
                if (!_allwedExtentions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                    return BadRequest("Only .jpg or .png");

                if (dto.Poster.Length > 1048576)
                    return BadRequest("max allawed size for the poster is 1Mb");

                using var dataStream = new MemoryStream();
                await dto.Poster.CopyToAsync(dataStream);
               film.Poster = dataStream.ToArray();

            }
            //film.Title = dto.Title;
            //film.Year = dto.Year;
            //film.Rate = dto.Rate;
            //film.GenreId = dto.GenreId;

             film = _mapper.Map<movie>(dto);
            _context.SaveChanges();
            return Ok(film);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var film= await _context.movies.FindAsync(id);
            if (film == null)
                return NotFound("there's no movie with id:"+id);

            _context.Remove(film);
            _context.SaveChanges();

            return Ok(film);
        }


    }
}
