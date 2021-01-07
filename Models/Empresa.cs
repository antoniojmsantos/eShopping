using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TP_PWEB.Models
{
    public class Empresa
    {
        [Key]
        public int idEmpresa { get; set; }
        //[Display(Name = "Empresa")]
        //public string nomeEmpresa { get; set; }

        public string Id { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}