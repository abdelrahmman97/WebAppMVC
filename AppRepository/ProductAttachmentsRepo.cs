using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRepository
{
    public class ProductAttachmentsRepo : MainRepo<ProductAttachment>
    {
        public ProductAttachmentsRepo(ApplicationDBContext applicationDBContext) : base(applicationDBContext) { }

    }
}
