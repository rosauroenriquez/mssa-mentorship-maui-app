using MentorshipCompanionApp.ViewModels;

namespace MentorshipCompanionApp.Views.Startup;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}