using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roadpaint : MonoBehaviour {

    public TerrainManager terrainManager;

    private Vector2 TerrainSize;

    List<bool> ClickPressed = new List<bool>();
    Vector3 LastMousePos;
    Vector3 LastUVHitSpot;

    	// Use this for initialization
	void Start () {

        TerrainSize = terrainManager.TerrainSize;

        bool temp = new bool();
        temp = false;
        ClickPressed.Add(temp);

        LastMousePos = Input.mousePosition;
        LastUVHitSpot = new Vector3();
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0)) // is pressed from now
        {
            Debug.Log("pressed right click.");
            ClickPressed[0] = true;
        }


        if (Input.GetMouseButtonUp(0)) // has stopped being pressed
        {
            ClickPressed[0] = false;
        }

        if (ClickPressed[0]) // If pressed
        {
            Debug.Log("entering");
            if (LastMousePos != Input.mousePosition) // If mouse moved
            {
                LastMousePos = Input.mousePosition; //store it

                Vector3 InstancingUVPos = new Vector3();
                if (HitTestUVPosition(ref InstancingUVPos)) //raycasting
                {
                    InstancingUVPos.x = Mathf.Floor(InstancingUVPos.x * TerrainSize.x); // flooring to case
                    InstancingUVPos.y = Mathf.Floor(InstancingUVPos.y * TerrainSize.y);

                    if (InstancingUVPos != LastUVHitSpot) // optimisation: only raytrace when aiming another case
                    {
                        Debug.Log("hit!");
                        terrainManager.InstantiateRoad(InstancingUVPos);
                        LastUVHitSpot = InstancingUVPos;
                    }
                    
                }
            }
                

           

        }
	}


   

    private bool HitTestUVPosition(ref Vector3 uvWorldPosition) // return pos of the hit on the collider's UV
    {
        RaycastHit hit;
        Vector3 mousePos = Input.mousePosition; //screenPos
        Vector3 cursorPos = new Vector3(mousePos.x, mousePos.y, 0.0f);

        Ray cursorRay = Camera.main.ScreenPointToRay(cursorPos); // ray



        if(Physics.Raycast(cursorRay, out hit, 20000))
        {
            MeshCollider meshCollider = hit.collider as MeshCollider;
            if (meshCollider == null || meshCollider.sharedMesh == null)
            {
                Debug.Log("has no UV");

                return false; // Si le collider n'a pas de mesh, c'est une erreur!
            }

            Vector2 pixelUV = new Vector2(hit.textureCoord.x, hit.textureCoord.y); // On choppe la position sur l'uv à l'endroit collidé

            uvWorldPosition.x = pixelUV.x; // 
            uvWorldPosition.y = pixelUV.y; // 
            uvWorldPosition.z = 0.0f; // Out this
            return true;
        }
        else
        {
            return false;
        }
    }
}
