﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TP_PWEB.Models
{
	public class SimpleProdutoViewModel
	{
		public int IdProduto { get; set; }

		[Display(Name = "Produto")]
		public string NomeProduto { get; set; }

		[Display(Name = "Antes")]
		[DataType(DataType.Currency)]
		[DisplayFormat(ApplyFormatInEditMode = true,
			DataFormatString = "{0:c}")]
		public decimal PrecoAntigo { get; set; }
		[Display(Name = "Depois")]
		[DataType(DataType.Currency)]
		[DisplayFormat(ApplyFormatInEditMode = true,
			DataFormatString = "{0:c}")]
		public decimal PrecoNovo { get; set; }

		[Display(Name = "Desconto de (%)")]
		public int Desconto { get; set; }

		[Display(Name = "Unidades Vendidas")]
		public int UnidadesVendidas { get; set; }

	}
}