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
        public string Nome { get; set; }
        public float Preco { get; set; }

        //[Display(Name = "Empresa")]
        public int IdEmpresa { get; set; }
        public virtual Empresa Empresa { get; set; }
    }
}