using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSpecCollector.Interfaces
{
    public interface ITextSplitter
    {
        IEnumerable<string> ToWords(string text);
    }
}
