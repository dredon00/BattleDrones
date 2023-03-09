using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour {

	public AudioClip shotFired;
	AudioSource audio;

	//attacks

	//Left and right attack launch spots
	public GameObject rightAttackSpot;
	public GameObject leftAttackSpot;

	//basic attack to be spawned
	public GameObject basicAttack;

	//Using gravity on the attacks so I will use the rigidbody to apply the force;
	public float attackSpeed = 110.0f;

	public float attackForce = 50.0f;

	//public Vector3 travelDir;

	void Start ()
	{
		audio = GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {
		//transform.Translate (travelDir * attackForce * Time.deltaTime);

		//basic attack
		if (Input.GetKeyDown (KeyCode.I)) 
		{
			Vector3 travleDir;

			audio.PlayOneShot (shotFired, 0.7f);

			GameObject bulletPrefabLeft;
			GameObject bulletPrefabRight;

			bulletPrefabLeft = Instantiate (basicAttack, leftAttackSpot.transform.position, leftAttackSpot.transform.rotation) as GameObject;
			bulletPrefabRight = Instantiate (basicAttack, rightAttackSpot.transform.position, rightAttackSpot.transform.rotation) as GameObject;

			//bulletPrefabLeft.GetComponent<BasicAttack> ().travelDir = gameObject.transform.forward;
			//bulletPrefabRight.GetComponent<BasicAttack> ().travelDir = gameObject.transform.forward;

			Rigidbody leftBulletRB;
			Rigidbody rightBulletRB;

			leftBulletRB = bulletPrefabLeft.GetComponent<Rigidbody> ();
			rightBulletRB = bulletPrefabRight.GetComponent<Rigidbody> ();	

			//adding the force to the bullet
			leftBulletRB.AddForce (leftAttackSpot.transform.forward * attackSpeed * -1 * Time.deltaTime);
			rightBulletRB.AddForce (rightAttackSpot.transform.forward * attackSpeed * -1 * Time.deltaTime);

			Destroy (bulletPrefabLeft, 1.5f);
			Destroy (bulletPrefabRight, 1.5f);
		}
	}
}
