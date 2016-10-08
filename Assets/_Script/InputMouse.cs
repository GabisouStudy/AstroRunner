using UnityEngine;
using System.Collections;

public class InputMouse : MonoBehaviour {

    public bool jump;
    public bool shrink;
    public bool up;

    void OnMouseDown()
    {
        if (up) jump = true;
        else shrink = true;
    }
    void OnMouseUp()
    {
        if (up) jump = false;
        else shrink = false;
    }
}
