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
    [Authorize(Roles ="Cliente")]
    public class ComprasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Compras
        public ActionResult Index()
        {
            List<Compra> compras;

            if (User.IsInRole("Cliente"))
			{
                var userId = User.Identity.GetUserId();

                compras = db.Compras
                    .Include(p => p.Produto)
                    .Where(p => p.ApplicationUserId == userId)
                    .ToList();

                return View(compras);
			}
			compras = db.Compras.Include(c => c.Produto).ToList();
			return View(compras);
        }

        // GET: Compras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra compra = db.Compras.Find(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            return View(compra);
        }

        // GET: Compras/Create
        [Authorize(Roles = "Cliente")]
        public ActionResult Create(int? id)
        {
            if (id != null)
			{
                var produto = db.Produtos.Where(p => p.IdProduto == id).FirstOrDefault();
                if (produto != null)
				{
                    ViewBag.Produto = produto;
                    return View();
				} else
				{
                    return RedirectToAction("Index", "Produtoes");
                }
                //ViewBag.IdProduto = new SelectList(db.Produtos.Where(p => p.IdProduto == id), "IdProduto", "Nome");
			}

            return RedirectToAction("Index", "Produtoes");

        }

        // POST: Compras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCompra,IdProduto,Unidades")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                compra.ApplicationUserId = User.Identity.GetUserId();
                compra.Estado = Estado.PENDENTE;
                compra.DataPedido = DateTime.Now;
                db.Compras.Add(compra);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdProduto = new SelectList(db.Produtos, "IdProduto", "Nome", compra.IdProduto);
            return View(compra);
        }

        // GET: Compras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra compra = db.Compras.Find(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdProduto = new SelectList(db.Produtos, "IdProduto", "Nome", compra.IdProduto);
            return View(compra);
        }

        // POST: Compras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCompra,IdProduto")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdProduto = new SelectList(db.Produtos, "IdProduto", "Nome", compra.IdProduto);
            return View(compra);
        }

        // GET: Compras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra compra = db.Compras.Find(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            return View(compra);
        }

        // POST: Compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Compra compra = db.Compras.Find(id);
            db.Compras.Remove(compra);
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
