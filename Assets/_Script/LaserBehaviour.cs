using UnityEngine;
using System.Collections;

public class LaserBehaviour : MonoBehaviour
{
    [Header("Laser pieces")]
    public GameObject laserStart;
    public GameObject laserMiddle;
    public GameObject laserEnd;

    private GameObject start;
    private GameObject middle;
    private GameObject end;

    void Update()
    {
        // Create the laser start from the prefab
        if (start == null)
        {
            start = Instantiate(laserStart) as GameObject;
            start.transform.parent = this.transform;
            start.transform.localPosition = Vector2.zero;
        }

        // Laser middle
        if (middle == null)
        {
            middle = Instantiate(laserMiddle) as GameObject;
            middle.transform.parent = this.transform;
            middle.transform.localPosition = Vector2.zero;
        }


        start.transform.eulerAngles = new Vector3(0, 0, 0);
        middle.transform.eulerAngles = new Vector3(0, 0, 0);
        // Define an "infinite" size, not too big but enough to go off screen
        float maxLaserSize = 20f;
        float currentLaserSize = maxLaserSize;

        // Raycast at the right as our sprite has been design for that
        Vector2 laserDirection = this.transform.right;
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, laserDirection, maxLaserSize);

        if (hit.collider != null)
        {
            // We touched something!

            // -- Get the laser length
            if (hit.collider.gameObject.tag == "Player")
                hit.collider.gameObject.GetComponent<Player>().dead = true;
            currentLaserSize = Vector2.Distance(hit.point, this.transform.position);

            // -- Create the end sprite
            if (end == null)
            {
                end = Instantiate(laserEnd) as GameObject;
                end.transform.parent = this.transform;
                end.transform.localPosition = Vector2.zero;
            }
        }
        else
        {
            // Nothing hit
            // -- No more end
            if (end != null) Destroy(end);
        }

        // Place things
        // -- Gather some data
        float startSpriteWidth = start.GetComponent<Renderer>().bounds.size.x;
        float endSpriteWidth = 0f;
        if (end != null) endSpriteWidth = end.GetComponent<Renderer>().bounds.size.x;

        // -- the middle is after start and, as it has a center pivot, have a size of half the laser (minus start and end)
        middle.transform.localScale = new Vector3(currentLaserSize - startSpriteWidth, middle.transform.localScale.y, middle.transform.localScale.z);
        middle.transform.localPosition = new Vector2((currentLaserSize / 2f), 0f);

        // End?
        if (end != null)
        {
            end.transform.localPosition = new Vector2(currentLaserSize, 0f);
        }

        end.transform.eulerAngles = new Vector3(0, 0, 0);

        start.transform.localRotation = Quaternion.Euler(0, 0, 0);
        middle.transform.localRotation = Quaternion.Euler(0, 0, 0);
        end.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}