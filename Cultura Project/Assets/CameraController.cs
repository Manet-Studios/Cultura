using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float sprintModifier;

    [SerializeField]
    private float zoom;

    [SerializeField]
    private Vector2 zoomBounds;

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
        zoom = cam.orthographicSize;
    }

    private void Update()
    {
        float effectiveSpeed = moveSpeed * (Input.GetKey(KeyCode.LeftShift) ? sprintModifier : 1) * Time.deltaTime;
        cam.transform.position += new Vector3(Input.GetAxis("Horizontal") * effectiveSpeed, Input.GetAxis("Vertical") * effectiveSpeed);
        zoom = Mathf.Clamp(zoom + Input.GetAxis("Mouse ScrollWheel"), zoomBounds.x, zoomBounds.y);
        cam.orthographicSize = zoom;
    }
}