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
        public ActionResult Estatisticas(int? mes, string ano)
        {
            var userId = User.Identity.GetUserId();
            var empresa = db.Empresas.Where(e => e.ApplicationUserId == userId).FirstOrDefault();

            if (empresa != null)
			{
                var comprasPesquisadas = db.LinhaCompras.Where(lc => lc.Produto.IdEmpresa == empresa.IdEmpresa);
                var topVendasQuery = db.LinhaCompras.Where(lc => lc.Produto.IdEmpresa == empresa.IdEmpresa);

                if (mes != null)
				{
                    comprasPesquisadas = comprasPesquisadas.Where(c => c.DataConfirmada.Value.Month == mes);
                }

                if (ano != null)
				{
                    if (ano.Length > 0)
					{
                        var anoAsInt = int.Parse(ano);
                        comprasPesquisadas = comprasPesquisadas.Where(c => c.DataConfirmada.Value.Year == anoAsInt);
					}
				}

                decimal totalVendas = 0;
                if (comprasPesquisadas.Count() > 0)
                {
                    totalVendas = comprasPesquisadas.Sum(c => c.Subtotal);
                }

                List<TopVendaViewModel> Top5Vendas = comprasPesquisadas
                    .GroupBy(p => p.Produto)
                    .Select(x => new TopVendaViewModel
                    {
                        NomeProduto = x.Key.Nome,
                        UnidadesVendidas = x.Sum(y => y.Unidades),
                        TotalVendas = x.Sum(y => y.Unidades) * x.Key.Preco,
                        PercentagemFaturacao = (double)(x.Sum(y => y.Unidades) * x.Key.Preco / totalVendas)
                    })
                    .OrderByDescending(x => x.UnidadesVendidas)
                    .Take(5)
                    .ToList();

         
         
                List<SelectListItem> listaMeses = new SelectList(
                    Enumerable.Range(1, 12).Select(i => new { val = i, txt = DateTimeFormatInfo.CurrentInfo.GetMonthName(i) }),
                    "val",
                    "txt"
                ).ToList();

                listaMeses.Insert(0, new SelectListItem
                {
                    Text = "- Selecionar -",
                    Value = null,
                    Selected = true
                });

                ViewBag.Mes = new SelectList(listaMeses, "Value", "Text");
                return View(new EstatisticasViewModel {
                    nVendas = comprasPesquisadas.Count(),
                    totalVendas = totalVendas,
                    nClientes = comprasPesquisadas.Select(c => c.Compra.ApplicationUserId).Distinct().Count(),
                    Vendas = comprasPesquisadas.Include(p => p.Produto).ToList(),
                    TopVendas = Top5Vendas,
                    Ano = ano,
                    Mes = mes
                });
			}

            return View();
        }
    }
}
