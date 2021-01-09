using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TP_PWEB.Models
{
    public enum Estado
    {
        PENDENTE = 0,
        CONFIRMADA = 1,
        ENTREGUE = 2
    }

    public class Compra
    {
        [Key]
        public int IdCompra { get; set; }

        public int Unidades { get; set; }

        [Display(Name = "Data de pedido")]
        public DateTime DataPedido { get; set; }

        [Display(Name = "Data de confirmação")]
        public DateTime? DataConfirmacao { get; set; }

        [Display(Name = "Data prevista de entrega")]
        public DateTime? DataEntrega { get; set; }

        public Estado Estado { get; set; }

        public int IdProduto { get; set; }
        public virtual Produto Produto { get; set; }


        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}