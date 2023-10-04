using MentorshipCompanionApp.ViewModels;

namespace MentorshipCompanionApp.Views.Dashboard;

public partial class UserManagementPage : ContentPage
{
	public UserManagementPage(UserManagementViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}