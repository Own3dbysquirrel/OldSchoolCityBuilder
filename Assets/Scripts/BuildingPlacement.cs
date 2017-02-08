using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour {

//	private Camera mainCamera;
	public int buildingSize = 1;
	private float gimzoGridSnap;
	private float buildingGridSnap;
	private Renderer rend;

	public bool canIBuildHere;
	public bool isBuildEnabled;
	public GameObject buildingPrefabSelected;
	private GameObject myBuildingInstance;

	// Use this for initialization
	void Start () {
		CancelBuildPlacement ();

		rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {

	
		// Position du gizmo
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Plane hPlane = new Plane(Vector3.up, new Vector3(0,0.01f,0));
		float distance = 0; 
		if (hPlane.Raycast(ray, out distance)){
			gameObject.transform.position = ray.GetPoint(distance);
			gameObject.transform.position = new Vector3(Mathf.Round(gameObject.transform.position.x)- gimzoGridSnap,0.01f,Mathf.Round(gameObject.transform.position.z) - gimzoGridSnap);
		}


		// Test de collision
		if (Physics.CheckBox (gameObject.transform.position, new Vector3 (buildingSize/2f - 0.1f, 0.001f, buildingSize/2f - 0.1f))) 
		{
			rend.material.color = new Color (1, 0, 0, 0.65f);
			canIBuildHere = false;
		//	Debug.Log (Physics.OverlapBox (gameObject.transform.position, new Vector3 (1.5f, 0.01f, 1.5f))[0]);
		} 
		else 
		{
			rend.material.color = new Color (0, 1, 0, 0.65f);
			canIBuildHere = true;
		}


		// Construction si on clique
		if (Input.GetMouseButtonDown(0) && canIBuildHere && isBuildEnabled)
		{
			BuildTheThing ();
		}

		// Annulation
		if (Input.GetMouseButtonDown(1))
		{
			CancelBuildPlacement ();
		}
	}

	void BuildTheThing()
	{
		myBuildingInstance = Instantiate (buildingPrefabSelected, GameObject.Find("Buildings").transform);
		myBuildingInstance.transform.position = new Vector3 (gameObject.transform.position.x + buildingGridSnap,0.5f,gameObject.transform.position.z + buildingGridSnap);
	}



	public void EnableBuildPlacement(GameObject myPrefab)
	{
		isBuildEnabled = true;
		buildingPrefabSelected = myPrefab;
		gameObject.GetComponent<MeshRenderer> ().enabled = true;


		// Taille de l'UI dépend de la taille du building selectionné
		buildingSize =	myPrefab.GetComponent<Building> ().size;
		gameObject.transform.localScale = new Vector3(buildingSize/10f,1,buildingSize/10f);

		// Si la taille du building est impaire, on décale de 0.5 les positions pour que ce soit sur la  grille
		if (buildingSize % 2 == 1) 
		{
			gimzoGridSnap = 0.5f;
			buildingGridSnap = 0;
		} 
		else 
		{
			gimzoGridSnap = 0;
			buildingGridSnap = 0.5f;
		}
	}


	public void CancelBuildPlacement()
	{
		isBuildEnabled = false;
		gameObject.GetComponent<MeshRenderer> ().enabled = false;
	}
}
