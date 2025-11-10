using Microsoft.AspNetCore.Mvc;
using backend.models;

namespace backend.controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController :ControllerBase {

    [HttpGet]
    public IActionResult GetBooks() {
        Book b = new Book();
        String rootFile = "models\\files\\book.json";
        List<Book> books = b.LoadBooks(rootFile);
        return Ok(books);
    }
}