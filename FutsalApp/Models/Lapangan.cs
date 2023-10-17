using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FutsalApp.Models
{
    public class Lapangan : BaseModels
    {
        public int Id { get; set; }
        public string Kode { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public int KaryawanId { get; set; }
        [NotMapped]
        public string Karyawan { get; set; }
        [NotMapped]
        public SelectList Karyawans { get; set; }
    }
}