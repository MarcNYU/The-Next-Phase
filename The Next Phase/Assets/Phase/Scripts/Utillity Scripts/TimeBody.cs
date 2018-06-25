using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    /*
    Attach This to all objects that will use the rewind
    */
    public float recordTime = 5f;
    private bool isRewinding = false;

    [HideInInspector]
    public bool triggerRewind;
    private bool endRewindAnimation;

    List<PointInTime> pointsInTime;

    Rigidbody rb;

    void Start()
    {
        pointsInTime = new List<PointInTime>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (triggerRewind)
            StartRewind();
        if (endRewindAnimation)
            StopRewind();

        //if (Input.GetKeyDown(KeyCode.Return))
        //    StartRewind();
        //if (Input.GetKeyUp(KeyCode.Return))
            //StopRewind();
    }

    private void FixedUpdate()
    {
        if (isRewinding)
            Rewind();
        else
            Record();
    }

    void Rewind()
    {
        if(pointsInTime.Count > 0)
        {
            PointInTime pointInTime = pointsInTime[0];
            transform.position = pointInTime.position;
            pointsInTime.RemoveAt(0);
        }
        else
        {
            StopRewind();
            ReapplyForces();
        }
    }

    void Record()
    {
        if(pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }

        pointsInTime.Insert(0,new PointInTime(transform.position,transform.rotation,rb.velocity,rb.angularVelocity));
    }

	void StartRewind()
    {
        isRewinding = true;
        rb.isKinematic = true;
    }

    void StopRewind()
    {
        isRewinding = false;
        rb.isKinematic = false;
    }

    void ReapplyForces()
    {
        rb.position = pointsInTime[0].position;
        rb.rotation = pointsInTime[0].rotation;
        rb.velocity = pointsInTime[0].velocity;
        rb.angularVelocity = pointsInTime[0].angularVelocity;
    }


    struct PointInTime 
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 velocity;
        public Vector3 angularVelocity;

        public PointInTime (Vector3 _position, Quaternion _rotation, Vector3 _velocity, Vector3 _angularVelocity)
        {
            position = _position;
            rotation = _rotation;
            velocity = _velocity;
            angularVelocity = _angularVelocity;
        }
    }



}
