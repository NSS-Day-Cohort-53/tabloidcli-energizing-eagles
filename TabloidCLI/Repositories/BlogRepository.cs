using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Models;
using Microsoft.Data.SqlClient;

namespace TabloidCLI.Repositories
{
    public class BlogRepository: DatabaseConnector
    {
        public BlogRepository(string connectionString) : base(connectionString) { }

        public string Title { set; get; }
        public string Url { get; set; }

        public void insert(Blog blog)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO  Blog (Title, Url)
                                        VALUES (@Title, @Url)";
                    cmd.Parameters.AddWithValue("@Title", blog.Title);
                    cmd.Parameters.AddWithValue("@Url", blog.Url);

                    cmd.ExecuteNonQuery();
                }
            }

        }

        public void GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Title, Url FROM Blog";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Blog> list = new List<Blog>();
                        while (reader.Read())
                        {
                            int titleColumnPosition = reader.GetOrdinal("Title");
                            string titleValue = reader.GetString(titleColumnPosition);

                            int urlColumnPosition = reader.GetOrdinal("URL");
                            string urlValue = reader.GetString(urlColumnPosition);

                            list.Add(new Blog() { Title = titleValue, Url = urlValue });
                        }

                        foreach (Blog blog in list)
                        {
                            Console.WriteLine($"Title: {blog.Title}, URL: {blog.Url} ");
                        }
                    }
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = Connection)
            {
                connection.Open();

                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Blog where Id = @id";
                }
            }
        }
        
    }
}
