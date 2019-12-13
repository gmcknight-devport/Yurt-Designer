using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InsideCam : MonoBehaviour
{      
    float pitch, yaw;//Variables for rotation
    [SerializeField] float speed = 400;//Spped of rotation
       
    /// <summary>
    /// Change rotation based on user input 
    /// Seperate actions for mobile input and Unity
    /// </summary>
    private void FixedUpdate()
    {

#if MOBILE_INPUT
        if (Input.touchCount == && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
            MoveCam();
        }
#endif

#if !MOBILE_INPUT
        if (Input.GetKey(KeyCode.Mouse0)) {

            MoveCam();
        }
#endif

    }

    private void MoveCam()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        float rotAmountX = mouseX * Time.deltaTime * speed;
        float rotAmountY = mouseY * Time.deltaTime * speed;

        pitch = Mathf.Clamp(pitch - rotAmountY, -80f, 80f);
        yaw += rotAmountX;

        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }
}
    
