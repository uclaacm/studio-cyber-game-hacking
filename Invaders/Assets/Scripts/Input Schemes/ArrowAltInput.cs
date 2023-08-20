using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAltInput : MonoBehaviour, IUnitInput
{
    public Vector2 inputVector => new Vector2(Input.GetAxisRaw("Alt_Horizontal"), Input.GetAxisRaw("Alt_Vertical"));
}
