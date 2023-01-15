using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeatherElement : MonoBehaviour
{
    public List<Transform> Weathers = new List<Transform>();
    public List<Transform> Clouds = new List<Transform>();

    public List<Transform> ActiveWeather = new List<Transform>();

    public bool WeatherActivation = false;

    // Start is called before the first frame update
    void Start()
    {
        int nbChild = transform.childCount;

        for (int i = 0; i < nbChild; ++i)
        {
            var child = transform.GetChild(i);

            if (child.CompareTag("Cloud"))
            {
                Clouds.Add(child);
            }
            else // Weather...
            {
                Weathers.Add(child);
                child.gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //ActivationWeather(WeatherActivation);
    }

    /// <summary>
    /// Active tous les objets Weather du type de la fl�che.
    /// </summary>
    /// <param name="typeWeatherArrow">Type de la fl�che.</param>
    public void ActivationWeather(TypeWeatherEnum typeWeatherArrow)
    {
        Debug.Log($"Activation weather{typeWeatherArrow.DisplayName()}");
        foreach (Transform t in Weathers)
        {
            var typeWeather = t.GetComponent<Weather>().TypeWeather;
            if (typeWeather == typeWeatherArrow)
            {
                t.gameObject.SetActive(true); // Active la m�t�o
                ActiveWeather.Add(t); // Ajoute � la liste des m�t�os active
            }
        }
    }

    /// <summary>
    /// D�sactive tous les objets Weather activ�.
    /// </summary>
    public void DesactivationWeather()
    {
        foreach (var t in ActiveWeather)
        {
            t.gameObject.SetActive(false); // D�sactive l'objet
        }
        ActiveWeather.Clear();
    }

}
