using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decalage : MonoBehaviour
{

   public GameObject[] TargetContour;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<MoveToTarget>().ChangeTarget(TargetContour);
        }
    }
}
