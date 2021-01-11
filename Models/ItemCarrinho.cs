using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP_PWEB.Models
{
	public class ItemCarrinho
	{
		public Produto Produto { get; set; }
		public int Unidades { get; set; }

		public decimal Subtotal { get; set; }
	}
}