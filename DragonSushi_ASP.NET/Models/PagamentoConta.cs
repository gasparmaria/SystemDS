using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DragonSushi_ASP.NET.Models
{
    public class PagamentoConta
    {
        public int fkPag { get; set; }
        public int fkComanda { get; set; }
    }
}