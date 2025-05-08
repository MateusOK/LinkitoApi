using System.Linq.Expressions;
using Linkito.Domain.Common;
using Linkito.Domain.Repositories;
using Linkito.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Linkito.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{

    private readonly LinkitoDbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(LinkitoDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }
    
    
    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.FirstOrDefaultAsync(predicate);
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }
}