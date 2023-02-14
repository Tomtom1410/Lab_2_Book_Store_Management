using BookStoresAPI.DataAccess.Interface;
using BookStoresAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace BookStoresAPI.DataAccess
{
    public class BookRepositoties : IBookRepositories
    {
        private readonly BookStoreContext _dbContext;
        private readonly ILogger<BookStoreContext> _logger;

        public BookRepositoties(BookStoreContext context, ILogger<BookStoreContext> logger)
        {
            _logger = logger;
            _dbContext = context;
            _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            if (context.Books.Count() == 0)
            {
                foreach (var book in init())
                {
                    context.Books.Add(book);
                    context.Presses.Add(book.Press);
                };
                context.SaveChanges();
            }
        }

        public async Task<bool> CreateAsync(Book book)
        {
            try
            {
                _dbContext.Books.Add(book);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var book = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
            if(book == null) return false;
            try
            {
                _dbContext.Books.Remove(book);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<IEnumerable> GetAllBook()
        {
            return await _dbContext.Books.Include(c => c.Press).ToListAsync();
        }

        public async Task<bool> UpdateAsync(Book book)
        {
            try
            {
                _dbContext.Books.Update(book);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        private IList<Book> init()
        {
            var books = new List<Book>();
            Press press = new Press
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Press 1",
                Category = Category.Book,
            };

            Book book = new Book
            {
                Id = Guid.NewGuid().ToString(),
                IBSN = "121212121212",
                Title = "Book 1",
                Author = "ABC",
                Price = 59.99m,
                City = "abc",
                Street = "xyz",
                Press = press,
                PressId = press.Id
            };
            books.Add(book);

            press = new Press
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Press 1",
                Category = Category.EBook,
            };
            book = new Book
            {
                Id = Guid.NewGuid().ToString(),
                IBSN = "121212121212",
                Title = "Course 1",
                Author = "ABC",
                Price = 59.99m,
                City = "abc",
                Street = "xyz",
                Press = press,
                PressId = press.Id
            };
            books.Add(book);

            press = new Press
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Press 1",
                Category = Category.Book,
            };
            book = new Book
            {
                Id = Guid.NewGuid().ToString(),
                IBSN = "121212121212",
                Title = "Lesson 1",
                Author = "ABC",
                Price = 59.99m,
                City = "abc",
                Street = "xyz",
                Press = press,
                PressId = press.Id
            };
            books.Add(book);

            press = new Press
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Press 1",
                Category = Category.Magazine,
            };
            book = new Book
            {
                Id = Guid.NewGuid().ToString(),
                IBSN = "121212121212",
                Title = "Period 1",
                Author = "ABC",
                Price = 59.99m,
                City = "abc",
                Street = "xyz",
                Press = press,
                PressId = press.Id
            };
            books.Add(book);
            return books;
        }
    }
}
