using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class datatable<T>
    {
        public List<T> result { get; set; }
        public int total { get; set; }
    }
}
