using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashQuiz.Models
{
    public partial class Card : ObservableObject
    {
        public int Id { get; set; }
        public string? Titre { get; set; }
        public string? Definition { get; set; }
        public DateTime AccomplishedDate { get; set; }

        [ObservableProperty]
        private string? userAnswer;
        [ObservableProperty]
        private bool showCheckOnUserAnswer;
        [ObservableProperty]
        private bool showCheckOnDefinition;

        public override string ToString()
        {
            return $"[Card {Id}]";
        }
    }
}
