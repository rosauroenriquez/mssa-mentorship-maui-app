using MentorshipCompanionApp.Models;
using MentorshipCompanionApp.ViewModels;
using System.Collections.ObjectModel;

namespace MentorshipCompanionApp.Views.Management;

public partial class EditUserPage : ContentPage
{
	EditUserPageViewModel _viewModel;
	public EditUserPage(EditUserPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
        _viewModel = viewModel;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
		AppShell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
        base.OnNavigatedTo(args);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (_viewModel != null)
        {
            _viewModel.GetPeopleCommand.Execute("default");
        }
    }

    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_viewModel != null) 
        { 
            _viewModel.GetPeopleCommand.Execute("default");
        }
    }
}