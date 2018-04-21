using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public static CameraController Instance;
    public Transform TargetLoookAt;

    public float Distance = 5F;
    public float DistMin = 3F;
    public float DistMax = 10F;

    public float DistSmooth = 0.05f;

    public float X_MouseSensitivity = 5f;//5f for mouse : 20f for stick
    public float Y_MouseSensitivity = 5f;
    public float MouseWheelSensitivity = 5f;
    public float Y_minLimit = -40f;
    public float Y_maxLimit = 80f;

    public float Xsmooth = 0.05f;
    public float Ysmooth = 0.1f;

    private float mouseX = 0f;
    private float mouseY = 0f;
    private float startDist = 0f;
    private float desiredDist = 0f;

    private float veloDist = 0.02f;
    private Vector3 desiredPosition = Vector3.zero;

    private float veloX = 0f;
    private float veloY = 0f;
    private float veloZ = 0f;

    private Vector3 position = Vector3.zero;

    static bool initialized = false;

    void Awake()
    {
        if (!initialized)
        {
            initialized = true;
            Instance = this;
        }
    }

    void Start()
    {
        Distance = Mathf.Clamp(Distance, DistMin, DistMax);
        startDist = Distance;
        Reset();
    }

    void LateUpdate()
    {
        if (TargetLoookAt == null)
            return;

        HandlePlayerInput();
        CalcDesiredPosition();
        UpdatePosition();
    }

    void HandlePlayerInput()
    {
        var DeadZone = 0.01f;

        mouseX += Input.GetAxis("Mouse X") * X_MouseSensitivity;
        mouseY -= Input.GetAxis("Mouse Y") * Y_MouseSensitivity;


        //		if (Input.GetMouseButton (1)) {
        //			mouseX += Input.GetAxis ("Mouse X") * X_MouseSensitivity;
        //			mouseY -= Input.GetAxis ("Mouse Y") * Y_MouseSensitivity;
        //		}

        mouseY = Helper.ClampAngle(mouseY, Y_minLimit, Y_maxLimit);


        if (Input.GetAxis("Mouse ScrollWheel") < -DeadZone || Input.GetAxis("Mouse ScrollWheel") > DeadZone)
        {
            desiredDist = Mathf.Clamp(Distance - Input.GetAxis("Mouse ScrollWheel") * MouseWheelSensitivity, DistMin, DistMax);
        }

    }

    void CalcDesiredPosition()
    {
        Distance = Mathf.SmoothDamp(Distance, desiredDist, ref veloDist, DistSmooth);
        desiredPosition = CalcPosition(mouseY, mouseX, Distance);
    }

    Vector3 CalcPosition(float rotationX, float rotationY, float distance)
    {
        Vector3 direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
        return TargetLoookAt.position + rotation * direction;
    }

    void UpdatePosition()
    {
        var positionX = Mathf.SmoothDamp(position.x, desiredPosition.x, ref veloX, Xsmooth);
        var positionY = Mathf.SmoothDamp(position.y, desiredPosition.y, ref veloY, Ysmooth);
        var positionZ = Mathf.SmoothDamp(position.z, desiredPosition.z, ref veloZ, Xsmooth);

        position = new Vector3(positionX, positionY, positionZ);

        transform.position = position;
        transform.LookAt(TargetLoookAt);
    }

    public void Reset()
    {

        mouseX = 0;
        mouseY = 10;
        Distance = startDist;
        desiredDist = Distance;


    }

    public static void UseExistingOrCreateNewCamera()
    {
        GameObject tempcamera;
        GameObject targetLookAt;
        CameraController myCamera;

        if (Camera.main != null)
        {
            tempcamera = Camera.main.gameObject;
        }
        else
        {
            tempcamera = new GameObject("Main Camera");
            tempcamera.AddComponent<Camera>();
            tempcamera.tag = "MainCamera";
        }

        tempcamera.AddComponent<CameraController>();
        myCamera = tempcamera.GetComponent("CameraController") as CameraController;

        targetLookAt = GameObject.Find("targetLookAt") as GameObject;

        if (targetLookAt == null)
        {
            targetLookAt = new GameObject("targetLookAt");
            targetLookAt.transform.position = Vector3.zero;
        }

        myCamera.TargetLoookAt = targetLookAt.transform;


    }
}
