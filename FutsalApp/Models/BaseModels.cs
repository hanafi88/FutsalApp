using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FutsalApp.Models
{
    public class BaseModels
    {
        public BaseModels()
        {
            TanggalDibuat = DateTime.Now;
            DibuatOleh = "SYSTEM";
        }
        public DateTime TanggalDibuat { get; set; }
        public string DibuatOleh { get; set; }
    }
}