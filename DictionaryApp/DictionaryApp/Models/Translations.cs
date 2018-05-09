using System;
using System.Collections.Generic;
using System.Text;

namespace DictionaryApp.Models
{

    public class Translations
    {
        public Metadata metadata { get; set; }
        public TransResult[] results { get; set; }
    }


    public class TransResult
    {
        public string id { get; set; }
        public string language { get; set; }
        public TransLexicalentry[] lexicalEntries { get; set; }
        public Pronunciation[] pronunciations { get; set; }
        public string type { get; set; }
        public string word { get; set; }
    }

    public class TransLexicalentry
    {
        public Derivativeof[] derivativeOf { get; set; }
        public Derivative[] derivatives { get; set; }
        public List<TransEntry> entries { get; set; }
        public Grammaticalfeature[] grammaticalFeatures { get; set; }
        public string language { get; set; }
        public string lexicalCategory { get; set; }
        public Note[] notes { get; set; }
        public Pronunciation[] pronunciations { get; set; }
        public string text { get; set; }
        public Variantform[] variantForms { get; set; }
    }

    public class Derivativeof
    {
        public string[] domains { get; set; }
        public string id { get; set; }
        public string language { get; set; }
        public string[] regions { get; set; }
        public string[] registers { get; set; }
        public string text { get; set; }
    }

    public class Derivative
    {
        public string[] domains { get; set; }
        public string id { get; set; }
        public string language { get; set; }
        public string[] regions { get; set; }
        public string[] registers { get; set; }
        public string text { get; set; }
    }

    public class TransEntry
    {
        public string[] etymologies { get; set; }
        public Grammaticalfeature[] grammaticalFeatures { get; set; }
        public string homographNumber { get; set; }
        public Note[] notes { get; set; }
        public Pronunciation[] pronunciations { get; set; }
        public List<TransSens> senses { get; set; }
        public Variantform[] variantForms { get; set; }
    }

    public class Pronunciation
    {
        public string audioFile { get; set; }
        public string[] dialects { get; set; }
        public string phoneticNotation { get; set; }
        public string phoneticSpelling { get; set; }
        public string[] regions { get; set; }
    }

    public class TransSens
    {
        public string[] crossReferenceMarkers { get; set; }
        public Crossreference[] crossReferences { get; set; }
        public string[] definitions { get; set; }
        public string[] domains { get; set; }
        public List<Example> examples { get; set; }
        public string id { get; set; }
        public Note[] notes { get; set; }
        public Pronunciation[] pronunciations { get; set; }
        public string[] regions { get; set; }
        public string[] registers { get; set; }
        public string[] short_definitions { get; set; }
        public TransSubsens[] subsenses { get; set; }
        public Thesauruslink[] thesaurusLinks { get; set; }
        public List<Translation> translations { get; set; }
        public Variantform[] variantForms { get; set; }
    }

    public class Crossreference
    {
        public string id { get; set; }
        public string text { get; set; }
        public string type { get; set; }
    }


    public class Thesauruslink
    {
        public string entry_id { get; set; }
        public string sense_id { get; set; }
    }

    public class TransSubsens
    {
        public List<Example> examples { get; set; }
        public string id { get; set; }
        public Note[] notes { get; set; }
        public List<Translation> translations { get; set; }
        public string[] domains { get; set; }
    }
}
