using BookStoresAPI.Models;
using System.Collections;

namespace BookStoresAPI.DataAccess.Interface
{
    public interface IBookRepositories
    {
        Task<bool> CreateAsync(Book book);
        Task<bool> DeleteAsync(string id);
        Task<IEnumerable> GetAllBook();
        Task<bool> UpdateAsync(Book book);
    }
}
