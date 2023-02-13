using BookStoresAPI.Models;

namespace BookStoresAPI.DataAccess.Interface
{
    public interface IPressRepositories
    {
        Task<IEnumerable<Press>> GetAllPress();
    }
}
