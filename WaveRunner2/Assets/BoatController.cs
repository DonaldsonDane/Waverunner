using UnityEngine;

public class BoatController : MonoBehaviour
{
    public float moveSpeed = 10f;           // Forward and backward speed
    public float turnSpeed = 5f;           // Turning speed
    public float pitchSpeed = 5f;          // Pitch (up and down) speed
    public float forwardDrag = 2f;         // Forward drag to simulate slowing down
    public float lateralDrag = 1f;         // Lateral drag for drifting effect

    private Rigidbody boatRigidbody;

    private void Start()
    {
        boatRigidbody = GetComponent<Rigidbody>();
        boatRigidbody.maxAngularVelocity = 10f; // Increase this value if the boat flips too easily
    }

    private void FixedUpdate()
    {
        // Get player input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Get pitch input for boat's upward/downward rotation
        float pitchInput = Input.GetAxis("VerticalPitch");

        // Calculate the desired movement direction based on input
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Calculate the boat's forward direction based on its movement direction
        Vector3 boatForward = Vector3.RotateTowards(transform.forward, moveDirection, turnSpeed * Time.deltaTime, 0f);

        // Apply boat rotation for turning using AddTorque with boatForward direction
        if (moveDirection != Vector3.zero)
        {
            Vector3 rotationTorque = Vector3.up * Vector3.SignedAngle(transform.forward, boatForward, Vector3.up) * turnSpeed;
            boatRigidbody.AddTorque(rotationTorque, ForceMode.Force);
        }

        // Apply pitch (upward/downward rotation) using AddTorque with pitchInput
        if (pitchInput != 0)
        {
            Vector3 pitchTorque = Vector3.right * pitchInput * pitchSpeed;
            boatRigidbody.AddTorque(pitchTorque, ForceMode.Force);
        }

        // Calculate the forward force based on the boat's forward direction
        Vector3 forwardForce = boatForward * verticalInput * moveSpeed;

        // Apply forward/backward movement using AddForce
        boatRigidbody.AddForce(forwardForce, ForceMode.Force);

        // Apply drag to simulate slowing down
        boatRigidbody.drag = forwardDrag;

        // Apply lateral drag for drifting effect when not turning
        if (horizontalInput == 0)
        {
            boatRigidbody.angularDrag = lateralDrag;
        }
        else
        {
            boatRigidbody.angularDrag = 0f;
        }
    }
}
