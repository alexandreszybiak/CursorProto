using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class Cursor : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 2f;
    public Transform initialTarget;
    public bool snap = false;
    public ContactFilter2D filter2D;
    public Transform cursorImageTransform;
    public Emote emote;
    public Transform stickerSpawner;
    public bool isMaster = false;

    private Vector2 movement;
    private Vector2 targetPosition;

    public FocusableItem currentItem = null;

    public ManagerPage managerPage = null;
    [SerializeField]
    private int playerIndex = 0;



    void Awake() {
        if ( initialTarget != null ) {
            targetPosition = new Vector2 (initialTarget.position.x, initialTarget.position.y);
        }
        else {
        targetPosition = Vector2.zero;
        }

        if (snap)
        {
            LookAroundForSnap();
        }
    }

    private void Update() {
        if ( snap ) {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards (transform.position, targetPosition, step);
            cursorImageTransform.position = transform.position + new Vector3(movement.x * 0.15f, movement.y * 0.15f, 0);
        }
        else {
            transform.Translate (new Vector3 (movement.x, movement.y, 0) * speed * Time.deltaTime);
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, playerIndex - 6);

        currentItem = null;
        Collider2D[] results = new Collider2D[2];

        int numResult = Physics2D.OverlapPoint (new Vector2 (transform.position.x, transform.position.y), filter2D, results);
        
        if ( numResult > 0 ) {
            FocusableItem item = results[0].gameObject.GetComponent<FocusableItem>();
            currentItem = item;
            item.Focus (true);
        }

        

    }
    public void ToggleSnap() {
        snap = !snap;
        if ( snap ) {
            LookAroundForSnap ();
        }
    }
    public void LookAroundForSnap() {
        FindNearestNeighbour (Vector2.left);
        FindNearestNeighbour (Vector2.right);
        FindNearestNeighbour (Vector2.up);
        FindNearestNeighbour (Vector2.down);
        FindNearestNeighbour (Vector2.left + Vector2.up);
        FindNearestNeighbour (Vector2.left + Vector2.down);
        FindNearestNeighbour (Vector2.right + Vector2.up);
        FindNearestNeighbour (Vector2.right + Vector2.down);
    }
    public void FindNearestNeighbour(Vector2 inputDirection, bool ignoreCurrentItem = false) {
        Vector2 direction = inputDirection;

#if UNITY_WEBGL
        direction.y *= -1;
#endif

        RaycastHit2D[] hits = new RaycastHit2D[2];

        Vector2 startPosition = new Vector2 (transform.position.x, transform.position.y);

        int totalObjectsHit = Physics2D.Raycast (startPosition, direction, filter2D, hits);
        RaycastHit2D hit;
        //Iterate the objects hit by the laser
        for ( int i = 0; i < totalObjectsHit; i++ ) {
            hit = hits[i];
            //Do something
            if ( hit.collider != null ) {
                if(hit.collider.gameObject.GetComponent<FocusableItem>() != currentItem || ignoreCurrentItem == false) {
                    Transform pos = hit.collider.gameObject.GetComponent<FocusableItem>().cursorPoints[playerIndex];
                    targetPosition = new Vector2 (pos.position.x, pos.position.y);
                    break;
                }
            }
        }
    }

    public void SetInputVector(Vector2 direction) {
        movement = direction;
    }

    public int GetPlayerIndex() {
        return playerIndex;
    }

    public void GotoNextPage() {
        if(managerPage != null && isMaster ) {
            managerPage.GotoNextPage ();
        }
    }

    public void PlaceSticker()
    {
        emote.transform.position = stickerSpawner.transform.position;
    }
}
