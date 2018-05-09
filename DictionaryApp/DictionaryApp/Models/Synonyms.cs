using System;
using System.Collections.Generic;
using System.Text;

namespace DictionaryApp.Models
{


    public class Synonyms
    {
        public Metadatas metadata { get; set; }
        public List<Results> results { get; set; }
    }

    public class Metadatas
    {
    }

    public class Results
    {
        public string id { get; set; }
        public string language { get; set; }
        public List<Lexicalentry> lexicalEntries { get; set; }
        public string type { get; set; }
        public string word { get; set; }
    }

    public class Lexicalentry
    {
        public Derivativeof[] derivativeOf { get; set; }
        public Derivative[] derivatives { get; set; }
        public List<Entry> entries { get; set; }
        public Grammaticalfeature[] grammaticalFeatures { get; set; }
        public string language { get; set; }
        public string lexicalCategory { get; set; }
        public Note[] notes { get; set; }
        public Pronunciation[] pronunciations { get; set; }
        public string text { get; set; }
        public Variantform[] variantForms { get; set; }
    }

    public class Entry
    {
        public string homographNumber { get; set; }
        public List<Sens> senses { get; set; }
        public List<Variantform> variantForms { get; set; }
    }
    
    public class Sens
    {
        public List<Antonym> antonyms { get; set; }
        public List<string> domains { get; set; }
        public List<Example> examples { get; set; }
        public string id { get; set; }
        public List<string> regions { get; set; }
        public List<string> registers { get; set; }
        public List<Subsens> subsenses { get; set; }
        public List<Synonym> synonyms { get; set; }
    }

    public class Antonym
    {
        public List<string> domains { get; set; }
        public string id { get; set; }
        public string language { get; set; }
        public List<string> regions { get; set; }
        public List<string> registers { get; set; }
        public string text { get; set; }
    }

    public class Example
    {
        public List<string> definitions { get; set; }
        public List<string> domains { get; set; }
        public List<Note> notes { get; set; }
        public List<string> regions { get; set; }
        public List<string> registers { get; set; }
        public List<string> senseIds { get; set; }
        public string text { get; set; }
        public List<Translation> translations { get; set; }
    }

    public class Note
    {
        public string id { get; set; }
        public string text { get; set; }
        public string type { get; set; }
    }

    public class Translation
    {
        public List<string> domains { get; set; }
        public List<Grammaticalfeature> grammaticalFeatures { get; set; }
        public string language { get; set; }
        public List<Note1> notes { get; set; }
        public List<string> regions { get; set; }
        public List<string> registers { get; set; }
        public string text { get; set; }
    }

    public class Grammaticalfeature
    {
        public string text { get; set; }
        public string type { get; set; }
    }

    public class Note1
    {
        public string id { get; set; }
        public string text { get; set; }
        public string type { get; set; }
    }

    public class Subsens
    {
    }

    public class Synonym
    {
        public List<string> domains { get; set; }
        public string id { get; set; }
        public string language { get; set; }
        public List<string> regions { get; set; }
        public List<string> registers { get; set; }
        public string text { get; set; }
    }

    public class Variantform
    {
        public List<string> regions { get; set; }
        public string text { get; set; }
    }

    public class Variantform1
    {
        public List<string> regions { get; set; }
        public string text { get; set; }
    }


}
