using BooksMVCWebAPI.API.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BooksMVCWebAPI.API.Controllers
{
    public class BookController : Controller
    {
        Uri baseUri = new Uri("https://localhost:7259/api");
        HttpClient client = new HttpClient();

        
        List<BookViewModel> BookList = new List<BookViewModel>();
        List<BookViewModel> bookdata = new List<BookViewModel>();

        public IActionResult Index()
        {
            client.BaseAddress = baseUri;
            HttpResponseMessage response = client.GetAsync(baseUri + "/Books").Result;
            if (response.IsSuccessStatusCode)
            {
                string BookData = response.Content.ReadAsStringAsync().Result;
                BookList = JsonConvert.DeserializeObject<List<BookViewModel>>(BookData); ;

            }
           
            return View(BookList);
        }
        public IActionResult Create()
        {
            return View();
        }
        
        public async Task<IActionResult> CreatBook(BookViewModel books)
        {
            using (var client = new HttpClient())
            {
            //https://localhost:7259/api/Books
                client.BaseAddress = new Uri("https://localhost:7259/api/Books");
                var postTask = client.PostAsJsonAsync<BookViewModel>("Books", books);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, "server error");
                return View(result);
            }
        }
        public ActionResult Edit(int id)
        {
            client.BaseAddress = baseUri;
            HttpResponseMessage response = client.GetAsync(baseUri + "/Books").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            BookList = JsonConvert.DeserializeObject<List<BookViewModel>>(data);
            var book = BookList.Where(e => e.BookId == id).FirstOrDefault();
            return View(book);
        }
        [HttpPost]
        public IActionResult Save(BookViewModel books)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7259/api/");
                var put = client.PutAsJsonAsync($"Books?BookId={books.BookId}", books);
                put.Wait();
                var result = put.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

            }
            ModelState.AddModelError(string.Empty, "server error");
            return View();

        }
        public IActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7259/api/Books/");
                var delete = client.DeleteAsync($"id?id={id}");
                delete.Wait();
                var result = delete.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
               
            }
            ModelState.AddModelError(string.Empty, "server error");
            return View();

        }
        public ActionResult Search(string searchString)
        {
            List<BookViewModel> bookList = new List<BookViewModel>();
            HttpResponseMessage response = client.GetAsync(baseUri + "/Books/" + searchString).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                BookList = JsonConvert.DeserializeObject<List<BookViewModel>>(data);
            }
            return View("Index", BookList);
        }
        public ActionResult Details(int id)
        {
            client.BaseAddress = baseUri;
            HttpResponseMessage response = client.GetAsync(baseUri + $"/Books/id?id={id}").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            var book = JsonConvert.DeserializeObject<BookViewModel>(data);
            return View(book);

        }
        public ActionResult AddCart(int id)
        {
            client.BaseAddress = baseUri;
            HttpResponseMessage response = client.GetAsync(baseUri + "/Cart").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            bookdata = JsonConvert.DeserializeObject<List<BookViewModel>>(data);
            var book = bookdata.Where(e => e.BookId == id).FirstOrDefault();
            if (book == null)
            {
                return NoContent();
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7259/api/");

                //HTTP POST

                var postTask = client.PostAsJsonAsync<BookViewModel>("Cart", book);

                postTask.Wait();

                var result = postTask.Result;

                if (result.IsSuccessStatusCode)

                {

                    return RedirectToAction("Index");
                }

            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            return View();

        }
        public ActionResult GetCart()
        {
            client.BaseAddress = baseUri;
            HttpResponseMessage response = client.GetAsync(baseUri + "/Cart").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            bookdata = JsonConvert.DeserializeObject<List<BookViewModel>>(data);
            return View(bookdata);
        }
        public ActionResult DeleteinCart(int id)
        {
            using (var client = new HttpClient())

            {
                client.BaseAddress = new Uri("https://localhost:7259/api/Cart/");
                var delete = client.DeleteAsync($"{id}");
                delete.Wait();
                var results = delete.Result;
                if (results.IsSuccessStatusCode)
                {
                    return RedirectToAction("Cart");
                }
            }
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View();
        }




    }
}
