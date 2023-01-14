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
        GiveTargetContour(other.gameObject);
    }

    public void GiveTargetContour(GameObject g)
    {
        if (g.CompareTag("Enemy"))
        {
            g.GetComponent<MoveToTarget>().ChangeTarget(TargetContour);
        }
    }

}
