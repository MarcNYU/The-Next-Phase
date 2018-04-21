using UnityEngine;
using System.Collections;

public class WallSound : MonoBehaviour {

	public AudioSource sfx;
	public AudioClip sfx_hit;
	public AudioClip sfx_slam;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col) {
		if (col.collider.tag == "Bullet") {
			sfx.PlayOneShot(sfx_hit);
		}
		if (col.collider.tag == "STBullet") {
			sfx.PlayOneShot(sfx_slam);
		}
	}
}
