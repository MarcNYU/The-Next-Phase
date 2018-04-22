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
        Screen.lockCursor = lockCursor;

        if (Camera.main == null)
            return;
        transform.forward = new Vector3(Camera.main.GetComponent<Transform>().transform.forward.x,
                                        0,
                                        Camera.main.GetComponent<Transform>().transform.forward.z);
        
    }
}
