using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingCharacter : MonoBehaviour {

	public Collider[] hitColliders;

	// Use this for initialization
	void Start () {
	//	InvokeRepeating ("gridMovement",0f, 1f);
		gameObject.GetComponent<Animator>().Play("WalkAnimation");
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void gridMovement()
	{
		// Détection routes valides


		// Déplacement
		gameObject.transform.parent.position += gameObject.transform.forward;

		// Tourner
		gameObject.transform.parent.Rotate (0, 90, 0);
	}
}
