using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour {

	public Text scoreText;
	// Use this for initialization
	void Start () 
	{
		//Ne pas afficher le menu de fin en debut de partie
		gameObject.SetActive(false);
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void ToggleEndMenu(float score_distance, int piece)
	{
		gameObject.SetActive(true);
		scoreText.text = "Distance = " + ((int)score_distance).ToString () + "\nPiece = " + piece.ToString ();
	}

	public void Restart()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

	public void ToMenu()
	{
		SceneManager.LoadScene ("Menu");
	}
}
