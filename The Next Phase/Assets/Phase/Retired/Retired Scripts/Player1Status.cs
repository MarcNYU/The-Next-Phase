using UnityEngine;
using System.Collections;

public class Player1Status : MonoBehaviour {

	public string current = "Health: ";
	public Player1 p1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		string num = (p1.currentHealth).ToString();
		GetComponent<TextMesh> ().text = current + num;
	}
}
