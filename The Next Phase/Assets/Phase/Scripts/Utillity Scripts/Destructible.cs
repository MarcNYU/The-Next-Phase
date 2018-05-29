using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour {
    
    Transform[] Pieces;

    [HideInInspector] public bool isBroken;

    // Use this for initialization
    void Awake()
    {
        InitArray();
        InitChildern();
    }

	void InitArray()
	{
        Pieces = new Transform[transform.childCount];
        int i = 0;
        foreach (Transform child in transform)
        {
            if(child.GetComponent<Rigidbody>() != null)
                Pieces[i] = child;
            
            i++;
        }
	}

    void InitChildern()
    {
        for (int i = 0; i < Pieces.Length; i++)
        {
            if(Pieces[i] != null)
                Pieces[i].gameObject.AddComponent<TimeBody>();
        }
    }

	void FixedUpdate()
    {
        if (isBroken) 
            FallApart();
        else 
            Rebuild();
    }

    void FallApart()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Rigidbody>().isKinematic = false;
            child.GetComponent<Rigidbody>().useGravity = true;
            child.transform.parent = null;
        }
    }

    void Rebuild()
    {
        foreach (Transform child in Pieces)
        {
            child.GetComponent<Rigidbody>().isKinematic = true;
            child.GetComponent<Rigidbody>().useGravity = false;
            child.transform.parent = transform;
        }
    }
}
