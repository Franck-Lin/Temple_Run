using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour 
{
	private CharacterController controller;
	private Vector3 moveVector;

	public float speed = 5.0f;
	private float verticalVelocity = 0.0f;
	private float gravity = 1.0f;

	private float animationDuration = 3.0f;
	//Blocage de la caméra quand on relance une partie à partir du DeathMenu
	private float startTime;

	private int rotate_controler_y = 0;

	public bool isDead = false;
	private bool cote_droit = false;
	private bool cote_gauche = false;
	private bool unique_cote = true;

	//private Rigidbody rbb;
	// Use this for initialization
	void Start () 
	{
		
		//rbb = GetComponent<Rigidbody> (); 

		startTime = Time.time;

	}
	
	// Update is called once per frame
	void Update () 
	{	
		moveVector = Vector3.zero;
		controller = GetComponent<CharacterController> ();
		if (isDead)
			return;
		
		//Blocage des mouvements durant l'animation
		if (Time.time - startTime < animationDuration) 
		{
			controller.Move (Vector3.forward * speed * Time.deltaTime);
			//Debug.Log ("non");
			return;

		}


		if (controller.isGrounded) 
		{
			verticalVelocity = -0.5f;
		} 
		else 
		{
			verticalVelocity -= gravity;
		}

		//Decalage du joueur à droite/gauche
		//Avec a et d pour droite et gauche voir input dans project settings

		moveVector.x = Input.GetAxisRaw ("Horizontal") * speed;
		moveVector.y = verticalVelocity;
		moveVector.z = speed;

		/*if (Input.GetButtonDown ("Fire1")) 
		{
			Debug.Log ("souris");
			for (int i = 0; i < 100; i++) {
				moveVector.y += (i * 0.05f);
			}
		} */



		if(Input.GetButtonDown ("Fire2") && unique_cote == true)
		{
			rotate_controler_y = 90;
			controller.transform.Rotate (0, rotate_controler_y, 0);

			cote_gauche = false;
			cote_droit = true;
			unique_cote = false;
			Debug.Log("r :" + rotate_controler_y);
			Changementcote(2);

		}	

		if(Input.GetButtonDown ("Fire1") && unique_cote == false)
		{
			rotate_controler_y = -90;
			controller.transform.Rotate (0, rotate_controler_y, 0);

			cote_gauche= true;
			cote_droit = false;
			unique_cote = true;
			//Debug.Log("r :" + rotate_controler_y);
			Changementcote(1);
		}

		if (cote_droit) {
			moveVector.z = Input.GetAxisRaw ("Vertical") * speed;
			moveVector.x = speed;
		}

		if (cote_gauche) {
			moveVector.z = speed;
			moveVector.x = Input.GetAxisRaw ("Horizontal") * speed;

		}
			
		//Lancement du mouvement position + vitesse
		controller.Move (moveVector * Time.deltaTime);
		
	}

	public void SetSpeed (float changement)
	{
		speed = 5.0f + changement ;
	}

	//Quand le joueur touche rentre en collision
	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		//Verification que c'est bien les objets vers l'avant et non sur le coté qui termine la partie
		if (cote_droit) {
			Debug.Log("cote_droit impact");
			if(hit.point.x > transform.position.x + controller.radius) 
			{
				Death ();
			}
		}
		else if(hit.point.z > transform.position.z + controller.radius) 
		{
			Death ();
		}
	}

	private void Death()
	{
		//Debug.Log ("mort");
		isDead = true;
		GetComponent<Score> ().OnDeath ();
	}

	public void Changementcote(int cote)
	{
		if (cote == 1) {
			
			GameObject.Find ("Main Camera").GetComponent<CameraMove> ().modif_cote_gauche ();
		}

		if (cote == 2) {
			GameObject.Find ("Main Camera").GetComponent<CameraMove> ().modif_cote_droit ();
		}
	}
}
