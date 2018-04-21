using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public AudioSource bgm;
	public AudioClip bgm_background;
	
	public Player1 player1;
	public Player2 player2;

	public Camera p1;
	public Camera p2;

	public Camera p1W;
	public Camera p2W;

	public Enemies redEnemy1;
	public Enemies redEnemy2;

	public Enemies blueEnemy1;
	public Enemies blueEnemy2;

	public Enemies yellowEnemy1;
	public Enemies yellowEnemy2;

	public Enemies greenEnemy1;
	public Enemies greenEnemy2;

	public Enemies[] phantoms;

	public Material red;
	public Material cyan;
	public Material green;

//	public Enemies Boss1;
//	public Enemies Boss2;
//	public Enemies Boss3;
//	public Enemies Boss4;

	public int wave;

	public Transform[] spawnPoints1;
	public Transform[] spawnPoints2;

	public Text timer;
	public Slider healthBar;
	public Slider healthBar2;
	public Text status1;
	public Text status2;

	public Text score1;
	public Text score2;
	
	public Transform[] w1;
	public Transform[] w2;
	//public GameObjects[] enemyArray;
	public bool grace = false;

	private GameObject[] enemyWave1;
	private GameObject[] enemyWave2;
	private float matchTimer = 30.0f;
	public float prepTimer = 5.0f;

	private bool gameOver;
	private bool restart;

	private int n = 0;
	private int m = 0;
	private int matches = 1;

	private float t = 30.0f;
	private float pt = 10.0f;
	private bool newW;

	public GameObject newParent;
	public Image redIcon;
	public Image blueIcon;
	public Image yellowIcon;
	public Image greenIcon;
	public Image whiteIcon;

	public GameObject t1;
	public GameObject t2;
	public GameObject mainT;
	public GameObject back;

	public GameObject st1;
	public GameObject st2;

	private Image tracker;

	public AudioSource sfx;
	public AudioClip sfx_grow;
	public AudioClip sfx_zoom;
	public AudioClip sfx_agro;
	public AudioClip sfx_blip;
	public AudioClip sfx_hit;
	public AudioClip sfx_error;
	

	// Use this for initialization

	void Start () {

		gameOver = false;
		restart = false;

		healthBar.maxValue = player1.maxHealth;
		healthBar2.maxValue = player2.maxHealth;

		SpawnEnemy();

	}
	
	// Update is called once per frame
	void Update () {
		int temp;
		healthBar.value = player1.currentHealth;
		healthBar2.value = player2.currentHealth;

		bgm.clip = bgm_background;
		if(!bgm.isPlaying)
		{
			bgm.Play ();
		}

		string s1 = player1.currentHealth.ToString ("#");
		if (player1.currentHealth <= 0f) {
			s1 = "0";
		}
		status1.text = s1;

		string s2 = player2.currentHealth.ToString ("#");
		if (player2.currentHealth <= 0f) {
			s2 = "0";
		}
		status2.text = s2;

		string s11 = player1.totalScore.ToString ("#");
		score1.text = "Score: " + s11;
		
		string s22 = player2.totalScore.ToString ("#");
		score2.text = s22 + " :Score";

		if (restart)
		{
			if (Input.GetKeyDown (KeyCode.R))
			{
				//Debug.Log ("Reset");
				Application.LoadLevel (Application.loadedLevel);
			}
		}

		if (matchTimer > 0.0f) {
			bgm.volume = .6f;
			prepTimer = 10.0f;
			matchTimer -= Time.deltaTime;
			timer.text = matchTimer.ToString ("#.##");
		} else {
			if (prepTimer > 0.0f) {
				timer.color = Color.white;
				prepTimer -= Time.deltaTime;
				timer.text = prepTimer.ToString ("#.##");
				//wave = 1;
				temp = wave;
				wave = Random.Range (1, 11);
//				if (temp == wave) {
//					wave = (wave + temp) % 8;
//				}
				CancelInvoke();
				Start();
			}
			else {
				timer.color = Color.red;
				matchTimer = 30.0f;

//				if (player1.dead == true) {
//					player1.dead = false;
//					player1.currentHealth = 100f;
//					player1.gameObject.SetActive(true);
//					player1.p.gameObject.SetActive(true);
//				}
//				if (player2.dead == true) {
//					player2.dead = false;
//					player2.currentHealth = 100f;
//					player2.gameObject.SetActive(true);
//					player2.p.gameObject.SetActive(true);
//				}
			}
		}
		if (matchTimer < 10.0f || matchTimer < 10.0f) {
			timer.text = "0" + timer.text;
		}
		if (matchTimer <= 0f ) {
			bgm.volume = .3f;
			t1.SetActive(false);
			t2.SetActive(false);
			grace = true;
		}
		if (prepTimer <= 0f) {
			grace = false;
			player1.gotItem = false;
			player2.gotItem = false;
		}
		if (matchTimer <= 1f && matchTimer > 0f) {
			newW = true;
			matches += 1;
			//Debug.Log (matches);
		} else {
			newW = false;
		}
		if (matchTimer <= 1.5f && matchTimer >= 0f) {
			player1.sheild.SetActive(false);
			player2.sheild.SetActive(false);
			player1.sh = false;
			player2.sh = false;
			player1.st = false;
			player2.st = false;
			player1.turnRight = player1.setRight;
			player2.turnRight = player2.setRight;
			player1.walkSpeed = 10f;
			player2.walkSpeed = 10f;
			st1.SetActive(false);
			st2.SetActive(false);
		}
		if (matchTimer <= 5f) {
			t1.SetActive(true);
			t2.SetActive(true);
			t1.GetComponent<Text> ().text = matchTimer.ToString("#");
			t2.GetComponent<Text> ().text = matchTimer.ToString("#");
		}
		if (player1.dead == true && player2.dead == true) {
			t1.SetActive(false);
			t2.SetActive(false);
			mainT.SetActive(false);
			back.SetActive(false);
		}
		if (grace == true) {
			if (player1.dead == true) {
				player2.dead = true;
				player2.gameObject.SetActive(false);
			}
			if (player2.dead == true) {
				player1.dead = true;
				player1.gameObject.SetActive(false);
			}
		}

	}

	void SpawnRed() {
		int spawnPointIndex1 = Random.Range (0, spawnPoints1.Length);
		int spawnPointIndex2 = Random.Range (0, spawnPoints2.Length);
		if (player1.dead == false) {

			Image thisImage = (Image)Instantiate(redIcon, new Vector3(spawnPoints1[spawnPointIndex1].localPosition.x, spawnPoints1[spawnPointIndex1].localPosition.z), Quaternion.identity);
			thisImage.transform.SetParent(newParent.transform);

			tracker = thisImage;

			redEnemy1.sfx = sfx;
			redEnemy1.sfx_hit = sfx_hit;
			redEnemy1.sfx_blip = sfx_blip;
			redEnemy1.gm = this;
			redEnemy1.p = tracker;
			redEnemy1.target = player1.transform;
			Instantiate (redEnemy1, spawnPoints1[spawnPointIndex1].position, spawnPoints1[spawnPointIndex1].rotation);
		}
		if (player2.dead == false) {

			Image thisImage = (Image)Instantiate(redIcon, new Vector3(spawnPoints2[spawnPointIndex2].localPosition.x, spawnPoints2[spawnPointIndex2].localPosition.z), Quaternion.identity);
			thisImage.transform.SetParent(newParent.transform);

			tracker = thisImage;

			redEnemy2.sfx = sfx;
			redEnemy2.sfx_hit = sfx_hit;
			redEnemy2.sfx_blip = sfx_blip;
			redEnemy2.gm = this;
			redEnemy2.p = tracker;
			redEnemy2.target = player2.transform;
			Instantiate (redEnemy2, spawnPoints2[spawnPointIndex2].position, spawnPoints2[spawnPointIndex2].rotation);
		}

	}

	void SpawnBlue() {
		int spawnPointIndex1 = Random.Range (0, spawnPoints1.Length);
		int spawnPointIndex2 = Random.Range (0, spawnPoints2.Length);
		if (player1.dead == false) {
			Image thisImage = (Image)Instantiate(blueIcon, new Vector3(spawnPoints1[spawnPointIndex1].localPosition.x, spawnPoints1[spawnPointIndex1].localPosition.z), Quaternion.identity);
			thisImage.transform.SetParent(newParent.transform);
			
			tracker = thisImage;

			blueEnemy1.mat = red;
			blueEnemy1.sfx = sfx;
			blueEnemy1.sfx_hit = sfx_hit;
			blueEnemy1.sfx_grow = sfx_grow;
			blueEnemy1.gm = this;
			blueEnemy1.p = tracker;
			blueEnemy1.target = player1.transform;
			Instantiate (blueEnemy1, spawnPoints1[spawnPointIndex1].position, spawnPoints1[spawnPointIndex1].rotation);
		}
		if (player2.dead == false) {
			Image thisImage = (Image)Instantiate(blueIcon, new Vector3(spawnPoints2[spawnPointIndex2].localPosition.x, spawnPoints2[spawnPointIndex2].localPosition.z), Quaternion.identity);
			thisImage.transform.SetParent(newParent.transform);
			
			tracker = thisImage;

			blueEnemy2.mat = red;
			blueEnemy2.sfx = sfx;
			blueEnemy2.sfx_hit = sfx_hit;
			blueEnemy2.sfx_grow = sfx_grow;
			blueEnemy2.gm = this;
			blueEnemy2.p = tracker;
			blueEnemy2.target = player2.transform;
			Instantiate (blueEnemy2, spawnPoints2[spawnPointIndex2].position, spawnPoints2[spawnPointIndex2].rotation);
		}
	}

	void SpawnYellow() {
		int spawnPointIndex1 = Random.Range (0, spawnPoints1.Length);
		int spawnPointIndex2 = Random.Range (0, spawnPoints2.Length);
		if (player1.dead == false) {
			Image thisImage = (Image)Instantiate(yellowIcon, new Vector3(spawnPoints1[spawnPointIndex1].localPosition.x, spawnPoints1[spawnPointIndex1].localPosition.z), Quaternion.identity);
			thisImage.transform.SetParent(newParent.transform);
			
			tracker = thisImage;

			yellowEnemy1.mat = green;
			yellowEnemy1.sfx = sfx;
			yellowEnemy1.sfx_hit = sfx_hit;
			yellowEnemy1.sfx_zoom = sfx_zoom;
			yellowEnemy1.sfx_blip = sfx_blip;
			yellowEnemy1.gm = this;
			yellowEnemy1.p = tracker;
			yellowEnemy1.target = player1.transform;
			Instantiate (yellowEnemy1, spawnPoints1[spawnPointIndex1].position, spawnPoints1[spawnPointIndex1].rotation);
		}
		if (player2.dead == false) {
			Image thisImage = (Image)Instantiate(yellowIcon, new Vector3(spawnPoints2[spawnPointIndex2].localPosition.x, spawnPoints2[spawnPointIndex2].localPosition.z), Quaternion.identity);
			thisImage.transform.SetParent(newParent.transform);
			
			tracker = thisImage;

			yellowEnemy2.mat = green;
			yellowEnemy2.sfx = sfx;
			yellowEnemy2.sfx_hit = sfx_hit;
			yellowEnemy2.sfx_zoom = sfx_zoom;
			yellowEnemy1.sfx_blip = sfx_blip;
			yellowEnemy2.gm = this;
			yellowEnemy2.p = tracker;
			yellowEnemy2.target = player2.transform;
			Instantiate (yellowEnemy2, spawnPoints2[spawnPointIndex2].position, spawnPoints2[spawnPointIndex2].rotation);
		}
	}

	void SpawnGreen() {
		int spawnPointIndex1 = Random.Range (0, spawnPoints1.Length);
		int spawnPointIndex2 = Random.Range (0, spawnPoints2.Length);
		if (player1.dead == false) {
			Image thisImage = (Image)Instantiate(greenIcon, new Vector3(spawnPoints1[spawnPointIndex1].localPosition.x, spawnPoints1[spawnPointIndex1].localPosition.z), Quaternion.identity);
			thisImage.transform.SetParent(newParent.transform);
			
			tracker = thisImage;

			greenEnemy1.mat = cyan;
			greenEnemy1.sfx = sfx;
			greenEnemy1.sfx_hit = sfx_hit;
			greenEnemy1.sfx_agro = sfx_agro;
			greenEnemy1.gm = this;
			greenEnemy1.p = tracker;
			greenEnemy1.pt = w1;
			greenEnemy1.wanderIndex = Random.Range (0,6);
			greenEnemy1.target = player1.transform;
			Instantiate (greenEnemy1, spawnPoints1[spawnPointIndex1].position, spawnPoints1[spawnPointIndex1].rotation);
		}
		if (player2.dead == false) {
			Image thisImage = (Image)Instantiate(greenIcon, new Vector3(spawnPoints2[spawnPointIndex2].localPosition.x, spawnPoints2[spawnPointIndex2].localPosition.z), Quaternion.identity);
			thisImage.transform.SetParent(newParent.transform);
			
			tracker = thisImage;

			greenEnemy2.mat = cyan;
			greenEnemy2.sfx = sfx;
			greenEnemy2.sfx_hit = sfx_hit;
			greenEnemy2.sfx_agro = sfx_agro;
			greenEnemy2.gm = this;
			greenEnemy2.p = tracker;
			greenEnemy2.pt = w2;
			greenEnemy2.wanderIndex = Random.Range (0,6);
			greenEnemy2.target = player2.transform;
			Instantiate (greenEnemy2, spawnPoints2[spawnPointIndex2].position, spawnPoints2[spawnPointIndex2].rotation);
		}
	}

	void SpawnP() {
		int phIndex = Random.Range (0, 3);
		if (player1.dead == false) {
			if (player2.totalScore > player1.totalScore) {
				int spawnPointIndex1 = Random.Range (0, spawnPoints1.Length);

				Image thisImage = (Image)Instantiate(whiteIcon, new Vector3(spawnPoints1[spawnPointIndex1].localPosition.x, spawnPoints1[spawnPointIndex1].localPosition.z), Quaternion.identity);
				thisImage.transform.SetParent(newParent.transform);
				
				tracker = thisImage;
				
				phantoms[phIndex].p = tracker;
				
				phantoms[phIndex].sfx = sfx;
				phantoms[phIndex].sfx_blip = sfx_error;
				phantoms[phIndex].gm = this;
				phantoms[phIndex].target = player1.transform;
				Instantiate (phantoms[phIndex], spawnPoints1[spawnPointIndex1].position, spawnPoints1[spawnPointIndex1].rotation);
			}
		}

		if (player2.dead == false) {
			if (player1.totalScore > player2.totalScore) {
				int spawnPointIndex2 = Random.Range (0, spawnPoints2.Length);

				Image thisImage = (Image)Instantiate(whiteIcon, new Vector3(spawnPoints2[spawnPointIndex2].localPosition.x, spawnPoints2[spawnPointIndex2].localPosition.z), Quaternion.identity);
				thisImage.transform.SetParent(newParent.transform);

				tracker = thisImage;

				phantoms[phIndex].p = tracker;
				phantoms[phIndex].sfx = sfx;
				phantoms[phIndex].sfx_blip = sfx_error;
				phantoms[phIndex].gm = this;
				phantoms[phIndex].target = player2.transform;
				Instantiate (phantoms[phIndex], spawnPoints2[spawnPointIndex2].position, spawnPoints2[spawnPointIndex2].rotation);
			}
		}

		if (player1.dead == false && player2.dead == true) {
			int spawnPointIndex1 = Random.Range (0, spawnPoints1.Length);
			
			Image thisImage = (Image)Instantiate(whiteIcon, new Vector3(spawnPoints1[spawnPointIndex1].localPosition.x, spawnPoints1[spawnPointIndex1].localPosition.z), Quaternion.identity);
			thisImage.transform.SetParent(newParent.transform);
			
			tracker = thisImage;
			
			phantoms[phIndex].p = tracker;
			
			phantoms[phIndex].sfx = sfx;
			phantoms[phIndex].sfx_blip = sfx_error;
			phantoms[phIndex].gm = this;
			phantoms[phIndex].target = player1.transform;
			Instantiate (phantoms[phIndex], spawnPoints1[spawnPointIndex1].position, spawnPoints1[spawnPointIndex1].rotation);
		}

		if (player1.dead == true && player2.dead == false) {
			int spawnPointIndex2 = Random.Range (0, spawnPoints2.Length);
			
			Image thisImage = (Image)Instantiate(whiteIcon, new Vector3(spawnPoints2[spawnPointIndex2].localPosition.x, spawnPoints2[spawnPointIndex2].localPosition.z), Quaternion.identity);
			thisImage.transform.SetParent(newParent.transform);
			
			tracker = thisImage;
			
			phantoms[phIndex].p = tracker;
			phantoms[phIndex].sfx = sfx;
			phantoms[phIndex].sfx_blip = sfx_error;
			phantoms[phIndex].gm = this;
			phantoms[phIndex].target = player2.transform;
			Instantiate (phantoms[phIndex], spawnPoints2[spawnPointIndex2].position, spawnPoints2[spawnPointIndex2].rotation);
		}
		if (player1.dead == false && player2.dead == false) {
			if (player1.totalScore == player2.totalScore) {
				int spawnPointIndex1 = Random.Range (0, spawnPoints1.Length);
				
				Image thisImage = (Image)Instantiate(whiteIcon, new Vector3(spawnPoints1[spawnPointIndex1].localPosition.x, spawnPoints1[spawnPointIndex1].localPosition.z), Quaternion.identity);
				thisImage.transform.SetParent(newParent.transform);
				
				tracker = thisImage;
				
				phantoms[phIndex].p = tracker;
				
				phantoms[phIndex].sfx = sfx;
				phantoms[phIndex].sfx_blip = sfx_error;
				phantoms[phIndex].gm = this;
				phantoms[phIndex].target = player1.transform;
				Instantiate (phantoms[phIndex], spawnPoints1[spawnPointIndex1].position, spawnPoints1[spawnPointIndex1].rotation);

				int spawnPointIndex2 = Random.Range (0, spawnPoints2.Length);
				
				Image thisOtherImage = (Image)Instantiate(whiteIcon, new Vector3(spawnPoints2[spawnPointIndex2].localPosition.x, spawnPoints2[spawnPointIndex2].localPosition.z), Quaternion.identity);
				thisImage.transform.SetParent(newParent.transform);
				
				tracker = thisOtherImage;
				
				phantoms[phIndex].p = tracker;
				phantoms[phIndex].sfx = sfx;
				phantoms[phIndex].sfx_blip = sfx_error;
				phantoms[phIndex].gm = this;
				phantoms[phIndex].target = player2.transform;
				Instantiate (phantoms[phIndex], spawnPoints2[spawnPointIndex2].position, spawnPoints2[spawnPointIndex2].rotation);
			}
		}
	}

	void SpawnEnemy()
	{
		switch (wave) {
		case 0:
			InvokeRepeating("SpawnRed", 1.5f, 1.5f);
			break;
		case 1:
			InvokeRepeating("SpawnBlue", 2.25f, 2.25f);

			InvokeRepeating("SpawnP", 3f, 3f);
			break;
		case 2:
			InvokeRepeating("SpawnRed", 1.5f, 1.5f);
			InvokeRepeating("SpawnBlue", 2.5f, 2.5f);

			InvokeRepeating("SpawnP", 3f, 3f);
			break;
		case 3:
			InvokeRepeating("SpawnYellow", 2.25f, 2.25f);

			InvokeRepeating("SpawnP", 3f, 3f);
			break;
		case 4:
			InvokeRepeating("SpawnRed", 1.5f, 1.5f);
			InvokeRepeating("SpawnYellow", 2.5f, 2.5f);

			InvokeRepeating("SpawnP", 3f, 3f);
			break;
		case 5:
			InvokeRepeating("SpawnRed", 2f, 2f);
			InvokeRepeating("SpawnGreen", 1.75f, 1.75f);

			InvokeRepeating("SpawnP", 3f, 3f);
			break;
		case 6:
			InvokeRepeating("SpawnBlue", 2.25f, 2.25f);
			InvokeRepeating("SpawnGreen", 1.75f, 1.75f);

			InvokeRepeating("SpawnP", 3f, 3f);
			break;
		case 7:
			InvokeRepeating("SpawnYellow", 2.25f, 2.25f);
			InvokeRepeating("SpawnGreen", 1.75f, 1.75f);

			InvokeRepeating("SpawnP", 3f, 3f);
			break;
		case 8:
			InvokeRepeating("SpawnGreen", 2.25f, 2.25f);

			InvokeRepeating("SpawnP", 3f, 3f);
			break;
		case 9:
			InvokeRepeating("SpawnYellow", 2.75f, 2.75f);
			InvokeRepeating("SpawnBlue", 3f, 3f);
			
			InvokeRepeating("SpawnP", 3f, 3f);
			break;
		case 10:
			InvokeRepeating("SpawnP", 2f, 2f);
			break;
		case 11:
			InvokeRepeating("SpawnRed", 1.5f, 1.5f);
			
			InvokeRepeating("SpawnP", 2f, 2f);
			break;
		}
	}

}
