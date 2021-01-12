using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TP_PWEB.Models
{
	public enum Entrega
	{
		ENTREGA_AO_DOMICILIO = 0,
		PONTO_DISTRIBUICAO = 1
	}
	public class Compra
	{
		[Key]
		public int IdCompra { get; set; }

		public Entrega Entrega { get; set; }

		public decimal Total { get; set; }

		public virtual ICollection<LinhaCompra> Itens { get; set; }

		public string ApplicationUserId { get; set; }

		[ForeignKey("ApplicationUserId")]
		public virtual ApplicationUser ApplicationUser { get; set; }
	}
}