using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLite_Testing
{
    class Program
    {
        public static SQLiteConnection m_dbConnection { get; set; }
        public static void Create()
        {
            using (m_dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;"))
            {
                m_dbConnection.Open();
                string sql = "insert into highscores (name, score) values ('Me', 3000)";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
                sql = "insert into highscores (name, score) values ('Myself', 6000)";
                command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
                sql = "insert into highscores (name, score) values ('And I', 9001)";
                command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
                m_dbConnection.Close();
            }
        }
        public static void GetAll()
        {
            using (m_dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;"))
            {
                m_dbConnection.Open();
                string sql = "select * from highscores order by score desc";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                Console.WriteLine("Name: " + reader["name"] + "\tScore: " + reader["score"]);
                m_dbConnection.Close();
            }
        }
        static void Main(string[] args)
        {
            //1. Run this in Package Manager Console
            //   "Install-Package System.Data.SQLite"
            SQLiteConnection.CreateFile("MyDatabase.sqlite");
            SQLiteConnection m_dbConnection;
            m_dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            m_dbConnection.Open();

            string sql = "create table highscores (name varchar(20), score int)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery(); //這一個 method 只會回傳有幾個row被動到

            Create();
            GetAll();

            m_dbConnection.Close();
        }
    }    
}
