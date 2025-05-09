using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashQuiz.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string? Titre { get; set; }
        public string? Definition { get; set; }
        public DateTime AccomplishedDate { get; set; }
        public override string ToString()
        {
            return $"[Card {Id}]";
        }
    }
}
