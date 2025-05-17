namespace FlashQuiz;

public partial class List : ContentPage
{
	public List()
	{
		InitializeComponent();
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		if (BindingContext is FlashQuiz.ViewModels.Mvvm1ViewModel vm)
			vm.RefreshCardsFromDB();
	}
}