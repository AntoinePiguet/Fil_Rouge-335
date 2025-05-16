using FlashQuiz.Models;

namespace FlashQuiz.Views;

public partial class EditCardPage : ContentPage
{
    private Card _card;
    public Action<Card> OnSave { get; set; }

    public EditCardPage(Card card)
    {
        InitializeComponent();
        _card = card;
        TitreEntry.Text = card.Titre;
        DefinitionEntry.Text = card.Definition;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TitreEntry.Text) || string.IsNullOrWhiteSpace(DefinitionEntry.Text))
        {
            await DisplayAlert("Erreur", "Veuillez remplir tous les champs", "OK");
            return;
        }

        _card.Titre = TitreEntry.Text;
        _card.Definition = DefinitionEntry.Text;
        OnSave?.Invoke(_card);
        await Navigation.PopAsync();
    }
} 