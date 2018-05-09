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
    class AntnymViewModel : INotifyPropertyChanged
    {
        public AntnymViewModel()
        {
            AntonymCommand = new Command(getAntonyms);
            InputLanguages.Add("English");
        }

        public Synonyms AntResult = new Synonyms();


        private ObservableCollection<Antonym> _antonyms = new ObservableCollection<Antonym>();

        public ObservableCollection<string> InputLanguages { get; set; } = new ObservableCollection<string>();

        public DictionaryService service = new DictionaryService();

        public ICommand AntonymCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ObservableCollection<Antonym> Antonyms

        {
            get { return _antonyms; }
            set

            {
                _antonyms = value;
                OnPropertyChanged("Antonyms");
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


        public async void getAntonyms()
        {
            string lang;
            if (SelectedInput.Equals("English"))
                lang = "en";
            else
                lang = " ";
            var uri = "/api/v1/entries/"+ lang +"/" + Word + "/antonyms";
            if (SelectedInput == null)
            {
                DependencyService.Get<IMessage>().LongAlert("Please select language!");
            }
            else
            {
                AntResult = await service.GetSynonymsAsync(uri);
                if (AntResult != null)
                    setAntonymValues();
                else
                    Antonyms.Clear();
            }
        }

        public void setAntonymValues()
        {
            Antonyms.Clear();
            foreach (var i in AntResult.results)
            {
                foreach (var j in i.lexicalEntries)
                {
                    var antonym = new Antonym();
                    antonym.Type = j.lexicalCategory;
                    antonym.AntonymItems = "";
                    foreach (var k in j.entries)
                    {
                        foreach (var l in k.senses)
                        {
                            foreach (var item in l.antonyms)
                            {
                                antonym.AntonymItems+= "\n"+ item.text;
                            }
                        }
                    }
                    Antonyms.Add(antonym);
                }

            }
        }
        public class Antonym
        {
            public string Type { get; set; }
            public string AntonymItems { get; set; }
        }
    }


}

