using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using TP_PWEB.Models;

namespace TP_PWEB.Controllers
{
    public class UtilizadoresController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> UserManager  = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

        [Authorize(Roles = "Admin")]
        public ActionResult ListaClientes()
        {
            var role = db.Roles.SingleOrDefault(m => m.Name == "Cliente");
            var usersInRole = db.Users.Where(m => m.Roles.Any(r => r.RoleId == role.Id));

            return View(usersInRole);

        }

        [Authorize(Roles = "Empresa")]
        public ActionResult ListaFuncionarios()
        {
            var userId = User.Identity.GetUserId();
            var empresa = db.Empresas.Where(e => e.ApplicationUserId == userId).FirstOrDefault();
           
            var funcionarios = db.Funcionarios.Where(f => f.IdEmpresa == empresa.IdEmpresa);
            var funcionariosEmpresa = db.Users.Where(u => funcionarios.Any(f => f.ApplicationUserId == u.Id));

            return View(funcionariosEmpresa);
        }

        public ActionResult Detalhes(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }


        // GET: Clientes/Edit/5
        public ActionResult EditarFuncionario(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }

            return View(applicationUser);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarFuncionario([Bind(Include = "Id,NomeCompleto,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AcessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicationUser).State = EntityState.Modified;
                db.SaveChanges();
                if (User.IsInRole("Admin"))
                    return RedirectToAction("ListaClientes");
                else return RedirectToAction("ListaFuncionarios");
            }
            return View(applicationUser);
        }

        [Authorize(Roles = "Empresa")]
        public ActionResult RegistarFuncionario()
        {
            
            CreateUserViewModel model = new CreateUserViewModel();
            var userId = User.Identity.GetUserId();
            model.Empresa = db.Empresas.Where(e => e.ApplicationUserId == userId).FirstOrDefault().NomeEmpresa;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Empresa")]
        public async Task<ActionResult> RegistarFuncionario(CreateUserViewModel model)
        {

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    NomeCompleto = model.NomeCompleto,
                    UserName = model.Email,
                    Email = model.Email,
                    PasswordHash = model.Password
                };
                var result = await UserManager.CreateAsync(user, model.Password);

                //Add User to the selected Roles 
                if (result.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "Funcionario");

                    var loggedUserId = User.Identity.GetUserId();
                    var empresa = db.Empresas.Where(e => e.ApplicationUserId == loggedUserId).FirstOrDefault();

                    if (empresa != null)
                    {
                        db.Funcionarios.Add(new EmpresaFuncionario
                        {
                            ApplicationUserId = user.Id,
                            IdEmpresa = empresa.IdEmpresa
                        });
                        db.SaveChanges();

                        return RedirectToAction("ListaFuncionarios");
                    } else
					{
                        var userId = User.Identity.GetUserId();
                        model.Empresa = db.Empresas.Where(e => e.ApplicationUserId == userId).FirstOrDefault().NomeEmpresa;
                        ModelState.AddModelError("", "Empresa não existe.");
                        return View(model);
					}
                }
                else
                {
                    var userId = User.Identity.GetUserId();
                    model.Empresa = db.Empresas.Where(e => e.ApplicationUserId == userId).FirstOrDefault().NomeEmpresa;
                    ModelState.AddModelError("", "Já existe um utilizador com este email.");
                    return View(model);
                }
            }

            return View(model);
        }

        // GET: Utilizadores/ApagarFuncionario/5
        public ActionResult ApagarFuncionario(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("ApagarFuncionario")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Empresa")]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            db.Users.Remove(applicationUser);
            db.SaveChanges();
            return RedirectToAction("ListaFuncionarios");
        }
    }

    
}