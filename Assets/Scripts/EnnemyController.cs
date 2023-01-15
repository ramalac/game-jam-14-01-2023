using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyController : MonoBehaviour
{
    public float MaxHealth;

    private Transform EnnemyPosition;
    private float Health;

    private void Awake()
    {
        EnnemyPosition = GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Je suis touché" + collision);
        if (collision.gameObject.CompareTag("Arrow"))
        {
            //Debug.Log("OMG J'AI MAL!!!!!");
            //collision.transform.SetParent(EnnemyPosition);
            TakeDamage(10);
        } 
    }

    public void TakeDamage(float quantity)
    {
        Health -= quantity;
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
