

using Models;

namespace AppRepository
{
    public class UnitOfWork
    {
        ApplicationDBContext _context { get; set; }
        public UnitOfWork(ApplicationDBContext applicationDBContext)
        {
            _context = applicationDBContext;
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }
    }
}
