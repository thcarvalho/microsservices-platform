using System.Linq.Expressions;
using MongoDB.Driver;
using MSP.Courses.Domain.Entities;
using MSP.Courses.Domain.Interfaces;

namespace MSP.Courses.Infra.Data.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ICoursesContext _context;

    public CategoryRepository(ICoursesContext context)
    {
        _context = context;
    }

    public async Task<Category> CreateAsync(Category category)
    {
        await _context.Categories.InsertOneAsync(category);
        return category;
    }

    public async Task<bool> ExistsAsync(Expression<Func<Category, bool>> filter)
        => await _context.Categories.Find(filter).AnyAsync();

    public bool DeleteById(string id)
    {
        var result = _context.Categories.DeleteOne(Builders<Category>.Filter.Eq(x => x.Id.ToString(), id));
        return result.IsAcknowledged && result.DeletedCount > 0;
    }

    public async Task<bool> ExistsByIdAsync(string id)
        => await _context.Categories.Find(Builders<Category>.Filter.Eq(x => x.Id.ToString(), id)).AnyAsync();

    public async Task<IEnumerable<Category>> GetAllAsync()
        => await _context.Categories.Find(x => true).ToListAsync();

    public async Task<Category> GetByIdAsync(string id)
        => await _context.Categories.Find(Builders<Category>.Filter.Eq(x => x.Id.ToString(), id)).FirstOrDefaultAsync();

    public bool Update(Category current)
    {
        current.UpdatedAtNow();
        var result = _context.Categories.ReplaceOne(Builders<Category>.Filter.Eq(x => x.Id, current.Id), current);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }
}