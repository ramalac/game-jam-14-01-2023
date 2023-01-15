using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float Speed = 2000.0f;
    public Transform Tip = null;
    public Material[] Texture;
    public TypeWeatherEnum TypeArrow;
    private Rigidbody ArrowRigidbody = null;
    private bool IsStopped = true;
    private Vector3 LastPosition = Vector3.zero;

    private void Awake()
    {
        ArrowRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (IsStopped)
        {
            return;
        }
        ArrowRigidbody.MoveRotation(Quaternion.LookRotation(ArrowRigidbody.velocity, transform.up));
        if(Physics.Linecast(LastPosition, Tip.position))
        {
            Stop();
        }
        LastPosition = Tip.position;
    }

    private void Stop()
    {
        IsStopped = true;
        ArrowRigidbody.isKinematic = true;
        ArrowRigidbody.useGravity = false;
    }

    public void Fire(float pullValue)
    {
        IsStopped = false;
        transform.parent = null;
        ArrowRigidbody.isKinematic=false;
        ArrowRigidbody.useGravity = true;
        ArrowRigidbody.AddForce(transform.forward * (pullValue * Speed));

        Destroy(gameObject, 5.0f);
    }

    public void ApplyType(TypeWeatherEnum CurrentType)
    {
        TypeArrow = CurrentType;
        foreach (Renderer ArrowPart in gameObject.GetComponentsInChildren<Renderer>())
        {
            ArrowPart.material = Texture[((int)TypeArrow)];
        }
    }
    
}
