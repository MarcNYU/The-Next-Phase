using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player1 : MonoBehaviour {
	public WeaponController1 weaponController;
	public GameObject weapon;
	Rigidbody rb;

	public float setDrag = .9f;
	public float setRight = 2.5f;
	public int setGunDamage = 1;
	public int setAxeDamage = 3;

	public GameObject opponet;
	public GameObject sheild;

	public Bullet bullet;
	public STBullet stBullet;
	public float maxHealth;
	public float currentHealth;
	public float walkSpeed;
	public float turnRight = 2.5f;
	public float gunDamage;
	public float axeDamage;
	public int totalScore = 0;
	public float t = 1f;
	public GameObject g;

	public bool dead = false;
	public bool gotItem = false;
	public bool sh = false;
	public bool st = false;
	private float shHP = 50f;

	private Bullet[] spread;
	private Vector3 s;

	public AudioSource sfx;
	public AudioClip sfx_shoot;

	public Image p;
	Vector2 relativePosition;

	public GameObject buff;
	public GameObject debuff;

	bool grounded = false;
	private bool wall = false;
	
	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody>();
	
	}

	// Update is called once per frame
	void Update () {
		//if the player's health is zero, deactivate the player
		//p.rectTransform.anchoredPosition = transform.TransformVector(gameObject.transform.position.x - 54.5f, gameObject.transform.position.z, 0f);
		//Debug.Log (p.rectTransform.position.x);
		//p.rectTransform.anchoredPosition.x.Equals (gameObject.transform.position.x - 53.5f);
		//p.rectTransform.anchoredPosition.y.Equals (gameObject.transform.position.z - 15.4f);

		Vector3 mapIndicatorPos = new Vector3((transform.localPosition.x+1f)/2.5f, (transform.localPosition.z-3.2f)/1.9f, 0);
		p.transform.localPosition = mapIndicatorPos * (100f/12.13f);

		Debug.Log (p.rectTransform.position.x);

		wall = false;
		if (currentHealth <= 0)
		{
			dead = true;
			p.gameObject.SetActive(false);
			gameObject.SetActive(false);
		}
		if (sh == true) {
			sheild.SetActive(true);
		}

		if (gameObject.tag == "Player1") {
			if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.S))
			{
				FixedUpdate();
			}
			if (Input.GetKey (KeyCode.A))
			{
				p.GetComponent<RectTransform> ().transform.RotateAround(p.rectTransform.position, p.rectTransform.forward, turnRight);
				transform.RotateAround(transform.position, transform.up, -turnRight);
				//transform.position -= transform.right * 8 * Time.deltaTime;
			}
			if (Input.GetKey (KeyCode.D))
			{
				p.GetComponent<RectTransform> ().transform.RotateAround(p.rectTransform.position, p.rectTransform.forward, -turnRight);
				transform.RotateAround(transform.position, transform.up, turnRight);
				//transform.position += transform.right * 8 * Time.deltaTime;
			}
		}

		if(Input.GetKeyDown(KeyCode.C)){ 
			if(weaponController.axeEquipped == false){
				weapon.SetActive(false);
				Shoot();
			}
			if(weaponController.axeEquipped == true){
				//Debug.Log ("act");
				if (t > 0f) {
					t -= Time.deltaTime;
					gameObject.GetComponent<Animator> ().SetBool("axeS", true);
				}
				if (t <= 0f) {
					gameObject.GetComponent<Animator> ().SetBool("axeS", false);
				}
				gameObject.GetComponent<Animator> ().SetBool("axeS", true);

			}
		}
		if (!(Input.GetKeyDown (KeyCode.C))) {
			gameObject.GetComponent<Animator> ().SetBool("axeS", false);
		}

		//if (Input.GetKeyDown (KeyCode.C) && weapCtrl.hasSword) {
		//	Swing();
		//}
	}

	void OnCollisionEnter(Collision col)
	{
		//decrease the health if the collider's tag tells us it's an 'enemy'. We set the tag in the inspector underneath the object name.
		if (col.collider.tag == "Enemy")
		{
			if (sh == false) {
				currentHealth -= 0.5f;
			}
			if (sh == true) {
				shHP -= 0.5f;
			}
			if (shHP <= 0f) {
				sheild.SetActive(false);
				sh = false;
				shHP = 50f;
			}
		}
		
	}
	void OnCollisionStay(Collision col)
	{
		if (col.collider.tag == "Ground")
		{
			grounded = true;
		}
		if (col.collider.tag == "Enemy")
		{
			if (sh == false) {
				currentHealth -= 0.5f;
			}
			if (sh == true) {
				shHP -= 0.5f;
			}
			if (shHP <= 0f) {
				sheild.SetActive(false);
			}
		}
	}
	
	void OnCollisionExit(Collision col)
	{
		if (col.collider.tag == "Ground")
		{
			grounded = false;
		}
	}
	

	void FixedUpdate()
	{

		if (Input.GetKey(KeyCode.W))
		{
			//p.GetComponent<RectTransform> ().transform.position += p.rectTransform.up * Time.deltaTime * walkSpeed;
			rb.AddForce(transform.forward * walkSpeed, ForceMode.Acceleration);
			//transform.position += transform.forward * walkSpeed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.S))
		{
			//p.GetComponent<RectTransform> ().transform.position -= p.rectTransform.up * Time.deltaTime * walkSpeed;
			rb.AddForce(-transform.forward * walkSpeed, ForceMode.Acceleration);
			//transform.position -= transform.forward * walkSpeed * Time.deltaTime;
		}
	}

	void Shoot(){
		//Instantiate a bullet and set it to a newBullet
		sfx.PlayOneShot(sfx_shoot);
		if (st == false) {
			Bullet newBullet = (Bullet)Instantiate (bullet, g.transform.position + transform.forward, Quaternion.identity);
			newBullet.direction = transform.forward;
		}
		if (st == true) {
			STBullet newBullet = (STBullet)Instantiate (stBullet, g.transform.position + transform.forward, Quaternion.identity);
			newBullet.direction = transform.forward;
		}
		//if (weapCtrl.hasGun) {
//			Bullet newBullet = (Bullet)Instantiate (bullet, transform.position + transform.forward, Quaternion.identity);
//			newBullet.direction = transform.forward;
		//}
		

	}


//	void Spread () {
//		//Instantiate a bullet and set it to a newBullet
//		Bullet newBullet1 =  (Bullet)Instantiate (bullet, transform.position + transform.forward, Quaternion.identity);
//		newBullet1.direction = transform.forward;
//		Bullet newBullet2 =  (Bullet)Instantiate (bullet, transform.position + transform.forward, Quaternion.identity);
//		newBullet2.direction = new Vector3 (-.2f, 0f, .2f);
//		Bullet newBullet3 =  (Bullet)Instantiate (bullet, transform.position + transform.forward, Quaternion.identity);
//		newBullet3.direction = new Vector3 (.2f, 0f, .2f);
//	}
}
