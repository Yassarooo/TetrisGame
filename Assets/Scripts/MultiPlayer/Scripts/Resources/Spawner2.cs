using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Mirror;

public class Spawner2 :MonoBehaviour
{
    public GameObject[] blocks;
    public Block2 activeBlock;
    public GameObject activeObject;
    public GameObject nextObjectPanel;
    public Transform pos;
    public Transform Nextpos;
    public Vector2 speed = new Vector2 (1f, 1f);
    public bool FallDown;
    public GameObject Clone;
    int randomIndex ;
    System.Random random = new System.Random ();
    void Start () {
        CreateNewBlock ();
    }

    void Update () {
    }

    
    int r = -1;
    public void CreateNewBlock (bool useNext = false) {
        r = -1;
        if (useNext)
            r = randomIndex;

        if (r.Equals (-1))
            r = random.Next(blocks.ToList ().Count ());
        Debug.LogError(r);
        activeObject = blocks[r];

        activeBlock = activeObject.GetComponent<Block2>();
        
        //block
        activeBlock = Instantiate(activeBlock, new Vector3(pos.transform.position.x, pos.transform.position.y+4, pos.transform.position.z+1), Quaternion.identity);
        randomIndex = random.Next(blocks.ToList ().Count ());
        Destroy(Clone);
        //block
        Clone = Instantiate (blocks[randomIndex], new Vector3(Nextpos.transform.position.x - 13, Nextpos.transform.position.y, Nextpos.transform.position.z+1), Quaternion.identity);
        //PlayerPrefs.SetInt ("nextBlockIndex", randomIndex);

        /* GameObject newshadow = Instantiate (shadows[r], spawnpoint, Quaternion.identity);
        newshadow.GetComponent<shadowblock> ().SetParent (Clone); */
        
        /*foreach (Transform child in nextObjectPanel.transform) {
            child.gameObject.SetActive (false);
            if (child.name == blocks[PlayerPrefs.GetInt ("nextBlockIndex")].name) {
                child.gameObject.SetActive (true);
            }
        }*/
    }

     
    }