using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TP_PWEB.Models
{
    public class Funcionario
    {

        [Key]
        public int IdFuncionario { get; set; }


        [Display(Name = "IdEmpresa")]
        public int IdEmpresa { get; set; }
        public virtual Empresa Empresa { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}