using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TP_PWEB.Models
{
    public class Compra
    {
        [Key]
        public int IdCompra { get; set; }

        public int IdProduto { get; set; }
        public virtual Produto Produto { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}