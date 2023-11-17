using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float pursuitSpeed;
    public float wanderSpeed;
    public float currentSpeed;

    public float directionChangeInterval;
    public bool follwPlayer;

    Coroutine moveCoroutine;
    CircleCollider2D circleCollider2D;
    Rigidbody2D Rigidbody2D;
    Animator animator;

    Transform targetTransform = null;
    Vector3 endPosition;
    float currenAngle =0;


    private void Start()
    {
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
            ChooseNewEndPoint();
            if(moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            moveCoroutine = StartCoroutine(Move(Rigidbody2D, currentSpeed));
            yield return new WaitForSeconds(directionChangeInterval);
        }
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
                animator.SetBool("isWalking", true);

                if (IsWallInFront())
                {
                    // 벽이 감지되면 이동을 중지하도록 변경
                    animator.SetBool("isWalking", false);
                    break;
                }

                Vector3 newPosition = Vector3.MoveTowards(rigidbodyToMove.position, endPosition, speed * Time.fixedDeltaTime);
                Rigidbody2D.MovePosition(newPosition);
                remainingDistance = (transform.position - endPosition).sqrMagnitude;
            }

            yield return null;
        }

        animator.SetBool("isWalking", false);
    }
    private bool IsWallInFront()
    {
        // 현재 위치에서 이동 방향으로 Ray를 쏴서 벽이 있는지 확인
        RaycastHit2D hit = Physics2D.Raycast(transform.position, endPosition - transform.position, currentSpeed * Time.fixedDeltaTime);

        if (hit.collider != null && hit.collider.CompareTag("Wall"))
        {
            Debug.Log("Wall detected!");
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
    }
     void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("isWalking", false);
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
