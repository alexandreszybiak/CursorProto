using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler :MonoBehaviour {
    private PlayerInput playerInput;
    private Cursor cursor;
    private void Awake() {
        playerInput = GetComponent<PlayerInput> ();
        var cursors = FindObjectsOfType<Cursor> ();
        var index = playerInput.playerIndex;
        cursor = cursors.FirstOrDefault (m => m.GetPlayerIndex () == index);
    }

    public void OnMove(CallbackContext context) {
        if ( cursor != null ) {
            if ( context.performed ) {
                cursor.SetInputVector (context.ReadValue<Vector2> ());
            }
            else if ( context.canceled ) {
                cursor.SetInputVector (Vector2.zero);
            }
        }
    }
    public void ToggleSnap(CallbackContext context) {
        if ( context.performed ) {
            if ( cursor != null ) {
                cursor.ToggleSnap ();
            }
        }
    }

    public void GoToNextPage() {
        if ( cursor != null ) {
            cursor.GotoNextPage ();
        }
    }
    public void MoveGrid(CallbackContext context) {
        if ( context.performed ) {
            cursor.FindNearestNeighbour (context.ReadValue<Vector2> (), true);
        }
    }
}