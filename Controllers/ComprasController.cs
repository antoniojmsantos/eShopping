using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TP_PWEB.Models;

namespace TP_PWEB.Controllers
{
    public class ComprasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: lista de compras para cliente
        [Authorize(Roles = "Cliente")]
        public ActionResult ListaComprasCliente()
        {
            var userId = User.Identity.GetUserId();
            if (User.IsInRole("Cliente"))
			{
                return View(db.Compras.Where(c => c.ApplicationUserId == userId).ToList());
			}
 
            // todo: return compras for empresa of funcionario
            return View(db.Compras.ToList());
        }

        // GET: lista de compras para funcionario
        [Authorize(Roles = "Funcionario")]
        public ActionResult ListaComprasFuncionario()
		{
            List<LinhaCompra> compras;

            var userId = User.Identity.GetUserId();
            var funcionario = db.Funcionarios.FirstOrDefault(f => f.ApplicationUserId == userId);

            if (funcionario != null)
			{
                compras = db.LinhaCompras
                    .Where(c => c.Produto.IdEmpresa == funcionario.IdEmpresa)
                    .ToList();
                return View(compras);
			}

            return RedirectToAction("Index", "Home");
		}

        // GET: Confirmar compra
        [Authorize(Roles = "Funcionario")]
        public ActionResult Confirmar(int? id)
		{
            return View(db.LinhaCompras.Find(id));
		}

        // POST: Confirmar compra
        [Authorize(Roles = "Funcionario")]
        [HttpPost]
        public ActionResult Confirmar(FormCollection form)
		{
            var id = Convert.ToInt32(form["IdLinhaCompra"]);
            var linhaCompra = db.LinhaCompras.Find(id);

            linhaCompra.Estado = Estado.CONFIRMADA;
            linhaCompra.DataConfirmada = DateTime.Now;

            var produto = db.Produtos.Find(linhaCompra.IdProduto);
            produto.UnidadesEmStock -= linhaCompra.Unidades;
            if (produto.UnidadesEmStock <= 0)
			{
                produto.EmStock = false;
			}

            db.Entry(linhaCompra).State = EntityState.Modified;
            db.Entry(produto).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ListaComprasFuncionario");
		}

        // GET: Confirmar compra
        [Authorize(Roles = "Funcionario")]
        public ActionResult MarcarEntregue(int? id)
        {
            return View(db.LinhaCompras.Find(id));
        }

        // POST: Confirmar compra
        [Authorize(Roles = "Funcionario")]
        [HttpPost]
        public ActionResult MarcarEntregue(FormCollection form)
        {
            var id = Convert.ToInt32(form["IdLinhaCompra"]);
            var linhaCompra = db.LinhaCompras.Find(id);

            linhaCompra.Estado = Estado.ENTREGUE;
            linhaCompra.DataEntrega = DateTime.Now;

            db.Entry(linhaCompra).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ListaComprasFuncionario");
        }


        // GET: Finalizar compra
        [Authorize(Roles = "Cliente")]
        public ActionResult Finalizar()
		{
            // todo: do something like show prices and items and etc...
            return View();
		}

        // POST: Finalizar compra
        [Authorize(Roles = "Cliente")]
        [HttpPost]
        public ActionResult Finalizar([Bind(Include ="IdCompra,Entrega")] Compra compra)
		{
            if (ModelState.IsValid)
			{
                compra.ApplicationUserId = User.Identity.GetUserId();
                compra.Total = Convert.ToDecimal(Session["total"]);
                db.Compras.Add(compra);
                db.SaveChanges();

                List<ItemCarrinho> itens = (List<ItemCarrinho>) Session["carrinho"];
                itens.ForEach(i =>
                {
                    db.LinhaCompras.Add(new LinhaCompra
                    {
                        DataCriada = DateTime.Now,
                        DataConfirmada = null,
                        DataEntrega = null,
                        Estado = Estado.PENDENTE,
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

                return RedirectToAction("ListaComprasCliente");
			}
            return View(compra);
		}

        // Details

        public ActionResult DetalhesLinhaCompra(int? id)
        {
            if (id != null)
            {
                return View(db.LinhaCompras.Find(id));
            }

            return RedirectToAction("ListaComprasFuncionario");
        }
        public ActionResult DetalhesCompra(int? id)
		{
            if (id != null)
			{
                return View(db.Compras.Find(id));
			}

            return RedirectToAction("ListaComprasCliente");
		}
    }
}