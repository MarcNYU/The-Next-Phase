using UnityEngine;
using System.Collections;

public class transition : MonoBehaviour {
	public int back;
	public int forward;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.LoadLevel(back);
		}

		if (Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown (KeyCode.KeypadEnter)) {
			Application.LoadLevel(forward);
		}
	}
}
