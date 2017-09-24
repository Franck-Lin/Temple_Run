using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

	private Transform lookAt;
	private Vector3 startOffset;
	private Vector3 moveVector;

	//Animation plongeante en debut de course
	private float transition = 0.0f;
	private float animationDuration = 3.0f;
	private Vector3 animationOffset = new Vector3 (0, 5, 4);

	private int  rotate_camera_y = 0;



	private float xVal = 0.0f;
	private float yVal = 0.0f;
	private float zVal = 0.0f;

	private int rotateY = 0;
	private int nombre_rotation_gauche = 0 ;
	private int nombre_rotation_droite = 0 ;

	private bool dead = false;

	private bool cote_droit = false;
	private bool cote_gauche = false;
	/*private float strenght = 0.5f;
	private string camera = GetComponent<Transform>();*/
	// Use this for initialization
	void Start () 
	{
		lookAt = GameObject.FindGameObjectWithTag ("Player").transform;
		startOffset = transform.position - lookAt.position;

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

		//Vector3 pos = transform.forward * speed * Time.deltaTime + oldPos;

		moveVector = lookAt.position + startOffset;
		moveVector.x = 0;
		moveVector.y = Mathf.Clamp (moveVector.y, 3, 5);
		if (transition > 1.0f) 
		{
			
			//Debug.Log (moveVector);
			if (cote_droit) {
				//GetComponent<Rigidbody> ().position = new Vector3 (-2, transform.position.y, (transform.position.z - 3.5f));
				moveVector.x += (lookAt.position.x + -4.5f);
				moveVector.z = lookAt.position.z;
				transform.position = moveVector;
			} else if (cote_gauche) {
				moveVector.x = (lookAt.position.x );
				moveVector.z = (lookAt.position.z - 4.0f);
				transform.position = moveVector;
			}
			else
			transform.position = moveVector;
		} // Debut du jeu
		else 
		{
			transform.position = Vector3.Lerp (moveVector + animationOffset, moveVector, transition);
			transition += Time.deltaTime * 1 / animationDuration;
			//Rotation de la caméra
			transform.LookAt (lookAt.position + Vector3.up);
		}



		//Rotation de la caméra
		//transform.LookAt (lookAt.position + Vector3.up);
		//Debug.Log("moveVector 1 :" + moveVector);
		//transform.position = moveVector;

		/*if (Input.GetButtonDown ("Fire2")) {
			

		}

		if (Input.GetButtonDown ("Fire1")) {
			

			//GetComponent<Rigidbody> ().position = new Vector3 ((transform.position.x ), transform.position.y, transform.position.z);
		}*/

		GetComponent<Rigidbody> ().velocity = new Vector3 (xVal, yVal, zVal);
	}
		
	IEnumerator stopRotation(int rotate)
	{

		yield return new WaitForSeconds (0.8f);
		GetComponent<Rigidbody> ().angularVelocity = new Vector3 (0, 0, 0);
		GetComponent<Transform> ().eulerAngles= new Vector3 (0, rotate , 0);

	}

	public void modif_cote_gauche()
	{
		rotateY = 0;
		zVal = (GameObject.Find ("Player").GetComponent<PlayerMotor> ().speed);
		xVal = 0;

		GetComponent<Rigidbody> ().angularVelocity = new Vector3 (0, -2, 0);
		StartCoroutine (stopRotation(rotateY));
		cote_gauche = true;
		cote_droit = false;
		Debug.Log ("cote droit activé");
	}

	public void modif_cote_droit()
	{
		rotateY = 90;
		xVal = (GameObject.Find ("Player").GetComponent<PlayerMotor> ().speed);
		zVal = 0;

		GetComponent<Rigidbody> ().angularVelocity = new Vector3 (0, 2, 0);
		StartCoroutine (stopRotation(rotateY));
		cote_droit = true;
		cote_gauche = false;
	}
}
