using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Pagination;
using System;

namespace RestaurantReservation.Db.Repositories
{
    public abstract class Repository<T> : IRepositoryServices<T> where T : BaseEntity
    {
        private readonly RestaurantReservationDbContext _dbContext;
        private readonly DbSet<T> _entities;
        private const int DefaultPageSize = 10;

        public Repository(RestaurantReservationDbContext dbContext)
        {
            _dbContext = dbContext;
            _entities = _dbContext.Set<T>();
        }
        public async Task<(IEnumerable<T>, PaginationMetaData)> GetAllAsync(int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }
            if (pageSize < 1)
            {
                pageSize = DefaultPageSize;
            }
            var entities = _entities as IQueryable<T>;
            var totalRecords = await entities.CountAsync();
            var paginationMetaData = new PaginationMetaData(totalRecords, pageSize, pageNumber);
            var entitiesToReturn = await entities
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();
            return (entitiesToReturn, paginationMetaData);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _entities
                .Where(entity => entity.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            _entities.Remove(entity);
            return await Save();
        }

        public async Task<bool> Save()
        {
            var saved = await _dbContext.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> AddAsync(T entity)
        {
            await _entities.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            return await Save();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _entities.AnyAsync(entity => entity.Id == id);
        }
    }
}
