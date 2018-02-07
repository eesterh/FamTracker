using System;
using System.Timers;

using AppKit;
using Foundation;

namespace FamTracker
{
    public partial class ViewController : NSViewController
    {
        Timer MainTimer;
        int TimeGoal = 60; // seconds
        int counter = 0;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            TimeGoal = Int32.Parse(InputSeconds.StringValue);

            // Fire the timer once a second
            MainTimer = new Timer(1000);
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

                // If goal entered has passed
                if (TimeGoal == counter)
                {
                    // Stop the timer and reset
                    MainTimer.Stop();
                    counter = 0;
                    InvokeOnMainThread(() => {
                        // Reset the UI
                        TimerLabel.StringValue = "0:00";
                        StartStopButton.Title = "Start";
                        // Set the style and message text

                        NSAlert alert = new NSAlert();
                        alert.AlertStyle = NSAlertStyle.Informational;
                        alert.MessageText = InputSeconds.StringValue + " seconds elapsed!";
                        // Display the NSAlert from the current view
                        alert.BeginSheet(View.Window);
                    });
                }
            };
        }

        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
                // Update the view, if already loaded.
            }
        }

        partial void StartStopButtonClicked(NSObject sender)
        {
            // If the timer is running, we want to stop it,
            // otherwise we want to start in
            if (MainTimer.Enabled)
            {
                MainTimer.Stop();
                StartStopButton.Title = "Start";
            }
            else
            {
                MainTimer.Start();
                StartStopButton.Title = "Stop";
            }
        }

        partial void SecondsEntered(Foundation.NSObject sender)
        {
       //     String timeEntered = InputSeconds.
         //       .ToString();

            NSAlert alert = new NSAlert();
            alert.AlertStyle = NSAlertStyle.Informational;
            alert.MessageText = InputSeconds.StringValue + " Entered in Field!";
            // Display the NSAlert from the current view
            alert.BeginSheet(View.Window);

            //  TimeLeft = new Integer(timeEntered);

            TimeGoal = Int32.Parse(InputSeconds.StringValue);
            TimerLabel.StringValue = TimeGoal.ToString();

           // sender.ToString();

        }

    }
}
