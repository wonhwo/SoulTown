using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform을 저장하기 위한 변수
    public float moveSpeed = 5f; // 보스 이동 속도

    public bool isMoving = false; // 이동 중인지 여부를 나타내는 변수
    private Animator animator;
    [SerializeField]
    private Animator Attackanimator;
    private Rigidbody2D rb;
    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("RunState", 0);
    }
    private void Update()
    {
        if (isMoving && player != null)
        {
            // 플레이어 방향으로 이동
            MoveTowardsPlayer();
        }
    }
    private bool isPlayerInAttackRange = false;
    private bool isAttackCoroutineRunning = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isAttackCoroutineRunning)
        {
            isPlayerInAttackRange = true;
            StartCoroutine(RepeatedAttackCoroutine());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInAttackRange = false;
        }
    }

    private IEnumerator RepeatedAttackCoroutine()
    {
        isAttackCoroutineRunning = true;
        

        while (isPlayerInAttackRange)
        {
            int selectAttack = Random.Range(0, 2);
            if (isPlayerInAttackRange)
            {
                if(selectAttack==0)
                    StartCoroutine(Attack1()); // 플레이어가 여전히 공격 범위 내에 있다면 어택 실행
                if (selectAttack == 1)
                    StartCoroutine(Attack2()); // 플레이어가 여전히 공격 범위 내에 있다면 어택 실행
            }
            yield return new WaitForSeconds(2.0f); // 2초 동안 대기
        }

        isAttackCoroutineRunning = false;
    }

    private IEnumerator Attack1()
    {
        Debug.Log("a1");
        StopMoving();
        animator.SetTrigger("Attack");
        animator.SetFloat("NormalState", 0);
        yield return new WaitForSeconds(1.5f);
        if(!isPlayerInAttackRange)
            StartMoving();


    }
    private IEnumerator Attack2()
    {
        Debug.Log("a2");
        StopMoving();
        animator.SetTrigger("Attack");
        animator.SetFloat("NormalState", 0.5f);
        yield return new WaitForSeconds(0.5f);
        Attackanimator.Play("batleSparks");
        yield return new WaitForSeconds(2.5f);

        if (!isPlayerInAttackRange)
            StartMoving();

    }

    // 이동 시작 함수
    public void StartMoving()
    {
        isMoving = true;
    }

    // 이동 종료 함수
    private void StopMoving()
    {
        animator.SetFloat("RunState", 0);
        isMoving = false;
    }

    void MoveTowardsPlayer()
    {

        animator.SetFloat("RunState", 0.5f);
        // 플레이어를 향해 이동 방향 계산
        Vector3 direction = (player.position - transform.position).normalized;

        // 보스를 플레이어 방향으로 이동
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        // 이동 방향의 X 성분을 확인하여 플립 여부 결정
        bool isFacingRight = direction.x  > 0;

        // 플립 설정
        if (isFacingRight)
        {
            transform.localScale = new Vector3(-1, 1, 1); // 우측 방향
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1); // 좌측 방향
        }
    }
}
