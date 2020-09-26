using System;
using static System.Console;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text;

namespace Document_Serialize
{
    class Program
    {
        static async Task Main(string[] args)
        {

            const string host = "mysql60.hostland.ru";
            const string port = "3306";
            const string username = "host1323541_itstep";
            const string password = "269f43dc";
            const string database = "host1323541_itstep27";

            const string connString = "Server=" + host + ";Database=" + database + ";port=" + port + ";User Id=" + username + ";password=" + password;
            MySqlConnection db = new MySqlConnection(connString);
           

            db.Open();

            const string SQL_Easy = "SELECT id, description, term, creation_date FROM document";
            MySqlCommand command = new MySqlCommand();
            command.Connection = db;
            command.CommandText = SQL_Easy;
            var result = command.ExecuteReader();

            List<Document_Model> documents = new List<Document_Model>();

            while(result.Read())
            {
                Document_Model document = new Document_Model();
                document.id = result.GetInt32("id");
                document.description = result.GetString("description");
                document.term = result.GetDateTime("term");
                document.creation_date = result.GetDateTime("creation_date");

                documents.Add(document);
            }

            db.Close();




            db.Open();

            const string SQL_type = "SELECT document_type.id, document_type.type, document_type.is_active FROM document_type, document WHERE document.document_type_id=document_type.id";
            MySqlCommand command_type = new MySqlCommand();
            command_type.Connection = db;
            command_type.CommandText = SQL_type;
            var result_type = command_type.ExecuteReader();

            while (result_type.Read())
            {
                Document_type type = new Document_type();
                type.id = (int)result_type.GetValue(0);
                type.type = (string)result_type.GetValue(1);
                type.is_active = (bool)result_type.GetValue(2);

                documents[0].type = type;
            }

            db.Close();



            db.Open();
            const string SQL_files = "SELECT document_files.id, document_files.link FROM document_files, document, document_file_relation WHERE document_file_relation.document_id = document.id AND document_file_relation.document_file_id = document_files.id";
            MySqlCommand command_files = new MySqlCommand();
            command_files.Connection = db;
            command_files.CommandText = SQL_files;
            var result_files = command_files.ExecuteReader();

            while (result_files.Read())
            {
                Document_files files = new Document_files();
                files.id = (int)result_files.GetValue(0);
                files.link = (string)result_files.GetValue(1);


                documents[0].files.Add(files);
            }
            db.Close();



            db.Open();
            const string SQL_respons = "SELECT document_respons.id, document_respons.name, document_respons.position FROM document_respons, document, document_respons_relation WHERE document_respons_relation.document_id=document.id AND document_respons_relation.document_respons_id=document_respons.id";
            MySqlCommand command_respons = new MySqlCommand();
            command_respons.Connection = db;
            command_respons.CommandText = SQL_respons;
            var result_respons = command_respons.ExecuteReader();

            while (result_respons.Read())
            {
                Document_respons resp = new Document_respons();
                resp.id = (int)result_respons.GetValue(0);
                resp.name = (string)result_respons.GetValue(1);
                resp.position = (string)result_respons.GetValue(2);


                documents[0].respons.Add(resp);
            }
            db.Close();


            db.Open();
            const string SQL_creator = "SELECT document_creator.id, document_creator.name, document_creator.position FROM document_creator, document WHERE document.creator_id = document_creator.id";
            MySqlCommand command_creator = new MySqlCommand();
            command_creator.Connection = db;
            command_creator.CommandText = SQL_creator;
            var result_creator = command_creator.ExecuteReader();

            while (result_creator.Read())
            {
                Document_creator creat = new Document_creator();
                creat.id = (int)result_creator.GetValue(0);
                creat.name = (string)result_creator.GetValue(1);
                creat.position = (string)result_creator.GetValue(2);


                documents[0].creator = creat;
            }
            db.Close();


            await using (FileStream fileWrite = new FileStream("doc.json", FileMode.OpenOrCreate))
            {
                await JsonSerializer.SerializeAsync(fileWrite, documents);
            }
           
        }
    }
}

