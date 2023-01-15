using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalageMurPhysique : MonoBehaviour
{
    public Decalage ScriptDecalage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        ScriptDecalage.GiveTargetContour(collision.gameObject); 
    }

}
