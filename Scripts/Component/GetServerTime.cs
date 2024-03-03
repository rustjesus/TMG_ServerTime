using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace TMG_DailyTimer
{
    public class GetServerTime : MonoBehaviour
    {

        [SerializeField] private bool updateTime = true;
        [SerializeField] private float updateInterval = 0.5f;
        [Header("Pick a time zone from http://worldtimeapi.org/api/timezone/")]
        [SerializeField] private string timeZone = "America/Chicago";
        [SerializeField] private bool useEnum = false;
        [SerializeField] private TimeZoneEnum timeZ= TimeZoneEnum.CET;
        public enum TimeZoneEnum
        {
            CET,
            CST6CDT,
            EET,
            EST,
            EST5EDT,
            Etc_GMT,
            Etc_GMTplus1,
            Etc_GMTplus10,
            Etc_GMTplus11,
            Etc_GMTplus12,
            Etc_GMTplus2,
            Etc_GMTplus3,
            Etc_GMTplus4,
            Etc_GMTplus5,
            Etc_GMTplus6,
            Etc_GMTplus7,
            Etc_GMTplus8,
            Etc_GMTplus9,
            Etc_GMTminus1,
            Etc_GMTminus10,
            Etc_GMTminus11,
            Etc_GMTminus12,
            Etc_GMTminus13,
            Etc_GMTminus14,
            Etc_GMTminus2,
            Etc_GMTminus3,
            Etc_GMTminus4,
            Etc_GMTminus5,
            Etc_GMTminus6,
            Etc_GMTminus7,
            Etc_GMTminus8,
            Etc_GMTminus9,
            Etc_UTC,
        }

        private float timer = 0f;
        public static DateTime currentServerTime;
        public static bool serverTimeFetched = false;
        public static string currentServerTimeString;
        private string convertedTimeZone;
        private void Awake()
        {
            if (useEnum)
            {
                ConvertEnumStrings();
            }
            serverTimeFetched = false;
            StartCoroutine(ServerTime_Get());

        }
        // Combined function
        private string ConvertEnumToStringWithModifiers(TimeZoneEnum timeZone, string positiveModifier, string negativeModifier, string dashmMdifier, string positiveChange, string negativeChange, string dashChange)
        {
            string timeZoneString = timeZone.ToString();
            timeZoneString = timeZoneString.Replace(positiveModifier, positiveChange);
            timeZoneString = timeZoneString.Replace(negativeModifier, negativeChange);
            timeZoneString = timeZoneString.Replace(dashmMdifier, dashChange);
            return timeZoneString;
        }

        // Example usage:
        public void ConvertEnumStrings()
        {
            convertedTimeZone = ConvertEnumToStringWithModifiers(timeZ, "plus", "minus", "_", "+", "-", "/");
            // Use convertedTimeZone as needed
            //Debug.Log(convertedTimeZone);
            timeZone = convertedTimeZone;
        }
        private void Update()
        {
            if(updateTime == true)
            {
                // Update the timer with the time since the last frame
                timer += Time.deltaTime;

                // Check if it's time to perform the update
                if (timer >= updateInterval)
                {
                    StartCoroutine(ServerTime_Get());
                    // Reset the timer
                    timer = 0f;
                }
            }

        }
        private IEnumerator ServerTime_Get()
        {
            if (useEnum)
            {
                ConvertEnumStrings();
            }
            // Replace "http://worldtimeapi.org/api/timezone/Europe/London" with the URL of your desired time API
            using (UnityWebRequest www = UnityWebRequest.Get("http://worldtimeapi.org/api/timezone/" + timeZone))
            {
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.Success)
                {
                    StopCoroutine(RetyConnection());
                    // Parse the server response to get the current time

                    string jsonResponse = www.downloadHandler.text;
                    //Debug.Log(jsonResponse); exposes ip be careful :)
                    int start = jsonResponse.IndexOf("datetime") + 11;
                    int end = start + 19; // length of the time string
                    string timeString = jsonResponse.Substring(start, end - start);
                    currentServerTimeString = timeString;
                    DateTime serverTime = DateTime.ParseExact(timeString, "yyyy-MM-ddTHH:mm:ss", null);
                    //Debug.Log("Server time: " + serverTime);
                    currentServerTime = serverTime;
                    serverTimeFetched = true;
                }
                else
                {
                    StartCoroutine(RetyConnection());
                    Debug.LogError("Failed to get server time: " + www.error);
                }
            }
        }
        IEnumerator RetyConnection()
        {
            //reconnecting...
            yield return new WaitForSeconds(3f);
            StartCoroutine(ServerTime_Get());
        }


    }
}

