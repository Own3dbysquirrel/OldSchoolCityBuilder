using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class ConstructionGizmo : MonoBehaviour {



	private int sizeX = 7;
	private int sizeY = 7;
	private float offsetX = 0f;
	private float offsetZ = -1f;
	private string gizmoOrientation = "North";
	private float gizmoHeight;
	private bool globalCanIBuildHere;
	private GameObject myBuildingInstance;

	public bool isBuildingPlacementActive;
	public GameObject buildingPrefabSelected;
	public LayerMask layerMask;
	public GameObject buildingTilePrefab;
	[HideInInspector]
	public CustomGUIArrayLayout buildingSizeData;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		// Annulation du placement
		if (Input.GetMouseButtonDown(1) && isBuildingPlacementActive)
		{
			CancelBuildPlacement ();
		}

		if (isBuildingPlacementActive)
		{
			// Position du gizmo
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Plane hPlane = new Plane (Vector3.up, new Vector3 (0, gizmoHeight + 0.01f, 0));



			float distance = 0; 
			if (hPlane.Raycast (ray, out distance)) {

				gameObject.transform.position = ray.GetPoint(distance);

				// Snap sur la grille
				gameObject.transform.position = new Vector3(Mathf.Round(gameObject.transform.position.x),gizmoHeight + 0.01f,Mathf.Round(gameObject.transform.position.z));

			}

			// Elevation du gizmo si le terrain est en hauteur
			if (Physics.Raycast (ray, out hit, 1000f, layerMask)) {
				gizmoHeight = hit.transform.position.y;
			} 

			// rotation du Gizmo
			if(Input.GetKeyDown(KeyCode.T))
			{
				RotateGizmo ();
			}
		}



		// On vérifie que chaque tile du batiment à placer est bien valide (verte)
		if (Input.GetMouseButtonDown(0) && isBuildingPlacementActive)
		{
			CheckIfICanBuildTheThing ();
		}


	}

	public void CreateGizmoGrid()
	{
		// Destruction de tout les anciens enfants (reset)
		gameObject.transform.position = Vector3.zero;
		foreach (Transform myChild in gameObject.transform)
			Destroy (myChild.gameObject);

		// Regénération d'un grille d'enfants conforme aux nouvelles dimensions du bâtiment
		for (int i = 0; i < sizeX; i++) 
		{
			for (int j = 0; j < sizeY; j++) {
				if (buildingSizeData.rows [i].row [j] == true) 
				{
					Instantiate (buildingTilePrefab, new Vector3 (3 + gameObject.transform.position.x - i, 0f, 3 + gameObject.transform.position.z - j), Quaternion.identity, gameObject.transform);
				}
					
			}
		}
	}

	public void EnableBuildPlacement(GameObject myPrefab)
	{
		gameObject.SetActive (true);

		gizmoOrientation = "North";
		offsetX = 0f;
		offsetZ = -1f;

		buildingPrefabSelected = myPrefab;
		buildingSizeData = buildingPrefabSelected.GetComponent<Building> ().buildingSizeData;

		CreateGizmoGrid();
		isBuildingPlacementActive = true;
	}

	void CheckIfICanBuildTheThing()
	{
		globalCanIBuildHere = true;
		BuildingPlacementCell[] buildingTilesChildren = gameObject.GetComponentsInChildren<BuildingPlacementCell> ();
		foreach(BuildingPlacementCell cell in buildingTilesChildren)
		{
			if (cell.canIBuildHere == false) {
				globalCanIBuildHere = false;
			}
		}

		if (globalCanIBuildHere) {
			BuildTheThing ();
		}
		else
		{
			Debug.Log ("can't build here");
		}
			
	}

	void BuildTheThing()
	{
		myBuildingInstance = Instantiate (buildingPrefabSelected, GameObject.Find("Buildings").transform);
		myBuildingInstance.transform.position = new Vector3 (gameObject.transform.position.x + offsetX,0.5f + gizmoHeight,gameObject.transform.position.z + offsetZ);
		myBuildingInstance.transform.rotation = gameObject.transform.rotation;
	}

	public void CancelBuildPlacement()
	{
		gameObject.transform.rotation = Quaternion.identity;
		isBuildingPlacementActive = false;
		gameObject.SetActive (false);
	}

	public void RotateGizmo()
	{
		gameObject.transform.Rotate (0, 90, 0);


		if (gizmoOrientation == "West") {
			gizmoOrientation = "North";
			offsetX = 0f;
			offsetZ = -1f;
		} else if (gizmoOrientation == "South") {
			gizmoOrientation = "West";
			offsetX = 1f;
			offsetZ = 0f;
		} else if (gizmoOrientation == "East") {
			gizmoOrientation = "South";
			offsetX = 0f;
			offsetZ = 1f;
		} else if (gizmoOrientation == "North") {
			gizmoOrientation = "East";
			offsetX = -1f;
			offsetZ = 0f;
		}
	}
}
