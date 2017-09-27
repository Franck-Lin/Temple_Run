using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	private float score_distance ;

	private int difficultyLevel = 1;
	private int maxDifficultyLevel = 6;
	private int scoreToNextLevel = 10;

	public Text scoreText;

	private bool isDead = false;

	private int count_piece;

	//Recuperation de la classe deathMenu
	public DeathMenu deathMenu;

	// Use this for initialization
	void Start () 
	{
		count_piece = 0;
		score_distance = 0.0f;
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
		scoreToNextLevel *=3;
		difficultyLevel++;
		//Utilisation d'une fonction dans un autre script sur un même objet
		GetComponent<PlayerMotor>().SetSpeed (0.5f);
	}

	//Arret du score quand le joueur perd
	public void OnDeath()
	{
		isDead = true;
		if(PlayerPrefs.GetFloat("HighScore Distance :") < score_distance)
			PlayerPrefs.SetFloat ("HighScore Distance :" , score_distance);
		if(PlayerPrefs.GetFloat("HighScore Piece :") < count_piece)
			PlayerPrefs.SetFloat ("HighScore Piece :" , count_piece);
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
	}
}
