using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InsideUI : MonoBehaviour
{
    ActiveYurt activeYurt;//Ref for activeYurt
    GameObject cameraIn;//Ref for inside camera
    Quaternion posDefault = new Quaternion(0, 0, 0, 0);//Deafult position for camera

    private void Start()
    {
        activeYurt = GameObject.Find("Yurts").GetComponent<ActiveYurt>();//Get ActiveYurt script/obj
        cameraIn = GameObject.Find("InsideCam");
    }

    /// <summary>
    /// Call Hide() method of ActiveYurt
    /// </summary>
    public void HideRoof()
    {
        activeYurt.Hide();
    }

    /// <summary>
    /// Call Show() method of ActiveYurt
    /// </summary>
    public void ShowRoof()
    {
        activeYurt.Show();
    }

    /// <summary>
    /// Set camera position t default when button pressed.
    /// Needed in case user has previously moved camera to unusual location.
    /// </summary>
    public void SetCamPosDefault()
    {
        if (cameraIn)
        {
            cameraIn.transform.rotation = posDefault;
        }
    }

    /// <summary>
    /// Iterate through all structure objects in scene.
    /// If they are door or window structures then sendmessage
    /// that not in build mode
    /// </summary>
    public void ExitBuild()
    {
        foreach (GameObject temp in GameObject.FindGameObjectsWithTag("DoorStructure"))
        {           
             temp.SendMessageUpwards("ExitBuildMode");                       
        }
        foreach (GameObject temp in GameObject.FindGameObjectsWithTag("WindowStructure"))
        {            
            temp.SendMessageUpwards("ExitBuildMode");            
        }
    }
    
    /// <summary>
    /// Set State to Inside 
    /// </summary>
    public void InsideState()
    {
        Manager.ChangeState(Manager.States.Inside);
    }

    /// <summary>
    /// Set state to build
    /// </summary>
    public void BuildState()
    {
        Manager.ChangeState(Manager.States.Build);
    }
}

