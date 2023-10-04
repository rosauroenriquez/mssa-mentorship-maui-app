using MentorshipCompanionApp.ViewModels;

namespace MentorshipCompanionApp.Views.Management;

public partial class AddUserPage : ContentPage
{
	public AddUserPage(AddUserPageViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}