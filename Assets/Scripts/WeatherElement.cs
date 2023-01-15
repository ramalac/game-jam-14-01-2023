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
    /// Active tous les objets Weather du type de la flèche.
    /// </summary>
    /// <param name="typeWeatherArrow">Type de la flèche.</param>
    public void ActivationWeather(TypeWeatherEnum typeWeatherArrow)
    {
        Debug.Log($"Activation weather{typeWeatherArrow.DisplayName()}");
        foreach (Transform t in Weathers)
        {
            var typeWeather = t.GetComponent<Weather>().TypeWeather;
            if (typeWeather == typeWeatherArrow)
            {
                t.gameObject.SetActive(true); // Active la météo
                ActiveWeather.Add(t); // Ajoute à la liste des météos active
            }
        }
    }

    /// <summary>
    /// Désactive tous les objets Weather activé.
    /// </summary>
    public void DesactivationWeather()
    {
        foreach (var t in ActiveWeather)
        {
            t.gameObject.SetActive(false); // Désactive l'objet
        }
        ActiveWeather.Clear();
    }

}
