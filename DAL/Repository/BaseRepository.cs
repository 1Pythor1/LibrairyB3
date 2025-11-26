
using Microsoft.EntityFrameworkCore.Storage;

namespace DAL.Repository
{
    public abstract class BaseRepository
    {
        protected readonly LibrairyContext _dbContext;

        public BaseRepository(LibrairyContext dbContext) =>
            _dbContext = dbContext;

        protected async Task<T> MakeTransaction<T>(Func<Task<T>> func)
        {
            using IDbContextTransaction transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                T value = await func();

                await transaction.CommitAsync();
                return value;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
