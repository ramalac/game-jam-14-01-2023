using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionThunders : MonoBehaviour
{
    public List<Transform> Thunders = new List<Transform>();
    public List<Transform> EntityInArea = new List<Transform>();

    public float MaxThunderLife;
    public int MaxThunderTogether;

    float timeSinceLastCall = 0f; // variable pour stocker le temps écoulé depuis le dernier appel de la méthode
    public float callInterval; // intervalle de temps entre chaque appel de la méthode, ici 1 seconde


    // Start is called before the first frame update
    void Start()
    {
        // Récupération de tous les éclairs
        int nbChild = transform.childCount;
        for (int i = 0; i < nbChild; ++i)
        {
            var child = transform.GetChild(i);
            Thunders.Add(child);
            child.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastCall += Time.deltaTime; // ajoute le temps écoulé depuis le dernier frame à la variable timeSinceLastCall

        if (timeSinceLastCall >= callInterval) // si le temps écoulé est supérieur ou égal à l'intervalle de temps défini
        {
            timeSinceLastCall -= callInterval; // soustraction de l'intervalle de temps au temps écoulé pour maintenir la synchronisation

            //TriggerThunders();
            TriggerThunder();
        }
    }

    private void TriggerThunder()
    {
        int thunderIndice, targetIndice;
        do
        {
            thunderIndice = Random.Range(0, Thunders.Count);
        } while (Thunders[thunderIndice].gameObject.activeInHierarchy);

        targetIndice = Random.Range(0, EntityInArea.Count);
        Transform targetTransform = EntityInArea[targetIndice].transform;

        GameObject t = Thunders[thunderIndice].gameObject;
        t.SetActive(true);
        t.GetComponent<Thunder>().SetTargetThunder(targetTransform);
        t.GetComponent<Thunder>().LifeTime = Random.Range(0.1f, MaxThunderLife);
    }


    private void TriggerThunders()
    {
        var listEntity = new List<Transform>(EntityInArea);
        var listThunders = new List<Transform>(Thunders);

        int nbThunders = Random.Range(1, Mathf.Min(MaxThunderTogether + 1, listEntity.Count + 1, listThunders.Count + 1));

        Debug.Log($"nb thenders {nbThunders}");

        List<int> thunderIndiceList = new List<int>();
        List<int> targetIndiceList = new List<int>();
        int thunderIndice, targetIndice;

        // Choix thunder
        for (int i = 0; i < nbThunders; ++i)
        {
            do
            {
                thunderIndice = Random.Range(0, listThunders.Count);
            } while (Thunders[thunderIndice].gameObject.activeInHierarchy ||
            thunderIndiceList.Contains(i));
            thunderIndiceList.Add(thunderIndice);
        }

        // Choix cible
        for (int i = 0; i < nbThunders; ++i)
        {
            do
            {
                targetIndice = Random.Range(0, listEntity.Count);
            } while (targetIndiceList.Contains(targetIndice));

            targetIndiceList.Add(targetIndice);
        }

        for (int k = 0; k < nbThunders; ++k)
        {
            thunderIndice = thunderIndiceList[k];
            targetIndice = targetIndiceList[k];

            GameObject t = listThunders[thunderIndice].gameObject;
            t.SetActive(true);

            t.gameObject.GetComponent<Thunder>().SetTargetThunder(listEntity[targetIndice]);
            t.gameObject.GetComponent<Thunder>().LifeTime = Random.Range(0.1f, MaxThunderLife);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
            EntityInArea.Add(other.gameObject.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
            EntityInArea.Remove(other.gameObject.transform);
    }
}
