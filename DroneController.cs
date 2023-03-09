using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DroneController : MonoBehaviour 
{
	public AudioClip shotFired;
	AudioSource audio;

	//attacks

	//Left and right attack launch spots
	public GameObject rightAttackSpot;
	public GameObject leftAttackSpot;

	//basic attack to be spawned
	public GameObject basicAttack;

	//heavy attack to be spawned
	//public GameObject heavyAttack;

	//Using gravity on the attacks so I will use the rigidbody to apply the force;
	public float attackSpeed = 110.0f;

	//movement things

	public int rotSpeed = 1;

	public float upSpeed = 0.5f;

	public float downSpeed = 0.75f;

	public float bankSpeed = 5.0f;

	public float flySpeed = 50.0f;

	public float boostSpeed = 100.0f;

	public bool isMoving = true;

	public bool boostOn = false;

	void Start ()
	{
		audio = GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () 
	{

		//cam follow script
		Vector3 moveCamTo = transform.position - transform.forward * 10.0f + Vector3.up * 15.0f;
		float bias = 0.96f;
		Camera.main.transform.position = Camera.main.transform.position * bias + moveCamTo * (1.0f - bias);
		Camera.main.transform.LookAt (transform.position + transform.forward * 10.0f);


		//always makes the player move forward
		transform.position += transform.forward * Time.deltaTime * flySpeed;

		//this will speep the player up if the go down and slow them if they go up
		flySpeed -= transform.forward.y * Time.deltaTime * flySpeed;

		//speed cap with out the boost
		if (boostOn == false && flySpeed >= 75.0f) 
		{
			flySpeed = 75.0f;
		}

		//speed cap with the boost
		if (boostOn == false && flySpeed == 100.0f) 
		{
			flySpeed = 100.0f;
		}

		//to cap the slowest speed
		if (isMoving == true && flySpeed < 30.0f) 
		{
			flySpeed = 30.0f;
		}

		//old movement controls
//		transform.Rotate (-Input.GetAxis ("Vertical"), 0.0f, -Input.GetAxis ("Horizontal"));

		//this is if gravity is on the player can float
//		if (Input.GetKey (KeyCode.X)) 
//		{
//			transform.GetComponent<Rigidbody> ().AddForce (Vector3.up * 5.0f);
//			Debug.Log ("I should be floating up???");
//		}

		//pitch up
		if (Input.GetKey (KeyCode.W)) 
		{
			transform.Rotate (-upSpeed, 0, 0);
		}

		//pitch down
		if (Input.GetKey (KeyCode.S)) 
		{
			transform.Rotate (downSpeed, 0, 0);
		}

		//bank left
		if (Input.GetKey (KeyCode.A)) 
		{
			transform.Rotate (0, 0, bankSpeed);
		}

		//bank right
		if (Input.GetKey (KeyCode.D)) 
		{
			transform.Rotate (0, 0, -bankSpeed);
		}

		//rotate left
		if (Input.GetKey (KeyCode.Q)) 
		{
			transform.Rotate (0, -rotSpeed, 0);
		}

		//rotate right
		if (Input.GetKey (KeyCode.E)) 
		{
			transform.Rotate (0, rotSpeed, 0);
		}

		//boost on
		if (Input.GetKeyDown (KeyCode.B)) 
		{
			boostOn = true;
			flySpeed = boostSpeed;
		}

		//boost off
		if (Input.GetKeyUp (KeyCode.B)) 
		{
			boostOn = false;
			flySpeed = 50.0f;
		}

		//stop moving
		if (Input.GetKey (KeyCode.Space)) 
		{
			isMoving = false;
			flySpeed = 0.0f;
		}

		//start moving again
		if (Input.GetKeyUp (KeyCode.Space)) 
		{
			isMoving = true;
			flySpeed = 30.0f;
		}

		//Attacks

		//basic attack
		if (Input.GetKeyDown (KeyCode.I)) 
		{
			Vector3 travleDir;

			audio.PlayOneShot (shotFired, 0.7f);

			GameObject bulletPrefabLeft;
			GameObject bulletPrefabRight;

			bulletPrefabLeft = Instantiate (basicAttack, leftAttackSpot.transform.position, leftAttackSpot.transform.rotation) as GameObject;
			bulletPrefabRight = Instantiate (basicAttack, rightAttackSpot.transform.position, rightAttackSpot.transform.rotation) as GameObject;

			bulletPrefabLeft.transform.position = transform.forward * attackSpeed * Time.deltaTime;
			bulletPrefabRight.transform.position = transform.forward * attackSpeed * Time.deltaTime;

			Rigidbody leftBulletRB;
			Rigidbody rightBulletRB;

			//leftBulletRB = bulletPrefabLeft.GetComponent<Rigidbody> ();
			//rightBulletRB = bulletPrefabRight.GetComponent<Rigidbody> ();	

			//adding the force to the bullet
			//leftBulletRB.AddForce (leftAttackSpot.transform.up * attackSpeed * -1 * Time.deltaTime);
			//rightBulletRB.AddForce (rightAttackSpot.transform.up * attackSpeed * -1 * Time.deltaTime);

			Destroy (bulletPrefabLeft, 1.5f);
			Destroy (bulletPrefabRight, 1.5f);
		}


		//terrain height sample
		float terrain = Terrain.activeTerrain.SampleHeight (transform.position);

		//This will be where the drone can hit the ground
		if (terrain > transform.position.y) 
		{
			transform.position = new Vector3 (transform.position.x, terrain, transform.position.z);
		}
	}

	//Player on trigger enters
	void OnTriggerEnter (Collider other)
	{
		//this is for testing
		if (other.name == "ArenaWalls") 
		{
			Destroy (gameObject);
		}

		//this is for the player hitting the water
		if (other.tag == "Water") 
		{
			Destroy (gameObject);
		}
	}
}
