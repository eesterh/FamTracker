using System;
using System.Timers;

using AppKit;
using Foundation;

namespace FamTracker
{
    public partial class ViewController : NSViewController
    {
        // Global variables
        Timer MainTimer = new Timer(1000); // one second
        int TimeGoal = 59; // seconds
        int counter;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        public override NSObject RepresentedObject
        {
            get { return base.RepresentedObject; }
            set => base.RepresentedObject = value;
        }

        partial void StartStopButtonClicked(NSObject sender)
        {
            // If the timer is running, we want to stop it, otherwise we want to start it
            if (MainTimer.Enabled)
            {
                MainTimer.Stop();
                StartStopButton.Title = "Start";
            }
            else
            {
                MainTimer.Start();
                StartStopButton.Title = "Stop";
                RunTimer();
            }
        }

        public void RunTimer()
        {
            TimeGoal = Int32.Parse(InputSeconds.StringValue);

            counter = 0;
            MainTimer.Elapsed += (sender, e) => {
                counter++;

                // Format the remaining time nicely for the label
                TimeSpan time = TimeSpan.FromSeconds(counter);
                string timeString = time.ToString(@"mm\:ss");
                InvokeOnMainThread(() =>
                {
                    //We want to interact with the UI from a different thread,
                    // so we must invoke this change on the main thread
                    TimerLabel.StringValue = timeString;
                });

                // ProgressBar.IncrementBy(1); - This causes the following code to be ignored.... need to figure out how to init correctly I think


                // If goal entered has passed
                if (TimeGoal == counter)
                {
                    // Stop the timer and reset
                    MainTimer.Stop();
                    counter = 0;
                    InvokeOnMainThread(() =>
                    {
                        // Reset the UI
                        TimerLabel.StringValue = "0:00";
                        StartStopButton.Title = "Start";
                        // Set the style and message text

                        NSAlert alert = new NSAlert
                        {
                            AlertStyle = NSAlertStyle.Informational,
                            MessageText = InputSeconds.StringValue + " seconds elapsed!"
                        };
                        // Display the NSAlert from the current view
                        alert.BeginSheet(View.Window);
                    });
                }
            };

        }

        partial void SecondsEntered(Foundation.NSObject sender)
        {
       
            // Update new goal
            TimeGoal = Int32.Parse(InputSeconds.StringValue);
            TimerLabel.StringValue = "0:00";

            // Align progressbar to new 100%
            ProgressBar.MaxValue = TimeGoal;

        }

        partial void UsernameEntered(Foundation.NSObject sender)
        {
            // make sure email format username entered
            // FUTURE: Drop down with stored usernames in plist file

                String user = Username.StringValue;
        }

        partial void PasswordEntered(Foundation.NSObject sender)
        {

            // MAke sure value entered as password
            String pwd = Password.StringValue;
        }

        partial void UserCredentialsEntered(Foundation.NSObject sender)
        {
            
            InvokeOnMainThread(() =>
            {
                // Reset the UI
                NSAlert alert = new NSAlert
                {
                    AlertStyle = NSAlertStyle.Informational,
                    MessageText = "U: " + Username.StringValue + " P: " + Password.StringValue + " logging on..."
                };
                // Display the NSAlert from the current view
                alert.BeginSheet(View.Window);
            });


            // Trigger to connect to try and connect to iCloud



            // Passcode

            // Do magic

        }


    }
}
