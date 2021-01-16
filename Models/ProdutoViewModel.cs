using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TP_PWEB.Models
{
    public class ProdutoViewModel
    {

        public int? IdPromocao { get; set; }
        [Display(Name = "Produto")]
        public string NomeProduto { get; set; }
        public int IdProduto { get; set; }
        [Display(Name = "Categoria")]
        public string Categoria { get; set; }
        [Display(Name = "Vendido por")]
        public string Vendedor { get; set; }
        [Display(Name = "Em stock?")]
        public bool EmStock { get; set; }
        [Display(Name = "Unidades em stock")]
        public int Unidades { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:c}")]
        [Display(Name = "Preço p/ unidade")]
        public decimal Preco { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:c}")]
        public decimal? PrecoPromocional { get; set; }
        public int? Desconto { get; set; }
        public bool Apagado { get; set; }
    }

    public class CriarProdutoViewModel
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Preço p/ unidade (€)")]
        public decimal Preco { get; set; }

        [Required]
        [Display(Name = "Unidades em stock")]
        public int UnidadesEmStock { get; set; }

        [Display(Name = "Em stock?")]
        public bool EmStock { get; set; }


        [Display(Name = "Categoria")]
        public int IdCategoria { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> Categorias { get; set; }

        [Display(Name = "Empresa")]
        public int IdEmpresa { get; set; }
    }

}