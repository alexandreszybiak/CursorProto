using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSBoundariesOrthographic : MonoBehaviour
{
    private Camera MainCamera;
    public GameObject sprite;

    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    // Use this for initialization
    void Start() {
        MainCamera = Camera.main;
        screenBounds = MainCamera.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, MainCamera.transform.position.z));
        objectWidth = sprite.GetComponent<SpriteRenderer> ().bounds.extents.x; //extents = size of width / 2
        objectHeight = sprite.GetComponent<SpriteRenderer> ().bounds.extents.y; //extents = size of height / 2
    }

    // Update is called once per frame
    void LateUpdate() {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp (viewPos.x, screenBounds.x * -1 + objectWidth - sprite.transform.localPosition.x, screenBounds.x - objectWidth - sprite.transform.localPosition.x);
        viewPos.y = Mathf.Clamp (viewPos.y, screenBounds.y * -1 + objectHeight - sprite.transform.localPosition.y, screenBounds.y - objectHeight - sprite.transform.localPosition.y);
        transform.position = viewPos;
    }
}