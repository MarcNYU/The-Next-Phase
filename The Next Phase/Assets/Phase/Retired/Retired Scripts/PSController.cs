using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSController : MonoBehaviour {

	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	private Vector3 moveDirection = Vector3.zero;

//	private Rigidbody rb;
//
//	void Start () {
//
//		rb = GetComponent<Rigidbody>();
//
//	}

	void Update() {
		CharacterController controller = GetComponent<CharacterController>();
		if (controller.isGrounded) {
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, -Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;
			if (Input.GetButton("Jump"))
				moveDirection.y = jumpSpeed;

		}
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
		//transform.RotateAround(transform.position, transform.up, Input.GetAxis("HorizontalR"));
	}
}


/*
	private Rigidbody rb;
	public float moveSpeed;
	public float jumpStrength;
	public float rotateSpeed;

	public Bullet bullet;
	public AudioSource sfx;
	public AudioClip sfx_shoot;
	public GameObject g;

	public bool grounded = false;

	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody>();

	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.A))
		{
			transform.RotateAround(transform.position, transform.up, -5);
		}
		if (Input.GetKey (KeyCode.D))
		{
			transform.RotateAround(transform.position, transform.up, 5);
		}
		if (Input.GetKeyDown (KeyCode.Joystick1Button5)) 
		{
			Shoot();
		}

	}

	void Shoot(){
		//Instantiate a bullet and set it to a newBullet
		sfx.PlayOneShot(sfx_shoot);
		Bullet newBullet = (Bullet)Instantiate (bullet, g.transform.position + transform.forward, Quaternion.identity);
		newBullet.direction = transform.forward;
	}

	void OnCollisionStay(Collision col)
	{
		if (col.collider.tag == "Ground")
		{
			grounded = true;
		}
	}

	void OnCollisionExit(Collision col)
	{
		if (col.collider.tag == "Ground")
		{
			grounded = false;
		}
	}

	//FixedUpdate is called once per physics step
	void FixedUpdate()
	{
		if (Input.GetKeyDown(KeyCode.Joystick1Button4))
		{
			if (grounded)
			{
				rb.AddForce(transform.up * jumpStrength, ForceMode.VelocityChange);
			}
		}
		if (Input.GetAxis())
		{
			rb.AddForce(transform.forward * moveSpeed);
		}
		if (Input.GetKey(KeyCode.S) || (Input.GetKey(KeyCode.DownArrow)))
		{
			rb.AddForce(-transform.forward * moveSpeed);
		}

		if (Input.GetKey(KeyCode.A) || (Input.GetKey(KeyCode.LeftArrow)))
		{
			rb.AddForce(-transform.right * moveSpeed);
		}
		if (Input.GetKey(KeyCode.D) || (Input.GetKey(KeyCode.RightArrow)))
		{
			rb.AddForce(transform.right * moveSpeed);
		}


	}

}
*/