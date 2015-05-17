using System;
using JobSpecCollector.Data;
using JobSpecCollector.Interfaces;
using MySql.Data.MySqlClient;

namespace JobSpecCollector.Implementations
{
    public class Persistence : IPersistence, IDisposable
    {
        private ITextSplitter _splitter;
        private MySqlConnection _connection;


        public Persistence(ITextSplitter splitter, string connString)
        {
            _splitter = splitter;
            _connection = new MySqlConnection(connString);
            _connection.Open();
        }

        public void SaveJobSpec(JobSpec jobSpec)
        {
            var jsId = AddJobSpec(jobSpec);
            if (jsId <= 0) return;
            var words = _splitter.ToWords(jobSpec.SpecText);
            foreach(var word in words)
            {
                AddWord(jsId, word);
            }
        }


        private int AddJobSpec(JobSpec jobSpec)
        {
            var cmd = _connection.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "AddJobSpec";
            cmd.Parameters.Add(new MySqlParameter("title", jobSpec.Title));
            cmd.Parameters.Add(new MySqlParameter("link", jobSpec.Link));
            cmd.Parameters.Add(new MySqlParameter("source", jobSpec.Source));
            cmd.Parameters.Add(new MySqlParameter("guid", jobSpec.Guid.ToString()));
            cmd.Parameters.Add(new MySqlParameter("pubdate", jobSpec.PubDate));
            cmd.Parameters.Add(new MySqlParameter("description", jobSpec.Description));
            cmd.Parameters.Add(new MySqlParameter("spectext", jobSpec.SpecText));
            dynamic result = cmd.ExecuteScalar();
            if (result is uint) return (int)(uint)result;
            return (int)result;
        }

        private void AddWord(int jobSpecId, string word)
        {
            word = word.ToLower();
            var cmd = _connection.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "AddWord";
            cmd.Parameters.Add(new MySqlParameter("jobspecId", jobSpecId));
            cmd.Parameters.Add(new MySqlParameter("word", word));
            cmd.ExecuteNonQuery();
        }

        public void Dispose()
        {
            if (_connection != null)
            {
                if (_connection.State != System.Data.ConnectionState.Closed)
                {
                    try
                    {
                        _connection.Close();
                    }
                    catch { }
                }
                _connection.Dispose();
                _connection = null;
            }
        }

    }
}
