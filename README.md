# Introduction 
MauiAppCtrAppInsights in the MvvmExample folder is a .NET MAUI App, written in .NET 8 taking the standard template, modifying it with the Model-View-ViewModel (MVVM) Pattern, and logging to Azure Application Insights and App Center. Additionally crash data will be sent to App Center when the app is run without debugging.

MauiAppCenterCodeBehind in the CodeBehindExample folder uses the standard template for .NET MAUI as is and logs events and crashes to App Center. This is a simpler example and is not aligned with current guidance. It remains here as an example of how to include App Center in .NET MAUI, which does take some care to compile and work properly.

## References
 - https://vladislavantonyuk.github.io/articles/Adding-Application-Insights-to-.NET-MAUI-Application/
 - https://learn.microsoft.com/en-us/azure/azure-monitor/app/worker-service 
 - https://www.andreasnesheim.no/using-app-center-diagnostics-analytics-with-net-maui/ 

## Mobile App (MauiAppCenter)
This is based on the Visual Studio .NET MAUI standard template with the code-behind pattern. It is a simple app with a 
button that logs an event to Azure Application Insights.


## Mobile App (MauiAppCtrAppInsightsMvvm)
This is based on the Visual Studio .NET MAUI modified to us the MVVM pattern.

# Getting Started
## Mobile App
 - How to contribute to the App
    - Cloning the repository using git clone "https://AzMobileDevOps@dev.azure.com/AzMobileDevOps/ProcessTransformation/_git/MauiAppCenter"
    - Create a new branch. Branch name should be the card number for which you are amending the code changes with prefix as [U/T/B], U refers to User story, T refers to Task, B refers to Bug. For example: If you are working on User story 163 then branch name should be U0163-[XYZ Details]
 - Software dependencies
    - Visual Studio 2022
    - Android SDK
    - .NET 8
## Azure Application Insights
### Application Insights Setup
 - Login to https://portal.azure.com/
 - Create a new resource, selecting or searching for Application Insights
 - Add details, which likely includes a new resource group for this PoC
 - Once the Application Insights instance is created, navigate to the overview page to copy the Connection String
 - Use this connection string in the BuildApplicationInsights method found in MauiProgram.cs.
    - You may also test this connection string in the PowerShell script, powershellTest.ps1, included in the folder of this project. 
    ```
        # Info: Provide either the connection string or ikey for your Application Insights resource
        $ConnectionString = "your connection string here"
    ```
## How to Test
### Mobile App
 - Connection string should be added to the BuildApplicationInsights method in MauiProgram.cs
 - Run the app in an emulator.
 - click on the button on the MainPage, which will trigger the RelayCommand in the MainPageViewModel.
### Azure
 - Navigate to the Azure Portal
 - Select the Application Insights instance 
 - Select **Transaction search**
 - Click on the **See all data in the last 24 hours** button. After a delay all of the events logged in the mobile app will appear in the list.

### App Center
 - Navigate to the App Center
 - Select the App
 - For Crashes
   - Select **Diagnostics**
   - Select **Issues**
   - Select the **Crashes** Tab
 - For Event Logging (optional)
   - Select **Analytics**
   - Select **Events**
   - Select **TrackEvent** for any Events you may have logged.
   - Select **TrackException** for any Exception you may have logged.
 - After a delay all of the events logged in the mobile app will appear in the list.
    
# Build Strategy
- Clean the solution before initiating a build/re-build.
- Few checks to be done to mitigate common build issues:
    - Make sure only Android target is enabled for the solution. It can be checked here Solution -> Properties -> Application
    - Android Targets (other targets may be used depending on your configuration)
        - Target Android Framework  as Android 13.0 (API Level 33)
        - Minimum Target Android Framework as Android 11.0 (API Level 30)

