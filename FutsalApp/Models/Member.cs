using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FutsalApp.Models
{
    public class Member : BaseModels
    {
        public int Id { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string NomorHP { get; set; }
    }
}