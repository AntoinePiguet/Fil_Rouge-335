using FlashQuiz.Models;
using FlashQuiz.ViewModels;
using FlashQuiz.Services;
using Microsoft.EntityFrameworkCore;

namespace FlashQuiz;

public partial class MainPage : ContentPage
{
    private Card currentCard;
    private Mvvm1ViewModel viewModel;
    private bool isAnswered = false;
    private bool isCorrect = false;

    public MainPage()
    {
        InitializeComponent();
        viewModel = (Mvvm1ViewModel)BindingContext;
        LoadNewCard();
    }

    private async void LoadNewCard()
    {
        isAnswered = false;
        isCorrect = false;
        ResultStack.IsVisible = false;
        InputStack.IsVisible = true;
        UserAnswerEntry.Text = "";
        UserAnswerLabel.Text = "";
        UserAnswerIcon.IsVisible = false;
        UserAnswerBorder.BackgroundColor = Color.FromArgb("#E5E5E5");
        UserAnswerLabel.TextColor = Colors.Black;
        CorrectAnswerLabel.Text = "";
        NextButton.IsVisible = false;
        SkipButton.IsVisible = true;
        ValidateButton.IsVisible = true;
        UserAnswerStack.IsVisible = false;

        // Charger une nouvelle carte
        using (var dbContext = new AladdinContext())
        {
            var random = new Random();
            var count = await dbContext.Cards.CountAsync();
            if (count > 0)
            {
                var skip = random.Next(0, count);
                currentCard = await dbContext.Cards.Skip(skip).FirstOrDefaultAsync();
                if (currentCard != null)
                {
                    CardTitleLabel.Text = currentCard.Titre;
                }
            }
            else
            {
                await DisplayAlert("Information", "Aucune carte disponible dans la base de données", "OK");
            }
        }
    }

    private void OnSkip(object sender, EventArgs e)
    {
        // Passe à la carte suivante sans valider
        LoadNewCard();
    }

    private void OnValidate(object sender, EventArgs e)
    {
        if (currentCard == null) return;
        var userAnswer = UserAnswerEntry.Text?.Trim() ?? "";
        if (string.IsNullOrEmpty(userAnswer))
        {
            DisplayAlert("Erreur", "Veuillez entrer une réponse", "OK");
            return;
        }
        isAnswered = true;
        isCorrect = userAnswer.Equals(currentCard.Definition, StringComparison.OrdinalIgnoreCase);

        // Affichage de la bulle réponse user
        UserAnswerLabel.Text = userAnswer;
        UserAnswerBorder.BackgroundColor = isCorrect ? Color.FromArgb("#7ED957") : Color.FromArgb("#E5E5E5");
        UserAnswerLabel.TextColor = isCorrect ? Colors.White : Colors.Black;
        UserAnswerIcon.IsVisible = true;
        UserAnswerIcon.Source = isCorrect ? "true.png" : "delete.png";
        UserAnswerIcon.BackgroundColor = isCorrect ? Colors.Transparent : Color.FromArgb("#F76C6C");
        UserAnswerIcon.Margin = isCorrect ? new Thickness(0) : new Thickness(0,0,0,0);
        UserAnswerStack.IsVisible = true;

        // Affichage de la bonne réponse si faux
        ResultStack.IsVisible = !isCorrect;
        CorrectAnswerLabel.Text = !isCorrect ? currentCard.Definition : "";

        // Masquer les boutons valider/passer, afficher suivant
        InputStack.IsVisible = false;
        NextButton.IsVisible = true;
    }

    private void OnNext(object sender, EventArgs e)
    {
        LoadNewCard();
    }
}
