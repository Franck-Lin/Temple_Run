using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	private float score_distance = 0.0f;

	private int difficultyLevel = 1;
	private int maxDifficultyLevel = 10;
	private int scoreToNextLevel = 10;

	public Text scoreText;

	private bool isDead = false;

	private int count_piece;

	//Recuperation de la classe deathMenu
	public DeathMenu deathMenu;

	// Use this for initialization
	void Start () {
		count_piece = 0;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isDead)
			return;
		if (score_distance >= scoreToNextLevel)
			LevelUp ();
		score_distance += Time.deltaTime;
		scoreText.text = "Distance = " + ((int)score_distance).ToString () + "\nPiece = " + count_piece.ToString ();
		
	}

	//Changement de niveau 
	void LevelUp()
	{
		if (difficultyLevel == maxDifficultyLevel) 
		{ 
			return;
		}
		//Modification du nombre à atteindre pour franchir un nouveau niveau 
		scoreToNextLevel *=2;
		difficultyLevel++;
		//Utilisation d'une fonction dans un autre script sur un même objet
		GetComponent<PlayerMotor>().SetSpeed (difficultyLevel);
	}

	//Arret du score quand le joueur perd
	public void OnDeath()
	{
		isDead = true;
		deathMenu.ToggleEndMenu (score_distance,count_piece);
	}

	//Compteur de pieces
	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.tag == "Piece") 
		{
			other.gameObject.SetActive (false);
			count_piece++;
		}
		/*if (other.gameObject.tag == "barril") 
		{
			//Debug.Log ("mort");
			OnDeath ();
		}*/
	}
}
