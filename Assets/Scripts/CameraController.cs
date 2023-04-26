using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform target;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate the parent object (which the camera is a child of) around the target object
        transform.parent.RotateAround(target.position, Vector3.up, mouseX);

        // Rotate the camera around the target object
        transform.RotateAround(target.position, transform.right, -mouseY);
    }
}
