using UnityEngine;

public class GameBoard : MonoBehaviour
{
    public float sensitivity = 25F;

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
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        float yaw = Input.GetAxis("Yaw");

        rb.MoveRotation(transform.rotation * Quaternion.Euler(new Vector3(vertical, yaw, -horizontal) * sensitivity * Time.deltaTime));
    }
}
