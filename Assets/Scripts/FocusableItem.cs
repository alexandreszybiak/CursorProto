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
        int numColliders = 10;
        Collider2D[] colliders = new Collider2D[numColliders];
        ContactFilter2D contactFilter = new ContactFilter2D ();
        // Set you filters here according to https://docs.unity3d.com/ScriptReference/ContactFilter2D.html

        if ( boxCollider.OverlapCollider (contactFilter, colliders) > 0 ) {
            stroke.SetActive (true);
        }
        else {
            stroke.SetActive (false);
        }
    }
}
