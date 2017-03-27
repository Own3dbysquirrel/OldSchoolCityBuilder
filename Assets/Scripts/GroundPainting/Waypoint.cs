using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {

    public List<Waypoint> neighbours;

    Vector3 WPpos;

	// Use this for initialization
	void Start () {

        // **** Idea ***** InEditor RayTrace to snap Waypoints? -> Useless, 'coz waypoints are procedurally placed!
        WPpos = transform.position;
	}

    bool Init(List<Waypoint> nearestPoints)
    {
        neighbours.Clear();
        neighbours.AddRange(nearestPoints);

        return true;
    }

    bool removePoint(Waypoint Point)
    {
        return neighbours.Remove(Point);
        /*
        for (int i = 0; i < neighbours.Count(); i++)
        {
            if (neighbours[i] == Point)
            {
                neighbours.Remove(Point);
                return false;
            }
        }
        */
    }

    bool AddPoint(Waypoint Point)
    {
        if (neighbours.Count >= 4)
        {
            Debug.Log("NeighbourList >= 4");
            return false;
        }

        for (int i = 0; i < neighbours.Count; i++)
        {
            if(neighbours[i] == Point)
            {
                Debug.Log("Neighbour already in list");
                return false;
            }
        }

        neighbours.Add(Point);
        return true;
    }

    public Vector3 Pos()
    {
        return WPpos;
    }


    // Update is called once per frame
    void Update () {
		
	}
}
