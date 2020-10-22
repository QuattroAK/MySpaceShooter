using UnityEngine.UI;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components links")]
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private Joystick joystick;

    [Header("Movement parametres")]
    [SerializeField] private float startSpeed;
    [SerializeField] private float speed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float boostSpeed;

    private Vector3 rotatePlayer;
    private PlayerHealth playerHealth;

    public void Init(PlayerHealth playerHealth)
    {
        this.playerHealth = playerHealth;
        speed = startSpeed;
    }

    public void Refresh()
    {
        float moveHorizontal = joystick.Horizontal;
        float moveVertical = joystick.Vertical;

        if (!playerHealth.IsDead)
        {
            Move(speed);
            Rotate(moveHorizontal, moveVertical);
        }
    }

    private void Move(float speed)
    {
        playerRigidbody.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);
    }

    private void Rotate(float horizontal, float vertical)
    {
        rotatePlayer = new Vector3(-vertical * rotateSpeed * Time.fixedDeltaTime, horizontal *  rotateSpeed *  Time.fixedDeltaTime, 0.0f);
        Quaternion rotate = Quaternion.Euler(rotatePlayer);
        playerRigidbody.MoveRotation(playerRigidbody.rotation * rotate);
    }

    public void TurnOnBoostSpeed()
    {
        speed = boostSpeed;
        rotateSpeed *= 2;
    }
    public void TurnOffBoostSpeed()
    {
        speed = startSpeed;
        rotateSpeed /= 2;
    }

}