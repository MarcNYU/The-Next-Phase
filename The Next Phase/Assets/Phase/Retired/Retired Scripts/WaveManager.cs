using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour {

	public Player1 player1;
	public Player2 player2;

	public GameObject[] items1;
	public GameObject[] items2;

	public Transform[] resetPoints1;
	public Transform[] resetPoints2;

	public GameObject sp1;
	public GameObject sp2;

	public GameManager GM;

	public GameObject t1;
	public GameObject t2;

	private int n;
	private int m;


	// Use this for initialization
	void Start () {
		n = Random.Range (0, 9);
		m = Random.Range (0, 9);
	}
	
	// Update is called once per frame
	void Update () {
		if (GM.grace == true) {
			if (player1.dead == false) {
				if (GM.prepTimer > 9f) {
					sp1.SetActive (true);
					player1.gameObject.GetComponent<Transform> ().position = resetPoints1 [0].position;
					player1.transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation(t1.transform.position - transform.position), 0f);
				}
				if (player1.gotItem == false) {
					if (player1.currentHealth < player1.maxHealth/2f) {
						items1[1].SetActive(true);
					} else {
						if (n <= 4){
							items1[0].SetActive(true);
						}
						if (n >= 5){
							items1[1].SetActive(true);
						}
						//items1[0].SetActive(true);
					}
					items1[2].SetActive(true);
				}
			} 
			if (player2.dead == false) {
				if (GM.prepTimer > 8f) {
					sp2.SetActive (true);
					player2.gameObject.GetComponent<Transform> ().position = resetPoints2 [0].position;
					player2.transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation(t2.transform.position - transform.position), 0f);
				}
				if (player2.gotItem == false) {
					if (player2.currentHealth < player2.maxHealth/2f) {
						items2[1].SetActive(true);
					} else {
						if (m <= 4){
							items2[0].SetActive(true);
						}
						if (m >= 5){
							items2[1].SetActive(true);
						}
					}
					items2[2].SetActive(true);
				}
			} 
		} else {
			sp1.SetActive(false);
			sp2.SetActive(false);

			items1[0].SetActive(false);
			items1[1].SetActive(false);
			items1[2].SetActive(false);

			items2[0].SetActive(false);
			items2[1].SetActive(false);
			items2[2].SetActive(false);
			Start();
		}
	}
}
