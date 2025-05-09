using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlashQuiz.Models;
using FlashQuiz.Services;
using Microsoft.EntityFrameworkCore;

namespace FlashQuiz.ViewModels
{
    public partial class Mvvm1ViewModel : ObservableObject
    {
        [ObservableProperty]
        private int counter = 0;

        [RelayCommand]
        private void Increment(int incrementValue)
        {
            Counter += incrementValue;
        }

        [ObservableProperty]
        private ObservableCollection<Card> cards = new() {};

        [ObservableProperty]
        private string titre;

        [ObservableProperty]
        private string definition;

        [RelayCommand(CanExecute = nameof(AddCardCanExecute))]
        private async Task AddCard()
        {
            var card = new Card { Titre = Titre, Definition = Definition };
            using (var dbContext = new AladdinContext())
            {
                dbContext.Add(card);
                await dbContext.SaveChangesAsync();
            }
            Cards.Add(card);
            Titre = string.Empty;
            Definition = string.Empty;
        }
        private bool AddCardCanExecute()
        {
            return !string.IsNullOrEmpty(Titre) && !string.IsNullOrEmpty(Definition);
        }

        public Mvvm1ViewModel()
        {
            RefreshCardsFromDB();
        }
        private void RefreshCardsFromDB(AladdinContext? context=null)
        {
            Cards.Clear();
            using (var dbContext = context??new AladdinContext())
            {
                foreach (var dbCard in dbContext.Cards) 
                {
                    Cards.Add(dbCard);
                }
            }
        }

        [RelayCommand]
        private async Task Edit(Card card)
        {
            Trace.WriteLine($"Editing {card}");

            //Affiche un popup pour demander la modification
            // /!\ Court-circuite MVVM mais toléré pour ne pas ajouter plus de complexité pour l'instant/!\
            string updatedDefinition = await Shell.Current.DisplayPromptAsync(title: "Modifier ", message: "", placeholder:card.Definition);
            string updatedTitre = await Shell.Current.DisplayPromptAsync(title: "Modifier ", message: "", placeholder: card.Titre);

            //Si l'utilisateur n'appuie pas sur Cancel
            if (updatedDefinition != null && updatedTitre != null)
            {
                using (var dbContext = new AladdinContext())
                {
                    //TODO : Faire la mise à jour uniquement si la définition a changé

                    await dbContext.Cards
                        .Where(dbCard => dbCard.Id == card.Id)
                        .ExecuteUpdateAsync(setters => setters
                                .SetProperty(dbCard => dbCard.Definition, updatedDefinition)
                                .SetProperty(dbCard => dbCard.Titre, updatedTitre)
                            );
                    /* Version "old style" moins optimale laissée à des fins pédagogiques
                    var dbWish = dbContext.Wishes.Single(dbWish => dbWish.Id== wish.Id);
                    dbWish.Definition = updatedDefinition;
                    await dbContext.SaveChangesAsync();
                    */

                    //Et on rafraîchit la liste locale
                    RefreshCardsFromDB(dbContext);
                }
            }
        }
        [RelayCommand]
        private async Task Delete(Card card)
        {
            Trace.WriteLine($"Delete {card.Id}");
            using(var dbContext = new AladdinContext())
            {
                await dbContext.Cards
                    .Where(dbCard => dbCard.Id == card.Id)
                    .ExecuteDeleteAsync();
                RefreshCardsFromDB(dbContext);
            }
        }


    }
}
