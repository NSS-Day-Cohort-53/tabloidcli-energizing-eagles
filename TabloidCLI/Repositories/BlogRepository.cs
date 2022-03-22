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

        public List<Blog> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id,
                                               Title,
                                               URL
                                         FROM  Blog";

                    List<Blog> blogs = new List<Blog>();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Blog blog = new Blog()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Url = reader.GetString(reader.GetOrdinal("URL"))
                        };
                        blogs.Add(blog);
                    }

                    reader.Close();

                    return blogs;
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
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(int id, string title, string url)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Blog
                                      SET 
                                      Title= @title,
                                      Url = @url
                                      WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@url", url);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        
    }
}
