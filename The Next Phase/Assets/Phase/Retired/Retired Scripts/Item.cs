using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	public WaveManager WM;

	public GameObject sheild1;
	public GameObject sheild2;

	public GameObject st1;
	public GameObject st2;

	public Bullet b1;
	public Bullet b2;

	public AudioSource sfx;
	public AudioClip sfx_pickUp;

	private int n;
	private int m;

	// Use this for initialization
	void Start () {
		n = Random.Range (0, 9);
		m = Random.Range (0, 9);
	}
	
	// Update is called once per frame
	void Update () {
		Start ();
	}

	void OnTriggerEnter (Collider col) {
		if (col.GetComponent<Collider>().tag == "Player1" || col.GetComponent<Collider>().tag == "Player2" ) {
			sfx.PlayOneShot(sfx_pickUp);
			if (gameObject.tag == "Health") {
				if (col.GetComponent<Collider>().tag == "Player1") {
					if (col.GetComponent<Collider>().GetComponent<Player1> ().currentHealth < 15f) {
						col.GetComponent<Collider>().GetComponent<Player1> ().currentHealth += 35;
					} else {
						col.GetComponent<Collider>().GetComponent<Player1> ().currentHealth += Random.Range (25, 35);
					}
					col.GetComponent<Player1>().gotItem = true;
					WM.items1[2].SetActive(false);
				}
				if (col.GetComponent<Collider>().tag == "Player2") {
					if (col.GetComponent<Collider>().GetComponent<Player2> ().currentHealth < 15f) {
						col.GetComponent<Collider>().GetComponent<Player2> ().currentHealth += 35;
					} else {
						col.GetComponent<Collider>().GetComponent<Player2> ().currentHealth += Random.Range (25, 35);
					}
					col.GetComponent<Player2>().gotItem = true;
					WM.items2[2].SetActive(false);
				}
			}
			if (gameObject.tag == "Buff") {
				Debug.Log ("Active");
				//Temporally Help Current Player

				// Increase Damage
				// Sheild limit
				// ????

				if (col.GetComponent<Collider>().tag == "Player1") {
					if (n <= 4) {
						col.GetComponent<Player1> ().sh = true;
					}
					if (n >= 5) {
						col.GetComponent<Player1> ().st = true;
					}
					col.GetComponent<Player1>().gotItem = true;
					WM.items1[2].SetActive(false);
				}
				if (col.GetComponent<Collider>().tag == "Player2") {
					if (m <= 4) {
						col.GetComponent<Player2> ().sh = true;
					}
					if (m >= 5) {
						col.GetComponent<Player2> ().st = true;
					}
					col.GetComponent<Player2>().gotItem = true;
					WM.items2[2].SetActive(false);
				}
			}
			if (gameObject.tag == "Debuff") {
				Debug.Log ("Active");
				//Temporally Hinder Opponet

				// Slow Down
				// Invert Control
				// Increase Damage

				if (col.GetComponent<Collider>().tag == "Player1") {
					col.GetComponent<Player1>().gotItem = true;
					st2.SetActive(true);
					if (n <= 4) {
						col.GetComponent<Player1>().opponet.GetComponent<Player2>().turnRight = -2.5f;
					}

					if (n >= 5) {
						col.GetComponent<Player1>().opponet.GetComponent<Player2>().walkSpeed = 8f;
					}

					WM.items1[0].SetActive(false);
					WM.items1[1].SetActive(false);
				}
				if (col.GetComponent<Collider>().tag == "Player2") {
					col.GetComponent<Player2>().gotItem = true;
					st1.SetActive(true);
					if (m <= 4) {
						col.GetComponent<Player2>().opponet.GetComponent<Player1>().turnRight = -2.5f;
					}

					if (m >= 5) {
						col.GetComponent<Player2>().opponet.GetComponent<Player1>().walkSpeed = 8f;
					}

					WM.items2[0].SetActive(false);
					WM.items2[1].SetActive(false);
				}
			}
			gameObject.SetActive (false);
		}
	}
}
