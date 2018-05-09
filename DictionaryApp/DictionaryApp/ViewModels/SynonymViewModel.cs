using DictionaryApp.Models;
using DictionaryApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DictionaryApp.ViewModels
{
    class SynonymViewModel : INotifyPropertyChanged
    {
        public SynonymViewModel()
        {
            SynonymCommand = new Command(getSynonyms);
            InputLanguages.Add("English");
        }

        public Synonyms SynResult = new Synonyms();

        public ObservableCollection<string> InputLanguages { get; set; } = new ObservableCollection<string>();


        private ObservableCollection<Synonym> _synonyms = new ObservableCollection<Synonym>();

        public DictionaryService service = new DictionaryService();

        public ICommand SynonymCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ObservableCollection<Synonym> Synonyms

        {
            get { return _synonyms; }
            set

            {
                _synonyms = value;
                OnPropertyChanged("Synonyms");
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
                    OnPropertyChanged();
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


        public async void getSynonyms()
        {
            string lang;
            if (SelectedInput.Equals("English") && SelectedInput !=null)
                lang = "en";
            else
                lang = " ";
            var uri = "/api/v1/entries/"+ lang + "/" +Word+"/synonyms";
            if (SelectedInput == null)
            {
                DependencyService.Get<IMessage>().LongAlert("Please select language!");
            }
            else
            {
                SynResult = await service.GetSynonymsAsync(uri);
                if (SynResult != null)
                    setSynonymValues();
                else
                    Synonyms.Clear();
            }     
        }

        public void setSynonymValues()
        {
            Synonyms.Clear();
            foreach (var i in SynResult.results)
            {
                foreach (var j in i.lexicalEntries)
                {
                    var synonym = new Synonym();
                    synonym.Type = j.lexicalCategory;
                    synonym.SynonymItems = "";
                    foreach (var k in j.entries)
                    {
                        foreach (var l in k.senses)
                        {
                            foreach (var item in l.synonyms)
                            {
                                synonym.SynonymItems += "\n" + item.text;
                                Console.WriteLine();
                            }
                        }
                    }
                    Synonyms.Add(synonym);
                }

            }
        }
        public class Synonym 
        {
            public string Type { get; set; }
            public string SynonymItems { get; set; }
        }
    }

}
