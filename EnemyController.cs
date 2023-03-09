using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public float flySpeed = 50.0f;

	// Update is called once per frame
	void Update () {

		transform.position += transform.forward * Time.deltaTime * flySpeed;
		//flySpeed -= transform.forward.y * Time.deltaTime * flySpeed;

//		if (flySpeed < 30.0f) {
//			flySpeed = 30.0f;
//		}

		//transform.Rotate (-Input.GetAxis ("Vertical"), 0.0f, -Input.GetAxis ("Horizontal"));

//		if (Input.GetKey (KeyCode.Q)) {
//			transform.Rotate (0, -rotSpeed, 0);
//		}
//
//		if (Input.GetKey (KeyCode.E)) {
//			transform.Rotate (0, rotSpeed, 0);
//		}
//
//		if (Input.GetKeyDown (KeyCode.B)) {
//			flySpeed = boostSpeed;
//		}
//		if (Input.GetKeyUp (KeyCode.B)) {
//			flySpeed = 50.0f;
//		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.name == "ArenaWalls") {
			Destroy (gameObject);
		}
	}
}

