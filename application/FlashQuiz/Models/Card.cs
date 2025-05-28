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
        [ObservableProperty]
        private int id;

        [ObservableProperty]
        private string? titre;

        [ObservableProperty]
        private string? definition;

        [ObservableProperty]
        private DateTime accomplishedDate;

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
