using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

	public Camera mainCamera;
	public Camera minimapCamera;
	public GameObject minimapImage;
	public string cameraOrientation = "North";

	public int zoomSpeed = 2;
	public float camSpeed;

	private int inputDirection = 1;
	private int inputDirection2 = 1;

	[Space(20)]
	public LineRenderer trapezoid;

	// Use this for initialization
	void Start () {
		
	}

	void OnPreRender() {
		minimapCamera.Render(); 
	}
	
	// Update is called once per frame
	void Update () {

		//Flèches directionnelles ou ZQSD pour déplacer la caméra

		// On change les inputs pour s'assurer que quand on tourne la caméra de 90 degrés, la gauche soit toujours vers la gauche
	
		if(Input.GetKey(KeyCode.UpArrow))
		{
			gameObject.transform.position += new Vector3 (inputDirection2 * 1,0,inputDirection * 1).normalized * camSpeed;
		}

		if(Input.GetKey(KeyCode.RightArrow))
		{
			gameObject.transform.position += new Vector3 (inputDirection * 1,0,inputDirection2 * -1).normalized * camSpeed;
		}

		if(Input.GetKey(KeyCode.DownArrow))
		{
			gameObject.transform.position += new Vector3 (inputDirection2 * -1,0,inputDirection * -1).normalized * camSpeed;
		}

		if(Input.GetKey(KeyCode.LeftArrow))
		{
			gameObject.transform.position += new Vector3 (inputDirection *-1,0,inputDirection2 * 1).normalized * camSpeed;
		}


	





		// Molette Zoom et Dézoom
		if (Input.GetAxis ("Mouse ScrollWheel") > 0 && mainCamera.orthographicSize > 2) {
			mainCamera.orthographicSize -= zoomSpeed;
		} 
		else if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
			mainCamera.orthographicSize += zoomSpeed;
		}


		// Rotation a 90° dans le sens des aiguilles d'une montre
		if(Input.GetKeyUp(KeyCode.R))
		{
			RotateCamera ();
		}




		// Création de raycast à chaque coin de la camera pour créer le trapèze sur la minimap

		RaycastHit myRay;
					
		if (Physics.Raycast(mainCamera.ViewportToWorldPoint (new Vector3 (0,0,0)), gameObject.transform.forward, out myRay, 10000.0f))
			trapezoid.SetPosition (0,  myRay.point);
		

		if (Physics.Raycast(mainCamera.ViewportToWorldPoint (new Vector3 (0,1,0)), gameObject.transform.forward, out myRay, 10000.0f))
			trapezoid.SetPosition (1,  myRay.point);
		
		if (Physics.Raycast(mainCamera.ViewportToWorldPoint (new Vector3 (1,1,0)), gameObject.transform.forward, out myRay, 10000.0f))
			trapezoid.SetPosition (2,  myRay.point);
		
		if (Physics.Raycast(mainCamera.ViewportToWorldPoint (new Vector3 (1,0,0)), gameObject.transform.forward, out myRay, 10000.0f))
			trapezoid.SetPosition (3,  myRay.point);
		
		if (Physics.Raycast(mainCamera.ViewportToWorldPoint (new Vector3 (0,0,0)), gameObject.transform.forward, out myRay, 10000.0f))
			trapezoid.SetPosition (4,  myRay.point);
		







	}

	public void RotateCamera()
	{
		gameObject.transform.Rotate(0, 90, 0, Space.World);
		minimapImage.transform.Rotate(0, 0, 90, Space.World);


		if (gameObject.transform.eulerAngles.y > 44 && gameObject.transform.eulerAngles.y < 46) // Bug, parfois la rotation est de 44.999, donc je check une range plutôt que == 45
		{
			gameObject.transform.position = new Vector3 (-250,200,-250);
			cameraOrientation = "North";
			inputDirection = 1;
			inputDirection2 = 1;
		}

		if (gameObject.transform.eulerAngles.y > 134 && gameObject.transform.eulerAngles.y < 136)
		{
			gameObject.transform.position = new Vector3 (-250,200,250);
			cameraOrientation = "East";
			inputDirection = -1;
			inputDirection2 = 1;
		}


		if (gameObject.transform.eulerAngles.y > 224 && gameObject.transform.eulerAngles.y < 226)
		{
			gameObject.transform.position = new Vector3 (250,200,250);
			cameraOrientation = "South";
			inputDirection = -1;
			inputDirection2 = -1;
		}
	
		if (gameObject.transform.eulerAngles.y > 314 && gameObject.transform.eulerAngles.y < 316)
		{
			gameObject.transform.position = new Vector3 (250,200,-250);
			cameraOrientation = "West";
			inputDirection = 1;
			inputDirection2 = -1;
		}

	}
}
