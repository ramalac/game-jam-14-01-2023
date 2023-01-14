using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    public Transform Target;
    public Transform TargetPlayer;
    public float Speed;
    public Vector3 VelocityForce;
    public Vector3 Direction;

    private Rigidbody _rb;


    // Start is called before the first frame update
    void Start()
    {
        Target = TargetPlayer;
        _rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Direction = Target.transform.position - transform.position;
        Direction.Normalize();
    }

    private void FixedUpdate()
    {
        //VelocityForce = _rb.velocity;
        VelocityForce = Direction * Speed;
        _rb.velocity = VelocityForce;
    }

    public void ChangeTarget(GameObject[] listNewTarget)
    {
        List<Transform> listTransform = new List<Transform>();

        Vector3 startPoint = transform.position;
        startPoint.y = 2;
        bool visible = true;

        // Récupère les targets visible par le gameobject
        foreach (var g in listNewTarget)
        {
            visible = true;
            Vector3 endPoint = g.transform.position; endPoint.y = 2;
            Vector3 direction = (endPoint - startPoint).normalized; // direction du rayon
            float distance = Vector3.Distance(startPoint, endPoint); // distance entre les deux points
            Ray ray = new Ray(startPoint, direction);
            //Debug.DrawRay(startPoint, direction*distance, Color.red, 10);
            RaycastHit[] hits = Physics.RaycastAll(ray, distance); // lancement du raycast

            foreach (RaycastHit hit in hits)
            {
                Debug.Log($"{hit.collider.gameObject.name}");
                if (hit.collider.gameObject.CompareTag("Mur"))
                {
                    visible = false; break;
                }
            }
            if (visible)
                listTransform.Add(g.transform);
        }


        // Calcul la target la plus près du player
        List<float> targetToPlayerDistance = new List<float>();
        foreach (var t in listTransform)
        {
            targetToPlayerDistance.Add(Vector3.Distance(TargetPlayer.position, t.position));
        }

        float minDistance = targetToPlayerDistance.Min();
        int indiceMin = targetToPlayerDistance.IndexOf(minDistance);
        ChangeTarget(listTransform[indiceMin]);
        Debug.Log($"Change target : {listTransform[indiceMin].name}");

    }

    public void ChangeTarget(Transform newTarget)
    {
        Target = newTarget;
    }
}
