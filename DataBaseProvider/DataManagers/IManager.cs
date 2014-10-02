using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    interface IManager<T>
    {
        void Add(T model);
        void Update(T model);
        void Delete(T model);
        List<T> ToList();
    }
}
