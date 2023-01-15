using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRController : MonoBehaviour
{
    public float Sensitivity = 0.1f;
    public float MaxSpeed = 1.0f;
    public float Gravity = 30.0f;
    public float RotateIncrement = 40.0f;

    public SteamVR_Action_Boolean RotatePress = null;
    public SteamVR_Action_Boolean MovePress = null;
    public SteamVR_Action_Vector2 MoveValue = null;
    public SteamVR_Action_Vector2 RotateValue = null;

    private float Speed = 0.0f;
    private CharacterController Character = null;
    private Transform CameraRig = null;
    private Transform Head = null;

    private void Awake()
    {
        Character = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        CameraRig = SteamVR_Render.Top().origin;
        Head = SteamVR_Render.Top().head;
    }

    // Update is called once per frame
    void Update()
    {
        HandleHead();
        HandleHeight();
        CalculateMovement(); 
    }

    private void HandleHead()
    {
        Vector3 oldPosition = CameraRig.position;
        Quaternion oldRotation = CameraRig.rotation;

        transform.eulerAngles = new Vector3(0.0f, Head.rotation.eulerAngles.y, 0.0f);

        CameraRig.position = oldPosition;
        CameraRig.rotation = oldRotation;
    }

    private void CalculateMovement()
    {
        // figure out movement orientation 
        Vector3 orientationEuler = new Vector3 (0.0f, transform.eulerAngles.y, 0.0f);
        //Vector3 orientationEuler = new Vector3 (0.0f, Head.eulerAngles.y, 0.0f);
        //Quaternion orientation = Quaternion.Euler(orientationEuler);
        Quaternion orientation = CalculateOrientation();
        Vector3 movement = Vector3.zero;

        // if not moving
        //if (MovePress.GetStateUp(SteamVR_Input_Sources.Any))
        if (MoveValue.axis.magnitude == 0.0f)
            Speed = 0.0f;


        Speed += MoveValue.axis.magnitude * Sensitivity;
        Speed = Mathf.Clamp(Speed, -MaxSpeed, MaxSpeed);

        movement += orientation * (Speed * Vector3.forward);
        movement.y -= Gravity * Time.deltaTime;

        // Apply
        Character.Move(movement * Time.deltaTime);
    }

    private void HandleHeight()
    {
        float headHeight = Mathf.Clamp(Head.localPosition.y, -1, 2);
        Character.height = headHeight;

        Vector3 newCenter = Vector3.zero;
        newCenter.y = headHeight/2;
        newCenter.y += Character.skinWidth;

        newCenter.x = Head.localPosition.x;
        newCenter.z = Head.localPosition.z;
        newCenter = Quaternion.Euler(0, -transform.eulerAngles.y, 0) * newCenter;


        Character.center = newCenter;
    }

    /*private void SnapRotatation()
    {
        float snapValue = 0.0f;
        if (RotatePress.GetStateDown(SteamVR_Input_Sources.Any))
        {
            snapValue = RotateIncrement * Mathf.Sign(RotateValue.axis.y);
        }

        transform.RotateAround(Head.position, Vector3.up, snapValue);
    }*/

    private Quaternion CalculateOrientation()
    {
        float rotation = Mathf.Atan2(MoveValue.axis.x, MoveValue.axis.y);
        rotation *= Mathf.Rad2Deg;

        Vector3 orientationEuler = new Vector3(0, Head.eulerAngles.y + rotation, 0);
        return Quaternion.Euler(orientationEuler);
    }

}
