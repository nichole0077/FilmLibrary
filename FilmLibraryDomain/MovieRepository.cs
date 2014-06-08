using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using FilmLibrary.Models;
using System.Configuration;
using System.Threading.Tasks;

namespace FilmLibraryDomain
{
    public class MovieRepository
    {
        private readonly string ConnectionString;

        public MovieRepository()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public List<Movie> ReadAllMovies()
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
            return movies;
        }

        public Movie ReturnDetails(int id)
        {
            var movie = new Movie();
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Select id, title, genre from movies where id='" + id + "'");
                cmd.Connection = conn;
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    movie.Id = (int)reader.GetValue(0);
                    movie.Title = (string)reader.GetString(1);
                    movie.Genre = (string)reader.GetString(2);
                }

            }
            return movie;
        }

    }
}
