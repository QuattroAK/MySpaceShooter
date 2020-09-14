using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private float speed;
    [SerializeField] private float tilt;
    [SerializeField] private float xMinPos;
    [SerializeField] private float xMaxPos;
    [SerializeField] private float zMinPos;
    [SerializeField] private float zMaxPos;
    

    private Vector3 playerMovement;

    public void Init()
    {

    }

    public void Refresh()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Move(moveHorizontal, moveVertical);
    }

    private void Move(float horizontal, float vertical)
    {
        playerMovement = new Vector3(horizontal, 0.0f, vertical);
        playerMovement = playerMovement.normalized;
        playerRigidbody.velocity = playerMovement * speed;

        playerRigidbody.position = new Vector3(Mathf.Clamp(playerRigidbody.position.x, xMinPos, xMaxPos), 0.0f, Mathf.Clamp(playerRigidbody.position.z, zMinPos, zMaxPos));
        playerRigidbody.rotation = Quaternion.Euler(0.0f, 0.0f, playerRigidbody.velocity.x * -tilt);
    }

    

}
