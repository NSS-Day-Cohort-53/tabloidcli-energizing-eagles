using System;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using TabloidCLI.Models;


namespace TabloidCLI.Repositories
{
    public class NoteRepository : DatabaseConnector, IRepository<Note>
    {
        public NoteRepository(string connectionString) : base(connectionString)
        {   }
        public List<Note> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select Id,Title, Content, CreateDateTime,PostId 
                    from Note";

                    List<Note> notes = new List<Note>();


                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) 
                    {
                        Note note = new Note()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Content= reader.GetString(reader.GetOrdinal("Content")),
                            CreateDateTime=reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                            PostId=reader.GetInt32(reader.GetOrdinal("PostId"))
                        };
                        notes.Add(note);
                    }
                    reader.Close();
                    return notes;
                }
               
            }
        }
            public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Note Get(int id)
        {
            throw new NotImplementedException();
        }

     

        public void Insert(Note note)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Note(Title,Content,CreateDateTime,PostId)
                        VALUES(@title,@content,@createDateTime,@postId)
                        ";
                    cmd.Parameters.AddWithValue("@title", note.Title);
                    cmd.Parameters.AddWithValue("@content", note.Content);
                    cmd.Parameters.AddWithValue("@createDateTime", note.CreateDateTime);
                    cmd.Parameters.AddWithValue("@postId", note.PostId);
                    cmd.ExecuteNonQuery();


                }    
            }
        }

        public void Update(Note entry)
        {
            throw new NotImplementedException();
        }
    }
}
