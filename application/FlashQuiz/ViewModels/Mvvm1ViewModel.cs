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
using FlashQuiz.Views;
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

        [RelayCommand]
        private async Task SelectRandomCard()
        {
            using (var dbContext = new AladdinContext())
            {
                var random = new Random();
                var count = await dbContext.Cards.CountAsync();
                
                if (count > 0)
                {
                    var skip = random.Next(0, count);
                    var randomCard = await dbContext.Cards.Skip(skip).FirstOrDefaultAsync();
                    
                    if (randomCard != null)
                    {
                        await Shell.Current.DisplayAlert(
                            "Carte Aléatoire",
                            $"Titre: {randomCard.Titre}\nDéfinition: {randomCard.Definition}",
                            "OK"
                        );
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert(
                        "Information",
                        "Aucune carte disponible dans la base de données",
                        "OK"
                    );
                }
            }
        }

        [ObservableProperty]
        private ObservableCollection<Card> cards = new() {};

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddCardCommand))]
        private string titre;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddCardCommand))]
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
        public void RefreshCardsFromDB(AladdinContext? context=null)
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
            
            var editPage = new EditCardPage(card);
            editPage.OnSave = async (updatedCard) =>
            {
                using (var dbContext = new AladdinContext())
                {
                    await dbContext.Cards
                        .Where(dbCard => dbCard.Id == updatedCard.Id)
                        .ExecuteUpdateAsync(setters => setters
                                .SetProperty(dbCard => dbCard.Definition, updatedCard.Definition)
                                .SetProperty(dbCard => dbCard.Titre, updatedCard.Titre)
                            );

                    RefreshCardsFromDB(dbContext);
                }
            };

            await Shell.Current.Navigation.PushAsync(editPage);
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

        public void SetUserAnswer(Card card, string userAnswer)
        {
            card.UserAnswer = userAnswer;
            if (!string.IsNullOrEmpty(userAnswer) && card.Definition != null && userAnswer.Trim().Equals(card.Definition.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                card.ShowCheckOnUserAnswer = true;
                card.ShowCheckOnDefinition = false;
            }
            else if (!string.IsNullOrEmpty(userAnswer))
            {
                card.ShowCheckOnUserAnswer = false;
                card.ShowCheckOnDefinition = true;
            }
            else
            {
                card.ShowCheckOnUserAnswer = false;
                card.ShowCheckOnDefinition = false;
            }
            // Notifie le changement pour la CollectionView
            var idx = Cards.IndexOf(card);
            if (idx >= 0)
            {
                Cards.RemoveAt(idx);
                Cards.Insert(idx, card);
            }
        }
    }
}
