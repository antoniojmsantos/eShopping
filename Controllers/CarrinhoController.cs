using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TP_PWEB.Models;

namespace TP_PWEB.Controllers
{

    [Authorize(Roles = "Cliente")]
    public class CarrinhoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Carrinho
        public ActionResult Index()
        {
            if (Session["carrinho"] == null)
			{
                return View(new List<ItemCarrinho>());       
			}
            return View((List<ItemCarrinho>)Session["carrinho"]);
        }

        // GET: Carrinho/AdicionarItem
        public ActionResult AdicionarItem(int? id)
        {
            if (id != null)
            {
                var produto = db.Produtos.Find(id);
                if (produto != null)
                {
                    ViewBag.Produto = produto;
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Produtoes");
                }
            }

            return RedirectToAction("Index", "Produtoes");
        }

        // POST: Carrinho/AdicionarItem
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdicionarItem(FormCollection collection)
        {
            List<ItemCarrinho> itens;
            if (Session["carrinho"] == null)
			{
                itens = new List<ItemCarrinho>();
                Session["nItens"] = 0;
                Session["total"] = 0;
            }
            else
			{
                itens = (List<ItemCarrinho>)Session["carrinho"];
            }

            var IdProduto = int.Parse(collection["IdProduto"]);
            var unidades = int.Parse(collection["Unidades"]);
            decimal subtotal = 0;

            var produto = db.Produtos.Find(IdProduto);
            if (produto != null)
			{
                var promocao = db.Promocoes.Where(p => p.IdProduto == IdProduto && p.Ativa == true).FirstOrDefault();
                if (promocao != null)
                {
                    subtotal = promocao.PrecoNovo * unidades;
                } else
				{
                    subtotal = produto.Preco * unidades;
				}

                itens.Add(new ItemCarrinho {
                    Produto = produto,
                    Unidades = unidades,
                    Subtotal = subtotal
                });
                Session["carrinho"] = itens;
                Session["nItens"] = Convert.ToInt32(Session["nItens"]) + 1;
                Session["total"] = Convert.ToDecimal(Session["total"]) + itens.Last().Subtotal;

                return RedirectToAction("Index");
            }

            return View(IdProduto);
        }

        // GET: Carrinho/EditarItem/5
        public ActionResult EditarItem(int position)
        {
            List<ItemCarrinho> itens = (List<ItemCarrinho>)Session["carrinho"];
            ViewBag.position = position;
            return View(itens[position]);
        }

        // POST: Carrinho/EditarItem/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarItem(FormCollection collection)
        {
            List<ItemCarrinho> itens = (List<ItemCarrinho>)Session["carrinho"];
        
            var item = itens[int.Parse(collection["position"])];

            var newTotal = Convert.ToDecimal(Session["total"]) - item.Subtotal;

            item.Unidades = int.Parse(collection["unidades"]);
            item.Subtotal = item.Unidades * item.Produto.Preco;

            Session["carrinho"] = itens;
            Session["total"] = newTotal + item.Subtotal;

            return RedirectToAction("Index");
        }

        // GET: Carrinho/RemoverItem/5
        public ActionResult RemoverItem(int position)
        {
            List<ItemCarrinho> itens = (List<ItemCarrinho>)Session["carrinho"];
            ViewBag.position = position;
            return View(itens[position]);
        }

        // POST: Carrinho/RemoverItem/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoverItem(FormCollection collection)
		{
			List<ItemCarrinho> itens = (List<ItemCarrinho>)Session["carrinho"];
            var position = int.Parse(collection["position"]);

            var IdProduto = itens[position].Produto.IdProduto;
            var subTotal = itens[position].Subtotal;

            itens.RemoveAt(position);

            Session["carrinho"] = itens;
            Session["nItens"] = Convert.ToInt32(Session["nItens"]) - 1;
            Session["total"] = Convert.ToDecimal(Session["total"]) - subTotal;

            return RedirectToAction("Index");
		}
	}
}
