using MentorshipCompanionApp.ViewModels;

namespace MentorshipCompanionApp.Views.Interaction;

public partial class GoalDetailsPage : ContentPage
{
    public GoalDetailsPage(GoalDetailsPageViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}