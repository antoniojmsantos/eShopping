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
        // GET: Utilizadores

        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> UserManager  = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

        [Authorize(Roles = "Admin")]
        public ActionResult IndexClientes()
        {
            //FUNCAO LINQ PARA OBTER SÓ OS UTILIZADORES COM O ROLE CLIENTE
            var role = db.Roles.SingleOrDefault(m => m.Name == "Cliente");
            var usersInRole = db.Users.Where(m => m.Roles.Any(r => r.RoleId == role.Id));

            return View(usersInRole);

        }

        [Authorize(Roles = "Empresa")]
        public ActionResult IndexFuncionarios()
        {
            var userId = User.Identity.GetUserId();
            var empresa = db.Empresas.Where(e => e.ApplicationUserId == userId).FirstOrDefault();
            //System.Diagnostics.Debug.WriteLine("Empresa: " + empresa.NomeEmpresa);
            //var role = db.Roles.SingleOrDefault(m => m.Name == "Funcionario");
            //var funcionarios = db.Users.Where(m => m.Roles.Any(r => r.RoleId == role.Id));
            //var funcionariosEmpresa = funcionarios.Where(u => u.Id == empresa.ApplicationUserId);

            var funcionarios = db.Funcionarios.Where(f => f.IdEmpresa == empresa.IdEmpresa);
            // QUERO PRECORRER A LISTA DE UTILIZADORES E PARA CADA USER COMPARAR O SEU ID COM CADA ID DA LISTA DE FUNCIONARIOS
            var funcionariosEmpresa = db.Users.Where(fe => funcionarios.Any(i=> i.ApplicationUserId == fe.Id));

            return View(funcionariosEmpresa.ToList());
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                //TEM DE SER CRIADO ASSIM PARA TER ID AUTO
                var user = new ApplicationUser
                {
                    NomeCompleto = model.NomeCompleto,
                    UserName = model.Email,
                    Email = model.Email
                };
                var result = await UserManager.CreateAsync(user, model.Password);

                //Add User to the selected Roles 
                if (result.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "Funcionario");

                    var loggedUserId = User.Identity.GetUserId();
                    var empresa = db.Empresas.Where(e => e.ApplicationUserId == loggedUserId).FirstOrDefault();
                    var funcionario = new Funcionario { ApplicationUserId = user.Id, IdEmpresa = empresa.IdEmpresa };
                    db.Funcionarios.Add(funcionario);
                    db.SaveChanges();



                    return RedirectToAction("IndexFuncionarios");
                }
                else
                {
                    ModelState.AddModelError("", "Já existe um utilizador com este email.");
                    return View(model);
                }
            }


            return View(model);
        }


        // GET: Clientes/Edit/5
        public ActionResult Edit(string id)
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
        public ActionResult Edit([Bind(Include = "Id,NomeCompleto,Email")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                applicationUser.UserName = applicationUser.Email;
                db.Entry(applicationUser).State = EntityState.Modified;
                db.SaveChanges();
                if(User.IsInRole("Admin"))
                    return RedirectToAction("IndexClientes");
                else return RedirectToAction("IndexFuncionarios");
            }
            return View(applicationUser);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(string id)
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            db.Users.Remove(applicationUser);
            db.SaveChanges();
            if (User.IsInRole("Admin"))
                return RedirectToAction("IndexClientes");
            else return RedirectToAction("IndexFuncionarios");
        }

        // GET: Clientes/Details/5
        public ActionResult Details(string id)
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




    }

    
}