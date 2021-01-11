using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TP_PWEB.Models;

namespace TP_PWEB.Controllers
{
    public class ComprasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Compras
        [Authorize(Roles = "Cliente")]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            if (User.IsInRole("Cliente"))
			{
                return View(db.Compras.Where(c => c.ApplicationUserId == userId).ToList());
			} 

            // todo: return compras for empresa of funcionario
            return View(db.Compras.ToList());
        }

        public ActionResult Finalizar()
		{
            // todo: do something like show prices and items and etc...
            return View();
		}


        [HttpPost]
        public ActionResult Finalizar([Bind(Include ="IdCompra,Entrega")] Compra compra)
		{
            if (ModelState.IsValid)
			{
                compra.ApplicationUserId = User.Identity.GetUserId();
                compra.DataCriada = DateTime.Now;
                compra.DataConfirmada = null;
                compra.Estado = Estado.PENDENTE;
                compra.Total = Convert.ToDecimal(Session["total"]);
                db.Compras.Add(compra);
                db.SaveChanges();

                List<ItemCarrinho> itens = (List<ItemCarrinho>) Session["carrinho"];
                itens.ForEach(i =>
                {
                    db.LinhaCompras.Add(new LinhaCompra
                    {
                        Unidades = i.Unidades,
                        Subtotal = i.Subtotal,
                        IdProduto = i.Produto.IdProduto,
                        IdCompra = compra.IdCompra
                    });
                    db.SaveChanges();
                });


                Session["carrinho"] = new List<ItemCarrinho>();
                Session["nItens"] = 0;
                Session["total"] = 0;

                return RedirectToAction("Index");
			}
            return View(compra);
		}

        public ActionResult Detalhes(int? id)
		{
            if (id != null)
			{
                return View(db.Compras.Find(id));
			}

            return RedirectToAction("Index");
		}
    }
}