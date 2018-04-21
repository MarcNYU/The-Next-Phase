using UnityEngine;
using System.Collections;

public class Player2Status : MonoBehaviour {
	public string current = "Health: ";
	public Player2 p2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		string num = (p2.currentHealth).ToString();
		GetComponent<TextMesh> ().text = current + num;
	}
}
