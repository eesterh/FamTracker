using System;

public class TrackerDataEngine
{
    // Engine variables
    private bool initDataLoaded = false;

    // Constructer
    public TrackerDataEngine()
    {
        // Load initial Data we want to load
        GetData();
    }

    // Load the data at start of the application
    private void GetData()
    {
        if (!initDataLoaded)
        {
            // 0 - Load app plist file with tags for iOS specific locations

            // 1 - Load Manifest db

            // 2 - Load Basic phone info for display

            // 3 - If option enabled, recreate file system friendly view of data in pre-configured folder

            // 4 - Process AddressBook

            // 5 - Build Image structure; no loading of images until that tab is pressed

            // 6 - Get text messages db read

            // 7 - Get Recordings, VoiceMail & SafariHistory

            // 8 - Create structures for WhatsApp, Kik, Viper, FB Messenger etc


        }

        initDataLoaded = true;
    }


}
