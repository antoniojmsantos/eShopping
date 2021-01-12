using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TP_PWEB.Models
{
	public class TopVendaViewModel
	{
		[Display(Name = "Produto")]
		public string NomeProduto { get; set; }
		[Display(Name = "Unidades Vendidas")]
		public int UnidadesVendidas { get; set; }
		[Display(Name = "Faturado")]
		public decimal TotalVendas { get; set; }

		[Display(Name = "Participação nos Rendimentos (%)")]
		[DisplayFormat(DataFormatString = "{0:P1}")]
		public double PercentagemFaturacao { get; set; }
	}

	public class EstatisticasViewModel
	{
		[Display(Name ="Nº de Vendas")]
		public int nVendas { get; set; }

		[Display(Name = "Rendimento Bruto")]
		public decimal totalVendas { get; set; }

		[Display(Name = "Clientes Ativos")]
		public int nClientes { get; set; }

		[Display(Name = "Ano")]
		public string Ano { get; set; }

		[Display(Name = "Mês")]
		public int? Mes { get; set; }

		[Display(Name = "Histórico de Vendas")]
		public List<LinhaCompra> Vendas { get; set; }

		[Display(Name = "Produtos mais vendidos")]
		public List<TopVendaViewModel> TopVendas { get; set; }
	}
}