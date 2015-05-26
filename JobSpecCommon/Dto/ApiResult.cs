using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSpecCommon.Dto
{
    public class ApiResult<T>
    {
        public bool HasError { get; set; }
        public List<string> ErrorMessages { get; set; }
        public List<T> Data { get; set; }
    }
}
