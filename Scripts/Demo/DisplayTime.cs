using System.Collections;
using System.Collections.Generic;
using TMG_DailyTimer;
using TMPro;
using UnityEngine;


namespace TMG_DailyTimer
{
    public class DisplayTime : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI serverTimeTxt;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(GetServerTime.serverTimeFetched == true)
            {
                serverTimeTxt.text = GetServerTime.currentServerTimeString.ToString();

            }
        }
    }

}
