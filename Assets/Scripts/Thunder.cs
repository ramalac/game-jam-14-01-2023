using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour
{
    public Transform StarThunderTransform;
    public Transform EndThunderTransform;

    public const TypeWeatherEnum TypeWeather = TypeWeatherEnum.Thunder;

    public Transform ThunderGestionTransform;

    public float LifeTime = 0.5f;

    float time; // variable pour stocker le temps écoulé depuis le dernier appel de la méthode
    float timeTrigger; // intervalle de temps entre chaque appel de la méthode, ici 1 seconde



    private void OnEnable()
    {
        time = Time.time;
        timeTrigger = time;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime; // ajoute le temps écoulé depuis le dernier frame à la variable timeSinceLastCall

        if (time >= timeTrigger + LifeTime) // si le temps écoulé est supérieur ou égal à l'intervalle de temps défini
        {
            this.gameObject.SetActive(false);
        }
    }

    public void SetTargetThunder(Transform target)
    {
        EndThunderTransform.position = target.position;
    }

    public void GiveEffect(Collider collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            //collider.gameObject.GetComponent<Enemy>()
            Debug.Log("degat foudre");
        }
    }

}
