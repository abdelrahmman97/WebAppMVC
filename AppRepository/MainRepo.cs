using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using System.Linq.Expressions;

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

        public IQueryable<T> GetList(
            Expression<Func<T, bool>> Filter,
            string OrderBy,
            bool IsAscending,
            int PageSize,
            int PageIndex)
        {
            var Quary = Set.AsQueryable();
            if (Filter != null)
            {
                Quary = Quary.Where(Filter);
            }
            Quary = Quary.OrderBy(OrderBy, IsAscending);
            if (PageIndex < 1)
            {
                PageIndex = 1;
            }

            var temp = Set.Count() / Convert.ToDouble(PageSize);
            if (PageIndex > temp + 1)
            {
                PageIndex = 1;
            }
            int ToBeSkiped = (PageIndex - 1) * PageSize;
            return Quary.Skip(ToBeSkiped).Take(PageSize);
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