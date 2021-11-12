using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherChange : MonoBehaviour
{
    [HideInInspector]
    public GameObject sun;
    [HideInInspector]
    public Light sunLight;

    [Range(0, 24)]
    public float timeOfDay = 12;

    public float secondsPerMinute = 60;
    [HideInInspector]
    public float secondsPerHour;
    [HideInInspector]
    public float secondsPerDay;

    public float timeMultiplier = 1;
    private bool isDay;
    private bool isNight;

    public Material day;
    public Material night;
    // Start is called before the first frame update
    void Start()
    {
        sun = gameObject;
        sunLight = gameObject.GetComponent<Light>();
        //secondsPerHour = secondsPerMinute * 60;
        //secondsPerDay = secondsPerHour * 24;
        isDay = true;
        isNight = false;
    }

    // Update is called once per frame
    void Update()
    {
        SunUpdate();
    }
    public void SunUpdate()
    {
        //30,-30,0 = sunrise
        //90,-30,0 = High noon
        //180,-30,0 = sunset
        //-90,-30,0 = Midnight

        //sun.transform.localRotation = Quaternion.Euler((timeOfDay / 24) * 360 - 0, -30, 0);
        sun.transform.localEulerAngles = new Vector3(Time.time * timeMultiplier, -30, 0);
        if(!isDay && (Time.time*timeMultiplier)%360 >= 0 && (Time.time * timeMultiplier) % 360 <= 180)
        {
            isDay = true;
            isNight = false;
            RenderSettings.skybox = day;
        }
        if (!isNight && (Time.time * timeMultiplier) % 360 > 180 && (Time.time * timeMultiplier) % 360 < 360)
        {
            isNight = true;
            isDay = false;
            RenderSettings.skybox = night;
        }
    }
}
