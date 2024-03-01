using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.Extensions.Logging;

namespace MauiAppInsights
{
    /// <summary>
    /// MyService class to demonstrate logging and tracking events and exceptions
    /// at the service level.
    /// </summary>
    public class MyService : IMyService
    {
        private readonly ILogger<MyService> _logger;

        public MyService(ILogger<MyService> logger)
        {
            _logger = logger;
            _logger.LogTrace("MyService created");
        }

        public void MyAction(string parameter)
        {
            _logger.LogInformation("My action executed with parameter: {Parameter}", parameter);
        }

        public void TrackEvent(string eventText)
        {
            _logger.LogInformation("TrackEvent executed with parameter: {EventText}", eventText);
            Analytics.TrackEvent("TrackEvent", new Dictionary<string, string> { { "EventText", eventText } }); // TrackEvent as an event is optional per guidance.
        }

        internal void TrackException(string v)
        {

            try
            {
                // Do something
                throw new Exception("TrackException");
            }
            catch (Exception ex)
            {
                _logger.LogError("TrackException executed with parameter: {EventText}", v);
                Analytics.TrackEvent("TrackException", new Dictionary<string, string> { { "Exception", ex.Message } }); // TrackException as an event is optional per guidance.
                Crashes.TrackError(ex);
            }
        }
    }
}
