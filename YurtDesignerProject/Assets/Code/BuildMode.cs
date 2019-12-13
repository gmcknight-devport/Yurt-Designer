using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMode : MonoBehaviour
{
    
    //Variables for object spawning
    Vector3 objInsPosition = new Vector3(0, -1.85f, 0); //Variable to store instantiation location
    Quaternion objInsRotation = new Quaternion(0f, 180f, 0f, 0f); // Variable to store instantiation rotation
    [SerializeField] GameObject spawnObject; //Object to instantiate

    Vector3 mainDoorPos = new Vector3(-0.06f, -0.5f, -0.05f); //Position to instantiate structures for Yurt

    private int spawnLimit = 6; //Set limit to number of object spawns
    int currentSpawns = 0; //Current number of instantiated objects

    float radius = 0.01f; //Set radius to check if object already exists at spawn Vector
      
    
    /// <summary>
    /// Instantiate object with button press.
    /// If spawnlimit reached then retrieve from object pool.
    /// </summary>
    public void AddObjectToScene()
    {
        if((objInsPosition != null && spawnObject != null && objInsRotation != null) && currentSpawns < spawnLimit)
        {    
            Instantiate(spawnObject, objInsPosition, objInsRotation);
            currentSpawns++;
        }
        else
        {            
            GetObjFromPool();
        }
    }

    /*Check if there is an object in spawn location already with Physcis.Overlap Sphere
    Detroy it if there is and call AddObjectToScene()
    This is an extension to AddObjectToScene method and doesn't have to be used
    */
    public void CheckSpawnLocation()
    {
      
        if (objInsPosition != null && currentSpawns < spawnLimit)
        {
            Collider[] hitColliders = Physics.OverlapSphere(objInsPosition, radius);
            
            Debug.Log(hitColliders[0]);

            if (hitColliders[0].tag != "Camera")//Ignore camera objects
            {
                Destroy(hitColliders[0].gameObject);
            }
            AddObjectToScene();
        }
    }

    /// <summary>
    /// Spawn Structure Object in main door place
    /// </summary>
    public void AddStructureToScene()
    {
        if (spawnObject != null)
        {            
            Instantiate(spawnObject, mainDoorPos, objInsRotation);
        }
    }        

    /// <summary>
    /// Get gameobject to spawn from object pool
    /// using the prefab attached to this script.
    /// Set it's position and rotation and set to active.
    /// </summary>
    private void GetObjFromPool()
    {
        GameObject toSpawn = ObjectPool.Instance.GetPoolObj(spawnObject);

        if (toSpawn)
        {
            toSpawn.transform.position = objInsPosition;
            toSpawn.transform.rotation = objInsRotation;
            toSpawn.SetActive(true);
        }
    }
}
