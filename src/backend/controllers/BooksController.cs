using Microsoft.AspNetCore.Mvc;
using backend.models;
using System.IO;

namespace backend.controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{

    [HttpGet]
    public IActionResult GetBooks()
    {
        Book b = new Book();
        var rootFile = Path.Combine(AppContext.BaseDirectory, "data", "book.json");
        List<Book> books = b.LoadBooks(rootFile);
        return Ok(books);
    }
}
