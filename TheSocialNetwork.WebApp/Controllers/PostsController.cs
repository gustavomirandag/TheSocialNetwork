using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TheSocialNetwork.DataAccess.Contexts;
using TheSocialNetwork.DomainModel.Entities;

namespace TheSocialNetwork.WebApp.Controllers
{
    public class PostsController : Controller
    {
        private SocialNetworkContext db = new SocialNetworkContext();

        // GET: Posts
        public ActionResult Index()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = 
                new Uri("https://thesocialnetworkpostwebapi.azurewebsites.net");
            httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            //Http Get
            HttpResponseMessage response = httpClient.GetAsync("/api/posts").Result;
            string serializedPostsCollection = response.Content.ReadAsStringAsync().Result;
            Post[] posts = Newtonsoft
                .Json.JsonConvert
                .DeserializeObject<Post[]>(serializedPostsCollection);

            return View(posts.ToList());
        }

        // GET: Posts/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PublishDateTime,Content")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.Id = Guid.NewGuid();

                //==== Acesso a PostWebAPI ====
                var httpClient = new HttpClient();
                httpClient.BaseAddress =
                    new Uri("https://thesocialnetworkpostwebapi.azurewebsites.net");
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string serializedPost = Newtonsoft.Json.JsonConvert.SerializeObject(post);
                var httpContent = new StringContent(serializedPost, Encoding.UTF8, "application/json");

                //Http Post
                HttpResponseMessage response = httpClient.PostAsync("/api/posts",httpContent).Result;
                //=============================

                return RedirectToAction("Index");
            }

            return View(post);
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PublishDateTime,Content")] Post post)
        {
            if (ModelState.IsValid)
            {
                //==== Acesso a PostWebAPI ====
                var httpClient = new HttpClient();
                httpClient.BaseAddress =
                    new Uri("https://thesocialnetworkpostwebapi.azurewebsites.net");
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string serializedPost = Newtonsoft.Json.JsonConvert.SerializeObject(post);
                var httpContent = new StringContent(serializedPost, Encoding.UTF8, "application/json");

                //Http Post
                HttpResponseMessage response = httpClient.PutAsync($"/api/posts/{post.Id}", httpContent).Result;
                //=============================
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            //==== Acesso a PostWebAPI ====
            var httpClient = new HttpClient();
            httpClient.BaseAddress =
                new Uri("https://thesocialnetworkpostwebapi.azurewebsites.net");
            httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            //Http Post
            HttpResponseMessage response = httpClient.DeleteAsync($"/api/posts/{id}").Result;
            //=============================
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
