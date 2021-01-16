using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TP_PWEB.Models;

namespace TP_PWEB.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var homeViewModel = new HomeViewModel();

            var produtos = db.Produtos.Where(x => x.Apagado == false);

            db.Promocoes
                .Where(x => produtos.Any(y => y.IdProduto == x.IdProduto) && x.Ativa)
                .OrderByDescending(x => x.Percentagem)
                .ThenBy(x => x.PrecoNovo)
                .Take(5)
                .ToList()
                .ForEach(x =>
                {
                    homeViewModel.oportunidades.Add(new SimpleProdutoViewModel {
                        IdProduto = x.Produto.IdProduto,
                        NomeProduto = x.Produto.Nome,
                        PrecoAntigo = x.Produto.Preco,
                        PrecoNovo = x.PrecoNovo,
                        Desconto = x.Percentagem
                    });
                });

            homeViewModel.tendencias =  db.LinhaCompras
                .GroupBy(x => x.Produto)
                .Where(x => x.Key.Apagado == false)
                .Select(x => new SimpleProdutoViewModel
                {
                    IdProduto = x.Key.IdProduto,
                    NomeProduto = x.Key.Nome,
                    PrecoAntigo = x.Key.Preco,
                    UnidadesVendidas = x.Sum(y => y.Unidades)
                })
                .OrderByDescending(x => x.UnidadesVendidas)
                .Take(5)
                .ToList();


			return View(homeViewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}