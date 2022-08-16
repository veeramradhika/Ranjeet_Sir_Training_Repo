using AutoMapper;
using BooksAPI.API.API;
using BooksAPI.API.Models;
using MailChimp.Net.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BooksAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly BooksDbContext _cartcontext;
        private readonly IMapper _mapper;

        public CartController(BooksDbContext cartcontext, IMapper mapper)
        {
            _cartcontext = cartcontext;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult<AddCart>> CartAdd(ModelApiCart modelApi)
        {
            var book = _mapper.Map<AddCart>(modelApi);
            _cartcontext.Cart.Add(book);
            await _cartcontext.SaveChangesAsync();
            return Ok(_cartcontext.Cart);
        }
        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            if (_cartcontext.Book == null)
            {
                return NoContent();
            }
            var book_details = await _cartcontext.Cart.ToListAsync();
            return Ok(book_details);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletebyId(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            if (_cartcontext.Cart == null)
            {
                return NoContent();
            }
            var book = await _cartcontext.Cart.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            _cartcontext.Cart.Remove(book);
            await _cartcontext.SaveChangesAsync();
            return Ok(_cartcontext.Cart);
        }
    }
}
