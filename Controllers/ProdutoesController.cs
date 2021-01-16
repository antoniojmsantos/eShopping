using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TP_PWEB.Models;

using Microsoft.AspNet.Identity;

namespace TP_PWEB.Controllers
{
    public class ProdutoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Produtoes
        public ActionResult Index(string produto, int? IdCategoria)
        {
            List<ProdutoViewModel> prods = new List<ProdutoViewModel>();
            var userId = User.Identity.GetUserId();
            List<Produto> produtos = new List<Produto>();

            if (User.IsInRole("Empresa"))
            {
                var empresa = db.Empresas.FirstOrDefault(e => e.ApplicationUserId == userId);
                if (empresa == null)
                {
                    return View(prods);
                }

                produtos = db.Produtos.Where(p => p.IdEmpresa == empresa.IdEmpresa).OrderBy(p => p.Categoria.NomeCategoria).ThenBy(p => p.Nome).ToList();
            }
            else if (User.IsInRole("Funcionario"))
            {
                var funcionario = db.Funcionarios.FirstOrDefault(f => f.ApplicationUserId == userId);
                if (funcionario == null)
                {
                    return View(prods);
                }

                produtos = db.Produtos.Where(p => p.IdEmpresa == funcionario.IdEmpresa).OrderBy(p => p.Categoria.NomeCategoria).ThenBy(p => p.Nome).ToList();
            }
            else if (User.IsInRole("Cliente"))
			{
                IQueryable<Produto> produtosPesquisados = db.Produtos;

                if (produto != null)
				{
                    produtosPesquisados = produtosPesquisados.Where(p => p.Nome.Contains(produto));
                    
				}

                if (IdCategoria != null)
				{
                    produtosPesquisados = produtosPesquisados.Where(p => p.Categoria.IdCategoria == IdCategoria);
				}

				SelectList categorias = new SelectList(db.Categorias, "IdCategoria", "NomeCategoria");
				List<SelectListItem> listaCategorais = categorias.ToList();
				listaCategorais.Insert(0, new SelectListItem
				{
					Text = "- Selecionar -",
					Value = null,
					Selected = true
				});

                ViewBag.Produto = produto;
                ViewBag.IdCategoria = new SelectList(listaCategorais, "Value", "Text");

                produtos = produtosPesquisados.Where(p => p.Apagado == false).OrderBy(p => p.Categoria.NomeCategoria).ThenBy(p => p.Nome).ToList();
            }
            else
            {
                produtos = db.Produtos.OrderBy(p => p.Categoria.NomeCategoria).ThenBy(p => p.Nome).ToList();
            }

            produtos.ForEach(x => {
                var prodviewmodel = new ProdutoViewModel
                {
                    NomeProduto = x.Nome,
                    IdProduto = x.IdProduto,
                    Categoria = x.Categoria.NomeCategoria,
                    Vendedor = x.Empresa.NomeEmpresa,
                    EmStock = x.EmStock,
                    Unidades = x.UnidadesEmStock,
                    Preco = x.Preco,
                    Apagado = x.Apagado
                };

                var promocao = db.Promocoes.Where(p => p.IdProduto == x.IdProduto && p.Ativa == true).FirstOrDefault();
                if (promocao != null)
                {
                    prodviewmodel.IdPromocao = promocao.IdPromocao;
                    prodviewmodel.PrecoPromocional = promocao.PrecoNovo;
                    prodviewmodel.Desconto = promocao.Percentagem;
                }

                prods.Add(prodviewmodel);
            });

            return View(prods);
        }

