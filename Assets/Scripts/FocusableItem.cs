using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusableItem : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject stroke;

    void Start()
    {
        //cursorCollider = masterCursor.GetComponent<BoxCollider2D> ();
    }

    // Update is called once per frame
    void Update() {
        Focus (false);
        /*var cursors = FindObjectOfType(typeof(Cursor));

        for ( int i = 0; i < cursors.Length; i++) {
            if ( boxCollider.OverlapPoint (masterCursor.transform.position) ) {
                stroke.SetActive (true);

            }
            else {
                stroke.SetActive (false);
                
            }
        }*/
    }

    public void Focus(bool value) {
        stroke.SetActive (value);
    }
}
