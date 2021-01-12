using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

	public class LinhaCompra
	{
		[Key]
		public int IdLinhaCompra { get; set; }

		public int Unidades { get; set; }

		public decimal Subtotal { get; set; }

		public Estado Estado { get; set; }

		[Display(Name = "Data de Compra")]
		public DateTime DataCriada { get; set; }

		[Display(Name = "Data de Venda")]
		public DateTime? DataConfirmada { get; set; }

		[Display(Name = "Data de Entrega")]
		public DateTime? DataEntrega { get; set; }

		public int IdCompra { get; set; }
		public virtual Compra Compra { get; set; }

		public int IdProduto { get; set; }
		public virtual Produto Produto {get; set;}
	}
}