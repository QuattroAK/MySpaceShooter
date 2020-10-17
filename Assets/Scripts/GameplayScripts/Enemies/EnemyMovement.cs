using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float angleRotate;
    [SerializeField] private float distancePatrol;
    [SerializeField] private float rangeDetection;
    [SerializeField] private float distanceAttack;
    [SerializeField] private float smoothDirecion;
    [SerializeField] private float rotationSpeed;

    private EnemyShooting enemyShooting;
    private Transform targetAsteroid;
    private Transform targetPatrol;
    private Transform playerTarget;
    private Quaternion enemyRotation;
    private Vector3 directionPlayer;
    private Vector3 directionAsteroid;
    private float normalizedSpeed = 15f;
    private float distAsteroid;
    private float distPlayer;
    private bool moveAndAttack;
    private bool goBack;
    private bool patrol;

    public void Init(Transform playerTarget, Transform targetPatrol, Transform targetAsteroid, EnemyShooting enemyShooting)
    {
        this.playerTarget = playerTarget;
        this.targetPatrol = targetPatrol;
        this.enemyShooting = enemyShooting;
        this.targetAsteroid = targetAsteroid;
    }

    // TODO Изменить логику ИИ
    public void Refresh()
    {
        distAsteroid = Vector3.Distance(targetPatrol.transform.position, transform.position);
        distPlayer = Vector3.Distance(playerTarget.transform.position, transform.position);

        if(distAsteroid >= distancePatrol)
        {
            goBack = true;
            patrol = false;
            moveAndAttack = false;
        }
        else if(distAsteroid <= distancePatrol)
        {
            patrol = true;
            goBack = false;
            moveAndAttack = false;
        }

        if(distPlayer <= rangeDetection)
        {
            moveAndAttack = true;
            patrol = false;
            goBack = false;
        }

        if(goBack) GoBack();
        else if(patrol) Patrol();
        else if(moveAndAttack) MoveAndAttack();
    }

    private void Patrol()
    {
        Debug.DrawLine(targetAsteroid.position, transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetAsteroid.position - transform.position), rotationSpeed * Time.fixedDeltaTime);
        transform.Translate(0f, 0f, speed * normalizedSpeed * Time.fixedDeltaTime);
    }

    private void GoBack()
    {
        speed = maxSpeed;
        directionAsteroid = (targetPatrol.transform.position - transform.position).normalized;
        enemyRotation = Quaternion.LookRotation(directionAsteroid);
        transform.rotation = Quaternion.Lerp(transform.rotation, enemyRotation, smoothDirecion * Time.fixedDeltaTime);
        transform.position = Vector3.Lerp(transform.position, targetPatrol.transform.position, speed * Time.fixedDeltaTime);
    }

    private void MoveAndAttack()
    {
        directionPlayer = (playerTarget.transform.position - transform.position).normalized;
        enemyRotation = Quaternion.LookRotation(directionPlayer);
        transform.rotation = Quaternion.Lerp(transform.rotation, enemyRotation, smoothDirecion * Time.fixedDeltaTime);
        
        if(distPlayer <= distanceAttack)
        {
            speed = minSpeed;
            transform.position = Vector3.Lerp(transform.position, playerTarget.transform.position, speed * Time.fixedDeltaTime);
            enemyShooting.Attack();
        }
    }
}