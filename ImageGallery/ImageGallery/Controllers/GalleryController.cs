using ImageGallery.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SqlClient;
using System.IO;

namespace ImageGallery.Controllers
{
    public class GalleryController : Controller
    {

        private readonly string wwwrootDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images");
        public List<Movie> Movie = new List<Movie>();
        public IActionResult Index()
        {
            string constr = "Data Source=ADNAN;Initial Catalog=TestDB;Integrated Security=True";
            Documents documents = new Documents();

            using (SqlConnection connection = new SqlConnection(constr))
            {
                connection.Open();
                string sql = "SELECT * from Movie";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Movie movies = new Movie();
                            movies.Id = "" + reader.GetInt32(0).ToString();
                            movies.MovieName = reader.GetString(1);
                            movies.FileName = reader.GetString(2);
                            movies.UploadDate = reader.GetDateTime(3).ToString("yyyy-MM-dd");
                            Movie.Add(movies);
                        }
                    }

                }

                documents.Movie = Movie;



            }
            return View(documents);
        }

        [HttpPost]
        public async Task<IActionResult> Index(IFormFile myFile, Movie movie)
        {
            string constr = "Data Source=ADNAN;Initial Catalog=TestDB;Integrated Security=True";
            
            movie.MovieName = Request.Form["name"];

            try
            {
                string newfilename = DateTime.Now.Ticks.ToString() + myFile.FileName;

                using (SqlConnection con = new SqlConnection(constr))
                {
                    string query = "INSERT INTO Movie(MovieName, FileName) VALUES(@MovieName,@FileName)";
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        con.Open();
                        cmd.Parameters.AddWithValue("@MovieName", movie.MovieName);
                        cmd.Parameters.AddWithValue("@FileName", newfilename);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                }

                if (myFile != null)
                {
                    var path = Path.Combine(wwwrootDirectory, newfilename);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await myFile.CopyToAsync(stream);
                    }
                    return RedirectToAction("Index");
                }

            }
            catch (Exception)
            {

                throw;
            }
            

            return View();
        }

        public IActionResult Details(string Id) 
        {
            //var path = Path.Combine(
            //                Directory.GetCurrentDirectory(),
            //                "wwwroot/images", movie);
            
            string constr = "Data Source=ADNAN;Initial Catalog=TestDB;Integrated Security=True";
            Documents details = new Documents();

            using (SqlConnection connection = new SqlConnection(constr))
            {
                connection.Open();
                
                string sql = "SELECT * from Movie WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {   
                    command.Parameters.AddWithValue("@Id", Id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Movie movie = new Movie();
                            movie.Id = "" + reader.GetInt32(0).ToString();
                            movie.MovieName = reader.GetString(1);
                            movie.FileName = reader.GetString(2);
                            movie.UploadDate = reader.GetDateTime(3).ToString("yyyy-MM-dd");
                            Movie.Add(movie);
                        }
                    }

                }

                details.Movie = Movie;

            }
            return View(details);
        }
        public IActionResult Delete(string Id,string filename)
        {
            try
            {
                string connectionString = "Data Source=ADNAN;Initial Catalog=TestDB;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String sql = "DELETE from Movie WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.Parameters.AddWithValue("@Id", Id);

                        command.ExecuteNonQuery();
                        connection.Close();

                    }
                }

                if (filename != null || filename != string.Empty)
                {
                    if (System.IO.File.Exists(Path.Combine(wwwrootDirectory, filename)))
                    {
                        System.IO.File.Delete(Path.Combine(wwwrootDirectory, filename));
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }

            return Redirect("/Gallery");
        }
        public async Task<IActionResult> DownloadFile(string filePath)
        {
            var path = Path.Combine(
                            Directory.GetCurrentDirectory(),
                            "wwwroot/images", filePath);

            var memory = new MemoryStream();

            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;
            var contentType = "APPLICATION/octet-stream";
            var fileName = Path.GetFileName(path);
            return File(memory, contentType, fileName);
        }
    }
}
