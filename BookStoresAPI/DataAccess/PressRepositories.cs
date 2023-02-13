using BookStoresAPI.DataAccess.Interface;
using BookStoresAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoresAPI.DataAccess
{
    public class PressRepositories : IPressRepositories
    {
        private readonly BookStoreContext _dbContext;

        public PressRepositories(BookStoreContext context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<Press>> GetAllPress()
        {
            return await _dbContext.Presses.ToListAsync();
        }
    }
}
