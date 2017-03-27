using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NavWaypoints : MonoBehaviour {

    public Waypoint NextWaypoint;
    public float speed = 0.5f;
    Vector3 nextWPpos;
    
    GameObject AlterEgo;
    

	// Use this for initialization
	void Start () {

        Transform TM = GameObject.Find("TerrainManager").transform;

        AlterEgo = Instantiate((GameObject)Resources.Load("prefabs/FoodPaint"), TM);

        Vector3 tmpPos = new Vector3();
        Quaternion tmpQuat = Quaternion.AngleAxis(90.0f, Vector3.right);

        tmpPos.x = transform.localPosition.x;
        tmpPos.y = transform.localPosition.z;

        AlterEgo.transform.localPosition = tmpPos;
        AlterEgo.transform.localRotation = tmpQuat;

        if (!NextWaypoint)
        {
            Debug.Log("Wanderer has no following waypoint. Destroying to prevent error flood");
            Destroy(this);
            return;
        }

        

        nextWPpos = NextWaypoint.Pos();
	}
	
	// Update is called once per frame
	void Update () {
        if(Vector3.Distance(transform.position, nextWPpos) < 0.1)
        {
            NextWaypoint = NextWaypoint.neighbours[0];
            nextWPpos = NextWaypoint.Pos();
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, nextWPpos, speed);

            // Painting System ***************

            Vector3 tmpPos = new Vector3();
            tmpPos.x = transform.localPosition.x;
            tmpPos.y = transform.localPosition.z;

            AlterEgo.transform.localPosition = tmpPos;
        }
        

	}
    /*
    Waypoint NearestWP()
    {
        for(int i = 0; i < )
    }*/

}
