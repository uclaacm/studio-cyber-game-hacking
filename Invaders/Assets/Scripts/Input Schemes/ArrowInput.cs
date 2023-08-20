using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowInput : MonoBehaviour, IUnitInput
{
    public Vector2 inputVector => new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));


}
