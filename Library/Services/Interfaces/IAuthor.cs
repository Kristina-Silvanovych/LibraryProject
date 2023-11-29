using DataLayer.Entityes;

namespace Library.Services.Interfaces
{
    public interface IAuthor
    {
        Task<IEnumerable<Author>> GetAuthor();
        Task<Author> CreateAuthor(Author author);
        Task<bool> DeleteAuthor(int id);
        Task<Author> UpdateAuthor(Author author);

        Task<Author> FindAuthorBySurname(string surname);
        bool ExistAuthor(string surname, string name);
    }
}
