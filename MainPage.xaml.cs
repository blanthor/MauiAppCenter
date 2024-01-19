using Microsoft.AppCenter.Analytics;

namespace MauiAppCenter3;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

        Analytics.TrackEvent("MauiAppCenter 3 Counter Clicked", new Dictionary<string, string> { { "count", count.ToString() } });

        SemanticScreenReader.Announce(CounterBtn.Text);
	}
}

