using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {
	private float t = 30.0f;
	private float pt = 10.0f;
	private string s;
	public GameObject l;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (t > 0.0f) {
			pt = 10.0f;
			t -= Time.deltaTime;
			s = t.ToString ("#.##");
			//l.transform.RotateAround(transform.position, transform.right, -0.05f);
		} else {
			GetComponent<TextMesh> ().color = Color.red;
			if (pt > 0.0f) {
				pt -= Time.deltaTime;
				s = pt.ToString ("#.##");
			}
			else {
				GetComponent<TextMesh> ().color = Color.black;
				t = 20.0f;
			}
		}

		if (t < 10.0f || pt < 10.0f) {
			s = "0" + s;
		}


		GetComponent<TextMesh> ().text = s;
		//Debug.Log (l.transform.rotation);
	}
}
