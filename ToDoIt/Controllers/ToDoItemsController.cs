using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ToDoIt.Models;

namespace ToDoIt.Controllers
{
    public class ToDoItemsController : Controller
    {
        private ToDoItContext db = new ToDoItContext();

        // GET: ToDoItems
        public ActionResult Index()
        {
            return View(db.ToDoItems.ToList());
        }

        // GET: ToDoItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoItem toDoItem = db.ToDoItems.Find(id);
            if (toDoItem == null)
            {
                return HttpNotFound();
            }
            return View(toDoItem);
        }

        // GET: ToDoItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ToDoItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ToDoItemID,ItemName,ItemNote,Priority,DateDue")] ToDoItem toDoItem)
        {
            toDoItem.DateCreated = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.ToDoItems.Add(toDoItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(toDoItem);
        }

        // GET: ToDoItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoItem toDoItem = db.ToDoItems.Find(id);
            if (toDoItem == null)
            {
                return HttpNotFound();
            }
            return View(toDoItem);
        }

        // POST: ToDoItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ToDoItemID,ItemName,ItemNote,Priority,DateDue")
            ] ToDoItem toDoItem)
        {
            // Look up the item that's currently in the database
            var existingToDoItem = db.ToDoItems.Find(toDoItem.ToDoItemID);

            if (existingToDoItem == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                //db.Entry(toDoItem).State = EntityState.Modified;
                existingToDoItem.ItemName = toDoItem.ItemName;
                existingToDoItem.ItemNote = toDoItem.ItemNote;
                existingToDoItem.Priority = toDoItem.Priority;
                existingToDoItem.DateDue = toDoItem.DateDue;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(toDoItem);
        }
        // POST: ToDoItems/Finish/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Finish(int? id)
        {
            // Look up the item that's currently in the database
            var existingToDoItem = db.ToDoItems.Find(id);

            if (existingToDoItem == null)
            {
                return HttpNotFound();
            }

            //db.Entry(toDoItem).State = EntityState.Modified;
            existingToDoItem.DateCompleted = DateTime.Now;

            db.SaveChanges();
            return RedirectToAction("Index");
            
            
        }

        // POST: ToDoItems/Unfinish/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UnFinish(int? id)
        {
            // Look up the item that's currently in the database
            var existingToDoItem = db.ToDoItems.Find(id);

            if (existingToDoItem == null)
            {
                return HttpNotFound();
            }

            //db.Entry(toDoItem).State = EntityState.Modified;
            existingToDoItem.DateCompleted = null;

            db.SaveChanges();
            return RedirectToAction("Index");


        }
        // GET: ToDoItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoItem toDoItem = db.ToDoItems.Find(id);
            if (toDoItem == null)
            {
                return HttpNotFound();
            }
            return View(toDoItem);
        }

        // POST: ToDoItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ToDoItem toDoItem = db.ToDoItems.Find(id);
            db.ToDoItems.Remove(toDoItem);
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
