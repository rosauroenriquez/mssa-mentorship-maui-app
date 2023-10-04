using MentorshipCompanionApp.ViewModels;

namespace MentorshipCompanionApp.Views.Management;

public partial class MentorshipPage : ContentPage
{
	private MentorshipPageViewModel _viewModel;
	public MentorshipPage(MentorshipPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		_viewModel = viewModel;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
		_viewModel.GetStudentFromSearchCommand.Execute(this);
    }
}