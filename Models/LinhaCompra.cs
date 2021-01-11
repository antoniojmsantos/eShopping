using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TP_PWEB.Models
{
	public class LinhaCompra
	{
		[Key]
		public int IdLinhaCompra { get; set; }

		public int Unidades { get; set; }

		public decimal Subtotal { get; set; }

		public int IdCompra { get; set; }
		public virtual Compra Compra { get; set; }

		public int IdProduto { get; set; }
		public virtual Produto Produto {get; set;}
	}
}