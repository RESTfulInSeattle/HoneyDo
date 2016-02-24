using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using HoneyDo.Models;

//ToDo Put all business logic in controller to unit test it, and finish all tests

namespace HoneyDo.Controllers
{
    public class TdController : Controller
    {
        private Repository repository;
        private ApplicationDbContext db = new ApplicationDbContext();
        //We want everyone who registers to be able to create, edit, and delete their ToDo
        private const string DefaultRole = "canEdit";

        public TdController(Repository respository)
        {
            repository = respository;
        }

        public TdController()
        {
            this.repository = new WorkingTodoRepository();
        }

        // GET: Td
        [Authorize(Roles = DefaultRole)]
        public ViewResult Index()
        {
            var todoes = repository.GetAll();
            todoes = todoes.OrderByDescending(todo => todo.Deadline).ToList();
            return View(todoes);
        }

        // GET: Td/Details/5
        [Authorize(Roles = DefaultRole)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Todo todo = repository.Find(id);

            if (todo == null)
            {
                return HttpNotFound();
            }
            return View(todo);
        }

        // GET: Td/Create
        [Authorize(Roles = DefaultRole)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Td/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = DefaultRole)]
        public ActionResult Create([Bind(Include = "TodoId,OwnerId,TaskName,Deadline,Completed,Moredetails")] Todo todo)
        {
            if (ModelState.IsValid)
            {
                db.Todoes.Add(todo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(todo);
        }

        // GET: Td/Edit/5
        [Authorize(Roles = DefaultRole)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Todo todo = db.Todoes.Find(id);
            if (todo == null)
            {
                return HttpNotFound();
            }
            return View(todo);
        }

        // POST: Td/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = DefaultRole)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TodoId,OwnerId,TaskName,Deadline,Completed,Moredetails")] Todo todo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(todo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(todo);
        }

        // GET: Td/Delete/5
        [Authorize(Roles = DefaultRole)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Todo todo = db.Todoes.Find(id);
            if (todo == null)
            {
                return HttpNotFound();
            }
            return View(todo);
        }

        // POST: Td/Delete/5
        [Authorize(Roles = DefaultRole)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Todo todo = db.Todoes.Find(id);
            db.Todoes.Remove(todo);
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
