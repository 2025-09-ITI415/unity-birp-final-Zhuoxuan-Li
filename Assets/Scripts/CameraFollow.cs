using UnityEngine;

public class LeanCameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 1.6f, -3f);
    public float smoothSpeed = 10f;
    public float mouseSensitivity = 2f;
    private float pitch = 0f;

    void LateUpdate()
    {
        if (!target) return;


        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -40f, 60f);


        Vector3 desiredPos = target.position + target.TransformDirection(offset);
        transform.position = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);


        transform.rotation = Quaternion.Euler(pitch, target.eulerAngles.y, 0f);
    }
}
