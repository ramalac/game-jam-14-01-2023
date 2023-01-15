using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cloud : MonoBehaviour
{
    private WeatherElement _weatherElement;

    // Start is called before the first frame update
    void Start()
    {
        _weatherElement = GetComponentInParent<WeatherElement>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Arrow"))
        {
            //Debug.Log("trigger enter cloud par arrow");
            var arrowWeatherType = other.gameObject.GetComponent<Arrow>().TypeArrow;
            _weatherElement.ActivationWeather(arrowWeatherType);
        }
    }
}
