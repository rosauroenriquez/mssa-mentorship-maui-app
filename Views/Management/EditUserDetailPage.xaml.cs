using MentorshipCompanionApp.ViewModels;

namespace MentorshipCompanionApp.Views.Management;

public partial class EditUserDetailPage : ContentPage
{
	public EditUserDetailPage(EditUserDetailViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;

		
	}

  //  protected override void OnNavigatedTo(NavigatedToEventArgs args)
  //  {
  //      base.OnNavigatedTo(args);
		//AppShell.Current.FlyoutIsPresented = false;

  //  }
}