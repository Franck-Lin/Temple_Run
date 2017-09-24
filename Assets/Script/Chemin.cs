using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chemin : MonoBehaviour {

	//Objet à instancier : 
	public GameObject[] cheminPrefabs;

	private Transform playerTransform;

	private float spawnX = 0.0f;
	private float spawnZ = 0.0f;
	private float cheminLength = 13.0f;
	// safeZone pour eviter de detruire l'objet trop tot au passage du joueur
	private float safeZone = 15.0f;
	// Random chemins
	private int lastPrefabIndex = 0;

	private int amnCheminsOnScreen = 8;

	private int changementCote_droit = 0;
	private int changementCote_gauche = 0;
	private bool modificationCote_droit = false;
	private bool modificationCote_gauche = false;
	private int rotateY = 0;

	private int nombre_chemin = 0;
	private List<GameObject> activeChemins;

	// Use this for initialization
	private void Start () {
		activeChemins = new List<GameObject> ();
		playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;

		//Nombre de chemins instanciés en debut de partie
		for (int i = 0; i < amnCheminsOnScreen; i++) 
		{
			if(i < 4)
				SpawnChemin (0);
			else
				SpawnChemin ();
				
		}		
	}
	
	// Update is called once per frame
	private void Update () 
	{
		
		if (activeChemins.Count > 7 && (playerTransform.position.z > (activeChemins[0].transform.position.z + cheminLength) || playerTransform.position.x > (activeChemins[0].transform.position.x + cheminLength))) {
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


		/*if (changementCote_droit > 0 && changementCote_droit > modificationCote_droit) 
		{
			rotateY += 90;
			modificationCote_droit = changementCote_droit;
		}
		if (changementCote_gauche > 0 && changementCote_gauche > modificationCote_gauche) 
		{
			rotateY -= 90;
			modificationCote_gauche = changementCote_gauche;
		}	*/
		//go.transform.position = Vector3.forward * spawnZ;
		go.transform.Rotate (0, rotateY, 0);
		if (go.tag == "Corner_right") {
			
			rotateY += 90;
			modificationCote_droit = true;
			modificationCote_gauche = false;
			spawnX += cheminLength / 2;
		} 

		if (go.tag == "Corner_left") {
			rotateY -= 90;
			modificationCote_droit = false;
			modificationCote_gauche = true;
			spawnZ += cheminLength / 2;

		}
		go.transform.position =  new Vector3 ((1 * spawnX),0,(1 * spawnZ));
		if (modificationCote_droit) 
		{
			spawnX += 10.0f;
			spawnZ += 0;
		}

		if (modificationCote_gauche) 
		{
			spawnX += 0;
			spawnZ += 10.0f;
		}

		if (nombre_chemin < 10) {
			spawnZ += 10.0f;
		}

		/*if (changementCote_droit == 0 && changementCote_gauche == 0) 
		{
			spawnZ += cheminLength;
		}*/
		//Debug.Log ("vecteur :" + go.transform.position);


		activeChemins.Add (go);

	}

	private void DeleteChemin()
	{
		Destroy (activeChemins [0]);
		activeChemins.RemoveAt (0);
	}

	private int RandomPrefabIndex()
	{
		nombre_chemin++;
		if (cheminPrefabs.Length <= 1) 
		{
			return 0;		
		}
			
		int randomIndex = lastPrefabIndex;

		//Mettre un chemin simple après un pont
		if (randomIndex == (cheminPrefabs.Length - 2) || randomIndex == (cheminPrefabs.Length - 1)) {
			randomIndex = 0;
		}

		//Instanciation de virages tous les 10 chemins
		if (nombre_chemin % 10 == 0) {
			//Debug.Log ("nombre chemin : " + nombre_chemin);
			randomIndex = (cheminPrefabs.Length-2);
		}

		if (nombre_chemin % 20 == 0) {
			randomIndex = (cheminPrefabs.Length-1);
		}

		//Pour ne jamais avoir 2 chemins identiques à la suite
		while (randomIndex == lastPrefabIndex  ) 
		{
			randomIndex = Random.Range (0, (cheminPrefabs.Length-2));
		}


		//Debug.Log ("lastPrefabIndex : " + lastPrefabIndex);
		lastPrefabIndex = randomIndex;
		//Debug.Log ("lastPrefabIndex2 : " + lastPrefabIndex);
		// S'il y a eu deja un chemin avant alors ne pas le prendre!

		return randomIndex;
	}
}
