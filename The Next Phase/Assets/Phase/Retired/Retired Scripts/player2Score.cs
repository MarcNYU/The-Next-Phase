using UnityEngine;
using System.Collections;

public class player2Score : MonoBehaviour {
	public Player2 p;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		string s = p.totalScore.ToString ();
		GetComponent<TextMesh> ().text = s;
	
	}
}
