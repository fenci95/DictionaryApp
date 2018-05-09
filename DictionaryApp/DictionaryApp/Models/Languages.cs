using System;
using System.Collections.Generic;
using System.Text;

namespace DictionaryApp.Models
{
    
    public class Languages
    {
        public Metadata metadata { get; set; }
        public List<Result> results { get; set; }
    }
    
    public class Metadata
    {
    }

    public class Result
    {
        public string region { get; set; }
        public string source { get; set; }
        public Sourcelanguage sourceLanguage { get; set; }
        public Targetlanguage targetLanguage { get; set; }
        public string type { get; set; }
    }

    public class Sourcelanguage
    {
        public string id { get; set; }
        public string language { get; set; }
    }

    public class Targetlanguage
    {
        public string id { get; set; }
        public string language { get; set; }
    }

}
