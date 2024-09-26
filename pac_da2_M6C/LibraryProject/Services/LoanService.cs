using LibraryProject.Entities;
using LibraryProject.Interfaces;
using LibraryProject.Data;
using System.Linq;
using System.Collections.Generic;

namespace LibraryProject.Services
{
    public class LoanService : ILoanService
    {
        private readonly IBookService _bookService;
        private readonly IUserService _userService;
        private readonly LibraryContext _context;

        // Inyectamos IBookService, IUserService, y LibraryContext a través del constructor
        public LoanService(IBookService bookService, IUserService userService, LibraryContext context)
        {
            _bookService = bookService;
            _userService = userService;
            _context = context;
        }

        // Método para prestar un libro
        public void LoanBook(int userId, int bookId)
        {
            // Obtener el libro y validar su disponibilidad
            var book = _bookService.GetBookById(bookId);
            if (book == null || !book.IsAvailable)
            {
                throw new KeyNotFoundException("El libro no existe o no está disponible.");
            }

            // Validar que el usuario existe y está activo
            var user = _userService.GetUserById(userId);
            if (user == null || !user.IsActive)
            {
                throw new KeyNotFoundException("El usuario no existe o no está activo.");
            }

            // Crear el préstamo
            var loan = new Loan
            {
                UserId = userId,
                BookId = bookId,
                LoanDate = System.DateTime.Now
            };

            // Marcar el libro como no disponible
            book.IsAvailable = false;

            // Agregar el préstamo y actualizar el estado del libro
            _context.Loans.Add(loan);
            _context.Books.Update(book);
            _context.SaveChanges();
        }

        // Método para devolver un libro
        public void ReturnBook(int userId, int bookId)
        {
            // Buscar el préstamo activo para el libro y usuario
            var loan = _context.Loans.FirstOrDefault(l => l.UserId == userId && l.BookId == bookId && l.ReturnDate == null);
            if (loan == null)
            {
                throw new KeyNotFoundException("No se encontró un préstamo activo para este usuario y libro.");
            }

            // Registrar la fecha de devolución
            loan.ReturnDate = System.DateTime.Now;

            // Marcar el libro como disponible nuevamente
            var book = _context.Books.Find(bookId);
            if (book != null)
            {
                book.IsAvailable = true;
                _context.Books.Update(book);
            }

            // Actualizar el préstamo
            _context.Loans.Update(loan);
            _context.SaveChanges();
        }
    }
}