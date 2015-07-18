using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TerrainGenerator : MonoBehaviour 
    {
    [Range(2,150)]
    public int xLimit;
    [Range(2,150)]
    public int zLimit;
    [Range(-10,0)]
    public int yDepth;
    [Range(0,10)]
    public int yHeight;
    public bool spawnTrees;
    [Range(20,50)]
    public int spawnFrequency;
    
    //BLOCK & TREE
    public GameObject Block;
    public GameObject Tree;

    private int xi;
    private int zi;
	
	// Update is called once per frame
	void Update () 
    {
        Destroy (GameObject.Find ("Tree(Clone"));
        if (Input.GetButtonDown ("Jump"))
        {
            StartCoroutine (TerrainGeneration());  
        }
	}

    IEnumerator TerrainGeneration ()
    {
        GameObject[] Terrain;
        Terrain = GameObject.FindGameObjectsWithTag ("Terrain");
        for (int i = 0; i < Terrain.Length; i++)
        {
            Destroy (Terrain [i]);
        }

        int lastXY = 0;
        int lastZY = 0;
        //MAKE CUBES ALONG THE X-AXIS & Y AXIS
        for (xi = 0; xi < xLimit; xi++ )
        {
            GameObject xcube = Instantiate (Block, new Vector3 (xi, Random.Range (lastXY -1, lastXY + 2), zi), Quaternion.identity) as GameObject;
            if (xcube.transform.position.y < yDepth)
            {
                xcube.transform.position = new Vector3 (xi, yDepth, zi);
            }
            if (xcube.transform.position.y > yHeight)
            {
                xcube.transform.position = new Vector3 (xi, yHeight, zi);
            }
            lastXY = Mathf.RoundToInt(xcube.transform.position.y);
            lastZY = lastXY;
            xcube.tag = "Terrain";
            for (zi = 0; zi < zLimit; zi++)
            {
                //MAKE CUBES ALONG THE Z AXIS & Y AXIS
                GameObject zcube = Instantiate (Block, new Vector3 (xi, Random.Range (lastZY -1, lastZY + 1), zi), Quaternion.identity) as GameObject;
                if (zcube.transform.position.y < yDepth)
                {
                    zcube.transform.position = new Vector3 (xi, yDepth, zi);
                }
                if (zcube.transform.position.y > yHeight)
                {
                    zcube.transform.position = new Vector3 (xi, yHeight, zi);
                }
                //SPAWN TREES
                while (spawnTrees)
                {
                    int foliageGen = Random.Range (0, spawnFrequency / 2);
                    if (foliageGen == 0)
                    {
                        GameObject tree = Instantiate (Tree, new Vector3 (xi, zcube.transform.position.y + 2, zi), Quaternion.identity) as GameObject;
                        tree.tag = "Terrain";
                    }
                    break;
                }
                zcube.tag = "Terrain";
            }
            yield return new WaitForEndOfFrame ();          
        }
    }
}