using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour {

    public Vector2 TerrainSize;
    public Transform TerrainViewer;

    public GameObject RoadObject;

    private Camera RTcam;
    private RenderTexture myRT;



	// Use this for initialization
	void Start () {

        // Positionning and Resizing Cam
        RTcam = GetComponentInChildren<Camera>();
        // RTcam.aspect = 1.0f; // Setting Camera Aspect to 1:1 (instead of window related) (legacy, as aspect depends of terrainSize)
        RTcam.orthographicSize = TerrainSize.y / 2; // CamSize is half the width of the camera. Each Terrain case equals One in width&Height.
        RTcam.aspect = TerrainSize.y / TerrainSize.x; // set aspect according to terrain size.
        
        
        Vector3 TempPos = this.transform.position + new Vector3(TerrainSize.x / 2, TerrainSize.y / 2, 0.0f);
        RTcam.transform.position = TempPos;


        //Set RenderTexture

        float tmpMax = Mathf.Max(TerrainSize.x, TerrainSize.y);
        for(int i = 0; i < 13; i++)
        {
            float tmpSquare = Mathf.Pow(2.0f, i);
            if(tmpSquare > 2*tmpMax)
            {
                tmpMax = Mathf.Pow(2.0f, i - 1);
                break;
            }
        }

        myRT = new RenderTexture(tmpMax, tmpMax, 8);
        //myRT.enableRandomWrite = true;
        myRT.Create();

        TerrainViewer.GetComponent<Renderer>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void InstantiateRoad(Vector3 UVPos)
    {
        

        Instantiate(RoadObject, this.transform.position + UVPos , Quaternion.identity, this.transform);
    }

}
