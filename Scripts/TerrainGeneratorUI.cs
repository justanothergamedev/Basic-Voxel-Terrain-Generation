using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TerrainGeneratorUI : MonoBehaviour 
    {
    private int xLimit;
    private int zLimit;
    private int yDepth;
    private int yHeight;
    private bool spawnTrees;
    private int spawnFrequency;
    
    //BLOCK & TREE
    public GameObject Block;
    public GameObject Tree;

    private int xi;
    private int zi;

    //UI
    private Slider xS;
    private Slider zS;
    private Slider dL;
    private Slider hL;
    private Toggle tT;
    private Slider sF;
    private Text xSText;
    private Text zSText;
    private Text dLText;
    private Text hLText;
    private Text sFText;

    void Awake ()
    {
        xS = GameObject.Find ("X Slider").GetComponent<Slider> ();
        zS = GameObject.Find ("Z Slider").GetComponent<Slider> ();
        dL = GameObject.Find ("Y Depth Slider").GetComponent<Slider> ();
        hL = GameObject.Find ("Y Height Slider").GetComponent<Slider> ();
        tT = GameObject.Find ("Trees Toggle").GetComponent<Toggle> ();
        sF = GameObject.Find ("Spawn Frequency Slider").GetComponent<Slider> ();
        xSText = GameObject.Find ("xValue").GetComponent<Text> ();
        zSText = GameObject.Find ("zValue").GetComponent<Text> ();
        dLText = GameObject.Find ("dLValue").GetComponent<Text> ();
        hLText = GameObject.Find ("hLValue").GetComponent<Text> ();
        sFText = GameObject.Find ("tFValue").GetComponent<Text> ();
    }
	
	// Update is called once per frame
	void Update () 
    {
        xLimit = Mathf.RoundToInt(xS.value);
        zLimit = Mathf.RoundToInt(zS.value);
        yDepth = Mathf.RoundToInt(dL.value);
        yHeight = Mathf.RoundToInt(hL.value);
        spawnTrees = tT.isOn;
        spawnFrequency = Mathf.RoundToInt (sF.value);
        xSText.text = xS.value.ToString ();
        zSText.text = zS.value.ToString ();
        dLText.text = dL.value.ToString ();
        hLText.text = hL.value.ToString ();
        sFText.text = sF.value.ToString ();
        Destroy (GameObject.Find ("Block(Clone"));
        Destroy (GameObject.Find ("Tree(Clone"));
	}

    public void GenerateTerrain () 
    {
        StartCoroutine (TerrainGeneration());
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

        //yPos = Mathf.RoundToInt(Random.Range (yDepth, yHeight));

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