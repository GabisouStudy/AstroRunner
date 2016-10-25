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
    void Update() 
    { 

       if (Input.anyKeyDown && !Input.GetMouseButtonDown(0))
       {
            if (!menu)
            {
                tuto = false;
            }
            else
            {
                if (this.tag.Equals("EditorOnly"))
                {
                    this.gameObject.SetActive(false);
                    menu = false;
                    tuto = true;
                }
            }
        }
    }
    public bool GetShrink() { return shrink; }
    public bool GetJump() { return jump; }
    public void SetJump(bool j) { jump = j; }
    public void SetShrink(bool j) { shrink = j; }

    public void ExitTutorial()
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
    public void OnPointerDown(PointerEventData eventData)
    {
        ExitTutorial();
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
