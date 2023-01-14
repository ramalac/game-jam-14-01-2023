using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIntermediaire : MonoBehaviour
{
    public Transform TargetPlayer;
    public Decalage ScriptDecalage;

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
            other.gameObject.GetComponent<MoveToTarget>().ChangeTarget(TargetPlayer);
        }
    }
}
