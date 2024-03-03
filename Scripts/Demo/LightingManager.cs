using Unity.Collections;
using UnityEngine;

namespace TMG_DailyTimer
{
    public class LightingManager : MonoBehaviour
    {
        //[SerializeField] private Color daylightColor = Color.white;
        //[SerializeField] private Color nightlightColor = Color.white;
        [Range(0, 179.9f)][SerializeField] private float sunAngle = 170f;
        [Range(0, 1440)][SerializeField] private int nightEndMin = 480;
        //[Range(0, 1440)][SerializeField] private int nightStartMin = 1320;
        //[SerializeField] private float blendSpeed = 1.0f; // Adjust the speed of the blend
        [SerializeField] private Light sunLight;
        [SerializeField] private bool useServerTime = true;
        //[SerializeField] private Light moonLight;
        private float TimeOfDay;
        private int currentHour;
        private int currentMinute;
        private int currentMinutes;
        private void Start()
        {
           // sunLight = GetComponent<Light>();  
            
        }
        private void Update()
        {
            if(useServerTime)
            {
                if (GetServerTime.serverTimeFetched)
                {
                    TimeOfDay = GetServerTime.currentServerTime.Hour + (GetServerTime.currentServerTime.Minute / 60.0f);
                    //Debug.Log("time of day = " + TimeOfDay);   

                    currentHour = GetServerTime.currentServerTime.Hour;
                    currentMinute = GetServerTime.currentServerTime.Minute;

                    currentMinutes = currentHour * 60 + currentMinute;

                    float tod = TimeOfDay - (nightEndMin / 60);
                    tod %= 24; // Modulus to ensure always between 0-24
                    UpdateLighting(tod);


                    Debug.Log(tod);
                    /*
                    // Check if it's night
                    if (currentMinutes < nightEndMin || currentMinutes >= nightStartMin)
                    {
                        // Blend nightlight color
                        sunLight.color = Color.Lerp(sunLight.color, nightlightColor, blendSpeed * Time.deltaTime);
                    }
                    else
                    {
                        // Blend daylight color
                        sunLight.color = Color.Lerp(sunLight.color, daylightColor, blendSpeed * Time.deltaTime);
                    }
                    */
                }
            }
            else
            {
                TimeOfDay += Time.deltaTime * 1f;
                //Debug.Log("time of day = " + TimeOfDay);   
                TimeOfDay %= 24; // Modulus to ensure always between 0-24
                UpdateLighting(TimeOfDay);
            }

        }


        private void UpdateLighting(float timePercent)
        {
            // Set ambient and fog
            //RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
            //RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

            // If the directional light is set, then rotate and set its color
            if (sunLight != null)
            {
                // Calculate the angle based on the full 24-hour day
                float angle = (timePercent / 24.0f) * 360.0f;
                //Debug.Log(angle);
                // Apply the rotation
                sunLight.transform.localRotation = Quaternion.Euler(new Vector3(angle, sunAngle, 0));
                /*
                if (angle > 180 || angle <= -180)
                {
                    sunLight.transform.localRotation = Quaternion.Euler(new Vector3(-angle, sunAngle, 0));
                    Debug.Log("night time angle");
                }
                else
                {
                    sunLight.transform.localRotation = Quaternion.Euler(new Vector3(angle, sunAngle, 0));
                    Debug.Log("day time angle");
                }
                */
                // Set the color
                //DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);
            }

        }

        //Try to find a directional light to use if we haven't set one
        private void OnValidate()
        {
            if (sunLight != null)
                return;

            //Search for lighting tab sun
            if (RenderSettings.sun != null)
            {
                sunLight = RenderSettings.sun;
            }
            //Search scene for light that fits criteria (directional)
            else
            {
                Light[] lights = GameObject.FindObjectsOfType<Light>();
                foreach (Light light in lights)
                {
                    if (light.type == LightType.Directional)
                    {
                        sunLight = light;
                        return;
                    }
                }
            }
        }
    }
}
