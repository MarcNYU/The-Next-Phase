using UnityEngine;
using System.Collections;

public class Player1WeaponControl : MonoBehaviour {
	public GameObject[] player1Weapons;
	public int currentWeapon = 0;
	public GameObject sword;
	public bool hasGun = true;
	public bool hasSword = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (currentWeapon == 0) {
			if (Input.GetKeyDown (KeyCode.X)) {
				currentWeapon +=1;
				if (currentWeapon == 2){
					currentWeapon = 0;
				}
				ActivateWeapon(currentWeapon);
			}
		}

	}
	public void DeactivateAllWeaps(){
		//deactivate all cameras
		for (int i = 0; i < player1Weapons.Length; i++)
		{
			player1Weapons[i].SetActive(false);
		}
	}

	public void ActivateWeapon(int weapIndex){
		
		DeactivateAllWeaps();
		player1Weapons[weapIndex].SetActive(true);
		
	}
}
