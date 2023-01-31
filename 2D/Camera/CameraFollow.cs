using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ViewBounds
{
    public float minX;
    public float minY;
    public float maxX;
    public float maxY;
}

public class CameraFollow : MonoBehaviour
{
    // The camera's target
    public Transform target;

    // Distance the player can move before the camera follows
    public float xMargin = 1f;
    public float yMargin = 1f;

    // How smoothly the camera catches up with it's target movement
    public float xSmooth = 8f;
    public float ySmooth = 8f;

    // Boundary for camera
    public ViewBounds viewBounds = new ViewBounds();

    private void FixedUpdate()
    {
        TrackTarget();
    }

    private void TrackTarget()
    {
        // Set initial target to Camera's current position
        float targetX = transform.position.x;
        float targetY = transform.position.y;

        // Linear interpolation or Lerp used to smooth motion
        if (Mathf.Abs(target.position.x - transform.position.x) > xMargin)
        {
            targetX = Mathf.Lerp(transform.position.x, target.position.x, xSmooth * Time.deltaTime);
        }
        if (Mathf.Abs(target.position.y - transform.position.y) > yMargin)
        {
            targetY = Mathf.Lerp(transform.position.y, target.position.y, ySmooth * Time.deltaTime);
        }

        // Keep Camera target within boundaries with Clamp
        targetX = Mathf.Clamp(targetX, viewBounds.minX, viewBounds.maxX);
        targetY = Mathf.Clamp(targetY, viewBounds.minY, viewBounds.maxY);

        // Move the camera
        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }
}
