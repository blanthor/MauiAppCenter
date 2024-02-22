using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;

namespace MauiAppInsights
{
    public partial class MainPageViewModel : ObservableObject
    {
        ILogger<MainPage> _logger;
        MyService _myService;
        public MainPageViewModel(MyService myService, ILogger<MainPage> logger)
        {
            _myService = myService;
            _logger = logger;

            _counterText = "Click me!";
        }

        [ObservableProperty]
        private string _counterText;

        [ObservableProperty]
        private int _count = 0;

        [ObservableProperty]
        private int _eventCount = 0;


        public MyService MyService { get; set;}

        [RelayCommand]
        public void CounterClicked()
        {
            // CounterText is a generated property raising the PropertyChanged event, so it will update the UI.
            CounterText = $"Clicked {++Count} times";
            _myService.MyAction(CounterText);
            _logger.LogInformation($"Info: {CounterText}");
        }

        [RelayCommand]
        public void TrackEvent()
        {
            _myService.TrackEvent($"Clicked on TrackEvent {++EventCount} times.");
        }

        [RelayCommand]
        public void TrackException()
        {
            _myService.TrackException("TrackException");
        }

        [RelayCommand]
        public void CrashTheApp()
        {
            throw new Exception("CrashTheApp");
        }
    }
}
