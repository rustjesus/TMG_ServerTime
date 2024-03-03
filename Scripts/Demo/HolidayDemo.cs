using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMG_DailyTimer
{
    public class HolidayDemo : MonoBehaviour
    {
        [Header("Seasonal (use 13 as month end to start a new year)")]
        [SerializeField] private GameObject[] summerItems;
        [Range(0, 13)][SerializeField] private int summerStartMonth = 5;
        [Range(0, 13)][SerializeField] private int summerEndMonth = 8;
        [SerializeField] private GameObject[] fallItems;
        [Range(0, 13)][SerializeField] private int fallStartMonth = 8;
        [Range(0, 13)][SerializeField] private int fallEndMonth = 12;
        [SerializeField] private GameObject[] winterItems;
        [Range(0, 13)][SerializeField] private int winterStartMonth = 12;
        [Range(0, 13)][SerializeField] private int winterEndMonth = 2;
        [SerializeField] private GameObject[] springItems;
        [Range(0, 13)][SerializeField] private int springStartMonth = 2;
        [Range(0, 13)][SerializeField] private int springEndMonth = 5;

        [Header("Halloween")]
        [SerializeField] private GameObject[] hweenItems;
        [Range(0, 13)][SerializeField] private int hweenStartMonth = 10;
        [Range(0, 13)][SerializeField] private int hweenEndMonth = 11;
        [Range(0, 32)][SerializeField] private int hweenStartDay = 20;
        [Range(0, 32)][SerializeField] private int hweenEndDay = 32;

        [Header("Thanksgiving")]
        [SerializeField] private GameObject[] thankGiveItems;
        [Range(0, 13)][SerializeField] private int thankGiveStartMonth = 11;
        [Range(0, 13)][SerializeField] private int thankGiveEndMonth = 12;
        [Range(0, 32)][SerializeField] private int thankGiveStartDay = 20;
        [Range(0, 32)][SerializeField] private int thankGiveEndDay = 32;

        [Header("Christmas")]
        [SerializeField] private GameObject[] xmasItems;
        [Range(0, 13)][SerializeField] private int xmasStartMonth = 12;
        [Range(0, 13)][SerializeField] private int xmasEndMonth = 0;
        [Range(0, 32)][SerializeField] private int xmasStartDay = 20;
        [Range(0, 32)][SerializeField] private int xmasEndDay = 29;


        [Header("Easter")]
        [SerializeField] private GameObject[] easterItems;
        [Header("Easter Days (365 days format)")]
        [Range(0, 365)][SerializeField] private int easterStartDay = 82;
        [Range(0, 365)][SerializeField] private int easterEndDay = 112;

        private bool checkForItemsOnce = false;
        private void Start()
        {
            //disable items by default
            ToggleSummerItems(false);
            ToggleFallItems(false);
            ToggleWinterItems(false);
            ToggleSpringItems(false);
            //holidays
            ToggleChristmasItems(false);
            ToggleHalloweenItems(false);
            ToggleThanksgivingItems(false);
            ToggleEasterItems(false);
        }
        private void Update()
        {
            if(GetServerTime.serverTimeFetched == true)
            {
                if(checkForItemsOnce == false)
                {
                    CheckForSeasonalItems();
                    CheckForHolidayItems();
                    checkForItemsOnce = true;   
                }
            }
        }
        void CheckForSeasonalItems()
        {
            //enable seasonal items 
            if (GetServerTime.currentServerTime.Month >= winterStartMonth && GetServerTime.currentServerTime.Month < winterEndMonth)
            {
                ToggleWinterItems(true);
            }
            if (GetServerTime.currentServerTime.Month >= fallStartMonth && GetServerTime.currentServerTime.Month < fallEndMonth)
            {
                ToggleFallItems(true);
            }
            if (GetServerTime.currentServerTime.Month >= summerStartMonth && GetServerTime.currentServerTime.Month < summerEndMonth)
            {
                ToggleSummerItems(true);
            }
            if (GetServerTime.currentServerTime.Month >= springStartMonth && GetServerTime.currentServerTime.Month < springEndMonth)
            {
                ToggleSpringItems(true);
            }


        }
        void CheckForHolidayItems()
        {
            if (GetServerTime.currentServerTime.Month >= hweenStartMonth && GetServerTime.currentServerTime.Month < hweenEndMonth)
            {
                if (GetServerTime.currentServerTime.Day >= hweenStartDay && GetServerTime.currentServerTime.Day < hweenEndDay)
                {
                    ToggleHalloweenItems(true);
                }
            }

            if (GetServerTime.currentServerTime.Month >= thankGiveStartMonth && GetServerTime.currentServerTime.Month < thankGiveEndMonth)
            {
                if (GetServerTime.currentServerTime.Day >= thankGiveStartDay && GetServerTime.currentServerTime.Day < thankGiveEndDay)
                {
                    ToggleThanksgivingItems(true);
                }
            }

            if (GetServerTime.currentServerTime.Month >= xmasStartMonth && GetServerTime.currentServerTime.Month < xmasEndMonth)
            {
                if (GetServerTime.currentServerTime.Day >= xmasStartDay && GetServerTime.currentServerTime.Day < xmasEndDay)
                {
                    ToggleChristmasItems(true);
                }
            }
            if (GetServerTime.currentServerTime.DayOfYear >= easterStartDay && GetServerTime.currentServerTime.DayOfYear < easterEndDay)
            {
                ToggleEasterItems(true);
            }
        }
        void ToggleSummerItems(bool onOrOff)
        {
            for (int i = 0; i < summerItems.Length; i++)
            {
                summerItems[i].SetActive(onOrOff);
            }
        }
        void ToggleFallItems(bool onOrOff)
        {
            for (int i = 0; i < summerItems.Length; i++)
            {
                fallItems[i].SetActive(onOrOff);
            }
        }
        void ToggleWinterItems(bool onOrOff)
        {
            for (int i = 0; i < summerItems.Length; i++)
            {
                winterItems[i].SetActive(onOrOff);
            }
        }
        void ToggleSpringItems(bool onOrOff)
        {
            for (int i = 0; i < summerItems.Length; i++)
            {
                springItems[i].SetActive(onOrOff);
            }
        }
        void ToggleHalloweenItems(bool onOrOff)
        {
            for (int i = 0; i < hweenItems.Length; i++)
            {
                hweenItems[i].SetActive(onOrOff);
            }
        }
        void ToggleThanksgivingItems(bool onOrOff)
        {
            for (int i = 0; i < thankGiveItems.Length; i++)
            {
                thankGiveItems[i].SetActive(onOrOff);
            }
        }
        void ToggleChristmasItems(bool onOrOff)
        {
            for (int i = 0; i < xmasItems.Length; i++)
            {
                xmasItems[i].SetActive(onOrOff);
            }
        }
        void ToggleEasterItems(bool onOrOff)
        {
            for (int i = 0; i < easterItems.Length; i++)
            {
                easterItems[i].SetActive(onOrOff);
            }
        }
    }

}
