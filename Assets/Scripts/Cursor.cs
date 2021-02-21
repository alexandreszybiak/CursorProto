using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cursor : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 0.25f;
    MyGameActions gameActions;

    private Vector2 movement;


    void Awake()
    {
        gameActions = new MyGameActions ();

        gameActions.Player.Move.performed += ctx => movement = ctx.ReadValue<Vector2> ();
        gameActions.Player.Move.canceled += ctx => movement = Vector2.zero;
    }

    private void OnEnable() {
        gameActions.Player.Enable ();
    }

    private void OnDisable() {
        gameActions.Player.Disable ();
    }

    private void Update() {
        transform.Translate (new Vector3(movement.x, movement.y, 0) * speed);
    }
}
