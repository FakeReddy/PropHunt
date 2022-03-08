using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    protected float Horizontal { get => Input.GetAxisRaw(GlobalStringsVars.HorizontalAxis); }
    protected float Vertical { get => Input.GetAxisRaw(GlobalStringsVars.VerticalAxis); }
    protected bool Mouse0 { get => Input.GetKeyDown(KeyCode.Mouse0); }
    protected bool Mouse1 { get => Input.GetKeyDown(KeyCode.Mouse1); }
    protected bool Space { get => Input.GetKeyDown(KeyCode.Space); }
    protected bool Shift { get => Input.GetKey(KeyCode.LeftShift); }
}