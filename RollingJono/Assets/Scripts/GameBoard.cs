using UnityEngine;

public class GameBoard : MonoBehaviour
{
    public float sensitivity = 25F;
    public Transform sourceVector;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        JoyControl();
        MotionControl();
    }

    private void JoyControl()
    {
        float pitch = Input.GetAxis("Vertical");
        float yaw = Input.GetAxis("Yaw");
        float roll = Input.GetAxis("Horizontal");

        rb.MoveRotation(transform.rotation * CameraRelativeRotation(pitch, yaw, roll));
    }

    private Quaternion CameraRelativeRotation(float pitch, float yaw, float roll)
    {
        if (!sourceVector)
            return Quaternion.Euler(new Vector3(pitch, yaw, -roll) * sensitivity * Time.deltaTime);

        Vector3 relativePitch = sourceVector.TransformDirection(Vector3.right);
        Vector3 relativeYaw = sourceVector.TransformDirection(Vector3.down);
        Vector3 relativeRoll = sourceVector.TransformDirection(Vector3.back);

        Vector3 objectRelavivePitch = transform.InverseTransformDirection(relativePitch);
        Vector3 objectRelativeYaw = transform.InverseTransformDirection(relativeYaw);
        Vector3 objectRelaviveRoll = transform.InverseTransformDirection(relativeRoll);

        return Quaternion.AngleAxis(pitch * sensitivity * Time.deltaTime, objectRelavivePitch)
             * Quaternion.AngleAxis(-yaw * sensitivity * Time.deltaTime, objectRelativeYaw)
             * Quaternion.AngleAxis(roll * sensitivity * Time.deltaTime, objectRelaviveRoll);
    }

    private void MotionControl()
    {
    }
}
