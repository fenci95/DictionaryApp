using DictionaryApp.Models;
using DictionaryApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DictionaryApp.ViewModels
{
    public class TranslateViewModel : INotifyPropertyChanged
    {
        public TranslateViewModel()
        {
            TranslationCommand = new Command(getTranslation);
            Init();
        }
        public Languages Languages { get; set; } = new Languages();

        public Translations TransResult = new Translations();

        public ICommand TranslationCommand { get; private set; }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private ObservableCollection<string> _outputLanguages { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> OutputLanguages

        {
            get { return _outputLanguages; }
            set

            {
                _outputLanguages = value;
                OnPropertyChanged("OutputLanguages");
            }
        }
        private ObservableCollection<string> _inputLanguages { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> InputLanguages

        {
            get { return _inputLanguages; }
            set

            {
                _inputLanguages = value;
                OnPropertyChanged("InputLanguages");
            }
        }

        private ObservableCollection<TranslateEntry> _translationEntry = new ObservableCollection<TranslateEntry>();
        public ObservableCollection<TranslateEntry> TranslationEntry

        {
            get { return _translationEntry; }
            set

            {
                _translationEntry = value;
                OnPropertyChanged("TranslationEntry");
            }
        }

        private string _selectedInput { get; set; } = "";
        public string SelectedInput
        {
            get { return _selectedInput; }
            set
            {
                if (_selectedInput != value)
                {
                    _selectedInput = value;
                    getSecondaryLanguages(_selectedInput);
                }
            }
        }

        private string _selectedOutput { get; set; } = "";
        public string SelectedOutput
        {
            get { return _selectedOutput; }
            set
            {
                if (_selectedOutput != value)
                {
                    _selectedOutput = value;
                }
            }
        }

        private string _word { get; set; }
        public string Word
        {
            get { return _word; }
            set
            {
                if (_word != value)
                {
                    _word = value;
                    OnPropertyChanged();
                }
            }
        }

        public async Task Init()
        {
            var service = new DictionaryService();
            Languages = await service.GetLanguagesAsync();
            getInputLanguages();
        }

        public void getInputLanguages()
        {
            foreach (var item in Languages.results)
            {
                if (item.type.Equals("bilingual"))
                InputLanguages.Add(item.sourceLanguage.language);
            }

            InputLanguages = new ObservableCollection<string>(InputLanguages.Distinct());
            InputLanguages = new ObservableCollection<string>(InputLanguages.OrderBy(i => i));
        }

        public void getSecondaryLanguages(string name)
        {
            OutputLanguages.Clear();
            foreach (var item in Languages.results)
            {
                if (item.type.Equals("bilingual"))
                {
                    if (item.sourceLanguage.language.Equals(name))
                    {
                        OutputLanguages.Add(item.targetLanguage.language);
                    }
                }
            }
            OutputLanguages = new ObservableCollection<string>(OutputLanguages.OrderBy(i => i));
        }

        public async void getTranslation()
        {
            string source_lang = getLanguageId(SelectedInput);
            string target_lang = getLanguageId(SelectedOutput);
            var service = new DictionaryService();
            var uri = "/api/v1/entries/" + source_lang + "/" + Word + "/translations=" + target_lang;
            if( SelectedInput == null || SelectedOutput == null)
            {
                DependencyService.Get<IMessage>().LongAlert("Please select language!");
            }
            else
            {
                TransResult = await service.GetTranslationsAsync(uri);
                if (TransResult != null)
                    setTranslationValues();
                else
                    TranslationEntry.Clear();
            }           
        }

        public void setTranslationValues()
        {
            TranslationEntry.Clear();
            foreach (var k in TransResult.results)
            {
                if (k.lexicalEntries != null)
                {
                    foreach (var i in k.lexicalEntries)
                    {
                        var trans_item = new TranslateEntry();
                        trans_item.Type = i.lexicalCategory;
                        trans_item.Examples = "Examples: \n\n";
                        trans_item.Translations = "Translations: \n\n";
                        if (i.entries != null)
                        {
                            foreach (var j in i.entries)
                            {
                                if (j.senses != null)
                                {
                                    foreach (var y in j.senses)
                                    {

                                        if (y.translations != null)
                                        {
                                            foreach (var x in y.translations)
                                            {
                                                if (!trans_item.Translations.Contains(x.text))
                                                    trans_item.Translations += x.text + "\n";
                                            }
                                        }
                                        if (y.examples != null)
                                        {
                                            foreach (var example in y.examples)
                                            {
                                                if (example.translations != null) {
                                                    trans_item.Examples += example.text + "\n" + example.translations.ElementAt(0).text + "\n\n";
                                                }
                                            }
                                        }
                                        if (y.subsenses != null)
                                        {
                                            foreach (var x in y.subsenses)
                                            {
                                                if (x.translations != null)
                                                {
                                                    foreach (var trans in x.translations)
                                                    {
                                                        if (!trans_item.Translations.Contains(trans.text))
                                                            trans_item.Translations += trans.text + "\n";
                                                    }
                                                }
                                                if (x.examples != null)
                                                {
                                                    foreach (var example in x.examples)
                                                    {
                                                        if (example.translations != null)
                                                        {
                                                            trans_item.Examples += example.text + "\n" + example.translations.ElementAt(0).text + "\n\n";
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                    }
                                }

                            }
                        }

                        TranslationEntry.Add(trans_item);
                    }
                }
            }
        }

        public string getLanguageId(string lang)
        {
            switch (lang)
            {
                case "English":
                    return "en";
                case "Spanish":
                    return "es";
                case "isiZulu":
                    return "zu";
                case "Northern Sotho":
                    return "nso";
                case "Romanian":
                    return "ro";
                case "Hindi":
                    return "hi";
                case "Swahili":
                    return "sw";
                case "Latvian":
                    return "lv";
                case "Urdu":
                    return "ur";
                case "Malay":
                    return "ms";
                case "Setswana":
                    return "tn";
                case "Indonesian":
                    return "id";
                case "German":
                    return "de";
                case "Portuguese":
                    return "pt";
                case "Tamil":
                    return "ta";
                case "Gujarati":
                    return "gu";
                default:
                    return " ";
            }
        }

        public class Lang
        {
            public string Name { set; get; }
            public string Id { set; get; }
        }

        public class TranslateEntry
        {
            public string Type { set; get; }
            public string Translations { set; get; }
            public string Examples { set; get; }
        }

    }

}
