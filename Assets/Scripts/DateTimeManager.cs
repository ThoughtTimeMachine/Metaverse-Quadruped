using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System;
using UnityEngine;

public class DateTimeManager : Singleton<DateTimeManager>, IDataPersistence
{
    // Fetch the local DateTime. Save last DateTime before Game Ended. If the New Login Time is less than or more than the last saved DateTime then 
    //the distance is still calculated in either direction as a subtraction from the last saved time. That way there is no gain in attribute calculations
    //accounting for the time lapsed with the pet since last active session.
    DateTime localDate = DateTime.Now;
    DateTime utcDate = DateTime.UtcNow;
    String[] cultureNames = { "en-US", "en-GB", "fr-FR",
                                "de-DE", "ru-RU" };

    void Start()
    {
        // DateTime NewSessionLogin = FetchDateInLocalTime();
    }
    public DateTime GetLastSession()
    {

        return DateTime.Now;//This needs to be the serialized file time saved
    }
    public void OnApplicationQuit()
    {
        //SaveTime();
    }
    private void SaveTime()
    {
        foreach (var cultureName in cultureNames)
        {
            var culture = new CultureInfo(cultureName);
            Console.WriteLine("{0}:", culture.NativeName);
            Console.WriteLine("   Local date and time: {0}, {1:G}",
                              localDate.ToString(culture), localDate.Kind);
            Console.WriteLine("   UTC date and time: {0}, {1:G}\n",
                              utcDate.ToString(culture), utcDate.Kind);
        }
    }

    public void LoadData(GameData data)
    {
        //save Time in a format we can serialize
    }

    public void SaveData(ref GameData data)
    {
        //load Time in a format we can deserialize
    }
}
