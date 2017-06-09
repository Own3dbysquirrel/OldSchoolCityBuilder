using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Gatherer : MonoBehaviour {

    private NavMeshAgent myAgent;
    public Transform myDestination;
    public float pathUpdateDelay = 1;

	// Use this for initialization
	void Start () {
        myAgent = gameObject.GetComponent<NavMeshAgent>();
        StartCoroutine(CheckValidResourcesNodes());
    }
	
	// Update is called once per frame
	void Update () {
       	
	}

    public IEnumerator CheckValidResourcesNodes()
    {
        while(true)
        {
            myAgent.destination = myDestination.position;
            yield return new WaitForSeconds(pathUpdateDelay);
        } 
    }
}