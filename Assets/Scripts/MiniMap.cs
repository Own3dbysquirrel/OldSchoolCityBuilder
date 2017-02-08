using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class MiniMap : MonoBehaviour {

	public GameObject testPrefab;
	public GameObject mainCamera;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {




	}
		
	public void GetPointerCoordinates()
	{
	//	Debug.Log (Input.mousePosition - gameObject.transform.position); // De -100-100 à 100 100 car le pt de pivot est au centre, donc on rajoute +100 pour faire de 0 à 200 + new Vector3(100,100,0)
	
	//	mainCamera.transform.position = new Vector3 (Input.mousePosition.x - gameObject.transform.position.x - 250, mainCamera.transform.position.y, Input.mousePosition.y - gameObject.transform.position.y - 250);

	}
}
