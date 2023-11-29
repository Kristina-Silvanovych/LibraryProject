using DataLayer;
using DataLayer.Entityes;
using Library.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class AuthorService : IAuthor
    {
        private readonly EFDBContext _context;

        public AuthorService(EFDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAuthor()
        {
            return await this._context.Author.ToListAsync();
        }

        public async Task<Author> CreateAuthor(Author author)
        {
            this._context.Author.Add(author);
            await this._context.SaveChangesAsync();

            return author;
        }

        async Task<bool> IAuthor.DeleteAuthor(int id)
        {
            var author = await this._context.Author.FindAsync(id);

            if (author == null)
            {
                return false;
            }
            _context.Author.Remove(author);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Author> UpdateAuthor(Author author)
        {
            _context.Author.Update(author);
            await this._context.SaveChangesAsync();

            var updateAuthor = await _context.Author.FirstOrDefaultAsync(a => a.Surname == author.Surname);

                if (updateAuthor != null)
                {
                    return updateAuthor;
                }
                else
                {
                    throw new NullReferenceException();
                }
        }

        public bool ExistAuthor(string surname, string name)
        {
            var authorAlreadyExists = this._context.Author.Any(x => x.Surname == surname && x.Name == name);
            if (authorAlreadyExists)
            {
                return true;
            }
            return false;
        }

        public async Task<Author> FindAuthorBySurname(string surname)
        {
            var author = await this._context.Author.FirstOrDefaultAsync(x => x.Surname == surname);

            return author;
        }
    }
}
