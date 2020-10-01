using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float forwardSeed = 25f;
    [SerializeField] private float strafeSpeed = 7.5f;
    [SerializeField] private float forwardAcceleration = 2.5f;
    [SerializeField] private float strafeAcceleration = 2f;
    [SerializeField] private float hoverAcceleration = 2f;
    [SerializeField] private float lookRateSpeed = 45f;
    [SerializeField] private float rollSpeed = 45f;
    [SerializeField] private float rollAcceleration = 3.5f;

    private Vector2 lookInput;
    private Vector2 screenCenter;
    private Vector2 mouseDistance;
    private float activeForwardSpeed;
    private float activeStrafeSpeed;
    private float rollInput;

    public void Init()
    {
        screenCenter.x = Screen.width * 0.5f;
        screenCenter.y = Screen.height * 0.5f;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void Refresh()
    {
        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

        mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.x;
        mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;

        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

        rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll"), rollAcceleration * Time.deltaTime);

        transform.Rotate(-mouseDistance.y * lookRateSpeed * Time.deltaTime, mouseDistance.x * lookRateSpeed * Time.deltaTime, rollInput * rollSpeed * Time.deltaTime, Space.Self);

        activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, Input.GetAxisRaw("Vertical") * forwardSeed, forwardAcceleration);
        activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, Input.GetAxisRaw("Horizontal") * strafeSpeed, strafeAcceleration);

        transform.position += transform.forward * activeForwardSpeed * Time.deltaTime;
        transform.position += transform.right * activeStrafeSpeed * Time.deltaTime;
    }
    

    //[Header("Components links")]
    //[SerializeField] private Rigidbody playerRigidbody;

    //[Header("Movement parametres")]
    //[SerializeField] private float speed;
    //[SerializeField] private float tilt;
    //[SerializeField] private float xMinPos;
    //[SerializeField] private float xMaxPos;
    //[SerializeField] private float zMinPos;
    //[SerializeField] private float zMaxPos;

    //private Vector3 playerMovement;



    //public void Refresh()
    //{
    //    float moveHorizontal = Input.GetAxis("Horizontal");
    //    float moveVertical = Input.GetAxis("Vertical");

    //    Move(moveHorizontal, moveVertical);
    //}

    //private void Move(float horizontal, float vertical)
    //{
    //    playerMovement = new Vector3(horizontal, 0.0f, vertical);
    //    playerMovement = playerMovement.normalized;
    //    playerRigidbody.velocity = playerMovement * speed;

    //    playerRigidbody.position = new Vector3(Mathf.Clamp(playerRigidbody.position.x, xMinPos, xMaxPos), 0.0f, Mathf.Clamp(playerRigidbody.position.z, zMinPos, zMaxPos));
    //    playerRigidbody.rotation = Quaternion.Euler(0.0f, 0.0f, playerRigidbody.velocity.x * -tilt);
    //}
}