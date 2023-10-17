using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FutsalApp.Models
{
    public class Booking : BaseModels
    {
        public int Id { get; set; }
        //public Member MemberId { get; set; }
        //public Karyawan KaryawanId { get; set; }
        public int MemberId { get; set; }
        public int KaryawanId { get; set; }
        public int LapanganId { get; set; }
        public DateTime TanggalBooking { get; set; }
        public int DurasiBooking { get; set; }
        [NotMapped]
        public SelectList Members { get; set; }
        [NotMapped]
        public SelectList Karyawans { get; set; }
        [NotMapped]
        public SelectList Lapangans { get; set; }
        [NotMapped]
        public string Member { get; set; }
        [NotMapped]
        public string Karyawan { get; set; }
        [NotMapped]
        public string Lapangan { get; set; }
    }
}