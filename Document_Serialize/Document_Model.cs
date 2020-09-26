using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using static System.Console;

namespace Document_Serialize
{
    class Document_Model
    {

        public int id { get; set; }

        public Document_type type = new Document_type();
        public string description { get; set; }
        public DateTime term { get; set; }
        public DateTime creation_date { get; set; }

        public List<Document_files> files = new List<Document_files>();

        public List<Document_respons> respons = new List<Document_respons>();

        public Document_creator creator = new Document_creator();
        


    }

    class Document_type
    {
        public int id { get; set; }
        public string type { get; set; }
        public bool is_active { get; set; }


    }

    class Document_creator
    {
        public int id { get; set; }

        public string name { get; set; }

        public string position { get; set; }
    }

    class Document_files
    {
        public int id { get; set; }

        public string link { get; set; }
    }

    class Document_respons
    {
        public int id { get; set; }

        public string name { get; set; }

        public string position { get; set; }
    }


}
