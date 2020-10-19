using UnityEngine.UI;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components links")]
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private Joystick joystick;
    [SerializeField] private Text speedText;

    [Header("Movement parametres")]
    [SerializeField] private float startSpeed;
    [SerializeField] private float speed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float tilt;
    [SerializeField] private float boostSpeed;

    private Vector3 rotatePlayer;
    private Vector3 movePlayer;

    public void Init()
    {
        speed = startSpeed;
    }

    public void Refresh()
    {
        float moveHorizontal = joystick.Horizontal;
        float moveVertical = joystick.Vertical;

        Move(speed);
        Rotate(moveHorizontal, moveVertical);

    //    speed = Mathf.RoundToInt(playerRigidbody.velocity.magnitude * 2.237f); // M/H  - 2.237, KM/H  - 3.6, преобразуем в интовый тип нашу скорость. ( playerRb.velocity.magnitude - текущая скорость в м/с * 2.237f - умножаем и получаем мл/ч) 
    //    speedText.text = ($"Speed: {speed} mph"); // выводим на экран значение текущей скорости
    }

    private void Move(float speed)
    {
        playerRigidbody.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);
        //playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, playerRigidbody.velocity.y, startSpeed * Time.fixedDeltaTime);
        //playerRigidbody.AddRelativeForce(Vector3.forward * startSpeed * Time.fixedDeltaTime, ForceMode.Force);
    }

    private void Rotate(float horizontal, float vertical)
    {
        rotatePlayer = new Vector3(-vertical * rotateSpeed * Time.fixedDeltaTime, horizontal *  rotateSpeed *  Time.fixedDeltaTime, 0.0f);
        Quaternion rotate = Quaternion.Euler(rotatePlayer);
        playerRigidbody.MoveRotation(playerRigidbody.rotation * rotate);
        //playerRigidbody.rotation = Quaternion.Euler(0.0f, 0.0f, playerRigidbody.velocity.x * -tilt);
    }

    public void BoostSpeed()
    {
        playerRigidbody.MovePosition(transform.position + transform.forward * boostSpeed * Time.fixedDeltaTime);
    }
}