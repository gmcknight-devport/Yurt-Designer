using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureObj : MonoBehaviour
{
    bool validPlacement = true;//Indictaes with current placement is valid
    MeshRenderer placement;//Meshrenderer object for user visual cue    
     
    /// <summary>
    /// If children collide with object with with "Structure" tag 
    /// Turn on meshrenderer in child object for visual indication 
    /// for user.
    /// Set bool to indicate valid placement
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Structure" || collision.gameObject.tag == "DoorStructure" || collision.gameObject.tag == "WindowStructure")
        {
            validPlacement = false;

            placement = gameObject.transform.Find("Placement").GetComponent<MeshRenderer>();
            placement.enabled = true;
        }
        else
        {
            return;
        }
    }

    /// <summary>
    /// Turn mesh renderer off
    /// Update validplacement bool
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionExit(Collision collision)
    {
        validPlacement = true;

        if(placement != null)
        {
            placement.enabled = false;
        }
    }
    
    /// <summary>
    /// Message Receiver
    /// Delete structure object if invalid placement
    /// </summary>
    public void ExitBuildMode()
    {        
        if (!validPlacement)
        {
            Destroy(gameObject);
        }
    }
}
