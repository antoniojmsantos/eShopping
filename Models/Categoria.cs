using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TP_PWEB.Models
{
    public class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }
        [Column("Nome")]
        public string NomeCategoria { get; set; }
    }
}