using MentorshipCompanionApp.Views.Dashboard;
using MentorshipCompanionApp.Views.Interaction;
using MentorshipCompanionApp.Views.Management;
using MentorshipCompanionApp.Views.Search;
using MentorshipCompanionApp.Views.Startup;

namespace MentorshipCompanionApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(UserManagementPage), typeof(UserManagementPage));
            Routing.RegisterRoute(nameof(AddUserPage), typeof(AddUserPage));
            Routing.RegisterRoute(nameof(EditUserPage), typeof(EditUserPage));
            Routing.RegisterRoute(nameof(MentorshipPage), typeof(MentorshipPage));
            Routing.RegisterRoute(nameof(EditUserDetailPage), typeof(EditUserDetailPage));
            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
            Routing.RegisterRoute(nameof(SearchPage), typeof(SearchPage));
            Routing.RegisterRoute(nameof(GoalDetailsPage), typeof(GoalDetailsPage));
        }
    }
}