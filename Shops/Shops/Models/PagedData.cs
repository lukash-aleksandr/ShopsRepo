using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shops.Models
{
    public class PagedData<T> where T : class 
    {
        public IEnumerable<T> Data { get; set; }
        public int NumberOfPages { get; set; }
        public int CurrentPage { get; set; }
        public int StartSectionNumber { get; set; }
        public int EndSectionNumber { get; set; }
    }
}