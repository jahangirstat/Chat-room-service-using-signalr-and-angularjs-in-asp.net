﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using chatService.Models;

namespace chatService.Controllers
{
    public class GroupInfoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: GroupInfoes
        public ActionResult Index()
        {
            return View(db.GroupInfos.ToList());
        }
        [Authorize(Roles="Admin")]

        // GET: GroupInfoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupInfo groupInfo = db.GroupInfos.Find(id);
            if (groupInfo == null)
            {
                return HttpNotFound();
            }
            return View(groupInfo);
        }

        [Authorize(Roles = "Admin")]

        // GET: GroupInfoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GroupInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,GroupName")] GroupInfo groupInfo)
        {
            if (ModelState.IsValid)
            {
                db.GroupInfos.Add(groupInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(groupInfo);
        }

        [Authorize(Roles = "Admin")]
        // GET: GroupInfoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupInfo groupInfo = db.GroupInfos.Find(id);
            if (groupInfo == null)
            {
                return HttpNotFound();
            }
            return View(groupInfo);
        }

        [Authorize(Roles = "Admin")]
        // POST: GroupInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,GroupName")] GroupInfo groupInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(groupInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(groupInfo);
        }

        [Authorize(Roles = "Admin")]
        // GET: GroupInfoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupInfo groupInfo = db.GroupInfos.Find(id);
            if (groupInfo == null)
            {
                return HttpNotFound();
            }
            return View(groupInfo);
        }

        [Authorize(Roles = "Admin")]
        // POST: GroupInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GroupInfo groupInfo = db.GroupInfos.Find(id);
            db.GroupInfos.Remove(groupInfo);
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
