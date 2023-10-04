using MentorshipCompanionApp.ViewModels;

namespace MentorshipCompanionApp.Views.Dashboard;

public partial class HomePage : ContentPage
{
	public HomePage(HomePageViewModel homePageViewModel)
	{
		InitializeComponent();
		BindingContext = homePageViewModel;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        AppShell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
        base.OnNavigatedTo(args);
    }


    //protected override void OnAppearing()
    //{
    //    base.OnAppearing();


    //}
}