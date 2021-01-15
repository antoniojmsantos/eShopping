using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TP_PWEB.Models
{
	public class HomeViewModel
	{
		public List<SimpleProdutoViewModel> oportunidades = new List<SimpleProdutoViewModel>();
		public List<SimpleProdutoViewModel> tendencias = new List<SimpleProdutoViewModel>();
	}
}