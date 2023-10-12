using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;

namespace AppRepository
{
    public class MainRepo<T> where T : class
    {
        private readonly ApplicationDBContext _context;
        public readonly DbSet<T> Set;
        
        public MainRepo(ApplicationDBContext applicationDBContext)
        {
            _context = applicationDBContext;
            Set = _context.Set<T>();
        }

        public IQueryable<T> GetList()
        {
            return Set.AsQueryable();
        }

        public EntityEntry<T> Add(T Entry)
        {
            return Set.Add(Entry);
        }

        public EntityEntry<T> Update(T Entry)
        {
            return Set.Update(Entry);
        }

        public EntityEntry<T> Delete(T Entry)
        {
            return Set.Remove(Entry);
        }
    }
}