using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacementCell : MonoBehaviour {


	private Renderer rend;
	public bool canIBuildHere;
	public LayerMask layerMask;

	private float gizmoHeight;
	private bool isGizmoOutOfBounds;

	// Use this for initialization
	void Start () {

		rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {

	
		// Position du gizmo
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		// Elevation du gizmo si
		if (Physics.Raycast (ray, out hit, 1000f, layerMask)) {
			gizmoHeight = hit.transform.position.y;
		} 
		// On vérifie si le gizmo est bien juste au dessus d'une surface constructible
		if (Physics.Raycast (transform.position, Vector3.down, 0.5f, layerMask)) {
			
			isGizmoOutOfBounds = false;
		} else {
			isGizmoOutOfBounds = true;
		}


		// Test de collision
		if (!isGizmoOutOfBounds) {
			if (Physics.CheckBox (gameObject.transform.position, new Vector3 (0.1f, 0.001f, 0.1f))) {
				rend.material.color = new Color (1, 0, 0, 0.65f);
				canIBuildHere = false;
			} else {
				rend.material.color = new Color (0, 1, 0, 0.65f);
				canIBuildHere = true;
			}
		} else {
			rend.material.color = new Color (1, 0, 0, 0.65f);
			canIBuildHere = false;
		}
	}
}
