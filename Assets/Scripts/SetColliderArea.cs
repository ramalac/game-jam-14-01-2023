using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColliderArea : MonoBehaviour
{
    public ParticleSystem RainParticule;

    private SphereCollider _collider;

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<SphereCollider>();
        _collider.enabled = true;
        _collider.isTrigger = true;
        _collider.radius = RainParticule.shape.radius;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
