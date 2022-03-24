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

        // adds a new object to the blog table (title and url)
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
        public Blog GetById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    // gets blogs and their associated tag names and tag ids
                    cmd.CommandText = @"SELECT b.Id, 
                                               b.Title,
                                               b.URL,
                                               Tag.Name AS TagName,
                                               Tag.Id AS TagId
                                        FROM Blog b
                                        Left JOIN BlogTag bt ON bt.BlogId = b.Id
                                        Left JOIN Tag ON Tag.Id = bt.TagId
                                        WHERE b.Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    Blog blog = null;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        // if the blog is unassigned provide its id, title, and url values
                        if (blog == null)
                        {
                            blog = new Blog()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Url = reader.GetString(reader.GetOrdinal("URL")),
                            };
                        }

                        // if tag id is not empty, append the current tag into its tag list
                        if (!reader.IsDBNull(reader.GetOrdinal("TagId")))
                        {
                            blog.Tags.Add(new Tag()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("TagId")),
                                Name = reader.GetString(reader.GetOrdinal("TagName")),
                            });
                        }
                    }
                    reader.Close();
                    return blog;
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

        // this one adds a new object to the blog tag table
        public void AddTagToBlog(int blogId, int tagId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO BlogTag (BlogId, TagId)
                                         VALUES (@blogId, @tagId)";
                    cmd.Parameters.AddWithValue("@blogId", blogId);
                    cmd.Parameters.AddWithValue("@tagId", tagId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public int AmountOfObjects()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"select Count(Id) AS 'count' from Blog";

                    int count=0;

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        count = reader.GetInt32(reader.GetOrdinal("count"));
                    }
                   // Console.WriteLine($"{count}");
                    return count;
                }
            }
        }
        
        public List<int> blogIds()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"select Id from Blog";

                    List<int> ids = new List<int>();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ids.Add(reader.GetInt32(reader.GetOrdinal("Id")));
                    }
                    // Console.WriteLine($"{count}");
                    return ids;
                }
            }

        }

        public void DeleteTagFromBlog(int id)
        {
            using (SqlConnection connection = Connection)
            {
                connection.Open();

                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM BlogTag where Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public int SelectBlogTagId(int tagId, int blogId)
        {
            using (SqlConnection connection = Connection)
            {
                connection.Open();

                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"Select Id from BlogTag where BlogId = @blogId and TagId= @tagId";
                    cmd.Parameters.AddWithValue("@blogId", blogId);
                    cmd.Parameters.AddWithValue("@tagId", tagId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int btId=0;
                        while (reader.Read())
                        {
                             btId = reader.GetInt32(reader.GetOrdinal("Id"));
                        }
                        return btId;

                    }


                    
                }
            }

        }
    }
}
