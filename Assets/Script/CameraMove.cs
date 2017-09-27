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

	//Rotation
	private float xVal = 0.0f;
	private float yVal = 0.0f;
	private float zVal = 0.0f;

	private int rotateY = 0;
	private int nombre_rotation_gauche = 0 ;
	private int nombre_rotation_droite = 0 ;

	private bool dead = false;

	private bool cote_droit = false;
	private bool cote_gauche = false;

	private float position_save_z = 0.0f;
	private float position_save_x = 0.0f;

	// Use this for initialization
	void Start () 
	{
		lookAt = GameObject.FindGameObjectWithTag ("Player").transform;
		startOffset = transform.position - lookAt.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (dead)
			return;
		
		moveVector = lookAt.position + startOffset;
		moveVector.x = 0;
		moveVector.y = Mathf.Clamp (moveVector.y, 3, 5);
		if (transition > 1.0f) 
		{
			if (cote_droit) 
			{
				moveVector.x = (lookAt.position.x - 4.5f);
				moveVector.z = position_save_z;
			} 
			else if (cote_gauche) 
			{
				moveVector.x = position_save_x;
				moveVector.z = (lookAt.position.z - 4.0f);
			}
			transform.position = moveVector;
		} // Debut du jeu
		else 
		{
			transform.position = Vector3.Lerp (moveVector + animationOffset, moveVector, transition);
			transition += Time.deltaTime * 1 / animationDuration;
			transform.LookAt (lookAt.position + Vector3.up);
		}
		//Rotation de la caméra
		GetComponent<Rigidbody> ().velocity = new Vector3 (xVal, yVal, zVal);
	}
		
	IEnumerator stopRotation(int rotate)
	{
		yield return new WaitForSeconds (0.5f);
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
		position_save_x += 106.5f;
		//position_save_x = lookAt.position.x;
	}

	public void modif_cote_droit()
	{
		rotateY = 90;
		xVal = (GameObject.Find ("Player").GetComponent<PlayerMotor> ().speed);
		zVal = 0;

		GetComponent<Rigidbody> ().angularVelocity = new Vector3 (0, 2, 0);
		StartCoroutine (stopRotation(rotateY));
		// Si c'est le premier coté
		if (cote_droit == false && cote_gauche == false) {
			position_save_z += 130.0f;
		} else {
			position_save_z += 106.5f;
		}
		cote_droit = true;
		cote_gauche = false;
		//position_save_z = lookAt.position.z; //Erreur si le joueur swipe trop tot, camera non centré sur le chemin
	}
}
