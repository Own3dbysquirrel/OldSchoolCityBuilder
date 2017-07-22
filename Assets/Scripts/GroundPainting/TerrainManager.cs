using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour {

    public Vector2 MapSize;
    public Vector2 TempRatio;
    public static Vector2 TerrainSize;
    //public Transform TerrainViewer;

    private Camera RTcam;
    private RenderTexture myRT;

    public Transform TerrainRoot;
    public ComputeShader UVRescaler;

        //before Start, so that RoadPaint can access Terrain Size though being called on same event
    private void Awake()
    {
        TerrainSize = MapSize;
    }
    

    void Start() {
        
            // A Compute nearest superior square size
        int tmpMax = Mathf.Max((int)TerrainSize.x, (int)TerrainSize.y);
        for (int i = 0; i < 13; i++)  // 2pow13 = 8K
        {
            int tmpSquare = (int)Mathf.Pow(2, i);
            if (tmpSquare > tmpMax)
            {
                tmpMax = (int)Mathf.Pow(2, i);
                break;
            }
        }

        Debug.Log(tmpMax);

            // B Positionning Terrain

        Vector3 tmpSize = new Vector3(TerrainSize.x / TempRatio.x, 1.0f, TerrainSize.y / TempRatio.y);
        Vector3 tmpPos = new Vector3(TerrainSize.x / 2, 0.0f, TerrainSize.y / 2); // tmpMax / 2.0f - (tmpSize.x * tmpMax) / 2f, 0.0f, tmpMax / 2 - (tmpSize.y * tmpMax)/2.0f);
        TerrainRoot.localScale = tmpSize;
        TerrainRoot.position = tmpPos;

            // C Rescaling UVs

        Mesh tmpMesh = TerrainRoot.GetChild(0).GetComponent<MeshFilter>().mesh;

        Vector2[] tmpUV = tmpMesh.uv;
        Vector3[] tmpVerts = tmpMesh.vertices;

        for (int i = 0; i < tmpUV.Length; i++)
        {
            tmpVerts[i] = TerrainRoot.TransformPoint(tmpVerts[i]);
            tmpUV[i].x = (TerrainSize.x - tmpVerts[i].x) / tmpMax;
            tmpUV[i].y = (TerrainSize.y - tmpVerts[i].z) / tmpMax;
        }
        tmpMesh.uv = tmpUV;

        TerrainRoot.GetChild(0).GetComponent<MeshFilter>().mesh = tmpMesh;

    
            // D Compute Buffer Method (legacy because non-opti)
                
        //ComputeBuffer uvData = new ComputeBuffer(, );

        /*Vector4 UVBound = new Vector4(TerrainSize.x, TerrainSize.y, 0, 0);

        int Resc = UVRescaler.FindKernel("CSmain");

        UVRescaler.SetBuffer(Resc, "Vertex", uvData);
        UVRescaler.SetVector("Bounds", UVBound);

        UVRescaler.Dispatch(Resc, 4, 1, 1);
        */

            // E Positionning and Resizing Cam
        RTcam = GetComponentInChildren<Camera>();
        // RTcam.aspect = 1.0f; // Setting Camera Aspect to 1:1 (instead of window related) (legacy, as aspect depends of terrainSize)
        RTcam.orthographicSize = tmpMax / 2; // CamSize is half the width of the camera. Each Terrain case equals One in width&Height.
        //RTcam.aspect = TerrainSize.x / TerrainSize.y; // set aspect according to terrain size. -> No, coz texture is always Squared so keep it to One
        RTcam.aspect = 1.0f;
        
        Vector3 TempPos = this.transform.position + new Vector3(tmpMax / 2, tmpMax / 2, 0.0f);
        RTcam.transform.position = TempPos;


        // F Set RenderTexture ****************

                //Idées: Utiliser le depth buffer pour stocker plus d'infos? Pas bien, car c'est du HDR! (sauf si on arrive à écrire bit par bit)

        myRT = new RenderTexture(tmpMax, tmpMax, 8);
        myRT.filterMode = FilterMode.Point;
        //myRT.enableRandomWrite = true; // allows use for Compute Shaders.
        myRT.Create();
        RTcam.targetTexture = myRT;
        // RTcam.SetTargetBuffers(myRT.colorBuffer, myRT.depthBuffer); // Suite in Cam_MRT.cs

        //Apply Texture to terrain
        TerrainRoot.GetChild(0).GetComponent<Renderer>().material.SetTexture("_MainTex", myRT);

	}
	
    

	// Update is called once per frame
	void Update () {
		
	}
    

    public void InstantiateRoad(Vector3 UVPos)
    {
        

        GameObject Item = Instantiate((GameObject)Resources.Load("prefabs/Road"), this.transform.position + UVPos , Quaternion.identity, this.transform);
        Item.layer = 11; // 11 is MapManager

    }

}
