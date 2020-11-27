using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadowblock : MonoBehaviour {
    GameObject parent;
    Block parentTetris;

    // Start is called before the first frame update
    void Start () {
        StartCoroutine (RepositionBlock ());
    }

    public void SetParent (GameObject _parent) {
        parent = _parent;
        parentTetris = parent.GetComponent<Block> ();

    }

    void positionShadow () {
        transform.position = parent.transform.position;
        transform.rotation = parent.transform.rotation;
    }

    IEnumerator RepositionBlock () {
        while (parentTetris.enabled) {
            positionShadow ();
            // move downwards
            MoveDown ();

            yield return new WaitForSeconds (0.1f);
        }
        Destroy (gameObject);
        yield return null;

    }

    void MoveDown () {
        while (CheckValidMove ()) {
            transform.position += Vector3.down;
        }
        if (!CheckValidMove ()) {
            transform.position += Vector3.up;
        }

    }

    bool CheckValidMove () {
        return Move.ValidMove(this.transform);
        /* foreach (Transform children in transform) {
            int roundedX = Mathf.RoundToInt (children.transform.position.x);
            int roundedY = Mathf.RoundToInt (children.transform.position.y);
            int roundedZ = Mathf.RoundToInt (children.transform.position.z) - 1;

            if (roundedX < 0 || roundedX >= Grid.width || roundedY < 0 || roundedY >= GamePlayManager.instance.gridSizeY || roundedZ < 0 || roundedZ >= GamePlayManager.instance.gridSizeZ) {
                //GamePlayManager.instance.PlaySound (5);
                return false;
            }
            return true;

        }

        foreach (Transform children in transform) {
            int roundedX = Mathf.RoundToInt (children.transform.position.x);
            int roundedY = Mathf.RoundToInt (children.transform.position.y);
            int roundedZ = Mathf.RoundToInt (children.transform.position.z) - 1;

            Transform t;
            if (roundedY > GamePlayManager.instance.gridSizeY - 1) {
                t = null;
            } else {
                t = Grid.grid[(int) roundedX, (int) roundedY, (int) roundedZ];
            }

            if (t != null && t.parent == parent.transform) {
                return true;
            }

            if (t != null && t.parent != transform) {
                return false;
            }
        }
        return true; */

    }

}