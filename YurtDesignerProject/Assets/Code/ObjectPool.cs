using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance{ get; set; }

    private List<GameObject> spawnObjectsPool = new List<GameObject>();

    /// <summary>
    /// Singleton awake method
    /// </summary>
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    /// <summary>
    /// Return an object from the pool.
    /// Check if pooledObject is same kind as calling script prefab.
    /// </summary>
    /// <param name="spawnObj"></param>
    /// <returns></returns>
    public GameObject GetPoolObj(GameObject spawnObj)
    {        
        foreach(GameObject poolObj in spawnObjectsPool)
        {    
            //Make sure to add (Clone) to spawnObj name
            //as unity always spawns a prefab clone
            if(spawnObj.name + "(Clone)" == poolObj.name)
            {
                spawnObjectsPool.Remove(poolObj);
                return poolObj;
            }            
        }        
        return null;
    }

    /// <summary>
    /// Add gameobject to pool list
    /// </summary>
    /// <param name="spawnObj"></param>
    public void AddToPool(GameObject spawnObj)
    {
        spawnObjectsPool.Add(spawnObj);
    }
}
