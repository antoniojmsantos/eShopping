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
            List<Produto> produtos;
            var userId = User.Identity.GetUserId();

            if (User.IsInRole("Empresa"))
			{
                var empresa = db.Empresas.Where(e => e.ApplicationUserId == userId).FirstOrDefault();
                if (empresa != null) {

                    produtos = db.Produtos
                        .Include(p => p.Empresa)
                        .Where(p => p.IdEmpresa == empresa.IdEmpresa)
                        .ToList();

                    return View(produtos);
                }

                produtos = db.Produtos.Include(p => p.Empresa).ToList();
                return View(produtos);
            }
            else if (User.IsInRole("Funcionario"))
			{
                var funcionario = db.Funcionarios.FirstOrDefault(f => f.ApplicationUserId == userId);
                if (funcionario != null)
				{
                    produtos = db.Produtos
                        .Include(p => p.Empresa)
                        .Where(p => p.IdEmpresa == funcionario.IdEmpresa)
                        .ToList();

                    return View(produtos);
				}

                produtos = db.Produtos.Include(p => p.Empresa).ToList();
                return View(produtos);
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

				return View(produtosPesquisados.Include(p => p.Empresa).ToList());
            }

            return RedirectToAction("Index", "Home");
        }

        // GET: Produtoes/Details/5
        public ActionResult Details(int? id)
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

        [Authorize(Roles ="Empresa")]
        // GET: Produtoes/Create
        public ActionResult Create()
        {
            ViewBag.IdCategoria = new SelectList(db.Categorias, "IdCategoria", "NomeCategoria");
            return View();
        }

        // POST: Produtoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdProduto,Nome,Preco,UnidadesEmStock,IdCategoria")] Produto produto)
        {
            if (ModelState.IsValid)
            {
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

            ViewBag.IdEmpresa = new SelectList(db.Empresas, "IdEmpresa", "Id", produto.IdEmpresa);
            return View(produto);
        }

        // GET: Produtoes/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Produtoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdProduto,Nome,Preco,IdEmpresa,UnidadesEmStock,EmStock,IdCategoria")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(produto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(produto);
        }

        // GET: Produtoes/Delete/5
        public ActionResult Delete(int? id)
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Produto produto = db.Produtos.Find(id);
            db.Produtos.Remove(produto);
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
