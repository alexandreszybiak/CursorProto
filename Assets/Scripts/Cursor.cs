using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cursor : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 2f;
    MyGameActions gameActions;
    public Transform initialTarget;
    public bool snap = false;
    public ContactFilter2D filter2D;

    private Vector2 movement;
    private Vector2 targetPosition;

    public FocusableItem currentItem = null;

    public ManagerPage managerPage = null;
    


    void Awake()
    {
        targetPosition = new Vector2(initialTarget.position.x, initialTarget.position.y);

        gameActions = new MyGameActions ();

        gameActions.Player.Move.performed += ctx => movement = ctx.ReadValue<Vector2> ();
        gameActions.Player.Move.canceled += ctx => movement = Vector2.zero;
        gameActions.Player.MoveSnap.performed += ctx => FindNearestNeighbour(ctx);
        gameActions.Player.ToggleSnap.performed += ctx => ToggleSnap ();
        gameActions.Player.Validate.performed += ctx => GoToNextPage ();
    }

    private void OnEnable() {
        gameActions.Player.Enable ();
    }

    private void OnDisable() {
        gameActions.Player.Disable ();
    }

    private void Update() {
        if ( snap ) {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards (transform.position, targetPosition, step);
        }
        else {
            transform.Translate (new Vector3 (movement.x, movement.y, 0) * speed * Time.deltaTime);
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, -2);

    }

    private void FindNearestNeighbour(InputAction.CallbackContext context) {
        Vector2 direction = context.ReadValue<Vector2> ();

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
                if(hit.collider != currentItem ) {
                    targetPosition = new Vector2 (hit.transform.position.x, hit.transform.position.y);
                }
            }
        }
        /*
        int numResults = 2;
        ContactFilter2D = new ContactFilter2D ();
        RaycastHit2D[] results;
        RaycastHit2D hit = Physics2D.Raycast (startPosition, direction, Mathf.Infinity, );
        if ( hit.collider != null) {
            if(currentItem != null) print (currentItem.name);
            if(hit.collider != currentItem) {
                targetPosition = new Vector2(hit.transform.position.x,hit.transform.position.y);
            }
        }

        Debug.DrawRay (new Vector2 (transform.position.x, transform.position.y), Vector2.left, Color.red, 0.5f);
        */

    }

    private void ToggleSnap() {
        snap = !snap;
    }

    private void GoToNextPage() {
        managerPage.GotoNextPage ();
    }
}
