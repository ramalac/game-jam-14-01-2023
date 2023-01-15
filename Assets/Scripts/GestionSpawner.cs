using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionSpawner : MonoBehaviour
{

    //public List<GameObject> _spawner;
    public Transform[] Spawners;

    public GameObject[] PrefabEnemy;

    public int NbEnemy = 0;

    public float timeSinceLastCall = 0f; // variable pour stocker le temps �coul� depuis le dernier appel de la m�thode
    public float callInterval ; // intervalle de temps entre chaque appel de la m�thode, ici 10 seconde

    // Start is called before the first frame update
    void Start()
    {
        //Spawners = GameObject.FindGameObjectsWithTag("Spawn");
        Spawners = gameObject.GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        timeSinceLastCall += Time.deltaTime; // ajoute le temps �coul� depuis le dernier frame � la variable timeSinceLastCall

        if (timeSinceLastCall >= callInterval) // si le temps �coul� est sup�rieur ou �gal � l'intervalle de temps d�fini
        {
            timeSinceLastCall -= callInterval; // soustraction de l'intervalle de temps au temps �coul� pour maintenir la synchronisation

            int typeEnemy = Random.Range(0, PrefabEnemy.Length);
            int spawn = Random.Range(0, Spawners.Length);
            Instantiate(PrefabEnemy[typeEnemy], Spawners[spawn],false).SetActive(true);
            ++NbEnemy;
            Debug.Log("Spawn");
        }

    }
}
