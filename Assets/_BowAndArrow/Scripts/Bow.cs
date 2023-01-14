using System.Collections;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [Header("Assets")]
    public GameObject ArrowPrefab = null;

    [Header("Bow")]
    public float GrabThreshold = 0.15f;
    public Transform Start_pos = null;
    public Transform End = null;
    public Transform Socket = null;

    private Transform PullingHand = null;
    private Arrow CurrentArrow = null;
    private Animator Animation= null;

    private float PullValue = 0f;

    private void Awake()
    {
        Animation = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(CreateArrow(0.0f));
    }

    private void Update()
    {
        if (!PullingHand || !CurrentArrow)
            return;
        
        PullValue = CalculatePull(PullingHand);
        PullValue = Mathf.Clamp(PullValue, 0f, 1f);
        Animation.SetFloat("Blend", PullValue);
    }

    private float CalculatePull(Transform pullHand)
    {
        Vector3 direction = End.position - Start_pos.position;
        float magnetude = direction.magnitude;

        direction.Normalize();
        Vector3 difference = pullHand.position - Start_pos.position;

        return Vector3.Dot(difference, direction) / magnetude;
    }

    private IEnumerator CreateArrow(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        GameObject arrowObject = Instantiate(ArrowPrefab, Socket);
        arrowObject.transform.localPosition = new Vector3(0, 0, 0.425f);
        arrowObject.transform.localEulerAngles = Vector3.zero;
        CurrentArrow = arrowObject.GetComponent<Arrow>();
    }

    public void Pull(Transform hand)
    {
        float distance = Vector3.Distance(hand.position, Start_pos.position);
        if (distance < GrabThreshold)
            return;

        PullingHand = hand;
    }

    public void Release()
    {
        if (PullValue > 0.3f)
        {
            FireArrow();
        }
        PullingHand = null;
        PullValue = 0f;
        Animation.SetFloat("Blend",0);

        if (!CurrentArrow)
            StartCoroutine(CreateArrow(0.5f));
    }

    private void FireArrow()
    {
        CurrentArrow.Fire(PullValue);
        CurrentArrow = null;
    }

}
