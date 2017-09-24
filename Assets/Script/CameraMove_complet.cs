using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove_complet : MonoBehaviour {

	/*private Transform lookAt;
	private Vector3 startOffset;
	private Vector3 moveVector;
	/*
	//Animation plongeante en debut de course
	private float transition = 0.0f;
	private float animationDuration = 3.0f;
	private Vector3 animationOffset = new Vector3 (0, 5, 4);

	private int  rotate_camera_y = 0;*/



	private float xVal = 0.0f;
	private float yVal = 0.0f;
	private float zVal = 5.0f;

	private int rotateY = 0;
	private int nombre_rotation_gauche = 0 ;
	private int nombre_rotation_droite = 0 ;

	private bool dead = false;

	/*private float strenght = 0.5f;
	private string camera = GetComponent<Transform>();*/
	// Use this for initialization
	void Start () 
	{
		/*lookAt = GameObject.FindGameObjectWithTag ("Player").transform;
		startOffset = transform.position - lookAt.position;*/

		//zVal = GameObject.Find ("Player").GetComponent<PlayerMotor>().speed;

		//Debug.Log (startOffset);
		//Debug.Log(GetComponent<Transform>()); 
	}

	// Update is called once per frame
	void Update () 
	{
		//dead = GameObject.Find ("Player").GetComponent<PlayerMotor>().isDead;
		//Debug.Log ("dead : " + dead);
		if (dead)
			return;

		if (nombre_rotation_gauche > 3) {
			nombre_rotation_gauche = 0;
		}
		if (nombre_rotation_droite > 3) {
			nombre_rotation_droite = 0;
		}
		/*moveVector = lookAt.position + startOffset;/*
		moveVector.x = 0;
		moveVector.y = Mathf.Clamp (moveVector.y, 3, 5);
		if (transition > 1.0f) 
		{
			
			Debug.Log (moveVector);
			if (Input.GetButtonDown ("Fire1")) 
			{
				
			}
			transform.position = moveVector;
		} // Debut du jeu
		else 
		{
			transform.position = Vector3.Lerp (moveVector + animationOffset, moveVector, transition);
			transition += Time.deltaTime * 1 / animationDuration;
			//Rotation de la caméra
			transform.LookAt (lookAt.position + Vector3.up);
		}

		*/

		if (Input.GetButtonDown ("Fire2")) {
			rotateY += 90;
			switch (nombre_rotation_droite) 
			{
			case 0:
				xVal = (GameObject.Find ("Player").GetComponent<PlayerMotor> ().speed);
				zVal = 0;
				break;
			case 1: 
				xVal = 0;
				zVal = -(GameObject.Find ("Player").GetComponent<PlayerMotor> ().speed);
				break;
			case 2: 
				xVal = -(GameObject.Find ("Player").GetComponent<PlayerMotor> ().speed);
				zVal = 0;
				break;
			case 3: 
				xVal = 0;
				zVal = (GameObject.Find ("Player").GetComponent<PlayerMotor> ().speed);
				break;
			default:break;
			}
			//xVal = (GameObject.Find ("Player").GetComponent<PlayerMotor>().speed);
			GetComponent<Rigidbody> ().angularVelocity = new Vector3 (0, 2, 0);
			StartCoroutine (stopRotation(rotateY));
			//zVal = 0;
			//Debug.Log ("okok : " + moveVector.x);
			//Debug.Log ("position z : " + transform.position.z);
			changementPosition (nombre_rotation_droite);
			nombre_rotation_droite++;
			if (nombre_rotation_gauche > 0) {
				nombre_rotation_gauche--;
			}

		}

		if (Input.GetButtonDown ("Fire1")) {
			rotateY -= 90;
			switch (nombre_rotation_gauche) 
			{
			case 0:
				xVal = -(GameObject.Find ("Player").GetComponent<PlayerMotor> ().speed);
				zVal = 0;
				break;
			case 1: 
				xVal = 0;
				zVal = (GameObject.Find ("Player").GetComponent<PlayerMotor> ().speed);
				break;
			case 2: 
				xVal = (GameObject.Find ("Player").GetComponent<PlayerMotor> ().speed);
				zVal = 0;
				break;
			case 3: 
				xVal = 0;
				zVal = -(GameObject.Find ("Player").GetComponent<PlayerMotor> ().speed);
				break;
			default:break;
			}
			//xVal = (GameObject.Find ("Player").GetComponent<PlayerMotor>().speed);
			GetComponent<Rigidbody> ().angularVelocity = new Vector3 (0, 2, 0);
			StartCoroutine (stopRotation(rotateY));
			//zVal = 0;
			//Debug.Log ("okok : " + moveVector.x);
			//Debug.Log ("position z : " + transform.position.z);
			changementPosition (nombre_rotation_gauche);
			nombre_rotation_gauche++;
			if (nombre_rotation_droite > 0) {
				nombre_rotation_droite--;
			}
			//nombre_rotation_droite--;

		}

		//zVal = (GameObject.FindGameObjectWithTag ("Player").transform.position.z) - (4.5f) * (GameObject.Find ("Player").GetComponent<PlayerMotor>().speed);
		//zVal = GameObject.Find ("Player").GetComponent<PlayerMotor>().speed;

		GetComponent<Rigidbody> ().velocity = new Vector3 (xVal, yVal, zVal);

		//GetComponent<Transform> ().position = new Vector3 (xVal, 3.5, zVal);

		//Debug.Log ("okok2 : " + moveVector.x);
	}

	IEnumerator stopRotation(int rotate)
	{

		yield return new WaitForSeconds (0.1f);
		GetComponent<Rigidbody> ().angularVelocity = new Vector3 (0, 0, 0);
		GetComponent<Transform> ().eulerAngles= new Vector3 (0, rotate , 0);

	}

	private void changementPosition(int nombre)
	{
		switch (nombre) {
		case 0:
			GetComponent<Rigidbody> ().position = new Vector3 (transform.position.x, transform.position.y, (transform.position.z + 5));
			break;
		case 1: 
			GetComponent<Rigidbody> ().position = new Vector3 ((transform.position.x + 5), transform.position.y, transform.position.z);
			break;
		case 2:
			GetComponent<Rigidbody> ().position = new Vector3 (transform.position.x, transform.position.y, (transform.position.z - 5));
			break;
		case 3: 
			GetComponent<Rigidbody> ().position = new Vector3 ((transform.position.x  -5), transform.position.y, transform.position.z);
			break;
		default:break;
		}
		/*Vector3 cameraPosition = new Vector3 (0, 0, 0);
		camera.transform.position = Vector3.Lerp (transform.position, cameraPosition, strenght);*/
		//GetComponent<Rigidbody> ().position = new Vector3 (transform.position.x, transform.position.y, (transform.position.z+5));
	}

}
