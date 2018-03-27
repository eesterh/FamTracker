using System;
using System.Timers;
using Mono.Data.Sqlite;
using System.IO;

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

        String sql_result = "";
        String debug_result = "";

        OutputWriter sql_write;
        OutputWriter debug_write;


        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            sql_write = new OutputWriter(SQL_TextView);
            debug_write = new OutputWriter(Debug_TextView);

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
            MainTimer.Elapsed += (sender, e) =>
            {
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


            // NSTextView tv =  new NSTextView();

            NSAttributedString text = new NSAttributedString("U: " + Username.StringValue + " P: " + Password.StringValue + " logging on...");

   
          
            // Trigger to connect to try and connect to iCloud



            // Passcode

            // Do magic

        }

        partial void RunSQL(Foundation.NSObject sender)
        {

            SqliteConnection con;
            SqliteCommand command;
            SqliteDataReader reader;
            NSAlert alert;

            try
            {
                
                const string DBNAME = "Manifest.db";
                //      string B_PATH = ""; // "Library/MobileSync/Backup/Manifest.db";

                string gPath = Path.Combine(
                    System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),
                    DBNAME);
                Path.GetFullPath(gPath);

                //gPath = Path.Combine(gPath, DBNAME);
                // Directory.
                if (!File.Exists(gPath))
                {
            /*        debug_write.Append("File does not exist: " + gPath +
                            "\nPath.FullPath= " + Path.GetFullPath(gPath) +
                            "\nPath.DirectoryName= " + Path.GetDirectoryName(gPath) +
                                                      "\nFileName= " + Path.GetFileName(gPath));
              */  }
                // gPath = "Data Source=" + gPath + "; Version=3; Read Only=True";
                // When do we use Version = 3 tag?


                gPath = "Data Source=" + gPath + "; Read Only=True";

      //          debug_write.Append("SQL: ");
                con = new SqliteConnection(gPath);
                con.Open();
      //          debug_write.Append("<O>");
                command = new SqliteCommand();
                command.CommandText = "select fileID, domain, relativePath from Files where relativePath like '%db%'";
                command.Connection = con;

                reader = command.ExecuteReader();
              
      //          debug_write.Append("<E>");
      
                sql_result = "";

                //reader.
                while (reader.Read())
                {
                    sql_result +=
                        "\nFileID: " + reader.GetString(0) +
                                           "\nDOMAIN:  " + reader.GetString(1) +
                                           "\nR_PATH:  " + reader.GetString(2);
                }
              //  debug_write.Append("<R>\n");

             //   sql_write.Append(sql_result);
                sql_write.Append("\n\nSIZE: "+ sql_write.SizeOfBuffer().ToString() + "\n\n");

           //     debug_write.Append("SQL executed!\n");

                if (Debug_TextView is null)
                {
                    sql_write.Append("DW IS NULL\n\n\n");
                }
  

                reader.Close();
                command.Dispose();
                con.Close();
            }
            catch (Exception e)
            {
                alert = new NSAlert
                {
                    AlertStyle = NSAlertStyle.Critical,
                    MessageText = "\nEXCEPTION: \n" + e.Message + "\nSOURCE:   \n" + e.Source +
                                                     "\nDETAIL: \n" + e.ToString()
                };
                alert.BeginSheet(View.Window);
               // debug_write.Append(e.Message);
            }
            finally
            {
                // if (command != null) command.Dispose();
                // if (reader != null) reader.Close();
                // if (con != null) con.Close();
            }
        }

        partial void switchDebug(Foundation.NSObject sender)
        {
        }

    }
}
