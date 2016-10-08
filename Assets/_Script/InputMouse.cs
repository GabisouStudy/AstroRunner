using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class InputMouse : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public bool jump;
    public bool shrink;
    public bool up;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (up) jump = true;
        else shrink = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (up) jump = false;
        else shrink = false;
    }
}
