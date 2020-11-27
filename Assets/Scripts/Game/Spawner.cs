using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour {
    #region Singleton
    public static Spawner instance;
    public Spawner () {
        instance = this;
    }
    #endregion

    public GameObject[] blocks;
    public GameObject[] shadows;
    public Block activeBlock;
    public GameObject nextObjectPanel;
    public GameObject Clone;
    public GameObject shadow;
    public bool FallDown;

    public int gridSizeX;
    public int gridSizeY;
    public int gridSizeZ;

    System.Random random = new System.Random ();
    void Start () {
        CreateNewBlock ();
        /* for (int i = GamePlayManager.instance.gridSizeY - 1; i >= 0; i--) {
            for (int u = 0; u < Grid.width; u++) {
                Grid.grid[i, u, 1] = activeObject.transform.GetChild (0);
            }
        } */
    }
    void Awake () {
        Grid.grid = new Transform[gridSizeX, gridSizeY, gridSizeZ];
    }
    void Update () {
        if (Input.GetKeyDown (KeyCode.Space)) {
            HoldBlock ();
        }
    }

    public void CreateNewBlock (bool useNext = false) {

        Vector3 spawnpoint = new Vector3 ((int) (transform.position.x + (float) Spawner.instance.gridSizeX / 2),
            (int) transform.position.y + Spawner.instance.gridSizeY,
            (int) (transform.position.z + (float) Spawner.instance.gridSizeZ / 2));

        int r = -1;
        if (useNext)
            r = PlayerPrefs.GetInt ("nextBlockIndex");

        if (r.Equals (-1))
            r = random.Next (blocks.ToList ().Count ());
        activeBlock = blocks[r].GetComponent<Block> ();
        PlayerPrefs.SetInt ("activeBlockIndex", r);

        int randomIndex = random.Next (blocks.ToList ().Count ());

        //block
        Clone = Instantiate (blocks[r], spawnpoint, Quaternion.identity);
        PlayerPrefs.SetInt ("nextBlockIndex", randomIndex);

        shadow = Instantiate (shadows[r], spawnpoint, Quaternion.identity);
        shadow.GetComponent<shadowblock> ().SetParent (Clone);
        
        foreach (Transform child in nextObjectPanel.transform) {
            child.gameObject.SetActive (false);
            if (child.name == blocks[PlayerPrefs.GetInt ("nextBlockIndex")].name) {
                child.gameObject.SetActive (true);
            }
        }

    }

    public void HoldBlock () {
        Vector3 pos = Clone.transform.position;
        GameObject.Destroy (Clone);
        GameObject.Destroy (shadow);
        Clone = blocks[PlayerPrefs.GetInt ("nextBlockIndex")];
        Clone = Instantiate (Clone, pos, Quaternion.identity);


        shadow = shadows[PlayerPrefs.GetInt ("nextBlockIndex")];
        shadow = Instantiate (shadow, pos, Quaternion.identity);
        shadow.GetComponent<shadowblock> ().SetParent (Clone);

        int tmp = PlayerPrefs.GetInt ("nextBlockIndex");
        PlayerPrefs.SetInt ("nextBlockIndex", PlayerPrefs.GetInt ("activeBlockIndex"));
        PlayerPrefs.SetInt ("activeBlockIndex", tmp);

        

        foreach (Transform child in nextObjectPanel.transform) {
            child.gameObject.SetActive (false);
            if (child.name == blocks[PlayerPrefs.GetInt ("nextBlockIndex")].name) {
                child.gameObject.SetActive (true);
            }
        }
    }
}