        public ActionResult CriarPromocao(int? id)
		{
            if (id == null)
			{
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Produto produto = db.Produtos.Find(id);
            if (produto == null)
			{
                return RedirectToAction("Index");
			}

            ViewBag.Produto = produto;
            return View();
		}

        [Authorize(Roles = "Funcionario")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CriarPromocao([Bind(Include = "IdPromocao,IdProduto,Percentagem")] Promocao promocao)
		{
            if (ModelState.IsValid)
            {
                var produto = db.Produtos.Find(promocao.IdProduto);
                if (produto == null)
				{
                    ModelState.AddModelError("", "Produto não existe");
                    return View(promocao);
				}

                var prom = db.Promocoes.Where(p => p.IdProduto == produto.IdProduto && p.Ativa == true).FirstOrDefault();
                if (prom != null)
                {
                    prom.Ativa = false;
                }
                db.Entry(promocao).State = EntityState.Modified;

                promocao.Ativa = true;
                promocao.PrecoNovo = produto.Preco - (produto.Preco * (promocao.Percentagem / (decimal)100));
                db.Promocoes.Add(promocao);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
		}

        public ActionResult RemoverPromocao(int? id)
		{
            if (id != null)
			{
                return View(db.Promocoes.Find(id));
			}
            return RedirectToAction("Index");
		}

        [HttpPost]
        public ActionResult RemoverPromocao(int id)
		{
            var promocao = db.Promocoes.Find(id);
            if (promocao != null)
			{
                promocao.Ativa = false;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
		}

        // GET: Produtoes/Details/5
        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto prod = db.Produtos.Find(id);
            if (prod == null)
            {
                return HttpNotFound();
            }

            var produto = new ProdutoViewModel {
                NomeProduto = prod.Nome,
                IdProduto = prod.IdProduto,
                Categoria = prod.Categoria.NomeCategoria,
                Vendedor = prod.Empresa.NomeEmpresa,
                EmStock = prod.EmStock,
                Unidades = prod.UnidadesEmStock,
                Preco = prod.Preco,
                Apagado = prod.Apagado
            };

            var promocao = db.Promocoes.Where(p => p.IdProduto == prod.IdProduto && p.Ativa == true).FirstOrDefault();
            if (promocao != null)
            {
                produto.PrecoPromocional = promocao.PrecoNovo;
                produto.Desconto = promocao.Percentagem;
            }

            return View(produto);
        }


        // GET: Produtoes/Create
        [Authorize(Roles = "Empresa")]
        public ActionResult Create()
        {
            CriarProdutoViewModel model = new CriarProdutoViewModel();
            model.Categorias = new SelectList(db.Categorias, "IdCategoria", "NomeCategoria");
            return View(model);
        }

        // POST: Produtoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CriarProdutoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var produto = new Produto
                {
                    Preco = model.Preco, UnidadesEmStock = model.UnidadesEmStock,
                    IdCategoria = model.IdCategoria, Nome = model.Nome
                };
                var userId = User.Identity.GetUserId();
                produto.IdEmpresa = db.Empresas.Where(e => e.ApplicationUserId == userId).FirstOrDefault().IdEmpresa;
                
                if (produto.UnidadesEmStock == 0)
				{
                    produto.EmStock = false;
				}
                else
				{
                    produto.EmStock = true;
				}

                db.Produtos.Add(produto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else{
                model.Categorias = new SelectList(db.Categorias, "IdCategoria", "NomeCategoria");
                return View(model);
            }   
        }

        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = db.Produtos.Find(id);

            if (produto == null)
            {
                return HttpNotFound();
            }

            return View(produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "IdProduto,Nome,Preco,IdEmpresa,UnidadesEmStock,EmStock,IdCategoria")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                var promocao = db.Promocoes.Where(p => p.IdProduto == produto.IdProduto && p.Ativa == true).FirstOrDefault();
                if (promocao != null)
                {
                    promocao.Ativa = false;
                    db.Entry(promocao).State = EntityState.Modified;
                }

                db.Entry(produto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(produto);
        }

        // GET: Produtoes/Delete/5
        public ActionResult Apagar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = db.Produtos.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        // POST: Produtoes/Delete/5
        [HttpPost, ActionName("Apagar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Produto produto = db.Produtos.Find(id);
            produto.Apagado = true;

            var promocao = db.Promocoes.Where(x => x.IdProduto == id && x.Ativa == true).FirstOrDefault();
            if (promocao != null)
            {
                promocao.Ativa = false;
                db.Entry(promocao).State = EntityState.Modified;
            }

            db.Entry(produto).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
