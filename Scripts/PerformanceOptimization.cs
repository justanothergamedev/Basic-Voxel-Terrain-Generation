using UnityEngine;
using System.Collections;

public class PerformanceOptimization : MonoBehaviour {

    private MeshRenderer render;

	// Use this for initialization
	void Awake () {

        render = GetComponent<MeshRenderer> ();

	}

    void OnBecameInvisible() 
    {
        render.receiveShadows = false;
    }
    void OnBecameVisible() 
    {
        render.receiveShadows = true;
    }
}
