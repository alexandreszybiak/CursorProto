using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSet : MonoBehaviour
{
    public FocusableItem itemType;
    public int numberOfRow;
    public int numberOfColumn;
    public Vector2 spacing;

    private FocusableItem[] set;

    void Start()
    {
        InitSet ();
    }

    void InitSet() {
        for(var i = 0; i < numberOfRow; i++ ) {
            for(var j = 0; j < numberOfColumn; j++ ) {
                FocusableItem newItem = Instantiate (itemType, new Vector3 (i * spacing.x, j*spacing.y, 0 ), Quaternion.identity);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
