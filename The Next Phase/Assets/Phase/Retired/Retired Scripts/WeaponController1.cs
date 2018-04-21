using UnityEngine;
using System.Collections;

public class WeaponController1 : MonoBehaviour {
	public bool axeEquipped;
	public GameObject axe;
	Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.B)) {
			if (!axeEquipped){
				Debug.Log ("Press");
				axeEquipped = true;
				axe.SetActive(true);
				
			}
			if (axeEquipped){
				axeEquipped = false;
				axe.SetActive(false);
			}
		}


	}

}


