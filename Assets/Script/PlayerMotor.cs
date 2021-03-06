using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour 
{
	private CharacterController controller;
	private Vector3 moveVector;

	public float speed = 5.0f;
	private float verticalVelocity = 0.0f;
	private float gravity = 5.0f;

	private float animationDuration = 3.0f;

	private float startTime;

	private int rotate_controler_y = 0;

	public bool isDead = false;
	private bool cote_droit = false;
	private bool cote_gauche = false;
	private bool unique_cote = true;

	//Controle android
	private Vector3 touchePosition;
	//Limite minimum pour le swipe horizontale
	private float swipeResistanceX = 50.0f;
	private float swipeResistanceY = 50.0f;

	/*//Test Gyroscope
	private float initialOrientationX;
	private float initialOrientationZ;*/

	// Use this for initialization
	void Start () 
	{
		/* // Test Gyro
		Input.gyro.enabled = true;*/
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () 
	{	
		/* //Test Gyro
		initialOrientationX = Input.gyro.rotationRateUnbiased.x;
		initialOrientationZ = -Input.gyro.rotationRateUnbiased.z;
		*/
		moveVector = Vector3.zero;
		controller = GetComponent<CharacterController> ();
		if (isDead)
			return;
		
		//Blocage des mouvements durant l'animation
		if (Time.time - startTime < animationDuration) 
		{
			controller.Move (Vector3.forward * speed * Time.deltaTime);
			return;
		}
		//Gravité
		if (controller.isGrounded) 
		{
			verticalVelocity = -0.5f;
		} 
		else 
		{
			verticalVelocity -= gravity * Time.deltaTime;
		}

		//Mouvement du joueur
		moveVector.x = 0;
		moveVector.y = verticalVelocity;
		moveVector.z = speed;

		if (Input.GetMouseButtonDown(0)) 
		{
			touchePosition = Input.mousePosition;
		}

		if (Input.GetMouseButtonUp (0)) {
			Vector2 deltaSwipe = touchePosition - Input.mousePosition;

			//Test de touche du joueur
			if (Mathf.Abs (deltaSwipe.x) == 0) {
				if (touchePosition.x > Screen.width / 2) {
					//Deplacement droit
					controller.transform.Translate (1, 0, 0);
				} else {
					//Deplacement gauche
					controller.transform.Translate (-1, 0, 0);
				}

			}
			// Test pour le saut
			else if (Mathf.Abs (deltaSwipe.y) > swipeResistanceY) {
				if (deltaSwipe.y < 0) {
					moveVector.y = 120.0f;
				}
			} else if (Mathf.Abs (deltaSwipe.x) > swipeResistanceX) {
				if (deltaSwipe.x > 0 && unique_cote == false) {
					rotate_controler_y = -90;
					controller.transform.Rotate (0, rotate_controler_y, 0);
					controller.transform.Translate (0, 0, 0);
					cote_gauche = true;
					cote_droit = false;
					unique_cote = true;

					Changementcote (1);
				} else if (deltaSwipe.x < 0 && unique_cote == true) {
					rotate_controler_y = 90;
					controller.transform.Rotate (0, rotate_controler_y, 0);
					controller.transform.Translate (0, 0, 0);
					cote_gauche = false;
					cote_droit = true;
					unique_cote = false;

					Changementcote (2);
				} 
			}
		}
		if (cote_droit) 
		{
			moveVector.x = speed;
			moveVector.z = 0;
		}

		if (cote_gauche) 
		{
			moveVector.z = speed;
			moveVector.x = 0;
		}
		//Lancement du mouvement position + vitesse
		controller.Move (moveVector * Time.deltaTime);
		//Test de la hauteur pour stopper le jeu si chute
		if (controller.transform.position.y < -1) {
			Death ();
		}
		//Retour au menu avec le bouton return du mobile
		if(Input.GetKey(KeyCode.Escape))
		{
			Application.LoadLevel("Menu");
		}
		
	}
	//Augmentation de la vitesse
	public void SetSpeed (float changement)
	{
		speed = 5.0f + changement ;
	}

	//Quand le joueur touche rentre en collision
	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		//Verification que c'est bien les objets vers l'avant et non sur le coté qui termine la partie
		if (cote_droit) {
			if(hit.point.x > transform.position.x + controller.radius) 
			{
				Death ();
			}
		}
		else 
		{
			if(hit.point.z > transform.position.z + controller.radius) 
			{
				Death ();
			}
		}
	}

	private void Death()
	{
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
