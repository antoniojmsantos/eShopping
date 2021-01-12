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
using System.Globalization;

namespace TP_PWEB.Controllers
{

    public class EmpresasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Empresas
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var empresas = db.Empresas.Include(e => e.ApplicationUser);
            return View(empresas.ToList());
        }

        // GET: Empresas/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empresa empresa = db.Empresas.Find(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
        }


        // GET: Empresas/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empresa empresa = db.Empresas.Find(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }

            return View(empresa);
        }

        // POST: Empresas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "IdEmpresa,ApplicationUserId,NomeEmpresa")] Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empresa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(empresa);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [Authorize(Roles = "Empresa")]
        public ActionResult Estatisticas(int? mes, int? ano)
        {
            var userId = User.Identity.GetUserId();
            var empresa = db.Empresas.Where(e => e.ApplicationUserId == userId).FirstOrDefault();

            IQueryable<LinhaCompra> comprasPesquisadas = db.LinhaCompras.Where(c => c.Produto.IdEmpresa == empresa.IdEmpresa);

            if (empresa != null)
			{
                if (mes != null)
                {
                    comprasPesquisadas = comprasPesquisadas.Where(c => c.DataConfirmada.Value.Month == mes);

                }

                if (ano != null)
                {
                    comprasPesquisadas = comprasPesquisadas.Where(c => c.DataConfirmada.Value.Year == ano);
                }

                ViewBag.nMedioVendas = comprasPesquisadas.Count();
                if (comprasPesquisadas.Count() > 0)
				{
                    ViewBag.vMedioVendas = comprasPesquisadas.Sum(c => c.Subtotal);
                } else
				{
                    ViewBag.vMedioVendas = 0;
                }
                
                ViewBag.prodMaisVendidos = db.Produtos.Where(p => p.IdEmpresa == empresa.IdEmpresa).Take(5).ToList();
                ViewBag.nMedioClientes = comprasPesquisadas.Select(c => c.Compra.ApplicationUserId).Distinct().Count();

                SelectList meses = new SelectList(
                    Enumerable.Range(1, 12).Select(i => new { I = i, M = DateTimeFormatInfo.CurrentInfo.GetMonthName(i)}),
                    "I",
                    "M"
                );
                List<SelectListItem> listaMeses= meses.ToList();
				listaMeses.Insert(0, new SelectListItem
				{
					Text = "- Selecionar -",
					Value = null,
					Selected = true
				});

				ViewBag.Ano = ano;
                ViewBag.Mes = new SelectList(listaMeses, "Value", "Text");

                return View(comprasPesquisadas.Include(p => p.Produto).ToList());
            }


            return View();
        }
    }
}
