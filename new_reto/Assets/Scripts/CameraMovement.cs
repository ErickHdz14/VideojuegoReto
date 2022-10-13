using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    public float zoomStep, minCamSize, maxCamSize;

    private Vector3 dragOrigin;

    private void Update()
    {
        PanCamera();

        if (Input.mouseScrollDelta.y< 0)
        {
            ZoomOut();
        }

        if (Input.mouseScrollDelta.y> 0)
        {
            ZoomIn();
        }
    }

    private void PanCamera()
    {
        //save position of mouse in world space when drag stars (first time clicked)
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);

            cam.transform.position += difference;
        }

    }

    private void ZoomOut()
    {
        float newSize = cam.orthographicSize + zoomStep;
        cam.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);
    }

    private void ZoomIn()
    {
        float newSize = cam.orthographicSize - zoomStep;
        cam.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);
    }

}
