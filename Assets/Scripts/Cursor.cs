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

    private Vector2 movement;
    private Vector2 targetPosition;

    public FocusableItem currentItem = null;
    


    void Awake()
    {
        targetPosition = new Vector2(0,0);

        gameActions = new MyGameActions ();

        gameActions.Player.Move.performed += ctx => movement = ctx.ReadValue<Vector2> ();
        gameActions.Player.Move.canceled += ctx => movement = Vector2.zero;
        gameActions.Player.MoveSnap.performed += ctx => FindNearestNeighbour(ctx);
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
        
    }

    private void FindNearestNeighbour(InputAction.CallbackContext context) {
        Vector2 direction = context.ReadValue<Vector2> ();

        print (direction);

        RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.right);
        Debug.DrawRay (transform.position, direction * 100, Color.red, 20.0f, false);
        if ( hit.collider != null) {
            //print (currentItem.name);
            if(hit.collider != currentItem) {
                print (hit.distance);
            }
        }
    }
}
