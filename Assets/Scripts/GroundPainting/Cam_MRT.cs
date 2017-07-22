using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_MRT : MonoBehaviour {

    public RenderTexture RT_blurry;

    private Camera itCam;

	// Use this for initialization
	void Start () {
        itCam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {

        // Just duplicate the image into another RT, coz that RT has filtering...

        if (!itCam || !itCam.targetTexture || !RT_blurry)
        {
            Debug.Log("ERrooor");
        }

        Graphics.Blit(itCam.targetTexture, RT_blurry);
    }

    
}
