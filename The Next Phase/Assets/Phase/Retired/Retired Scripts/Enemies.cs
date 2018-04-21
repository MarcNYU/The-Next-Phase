using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemies : MonoBehaviour {

	Rigidbody rb;
	public Transform target;
	public Transform[] pt;
	public int wanderIndex;
	public string color;
	public GameManager gm;
	public Material mat;
	
	private float t = 60.0f;
	private float walkSpeed = 3.0f;
	private float runSpeed = 2.0f;
	private float shift = .1f;
	public int HP;
	public Image p;
	private float flash = .5f;
	//public GameObject core;

	public AudioSource sfx;
	public AudioClip sfx_grow;
	public AudioClip sfx_zoom;
	public AudioClip sfx_agro;
	public AudioClip sfx_blip;
	public AudioClip sfx_hit;

	private bool isHurt = false;

	
	// Use this for initialization
	void Start () {
		if (color == "blue")
		{
			mat.color = Color.red;
		}
		if (color == "yellow")
		{
			mat.color = Color.green;
		}
		if (color == "green")
		{
			mat.color = Color.cyan; 
		}
		if (color == "black")
		{
			sfx.PlayOneShot(sfx_blip);
		}
	}
	
	// Update is called once per frame
	void Update () {

//		Chasing (target);
		if (gm.grace == true) {
			gameObject.SetActive(false);
			Destroy(gameObject);
			p.gameObject.SetActive(false);
			Destroy(p);
		}

		switch (color) {
		case "red":
			Chasing (target);
			break;
		case "blue":
			Tank();
			Follow (target);
			break;
		case "yellow":
			Chasing (target);
//			Attack();
			break;
		case "green":
			Wandering ();
			break;
		case "black":
			Follow (target);
			break;
		}
		Vector3 mapIndicatorPos = new Vector3((transform.localPosition.x+.5f)/2.5f, (transform.localPosition.z-3.2f)/1.9f, 0);
		p.transform.localPosition = mapIndicatorPos * (100f/12.13f);
	}

	void Chasing (Transform t)
	{	
		transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation(t.position - transform.position), .2f);
		transform.position += transform.forward * runSpeed * Time.deltaTime;
	}

	void Follow (Transform t)
	{	
		transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation(t.position - transform.position), .2f);
		transform.position += transform.forward * 1.75f * Time.deltaTime;
	}
	
	void Wandering ()
	{

		if (HP == 7) {
			if (transform.position.x >= pt [wanderIndex].position.x && transform.position.z >= pt [wanderIndex].position.z) {
				wanderIndex = Random.Range (0, pt.Length);
				//Debug.Log (wanderIndex);
			}
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (pt [wanderIndex].position - transform.position), .2f);
			transform.position += transform.forward * walkSpeed * Time.deltaTime;
		} else {
			if (!isHurt) 
			{
				sfx.volume = 1f;
				sfx.PlayOneShot(sfx_agro);
				isHurt = true;
			}
			Chasing(target);
		}

	}

	void Tank (){
		if (HP < 10 && HP >= 8) {
			//mat.color = Color.Lerp (Color.red, Color.gray, .2f);
			transform.localScale = new Vector3(2f, 2f, 2f);
			if (!isHurt) 
			{ 
				sfx.volume = .3f;
				sfx.PlayOneShot(sfx_grow);
				isHurt = true;
			}
		}
		if (HP < 8 && HP >= 6) {
			//mat.color = Color.Lerp (Color.red, Color.gray, .4f); 
			transform.localScale = new Vector3(2.25f, 2.25f, 2.25f);
			isHurt = false;
		}
		if (HP < 6 && HP >= 4) {
			//mat.color = Color.Lerp (Color.red, Color.gray, .6f);
			transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
			if (!isHurt) 
			{
				sfx.volume = .3f;
				sfx.PlayOneShot(sfx_grow);
				isHurt = true;
			}
		}
		if (HP < 4 && HP >= 2) {
			//mat.color = Color.Lerp (Color.red, Color.gray, .8f);
			transform.localScale = new Vector3(2.75f, 2.75f, 2.75f);
			isHurt = false;
		}
		if (HP < 2 && HP >= 1) {
			//mat.color = Color.Lerp (Color.red, Color.gray, 1f);
			transform.localScale = new Vector3(3f, 3f, 3f);
			if (!isHurt) 
			{
				sfx.volume = .3f;
				sfx.PlayOneShot(sfx_grow);
				isHurt = true;
			}
		}
	}

	void OnTriggerEnter (Collider col) {
		if (color == "yellow") {
			if (target.tag == "Player1") {
				if (col.GetComponent<Collider>().tag == "Vision Cone1") {
					Attack(target);
					sfx.PlayOneShot(sfx_zoom);
				}
			}
			if (target.tag == "Player2") {
				if (col.GetComponent<Collider>().tag == "Vision Cone2") {
					Attack(target);
					sfx.PlayOneShot(sfx_zoom);
				}
			}
		}
	}

	void OnTriggerStay (Collider col) {
		if (color == "yellow") {
			if (target.tag == "Player1") {
				if (col.GetComponent<Collider>().tag == "Vision Cone1") {
					Attack(target);
				}
			}
			if (target.tag == "Player2") {
				if (col.GetComponent<Collider>().tag == "Vision Cone2") {
					Attack(target);
				}
			}
		}
	}

	void Attack (Transform t){
		transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation(t.position - transform.position), .2f);
		transform.position += transform.forward * 2.5f * Time.deltaTime;
	}

//	public void Killed (){
//		//set enemy to false
//		if (HP == 0){
//			gameObject.SetActive(false);
//			Destroy(gameObject);
//		}
//	}
	public void Killed (){
		shift += .1f;
		if (color != "red") {
			sfx.PlayOneShot (sfx_hit);
//			if (color == "blue") {
//				mat.color = Color.white;
//			}
//			if (color == "yellow") {
//				mat.color = Color.white;
//			}
//			if (color == "green") {
//				mat.color = Color.white;
//			}
		}
		if (HP <= 0){
			//p.SetActive(true);
			if (target.tag == "Player1")
			{
				if (color == "red")
				{
					sfx.PlayOneShot(sfx_blip);
					target.GetComponent<Player1> ().totalScore += 1;
				}
				if (color == "blue")
				{
					target.GetComponent<Player1> ().totalScore += 5;
				}
				if (color == "yellow")
				{
					sfx.PlayOneShot(sfx_blip);
					target.GetComponent<Player1> ().totalScore += 3;
				}
				if (color == "green")
				{
					sfx.PlayOneShot(sfx_agro);
					target.GetComponent<Player1> ().totalScore += 6;
				}
				if (color == "black")
				{
					target.GetComponent<Player1> ().totalScore += 3;
				}

			}
			
			if (target.tag == "Player2")
			{
				if (color == "red")
				{
					sfx.PlayOneShot(sfx_blip);
					target.GetComponent<Player2> ().totalScore += 1;
				}
				if (color == "blue")
				{
					target.GetComponent<Player2> ().totalScore += 5;
				}
				if (color == "yellow")
				{
					sfx.PlayOneShot(sfx_blip);
					target.GetComponent<Player2> ().totalScore += 3;
				}
				if (color == "green")
				{
					sfx.PlayOneShot(sfx_agro);
					target.GetComponent<Player2> ().totalScore += 6;
				}
				if (color == "black")
				{
					target.GetComponent<Player2> ().totalScore += 3;
				}
			}
			gameObject.SetActive(false);
			Destroy(gameObject);
			p.gameObject.SetActive(false);
			//Destroy(p);
		}
	}
}