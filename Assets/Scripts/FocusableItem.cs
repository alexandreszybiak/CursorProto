using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusableItem : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject masterCursor;

    public GameObject stroke;
    private BoxCollider2D boxCollider;
    private BoxCollider2D cursorCollider;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>(); 
        cursorCollider = masterCursor.GetComponent<BoxCollider2D> ();
    }

    // Update is called once per frame
    void Update()
    {
        if ( boxCollider.OverlapPoint (masterCursor.transform.position) ) {
            stroke.SetActive (true);
            masterCursor.GetComponent<Cursor> ().currentItem = this;
        }
        else {
            stroke.SetActive (false);
            masterCursor.GetComponent<Cursor> ().currentItem = null;
        }
    }
}
