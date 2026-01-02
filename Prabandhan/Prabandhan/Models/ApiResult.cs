using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parbandhan.Models
{
    public class ApiResult<T>
    {
        public bool IsPaginated { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public T? Data { get; set; }
    }
}
