using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSem3PT2
{
    public interface IQueue
    {
        int size { get; set; }
        double ReadHead();
    }
}
