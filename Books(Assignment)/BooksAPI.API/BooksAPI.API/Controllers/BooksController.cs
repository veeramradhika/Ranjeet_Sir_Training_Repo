using AutoMapper;
using BooksAPI.API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BooksAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BooksDbContext _booksDbContext;
        private readonly IMapper _mapper;
        public BooksController(BooksDbContext booksDbContext, IMapper mapper)
        {
            _booksDbContext = booksDbContext;
        }
        [HttpPost]
        public ActionResult Create(BooksModel booksModel)
        {
            if (booksModel == null)
            {
                return BadRequest();
            }
            else
            {
                var books = _mapper.Map<BooksModel>(booksModel);
                _booksDbContext.Book.Add(booksModel);
                _booksDbContext.SaveChanges();
                return Ok(_booksDbContext.Book);
            }
        }
        [HttpPut]
        public IActionResult UpdateBook(int BookId, BooksModel booksModel)
        {
            if (booksModel == null)
            {
                return BadRequest("Book object can't be null");
            }
            if (_booksDbContext == null)
            {
                return NotFound("Table doesn't exists");
            }
            var Update = _booksDbContext.Book.Where(e => e.BookId == BookId).FirstOrDefault();
            if (Update == null)
            {
                return NotFound("Book with this BookId doesn't exists");
            }
            var books = _mapper.Map<BooksModel>(booksModel);
            _booksDbContext.Book.Remove(Update);
            Update.BookName = booksModel.BookName;
            Update.BookZoner = booksModel.BookZoner;
            Update.RelaseDate = booksModel.RelaseDate;
            Update.Cost = booksModel.Cost;
            Update.BookImage = booksModel.BookImage;

            _booksDbContext.Book.Update(Update);
            _booksDbContext.SaveChanges();

            return Ok(_booksDbContext.Book);
        }
        [HttpGet]
        public ActionResult<IEnumerable<BooksModel>> GetBooks()
        {
            return Ok(_booksDbContext.Book);
        }
        [HttpDelete("id")]
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            else
            {
                var delbook = _booksDbContext.Book.Find(id);
                _booksDbContext.Book.Remove(delbook);
                _booksDbContext.SaveChanges();
                return Ok("Deleted Successfully");
            }

        }
        [HttpGet("{searchString}")]

        public async Task<IActionResult> Show(string searchString)
        {
            if (searchString == null)
            {
                return BadRequest("input can't be null");
            }
            if (_booksDbContext.Book == null)
            {
                return NotFound("Table doesn't exists");
            }
            var books = _booksDbContext.Book.Where(e => e.BookName.Contains(searchString) || e.BookZoner.Contains(searchString)).ToList();
            if (books == null)
            {
                return NotFound("Record doesn't exists");
            }
            return Ok(books);
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetbyId(int id)
        {
            if (_booksDbContext.Book == null)
            {
                return NoContent();
            }
            if (id == null)
            {
                return BadRequest();
            }
            var book = await _booksDbContext.Book.FindAsync(id);
            return Ok(book);

        }
    }
}
