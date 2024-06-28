using MSP.Courses.Domain.Entities;
using System.Linq.Expressions;

namespace MSP.Courses.Domain.Interfaces;

public interface ICategoryRepository
{
    Task<Category> CreateAsync(Category category);
    Task<bool> ExistsAsync(Expression<Func<Category, bool>> filter);
    bool DeleteById(string id);
    Task<bool> ExistsByIdAsync(string id);
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category> GetByIdAsync(string id);
    bool Update(Category current);
}