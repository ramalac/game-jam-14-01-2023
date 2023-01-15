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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Arrow"))
        {
            other.transform.SetParent(EnnemyPosition);

        }
    }

    public void TakeDamage()
    {

    }
}
