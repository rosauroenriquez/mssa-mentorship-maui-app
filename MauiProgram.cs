using MentorshipCompanionApp.Models;
using MentorshipCompanionApp.Services;
using MentorshipCompanionApp.ViewModels;
using MentorshipCompanionApp.Views.Dashboard;
using MentorshipCompanionApp.Views.Interaction;
using MentorshipCompanionApp.Views.Management;
using MentorshipCompanionApp.Views.Search;
using MentorshipCompanionApp.Views.Startup;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace MentorshipCompanionApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            Preferences.Set("BaseAddress", DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5013/" : "http://localhost:5013/");
#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
            builder.Services.AddSingleton<ActiveUser>();
            builder.Services.AddTransient<LoginService>();            
            builder.Services.AddTransient<LoginPageViewModel>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddSingleton<ProfileService>();
            builder.Services.AddSingleton<MentorshipService>();
            builder.Services.AddSingleton<GoalService>();
            builder.Services.AddTransient<GoalDetailsPageViewModel>();
            builder.Services.AddTransient<GoalDetailsPage>();
            builder.Services.AddSingleton<SearchPageViewModel>();
            builder.Services.AddTransient<SearchPage>();
            builder.Services.AddSingleton<HomePageViewModel>();
            builder.Services.AddSingleton<HomePage>();
            builder.Services.AddSingleton<UserManagementViewModel>();
            builder.Services.AddSingleton<UserManagementPage>();
            builder.Services.AddSingleton<UserManagementService>();
            builder.Services.AddSingleton<AddUserPageViewModel>();
            builder.Services.AddTransient<AddUserPage>();
            builder.Services.AddTransient<EditUserPageViewModel>();
            builder.Services.AddTransient<EditUserPage>();
            builder.Services.AddTransient<MentorshipPageViewModel>();
            builder.Services.AddTransient<MentorshipPage>();
            builder.Services.AddTransient<EditUserDetailViewModel>();
            builder.Services.AddTransient<EditUserDetailPage>();
            

            //For deletion
            builder.Services.AddHttpClient("default",client =>
            {
                client.BaseAddress = new Uri("http://localhost:5013/");
                client.Timeout = TimeSpan.FromMinutes(5);
                //client.BaseAddress = new Uri("https://localhost:7024/api/Token/login");
            });
            return builder.Build();
        }

    }
}