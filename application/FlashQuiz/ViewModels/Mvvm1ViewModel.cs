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
        private ObservableCollection<Wish> wishes = new() {};

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddWishCommand))]
        private string wishEntry = "";


        [RelayCommand(CanExecute = nameof(AddWishCanExecute))]
        private async Task AddWish(string definition)
        {
            var wish = new Wish { Definition = definition };
            using (var dbContext = new AladdinContext())
            {
                dbContext.Add(wish);
                await dbContext.SaveChangesAsync();
            }
            Wishes.Add(wish);
            WishEntry = "";
        }
        private bool AddWishCanExecute()
        {
            //Trace.WriteLine("toto");

            return !string.IsNullOrEmpty(WishEntry);
        }

        public Mvvm1ViewModel()
        {
            RefreshWishesFromDB();
        }
        private void RefreshWishesFromDB(AladdinContext? context=null)
        {
            Wishes.Clear();
            using (var dbContext = context??new AladdinContext())
            {
                foreach (var dbWish in dbContext.Wishes) 
                {
                    Wishes.Add(dbWish);
                }
            }
        }

        [RelayCommand]
        private async Task Edit(Wish wish)
        {
            Trace.WriteLine($"Editing {wish}");

            //Affiche un popup pour demander la modification
            // /!\ Court-circuite MVVM mais toléré pour ne pas ajouter plus de complexité pour l’instant/!\
            string updatedDefinition = await Shell.Current.DisplayPromptAsync(title: "Modifier ", message: "", placeholder: wish.Definition);

            //Si l’utilisateur n’appuie pas sur Cancel
            if (updatedDefinition != null)
            {
                using (var dbContext = new AladdinContext())
                {
                    //TODO : Faire la mise à jour uniquement si la définition a changé

                    await dbContext.Wishes
                        .Where(dbWish => dbWish.Id == wish.Id)
                        .ExecuteUpdateAsync(setters => setters.SetProperty(dbWish => dbWish.Definition, updatedDefinition));

                    /* Version "old style" moins optimale laissée à des fins pédagogiques
                    var dbWish = dbContext.Wishes.Single(dbWish => dbWish.Id== wish.Id);
                    dbWish.Definition = updatedDefinition;
                    await dbContext.SaveChangesAsync();
                    */

                    //Et on rafraîchit la liste locale
                    RefreshWishesFromDB(dbContext);
                }
            }
        }
        [RelayCommand]
        private async Task Delete(Wish wish)
        {
            Trace.WriteLine($"Delete {wish.Id}");
            using(var dbContext = new AladdinContext())
            {
                await dbContext.Wishes
                    .Where(dbWish => dbWish.Id == wish.Id)
                    .ExecuteDeleteAsync();
                RefreshWishesFromDB(dbContext);
            }
        }


    }
}
