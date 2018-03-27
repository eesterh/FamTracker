// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace FamTracker
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        AppKit.NSProgressIndicator ContProgressBar { get; set; }

        [Outlet]
        AppKit.NSTextView Debug_TextView { get; set; }

        [Outlet]
        AppKit.NSTextField InputSeconds { get; set; }

        [Outlet]
        AppKit.NSTextField Password { get; set; }

        [Outlet]
        AppKit.NSProgressIndicator ProgressBar { get; set; }

        [Outlet]
        AppKit.NSButton SQL { get; set; }

        [Outlet]
        AppKit.NSTextView SQL_TextView { get; set; }

        [Outlet]
        AppKit.NSButton StartStopButton { get; set; }

        [Outlet]
        AppKit.NSTextField TimerLabel { get; set; }

        [Outlet]
        AppKit.NSButton UserCredentialsConfirmed { get; set; }

        [Outlet]
        AppKit.NSTextField Username { get; set; }

        [Action ("PasswordEntered:")]
        partial void PasswordEntered (Foundation.NSObject sender);

        [Action ("progressRunning:")]
        partial void progressRunning (Foundation.NSObject sender);

        [Action ("RunSQL:")]
        partial void RunSQL (Foundation.NSObject sender);

        [Action ("SecondsEntered:")]
        partial void SecondsEntered (Foundation.NSObject sender);

        [Action ("StartStopButtonClicked:")]
        partial void StartStopButtonClicked (Foundation.NSObject sender);

        [Action ("UserCredentialsEntered:")]
        partial void UserCredentialsEntered (Foundation.NSObject sender);

        [Action ("UsernameEntered:")]
        partial void UsernameEntered (Foundation.NSObject sender);
        
        void ReleaseDesignerOutlets ()
        {
            if (SQL_TextView != null) {
                SQL_TextView.Dispose ();
                SQL_TextView = null;
            }

            if (ContProgressBar != null) {
                ContProgressBar.Dispose ();
                ContProgressBar = null;
            }

            if (Debug_TextView != null) {
                Debug_TextView.Dispose ();
                Debug_TextView = null;
            }

            if (InputSeconds != null) {
                InputSeconds.Dispose ();
                InputSeconds = null;
            }

            if (Password != null) {
                Password.Dispose ();
                Password = null;
            }

            if (ProgressBar != null) {
                ProgressBar.Dispose ();
                ProgressBar = null;
            }

            if (SQL != null) {
                SQL.Dispose ();
                SQL = null;
            }

            if (StartStopButton != null) {
                StartStopButton.Dispose ();
                StartStopButton = null;
            }

            if (TimerLabel != null) {
                TimerLabel.Dispose ();
                TimerLabel = null;
            }

            if (UserCredentialsConfirmed != null) {
                UserCredentialsConfirmed.Dispose ();
                UserCredentialsConfirmed = null;
            }

            if (Username != null) {
                Username.Dispose ();
                Username = null;
            }
        }
    }
}
