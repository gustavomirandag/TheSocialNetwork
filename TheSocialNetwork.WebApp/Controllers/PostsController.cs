using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TheSocialNetwork.DataAccess.Contexts;
using TheSocialNetwork.DomainModel.Entities;

namespace TheSocialNetwork.WebApp.Controllers
{
    public class PostsController : Controller
    {
        private SocialNetworkContext db = new SocialNetworkContext();

        public static async Task<string> GetAPIToken(string userName, string password, string apiBaseUri)
        {
            using (var client = new HttpClient())
            {
                //setup client
                client.BaseAddress = new Uri(apiBaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                //setup login data
                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", userName),
                    new KeyValuePair<string, string>("password", password),
                });

                //send request
                HttpResponseMessage responseMessage = client.PostAsync("/Token", formContent).Result;

                //get access token from response body
                var responseJson = await responseMessage.Content.ReadAsStringAsync();
                var jObject = JObject.Parse(responseJson);
                return jObject.GetValue("access_token").ToString();
            }
        }

        // GET: Posts
        public ActionResult Index()
        {
            string token = GetAPIToken(Session["profileIdentityUsername"].ToString(), 
                Session["profileIdentityPassword"].ToString(),
               WebApp.Properties.Settings.Default.PostWebApiBaseURI).Result;

            var httpClient = new HttpClient();
            httpClient.BaseAddress = 
                new Uri(WebApp.Properties.Settings.Default.PostWebApiBaseURI);
            httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            //Http Get
            HttpResponseMessage response = httpClient.GetAsync("/api/posts").Result;
            string serializedPostsCollection = response.Content.ReadAsStringAsync().Result;
            Post[] posts = Newtonsoft
                .Json.JsonConvert
                .DeserializeObject<Post[]>(serializedPostsCollection);

            return View(posts.ToList());
        }

        // GET: Posts/Details/5
        public ActionResult Details(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //======= Web API Access =======
            string token = GetAPIToken(Session["profileIdentityUsername"].ToString(),
                Session["profileIdentityPassword"].ToString(),
                WebApp.Properties.Settings.Default.PostWebApiBaseURI).Result;

            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(WebApp.Properties.Settings.Default.PostWebApiBaseURI);
            httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            //Http Get
            HttpResponseMessage response = httpClient.GetAsync($"/api/posts/{id}").Result;
            string serializedPost = response.Content.ReadAsStringAsync().Result;
            Post post = Newtonsoft
                .Json.JsonConvert
                .DeserializeObject<Post>(serializedPost);
            //==============================



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
                string token = GetAPIToken(Session["profileIdentityUsername"].ToString(),
                    Session["profileIdentityPassword"].ToString(),
                    WebApp.Properties.Settings.Default.PostWebApiBaseURI).Result;

                var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(WebApp.Properties.Settings.Default.PostWebApiBaseURI);
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                post.Sender = db.Profiles.Find(Guid.Parse(Session["profileId"].ToString()));

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
                string token = GetAPIToken(Session["profileIdentityUsername"].ToString(),
                    Session["profileIdentityPassword"].ToString(),
                    WebApp.Properties.Settings.Default.PostWebApiBaseURI).Result;

                var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(WebApp.Properties.Settings.Default.PostWebApiBaseURI);
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

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
            string token = GetAPIToken(Session["profileIdentityUsername"].ToString(),
                Session["profileIdentityPassword"].ToString(),
                WebApp.Properties.Settings.Default.PostWebApiBaseURI).Result;

            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(WebApp.Properties.Settings.Default.PostWebApiBaseURI);
            httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

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
