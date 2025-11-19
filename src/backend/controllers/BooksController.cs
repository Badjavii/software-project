using Microsoft.AspNetCore.Mvc;
using backend.models;
using System.Text.Json;

namespace backend.controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController :ControllerBase {

    [HttpGet]
    public IActionResult GetBooks() {
        Book b = new Book();
        String rootFile = "data\\book.json";
        List<Book> books = b.LoadBooks(rootFile);
        return Ok(books);
    }

    [HttpPost]
    public IActionResult PostBook([FromBody] Book book)
    {
        if (book == null) return BadRequest("Book is null");

        string rootFile = "data\\book.json";
        var loader = new Book();
        List<Book> books = loader.LoadBooks(rootFile) ?? new List<Book>();

        books.Add(book);

        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(books, options);
        System.IO.File.WriteAllText(rootFile, json);

        // Devolver 201 Created con el libro añadido
        return CreatedAtAction(nameof(GetBooks), null, book);
    }
}