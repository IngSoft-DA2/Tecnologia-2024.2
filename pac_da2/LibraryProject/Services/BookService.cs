using LibraryProject.Entities;
using LibraryProject.Interfaces;
using LibraryProject.Data;
using System.Collections.Generic;
using System.Linq;

namespace LibraryProject.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryContext _context;

        public BookService(LibraryContext context)
        {
            _context = context;
        }

        public Book CreateBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
            return book;
        }

        public Book? GetBookById(int id)
        {
            return _context.Books.Find(id);
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }

        public void UpdateBook(Book book)
        {
            var existingBook = _context.Books.Find(book.Id);
            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                existingBook.PublishedDate = book.PublishedDate;
                existingBook.IsAvailable = book.IsAvailable;
                _context.Books.Update(existingBook);
                _context.SaveChanges();
            }
        }

        public void DeleteBook(int id)
        {
            var book = _context.Books.Find(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }
    }
}