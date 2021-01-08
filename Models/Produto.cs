using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TP_PWEB.Models
{
    public class Produto
    {

        [Key]
        public int IdProduto { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        [Display(Name="Preço p/ unidade")]
        public float Preco { get; set; }

        [Required]
        [Display(Name="Unidades em stock")]
        public int UnidadesEmStock { get; set; }

        [Display(Name="Em stock?")]
        public bool EmStock { get; set; }


        public int IdEmpresa { get; set; }
        public virtual Empresa Empresa { get; set; }
    }
}