using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookAt : MonoBehaviour {

    public bool lockCursor;
    
    public static PlayerLookAt Instance;

    static bool initialized = false;

    void Awake()
    {
        if (!initialized)
        {
            initialized = true;
            Instance = this;
            CameraController.UseExistingOrCreateNewCamera();
        }
    }

    void Update()
    {
        if(lockCursor) 
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Camera.main == null)
            return;
        transform.forward = new Vector3(Camera.main.GetComponent<Transform>().transform.forward.x,
                                        0,
                                        Camera.main.GetComponent<Transform>().transform.forward.z);
        
    }
}
