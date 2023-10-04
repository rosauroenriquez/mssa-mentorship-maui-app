using MentorshipCompanionApp.ViewModels;

namespace MentorshipCompanionApp.Views.Search;

public partial class SearchPage : ContentPage
{
    SearchPageViewModel _viewModel;
    public SearchPage(SearchPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
        _viewModel = viewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (_viewModel != null)
        {
            _viewModel.GetPeopleCommand.Execute("default");
        }
    }
}