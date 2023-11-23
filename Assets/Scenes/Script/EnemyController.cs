using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private RangedAttack rangedAttack;
    [SerializeField]
    private EnemyStats enemyStats;
    private float pursuitSpeed;
    private float wanderSpeed;
    private float currentSpeed;

    public float directionChangeInterval;
    public bool follwPlayer;

    Coroutine moveCoroutine;
    CircleCollider2D circleCollider2D;
    Rigidbody2D Rigidbody2D;
    Animator animator;

    Transform targetTransform = null;
    Transform targetTransform1 = null;
    Vector3 endPosition;
    float currenAngle =0;


    private void Start()
    {
        pursuitSpeed = enemyStats.moveSpeed;
        wanderSpeed = enemyStats.moveSpeed;
        currentSpeed = enemyStats.moveSpeed;
        animator = GetComponent<Animator>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        currentSpeed = wanderSpeed;
        StartCoroutine(WanderRoutine());
    }
    private void Update()
    {
        
        Debug.DrawLine(Rigidbody2D.position, endPosition, Color.red);

    }
    public IEnumerator WanderRoutine()
    {
        while (true)
        {
            ChooseNewEndPointWithinRadius();
            //ChooseNewEndPoint();
            if(moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            } 
            moveCoroutine = StartCoroutine(Move(Rigidbody2D, currentSpeed));
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }
    void ChooseNewEndPointWithinRadius()
    {
        float circleRadius = circleCollider2D.radius;

        currenAngle = Random.Range(0, 360);
        float randomDistance = Random.Range(0, circleRadius);

        endPosition = transform.position + Vector3FromAngle(currenAngle) * randomDistance;
    }
    void ChooseNewEndPoint()
    {
        currenAngle += Random.Range(0, 360);
        currenAngle = Mathf.Repeat(currenAngle, 360);
        endPosition += Vector3FromAngle(currenAngle);
    }
    Vector3 Vector3FromAngle(float inputAngleDegrees)
    {
        float inputAngleRadians = inputAngleDegrees * Mathf.Deg2Rad;

        return new Vector3(Mathf.Cos(inputAngleRadians), Mathf.Sin(inputAngleRadians), 0);
    }
    public IEnumerator Move(Rigidbody2D rigidbodyToMove, float speed)
    {
        float remainingDistance = (transform.position - endPosition).sqrMagnitude;

        while (remainingDistance > float.Epsilon)
        {
            if (targetTransform != null)
            {
                endPosition = targetTransform.position;
            }

            if (rigidbodyToMove != null)
            {
                // 이동 방향의 X 성분을 확인하여 플립 여부 결정
                bool isFacingRight = (endPosition.x - rigidbodyToMove.position.x) > 0;

                // 플립 설정
                if (isFacingRight)
                {
                    transform.localScale = new Vector3(-1, 1, 1); // 우측 방향
                }
                else
                {
                    transform.localScale = new Vector3(1, 1, 1); // 좌측 방향
                }

                if(!isfinding)
                    animator.SetFloat("RunState", 0.5f);

                /*if (IsWallInFront())
                {
                    // 벽이 감지되면 이동을 중지하도록 변경
                    animator.SetFloat("RunState", 0.0f);
                    break;
                }*/

                Vector3 newPosition = Vector3.MoveTowards(rigidbodyToMove.position, endPosition, speed * Time.fixedDeltaTime);
                Rigidbody2D.MovePosition(newPosition);
                remainingDistance = (transform.position - endPosition).sqrMagnitude;
            }

            yield return null;
        }

        animator.SetFloat("RunState", 0);
    }
    private bool IsWallInFront()
    {
        // 현재 위치에서 이동 방향으로 Ray를 쏴서 벽이 있는지 확인
        RaycastHit2D hit = Physics2D.Raycast(transform.position, endPosition - transform.position, currentSpeed * Time.fixedDeltaTime);

        if (hit.collider != null && hit.collider.CompareTag("Wall"))
        {
            return true;
        }

        return false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && follwPlayer)
        {
            currentSpeed = pursuitSpeed;
            targetTransform = collision.gameObject.transform;
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            moveCoroutine = StartCoroutine(Move(Rigidbody2D, currentSpeed));
        }
        if (collision.gameObject.CompareTag("Player") && !follwPlayer)
        {

        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !follwPlayer)
        {
            Rigidbody2D.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
            animator.SetFloat("RunState", 0);

            isfinding = true;
            currentSpeed = pursuitSpeed;
            targetTransform = collision.gameObject.transform;
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            moveCoroutine = StartCoroutine(Move(Rigidbody2D, currentSpeed));

            if (!isRepeatedAttackCoroutineRunning)
            {
                StartCoroutine(RepeatedAttackCoroutine(collision));
            }
        }
            
    }
    private bool isfinding = false;
    private bool isRepeatedAttackCoroutineRunning = false;
    private IEnumerator RepeatedAttackCoroutine(Collider2D collision)
    {
        isRepeatedAttackCoroutineRunning = true;
        while (isfinding)
        {
            targetTransform1 = collision.gameObject.transform;

            rangedAttack.Attack(targetTransform1);

            yield return new WaitForSeconds(1.5f);
        }
        isRepeatedAttackCoroutineRunning = false;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Rigidbody2D.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        isfinding = false;
        if (collision.gameObject.CompareTag("Player")&&follwPlayer)
        {
            animator.SetFloat("RunState", 0);
            currentSpeed = wanderSpeed;
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            targetTransform = null;
            
        }
    }
    private void OnDrawGizmos()
    {
        if (circleCollider2D != null)
        {
            Gizmos.DrawWireSphere(transform.position, circleCollider2D.radius);
        }
    }
}
