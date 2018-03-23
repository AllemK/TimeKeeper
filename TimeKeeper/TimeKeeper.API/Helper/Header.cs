using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeKeeper.API.Helper
{
    public class Header
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public int sort { get; set; }
        public int totalItems { get; set; }
        public string filter { get; set; }

        public Header()
        {
            page = 0;
            pageSize = 10;
            sort = 0;
            filter = "";
            totalItems = 0;
        }
    }
}