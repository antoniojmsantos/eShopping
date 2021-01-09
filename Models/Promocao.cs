using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TP_PWEB.Models
{
    public class Promocao
    {
        [Key]
        public int IdPromocao { get; set; }
       
        public int IdProduto { get; set; }
        public virtual Produto Produto { get; set; }

        [Display(Name = "Promocao (%)")]
        public int Percentagem { get; set; }
    }
}