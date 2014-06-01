using FilmLibrary.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FilmLibrary.Controllers
{
    public class MovieController : Controller
    {
        static List<Movie> _movies = new List<Movie>();
        private readonly string ConnectionString;

        public MovieController()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        // GET: Movie
        public ActionResult Index()
        {
            var movies = new List<Movie>();
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select id, title, genre from movies");
                cmd.Connection = conn;
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    movies.Add(
                        new Movie
                        {
                            Id = (int)reader["id"],
                            Title = (string)reader["title"],
                            Genre = (string)reader["genre"]
                        }
                    );
                }
            }
            return View(movies);
        }

        // GET: Movie/Details/5
        public ActionResult Details(int id)
        {
            return View(_movies[id-1]);
        }

        // GET: Movie/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movie/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var movie = new Movie
                {
                    Id = _movies.Count + 1,
                    Title = collection["Title"],
                    Genre = collection["Genre"]
                };

                using (var conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Insert into movies (Title, Genre) Values ('" + movie.Title + "','" + movie.Genre + "');");
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                }
                

                _movies.Add(movie);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Movie/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_movies[id-1]);
        }

        // POST: Movie/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                var movie = _movies[id - 1];
                movie.Title = collection["Title"];
                movie.Genre = collection["Genre"];

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Movie/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_movies[id-1]);
        }

        // POST: Movie/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _movies.RemoveAt(id - 1);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
