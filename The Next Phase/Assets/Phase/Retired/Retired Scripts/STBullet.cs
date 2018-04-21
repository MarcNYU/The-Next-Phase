using UnityEngine;
using System.Collections;

public class STBullet : MonoBehaviour {
	public Vector3 direction;
	public float speed;
	public int damage;
	
	// Use this for initialization
	void Start () {
		Destroy (gameObject, 5.0f);
	}
	
	// Update is called once per frame
	void Update () {
		//move in a direction fast
		transform.position += direction * Time.deltaTime * speed;
	}
	
	void OnCollisionEnter(Collision col){
		Debug.Log ("Bullet Hit");
		//get rid of enemies
		// check if its an enemy
		if (col.collider.tag == "Enemy") {
			//set enemy to false
			col.collider.gameObject.GetComponent<Enemies> ().HP -= 3;
			col.collider.gameObject.GetComponent<Enemies> ().Killed();
			Destroy(gameObject);
		}
		if (col.collider.tag == "Wall") {
			Destroy(gameObject);
		}
		
		
	}
}
