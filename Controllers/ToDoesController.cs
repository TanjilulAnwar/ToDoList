using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ToDoList.Models;
using Microsoft.AspNet.Identity;
namespace ToDoList.Controllers
{
    public class ToDoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: ToDoes
        public ActionResult Index()
        {
            if (User.Identity.GetUserId() == null)
            {
                return RedirectToAction("login", "Account");
            }


            return View();
        }

        public ActionResult Complete()
        {
            if (User.Identity.GetUserId() == null)
            {
                return RedirectToAction("login", "Account");
            }

            return View();
        }

        private IEnumerable<ToDo> GetMyToDoes()
        {

            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            return db.ToDos.ToList().Where(x => x.User == currentUser);
        }


        private IEnumerable<Done> GetMyDones()
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            return db.Dones.ToList().Where(x => x.User == currentUser);
        }

        public ActionResult BuildToDoTable()
        {


            return PartialView("_ToDoTable", GetMyToDoes());

        }
        public ActionResult BuildDoneTable()
        {


            return PartialView("_doneTable", GetMyDones());

        }

        // GET: ToDoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDo toDo = db.ToDos.Find(id);
            if (toDo == null)
            {
                return HttpNotFound();
            }
            return View(toDo);
        }

        // GET: ToDoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ToDoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "id,Description,isDone")] ToDo toDo)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        string currentUserId = User.Identity.GetUserId();
        //        ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
        //        toDo.User = currentUser;
        //        db.ToDos.Add(toDo);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(toDo);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AJAXCreate(ToDo toDo)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
                toDo.User = currentUser;

                toDo.isDone = false;
                db.ToDos.Add(toDo);

                db.SaveChanges();

            }

            return PartialView("_ToDoTable", GetMyToDoes());
        }

        //

        // GET: ToDoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDo toDo = db.ToDos.Find(id);


            if (toDo == null)
            {
                return HttpNotFound();
            }


            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            if (toDo.User != currentUser)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(toDo);
        }
        /// <summary>
        /// ///DOneedit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DoneEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Done dun = db.Dones.Find(id);


            if (dun == null)
            {
                return HttpNotFound();
            }


            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            if (dun.User != currentUser)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(dun);
        }



        // POST: ToDoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoneEdit([Bind(Include = "id,Description,isDone")] Done dun)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dun).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dun);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "id,Description,isDone")] ToDo toDo)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(toDo).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(toDo);
        //}
        [HttpPost]

        public ActionResult AJAXEdit(int? id, bool value, Done dun)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDo toDo = db.ToDos.Find(id);


            if (toDo == null)
            {
                return HttpNotFound();
            }
            else
            {

                toDo.isDone = value;
                dun.User = toDo.User;
                dun.Description = toDo.Description;
                db.Dones.Add(dun);
                db.ToDos.Remove(toDo);
                db.Entry(toDo).State = EntityState.Modified;

                db.SaveChanges();

                return PartialView("_ToDoTable", GetMyToDoes());



            }

        }

        // GET: ToDoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDo toDo = db.ToDos.Find(id);
            if (toDo == null)
            {
                return HttpNotFound();
            }
            return View(toDo);
        }

        // POST: ToDoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ToDo toDo = db.ToDos.Find(id);
            db.ToDos.Remove(toDo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: ToDoes/Delete/5
        public ActionResult DoneDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Done dun = db.Dones.Find(id);
            if (dun == null)
            {
                return HttpNotFound();
            }
            return View(dun);
        }

        // POST: ToDoes/Delete/5
        [HttpPost, ActionName("DoneDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DoneDeleteConfirmed(int id)
        {
            Done dun = db.Dones.Find(id);
            db.Dones.Remove(dun);
            db.SaveChanges();
            return RedirectToAction("Complete");
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
