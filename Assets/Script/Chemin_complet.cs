using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chemin_complet : MonoBehaviour {

	//Objet à instancier : 
	public GameObject[] cheminPrefabs;

	private Transform playerTransform;

	private float spawnX = 0.0f;
	private float spawnZ = 0.0f;
	private float cheminLength = 10.0f;
	// safeZone pour eviter de detruire l'objet trop tot au passage du joueur
	private float safeZone = 15.0f;
	// Random chemins
	private int lastPrefabIndex = 0;

	private int amnCheminsOnScreen = 8;

	private int changementCote_droit = 0;
	private int changementCote_gauche = 0;
	private int modificationCote_droit = 0;
	private int modificationCote_gauche = 0;
	private int rotateY = 0;

	private List<GameObject> activeChemins;

	// Use this for initialization
	private void Start () {
		activeChemins = new List<GameObject> ();
		playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;

		//Nombre de chemins instanciés en debut de partie
		for (int i = 0; i < amnCheminsOnScreen; i++) 
		{
			if(i < 2)
				SpawnChemin (0);
			else
				SpawnChemin ();

		}		
	}

	// Update is called once per frame
	private void Update () 
	{
		if (changementCote_gauche > 3) {
			changementCote_gauche = 0;
		}
		if (changementCote_droit > 3) {
			changementCote_droit = 0;
		}
		//Instanciation de chemins supplémentaires en fonction de l'avancement du joueur
		/*if(playerTransform.position.z - safeZone > (spawnZ - amnCheminsOnScreen * cheminLength))
			{
				SpawnChemin ();
				DeleteChemin ();
			}	*/
		//Debug.Log (activeChemins.Count);
		if (activeChemins.Count > 8 && (playerTransform.position.z > (activeChemins[0].transform.position.z + 10.0f) || playerTransform.position.x > (activeChemins[0].transform.position.x + 10.0f))) {
			SpawnChemin ();
			DeleteChemin ();
		}
	}

	private void SpawnChemin(int prefabIndex = -1)
	{
		GameObject go;
		//Condition pour ne pas instancier un chemin avec obstacle dès le lancement
		if (prefabIndex == -1) {
			go = Instantiate (cheminPrefabs [RandomPrefabIndex ()]) as GameObject;
			//Debug.Log (activeChemins[].ToString());

		}
		else {
			go = Instantiate (cheminPrefabs [prefabIndex]) as GameObject;
			//Debug.Log (cheminPrefabs[prefabIndex].ToString());

		}
		go.transform.SetParent (transform);
		go.transform.position =  new Vector3 ((1 * spawnX),0,(1 * spawnZ));

		if (changementCote_droit > 0 && changementCote_droit > modificationCote_droit) 
		{
			rotateY += 90;
			modificationCote_droit = changementCote_droit;
		}
		if (changementCote_gauche > 0 && changementCote_gauche > modificationCote_gauche) 
		{
			rotateY -= 90;
			modificationCote_gauche = changementCote_gauche;
		}	
		//go.transform.position = Vector3.forward * spawnZ;
		if (go.tag == "Corner_right") {
			changementCote_droit++;
			if (changementCote_gauche > 0) {
				changementCote_gauche--;
			}

		} 

		if (go.tag == "Corner_left") {
			changementCote_gauche++;
			if (changementCote_droit > 0) {
				changementCote_droit--;
			}

		}
		switch (changementCote_droit) 
		{
		/*case 0:
			spawnZ += cheminLength;
			break;*/
		case 1:
			spawnX += cheminLength;
			break;
		case 2:
			spawnZ -= cheminLength ;
			//spawnX -= (cheminLength + 5.0f);
			break;
		case 3:
			spawnX -= cheminLength;
			break;

		default:break;
		}

		switch (changementCote_gauche) 
		{/*
		case 0:
			spawnZ += cheminLength;
			break;*/
		case 1:
			spawnX -= cheminLength;
			//spawnZ += cheminLength;
			break;
		case 2:
			spawnZ += cheminLength ;
			//spawnX -= (cheminLength + 5.0f);
			break;
		case 3:
			spawnX += cheminLength;
			break;

		default:break;
		}
		if (changementCote_droit == 0 && changementCote_gauche == 0) 
		{
			spawnZ += cheminLength;
		}
		//Debug.Log ("vecteur :" + go.transform.position);
		go.transform.Rotate (0, rotateY, 0);

		activeChemins.Add (go);

	}

	private void DeleteChemin()
	{
		Destroy (activeChemins [0]);
		activeChemins.RemoveAt (0);
	}

	private int RandomPrefabIndex()
	{
		if (cheminPrefabs.Length <= 1) 
		{
			return 0;		
		}

		int randomIndex = lastPrefabIndex;

		while (randomIndex == lastPrefabIndex || (randomIndex == 3 && lastPrefabIndex == 2) || (randomIndex == 2 && lastPrefabIndex == 3)) 
		{
			randomIndex = Random.Range (0, cheminPrefabs.Length);
		}
		Debug.Log ("lastPrefabIndex : " + lastPrefabIndex);
		lastPrefabIndex = randomIndex;
		Debug.Log ("lastPrefabIndex2 : " + lastPrefabIndex);
		// S'il y a eu deja un chemin avant alors ne pas le prendre!

		return randomIndex;
	}
}
