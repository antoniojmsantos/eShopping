using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP_PWEB.Models
{
	public class CompraViewModel
	{
		public int IdCompra { get; set; }
		public int Itens { get; set; }

		public Entrega Entrega { get; set; }

		public decimal Total { get; set; }
	}
}