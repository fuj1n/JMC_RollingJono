using UnityEngine;

public class GameBoard : MonoBehaviour
{
    public float sensitivity = 25F;
    public Camera pivotCamera;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        JoyControl();
    }

    private void JoyControl()
    {
        float pitch = Input.GetAxis("Horizontal");
        float yaw = Input.GetAxis("Yaw");
        float roll = Input.GetAxis("Vertical");

        rb.MoveRotation(CameraRelativeRotation(pitch, yaw, roll));
    }

    private Quaternion CameraRelativeRotation(float pitch, float yaw, float roll)
    {
        if (!pivotCamera)
            return transform.rotation * Quaternion.Euler(new Vector3(roll, yaw, -pitch) * sensitivity * Time.deltaTime);

        Vector3 relativePitch = pivotCamera.transform.TransformDirection(Vector3.down);
        Vector3 relativeYaw = pivotCamera.transform.TransformDirection(Vector3.back);
        Vector3 relativeRoll = pivotCamera.transform.TransformDirection(Vector3.right);

        Vector3 objectRelativePitch = transform.InverseTransformDirection(relativePitch);
        Vector3 objectRelaviveYaw = transform.InverseTransformDirection(relativeYaw);
        Vector3 objectRelaviveRoll = transform.InverseTransformDirection(relativeRoll);

        return transform.rotation
                * Quaternion.AngleAxis(pitch * sensitivity * Time.deltaTime, objectRelativePitch)
                * Quaternion.AngleAxis(yaw * sensitivity * Time.deltaTime, objectRelaviveYaw)
                * Quaternion.AngleAxis(roll * sensitivity * Time.deltaTime, objectRelaviveRoll);
    }
}
