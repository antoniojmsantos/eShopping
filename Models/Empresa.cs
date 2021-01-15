using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TP_PWEB.Models
{
    public class Empresa
    {
        [Key]
        public int IdEmpresa { get; set; }

        [Display(Name = "Nome Empresa")]
        [Column("Nome")]
        [Index(IsUnique = true)]
        [StringLength(50)]
        public string NomeEmpresa { get; set; }

        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}