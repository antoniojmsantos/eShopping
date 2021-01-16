using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TP_PWEB.Models
{
    public class Promocao
    {
        [Key]
        public int IdPromocao { get; set; }

        [Display(Name = "Produto")]
        public int IdProduto { get; set; }
        public virtual Produto Produto { get; set; }

        [Display(Name = "Promocao (%)")]
        public int Percentagem { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:c}")]
        public decimal PrecoNovo { get; set; }

        public bool Ativa { get; set; }
    }
}