using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using chatService.Models;
using System.IO;
using Microsoft.AspNet.SignalR;

namespace chatService.Controllers
{
    public class RequestInfoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        string gName = ""; 
        // GET: RequestInfoes
        public ActionResult Index()
        {
            var pending = db.RequestInfos.ToList();
            return View(pending);
        }

        public ActionResult PendingRequest()
        {
            var pending = db.RequestInfos.Where(w => w.Approved.Equals(false)).ToList();
            if (pending.Count>0)
            {
                return View(pending);
            }
            else
            {
                ModelState.AddModelError("", "There is No Pending request");
                return View(pending);
            }
        }
        // GET: RequestInfoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestInfo requestInfo = db.RequestInfos.Find(id);
            if (requestInfo == null)
            {
                return HttpNotFound();
            }
            return View(requestInfo);
        }

        //// GET: RequestInfoes/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: RequestInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Include = "Id,GroupName,ReqDateTime,UserName,Approved")] RequestInfo requestInfo)
        {
            if (ModelState.IsValid)
            {
                var reqtoInsert = new RequestInfo
                {
                    Approved = false,
                    UserName = User.Identity.Name,
                    GroupName = requestInfo.GroupName,
                    ReqDateTime = DateTime.Now.ToString()
                };
                db.RequestInfos.Add(reqtoInsert);
                if (db.SaveChanges() > 0)
                {
                    return Json(new { success = true, data = reqtoInsert });
                }
            }

            return Json(new { success = false });
        }

        public virtual string UploadFiles(object obj)
        {
            gName = db.RequestInfos.Where(u => u.UserName.Equals(User.Identity.Name)).Select(s => s.GroupName).SingleOrDefault();
            var length = Request.ContentLength;
           
		
            var bytes = new byte[length];
            if (bytes.Length <= 15728640)
            {
            Request.InputStream.Read(bytes, 0, length);
            var fileName = Request.Headers["X-File-Name"];
            var fileSize = Request.Headers["X-File-Size"];
            var fileType = Request.Headers["X-File-Type"];
            var ipath = "/Images/" + fileName;
            var saveToFileLoc = HttpContext.Server.MapPath("~/Images/") + fileName;

            var fileStream = new FileStream(saveToFileLoc, FileMode.Create, FileAccess.ReadWrite);
            fileStream.Write(bytes, 0, length);
            fileStream.Close();
            var hContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();


            db.MessageInfos.Add(new MessageInfo
            {
                MessageBody = ipath,
                PostDateTime = DateTime.Now.ToString(),
                UserName = User.Identity.Name

            });
            if (db.SaveChanges() > 0)
            {
                hContext.Clients.Group(gName).received(User.Identity.Name, ipath, "files");
            }
            return string.Format("{0} bytes uploaded", bytes.Length);
            }
            return string.Format("{0} bytes Limit exceed", bytes.Length);
        }  


        // GET: RequestInfoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestInfo requestInfo = db.RequestInfos.Find(id);
            if (requestInfo == null)
            {
                return HttpNotFound();
            }
            return View(requestInfo);
        }

        // POST: RequestInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,GroupName,ReqDateTime,UserName,Approved")] RequestInfo requestInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requestInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(requestInfo);
        }

        // GET: RequestInfoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestInfo requestInfo = db.RequestInfos.Find(id);
            if (requestInfo == null)
            {
                return HttpNotFound();
            }
            return View(requestInfo);
        }

        // POST: RequestInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RequestInfo requestInfo = db.RequestInfos.Find(id);
            db.RequestInfos.Remove(requestInfo);
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
