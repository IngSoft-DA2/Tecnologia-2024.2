using LibraryProject.Entities;
namespace LibraryProject.Interfaces
{
    public interface IBookService
    {
        Book CreateBook(Book book);
        Book? GetBookById(int id);
        IEnumerable<Book> GetAllBooks();
        void UpdateBook(Book book);
        void DeleteBook(int id);
    }
}