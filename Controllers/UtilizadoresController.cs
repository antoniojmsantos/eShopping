using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TP_PWEB.Models;

namespace TP_PWEB.Controllers
{
    public class UtilizadoresController : Controller
    {
        // GET: Utilizadores

        private ApplicationDbContext db = new ApplicationDbContext();

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
            var role = db.Roles.SingleOrDefault(m => m.Name == "Funcionario");
            var usersInRole = db.Users.Where(m => m.Roles.Any(r => r.RoleId == role.Id));
            return View(usersInRole);
        }
    }
}