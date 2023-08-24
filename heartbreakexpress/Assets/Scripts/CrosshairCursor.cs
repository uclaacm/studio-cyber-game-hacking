using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CrosshairCursor : MonoBehaviour {
    Vector2 mousePos;
    
    // Start is called before the first frame update
    void Awake() {
        Cursor.visible = false;   // disable the cursor
    }

    // Update is called once per frame
    void Update() {
        
        // Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        // Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        
        // Move the gameObject with the cursor
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        transform.position = mousePos;
    }
}
