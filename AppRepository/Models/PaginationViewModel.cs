
namespace AppRepository.Models
{
    public class PaginationViewModel<T>
    {
        public int PageIndex { get; set;}
        public int PageSize { get; set;}
        public int Count { get; set;}
        public T Data { get; set;}
    }
}
