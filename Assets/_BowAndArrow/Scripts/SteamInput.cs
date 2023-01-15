using UnityEngine;
using Valve.VR;

public class SteamInput : MonoBehaviour
{
    
    public Bow m_Bow = null;
    public SteamVR_Behaviour_Pose m_Pose = null;
    public SteamVR_Action_Boolean m_PullAction = null;
    public SteamVR_Action_Boolean ChangeTypeAction = null; 

    private void Update()
    {
        if (m_PullAction.GetStateDown(m_Pose.inputSource))
        {
            Debug.Log("Appui sur la gachette");
            m_Bow.Pull(m_Pose.gameObject.transform);
        }

        if (m_PullAction.GetStateUp(m_Pose.inputSource))
            m_Bow.Release();

        if (ChangeTypeAction.GetStateDown(m_Pose.inputSource))
        {
            Debug.Log("Appui sur le boutton");
            m_Bow.ChangeType();
        }
    }
    
}
