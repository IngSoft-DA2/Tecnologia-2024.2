namespace LibraryProject.Interfaces
{
    public interface ILoanService
    {
        void LoanBook(int userId, int bookId);
        void ReturnBook(int userId, int bookId);
    }
}