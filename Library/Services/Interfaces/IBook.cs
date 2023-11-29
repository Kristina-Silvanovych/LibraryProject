using DataLayer.Entityes;

namespace Library.Services.Interfaces
{
    public interface IBook
    {
        Task<IEnumerable<Book>> GetBook();
        Task<Book> CreateBook(Book book);
        Task<bool> DeleteBook(int id);
        Task<Book> UpdateBook(Book book);
        Task<Book> FindBookByTitle(string title);
        bool ExistBook(string title);

        Task<List<Book>> FindBookByAuthorId(int authorId);
    }
}
