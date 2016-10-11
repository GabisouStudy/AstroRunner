using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class InputMouse : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private bool jump, shrink, up;
    public static bool tuto, menu;

    void Start()
    {
        menu = true;
    }

    public bool GetShrink() { return shrink; }
    public bool GetJump() { return jump; }
    public void SetJump(bool j) { jump = j; }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!menu)
        {
            tuto = false;
            if (up) jump = true;
            else shrink = true;
        }
        else
        {
            menu = false;
            tuto = true;
            this.gameObject.SetActive(false);
        }
            
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (!menu)
        {
            if (up) jump = false;
            else shrink = false;
        }
    }
}
