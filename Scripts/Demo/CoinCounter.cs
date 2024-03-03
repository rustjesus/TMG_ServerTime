using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TMG_DailyTimer
{
    public class CoinCounter : MonoBehaviour
    {
        //this is an example of a coin counter
        [SerializeField] private Image firstCoinImage;
        [SerializeField] private Image secondCoinImage;
        [SerializeField] private Image thirdCoinImage;
        void Update()
        {
            if (PlayerPrefs.GetInt("Money") == 0)
            {
                firstCoinImage.gameObject.SetActive(false);
                secondCoinImage.gameObject.SetActive(false);
                thirdCoinImage.gameObject.SetActive(false);
            }
            else if (PlayerPrefs.GetInt("Money") >= 1)
            {
                firstCoinImage.gameObject.SetActive(true);
                secondCoinImage.gameObject.SetActive(false);
                thirdCoinImage.gameObject.SetActive(false);
            }
            else if (PlayerPrefs.GetInt("Money") >= 15000)
            {
                firstCoinImage.gameObject.SetActive(true);
                secondCoinImage.gameObject.SetActive(true);
                thirdCoinImage.gameObject.SetActive(false);
            }
            else if (PlayerPrefs.GetInt("Money") >= 30000)
            {
                firstCoinImage.gameObject.SetActive(true);
                secondCoinImage.gameObject.SetActive(true);
                thirdCoinImage.gameObject.SetActive(true);
            }
        }

    }
}

