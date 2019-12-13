using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedObject : MonoBehaviour
{
    Vector3 distance;
    float posX;
    float posY;
    float rotateSpeed = 180;
    Camera camera1;
    ActiveYurt activeYurt;

    private void Start()
    {
        camera1 = GameObject.Find("BuilderCam").GetComponent<Camera>();
        activeYurt = GameObject.Find("Yurts").GetComponent<ActiveYurt>();
    }

    public void OnMouseDown()
    {
        if (Manager.state == Manager.States.Build)
        {
            distance = camera1.WorldToScreenPoint(transform.position);
            posX = Input.mousePosition.x - distance.x;
            posY = Input.mousePosition.y - distance.y;
        }
    }


    private void OnMouseDrag()
    {
        if (Manager.state == Manager.States.Build)
        {
            float rotX = Input.GetAxis("Mouse X") * rotateSpeed * Mathf.Deg2Rad; 
            float rotY = Input.GetAxis("Mouse Y") * rotateSpeed * Mathf.Deg2Rad;

            //If gameObject is structural then only rotate around Yurt walls
            if (gameObject.tag == "DoorStructure" || gameObject.tag == "WindowStructure")
            {
                transform.Rotate(Vector3.up, -rotX);
                transform.Rotate(Vector3.down, rotY);
            }
            else
            {
                //If using 1 finger then drag
                //else if using 2 fingers or left shift and click, rotate
                //else default to drag for Unity. 
                if (Input.touchCount == 1)
                {
                    Vector3 cursorPosition = new Vector3(Input.mousePosition.x - posX, Input.mousePosition.y - posY, distance.z);

                    Vector3 worldPos = camera1.ScreenToWorldPoint(cursorPosition);
                    transform.position = worldPos;
                }
                else if (Input.touchCount == 2 || Input.GetKey(KeyCode.LeftShift))
                {
                    transform.Rotate(Vector3.up, -rotX);
                    transform.Rotate(Vector3.down, rotY);
                }
                else
                {
                    Vector3 cursorPosition = new Vector3(Input.mousePosition.x - posX, Input.mousePosition.y - posY, distance.z);

                    Vector3 worldPos = camera1.ScreenToWorldPoint(cursorPosition);
                    transform.position = worldPos;
                }
            }
        }

        LeftBounds(); //Check if object still within Yurt bounds
    }

    /// <summary>
    /// Delete object when pulled off screen
    /// Only in outside view to avoid accidental deletion
    /// 
    /// ENSURE PARENT OBJECT HAS A RENDERER!
    /// 
    /// </summary>
    private void OnBecameInvisible()
    {
        //If gameManager state is in outside
        //and this gameobject is not within bounds of active Yurt
        //then delete this game object
        if (Manager.state == Manager.States.Outside || Manager.state == Manager.States.Inside
                && !activeYurt.BoundsCol.bounds.Contains(gameObject.transform.position))
        {
            DestroyGameObject();
        }
    }

    /// <summary>
    /// If object detects it has left collider bounds
    /// check if that object is outside of the Yurt collider bounds.
    /// Delete if it is.
    /// </summary>
    private void LeftBounds()
    {
        if (!activeYurt.BoundsCol.bounds.Contains(gameObject.transform.position))
        {
            ReturnToPool();
        }
    }

    /// <summary>
    /// Method to destroy this gameobject
    /// </summary>
    private void DestroyGameObject()
    {
        ReturnToPool();
    }

    /// <summary>
    /// Return this gameobject to the pool
    /// </summary>
    private void ReturnToPool()
    {
        gameObject.SetActive(false);

        if (ObjectPool.Instance != null)
        {
            ObjectPool.Instance.AddToPool(gameObject);
        }
    }
}
   
