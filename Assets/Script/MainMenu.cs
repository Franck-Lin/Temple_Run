using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public Text highScoreDistance;
	// Use this for initialization
	void Start () {
		highScoreDistance.text = "Highscore Distance : " + ((int)PlayerPrefs.GetFloat("HighScore Distance :")).ToString() + "\nHighscore Piece : " + ((int)PlayerPrefs.GetFloat("HighScore Piece :")).ToString();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	public void ToGame()
	{
		SceneManager.LoadScene ("Scene1");
	}

	public void ToRules()
	{
		SceneManager.LoadScene ("Regles");
	}



}
