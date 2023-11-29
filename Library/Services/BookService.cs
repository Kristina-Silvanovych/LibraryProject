using DataLayer;
using DataLayer.Entityes;
using Library.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class BookService : IBook
    {
        private readonly EFDBContext _context;

        public BookService(EFDBContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Book>> GetBook()
        {
            return await this._context.Book.ToListAsync();
        }

        public async Task<Book> CreateBook(Book book)
        {
            this._context.Book.Add(book);
            await this._context.SaveChangesAsync();

            return book;
        }

        public bool ExistBook(string title)
        {
            var bookAlreadyExists = this._context.Book.Any(x => x.Title == title);
            if (bookAlreadyExists)
            {
                return true;
            }
            return false;
        }

        public async Task<Book> FindBookByTitle(string title)
        {
            var book = await this._context.Book.FirstOrDefaultAsync(x => x.Title == title);

            return book;
        }
        async Task<bool> IBook.DeleteBook(int id)
        {
            var book = await this._context.Book.FindAsync(id);

            if (book == null)
            {
                return false;
            }

            _context.Book.Remove(book);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Book> UpdateBook(Book book)
        {
            Console.WriteLine(book.Title);
            _context.Book.Update(book);
            await this._context.SaveChangesAsync();

            var updateBook = await _context.Book.FirstOrDefaultAsync(a => a.Title == book.Title);
            Console.WriteLine(updateBook.Title);

            if (updateBook != null)
            {
                return updateBook;
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        public async Task<List<Book>> FindBookByAuthorId(int authorId)
        {
            return await this._context.Book.Where(b => b.AuthorId == authorId).ToListAsync();
        }
    }
}
