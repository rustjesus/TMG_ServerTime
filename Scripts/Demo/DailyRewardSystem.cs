using System;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.Collections;

namespace TMG_DailyTimer
{
    public class DailyRewardSystem : MonoBehaviour
    {
        private const string LAST_REWARD_TIME_KEY = "LastRewardTime";
        public int cr_Reward_Amount = 10000;
        [SerializeField] private TextMeshProUGUI lastRewardTimeText;
        [SerializeField] private TextMeshProUGUI textCurrentTime;
        [SerializeField] private TextMeshProUGUI dailyStreak;
        [SerializeField] private TextMeshProUGUI bonusAddedText;
        [SerializeField] private TextMeshProUGUI totalMoneyText;
        private string lastRewardTimeTextString;
        private DateTime currentServerTime;
        public float hrsSinceLastReward;
        private void Start()
        {
            StartCoroutine(GetServerTime());
            bonusAddedText.gameObject.SetActive(false);

        }
        private IEnumerator GetServerTime()
        {
            // Replace "http://worldtimeapi.org/api/timezone/Europe/London" with the URL of your desired time API
            using (UnityWebRequest www = UnityWebRequest.Get("http://worldtimeapi.org/api/timezone/America/Chicago"))
            {
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.Success)
                {
                    StopCoroutine(RetyConnection());

                    string jsonResponse = www.downloadHandler.text;
                    //Debug.Log(jsonResponse); exposes ip be careful :)
                    int start = jsonResponse.IndexOf("datetime") + 11;
                    int end = start + 19; // length of the time string
                    string timeString = jsonResponse.Substring(start, end - start);

                    DateTime serverTime = DateTime.ParseExact(timeString, "yyyy-MM-ddTHH:mm:ss", null);
                    //Debug.Log("Server time: " + serverTime);
                    currentServerTime = serverTime;

                    StartCoroutine(WaitForSeverTime());
                }
                else
                {
                    StartCoroutine(RetyConnection());
                    //Debug.LogError("Failed to get server time: " + www.error);

                }
            }
        }
        IEnumerator RetyConnection()
        {
            yield return new WaitForSeconds(3f);
            //Debug.Log("attemption reconnet");
            StartCoroutine(GetServerTime());
        }
        IEnumerator WaitForSeverTime()
        {
            yield return new WaitForSeconds(0.1f);
            // Check if the player is eligible for a reward
            if (IsRewardAvailable())
            {
                //boost the cr reward ammount before rewarding
                MoneyDailyBoost();

                //increase money
                PlayerPrefs.SetInt("Money", cr_Reward_Amount);

                // Save the current time as the last reward time
                PlayerPrefs.SetString(LAST_REWARD_TIME_KEY, currentServerTime.ToString());
                //Debug.Log("current time = " + currentServerTime.ToString());
                lastRewardTimeText.text = "Last reward time: NOW!";


                bonusAddedText.gameObject.SetActive(true);
                bonusAddedText.text = "Bonus Today = " + cr_Reward_Amount;
            }
            else
            {

                lastRewardTimeText.text = "Last reward time: " + lastRewardTimeTextString;
            }

            int daily = PlayerPrefs.GetInt("Money_Daily_Boost") + 1;
            dailyStreak.text = "Daily Streak: " + daily;
        }
        private void Update()
        {
            textCurrentTime.text = "Current Time: " + currentServerTime.ToString();

            //Debug.Log("Hours since last reward: " + hrsSinceLastReward);

            totalMoneyText.text = "Total Money: " + PlayerPrefs.GetInt("Money");

        }
        //daily reward multipliers 
        private void MoneyDailyBoost()
        {
            switch (PlayerPrefs.GetInt("Money_Daily_Boost"))
            {
                case 0:
                    // Handle case 0
                    //normal reward
                    break;
                case 1:
                    // Handle case 1
                    cr_Reward_Amount = cr_Reward_Amount * 2;
                    break;
                case 2:
                    // Handle case 2
                    cr_Reward_Amount = cr_Reward_Amount * 3;
                    break;
                case 3:
                    // Handle case 3
                    cr_Reward_Amount = cr_Reward_Amount * 4;
                    break;
                case 4:
                    // Handle case 4
                    cr_Reward_Amount = cr_Reward_Amount * 5;
                    break;
                case 5:
                    // Handle case 5
                    cr_Reward_Amount = cr_Reward_Amount * 6;
                    break;
                case 6:
                    // Handle case 6
                    cr_Reward_Amount = cr_Reward_Amount * 7;
                    break;
                case 7:
                    // Handle case 7
                    cr_Reward_Amount = cr_Reward_Amount * 8;
                    break;
                case 8:
                    // Handle case 8
                    cr_Reward_Amount = cr_Reward_Amount * 9;
                    break;
                case 9:
                    // Handle case 9
                    cr_Reward_Amount = cr_Reward_Amount * 10;
                    break;
                default:
                    // Handle default case (optional)
                    break;
            }
        }
        private bool IsRewardAvailable()
        {
            // Get the last reward time from player preferences
            string lastRewardTimeString = PlayerPrefs.GetString(LAST_REWARD_TIME_KEY, "");
            DateTime lastRewardTime;
            // Try to parse the last reward time from string to DateTime format
            if (DateTime.TryParse(lastRewardTimeString, out lastRewardTime))
            {
                // Calculate the time elapsed since the last reward time
                TimeSpan timeSinceLastReward = currentServerTime - lastRewardTime;
                float hoursSinceLastReward = (float)timeSinceLastReward.TotalHours;

                lastRewardTimeTextString = lastRewardTime.ToString();

                hrsSinceLastReward = hoursSinceLastReward;

                // Check if 24 hours have elapsed since the last reward time
                if (hoursSinceLastReward >= 48) //if more than 48 hours we reset
                {

                    PlayerPrefs.SetInt("Money_Daily_Boost", 0);
                    return true;
                }
                else if (hoursSinceLastReward >= 24)
                {

                    PlayerPrefs.SetInt("Money_Daily_Boost", PlayerPrefs.GetInt("Money_Daily_Boost") + 1);
                    return true;
                }

            }
            else
            {
                PlayerPrefs.SetInt("Money_Daily_Boost", 0);
                // If the last reward time is not valid, assume the reward is available
                return true;
            }

            return false;
        }

    }
}
