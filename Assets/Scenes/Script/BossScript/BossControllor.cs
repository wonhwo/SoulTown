using System.Collections;
using TMPro;
using Unity.VisualScripting;
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
    [SerializeField]
    private GameObject BossAttack;
    [SerializeField] private GameObject Center;
    [SerializeField] private GameObject batleSparksRange;
    [SerializeField] private GameObject BloodRange;
    [SerializeField] private GameObject BossAttack3;
    [SerializeField] private GameObject BossAttack3Range;
    private Animator BossAttack3Anim;
    private BossAttack3 bossAttackScript;
    private void Start()
    {
        bossAttackScript=BossAttack3.GetComponent<BossAttack3>();
        BossAttack3Anim = BossAttack3.GetComponent<Animator>();
        animator = GetComponent<Animator>();
        animator.SetFloat("RunState", 0);
    }
    private void Update()
    {
        if (isMoving && player != null&&!is2page)
        {
            // 플레이어 방향으로 이동
            MoveTowardsPlayer();
        }
        if (is2page)
        {
            StartCoroutine(moveCenter());
        }
        //Debug.Log()
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
        // 자식 콜리더인 경우 무시
        if (collision.transform.IsChildOf(transform))
        {
            return;
        }

        // 부모 콜리더에서만 이벤트 처리
        if (collision.CompareTag("Player"))
        {
            Debug.Log(collision.gameObject.name);
            isPlayerInAttackRange = false;
        }
    }

    private IEnumerator RepeatedAttackCoroutine()
    {
        isAttackCoroutineRunning = true;
        

        while (isPlayerInAttackRange)
        {
            int selectAttack =Random.Range(0, 3);
            if (isPlayerInAttackRange)
            {
                if(selectAttack==0)
                    StartCoroutine(Attack1()); // 플레이어가 여전히 공격 범위 내에 있다면 어택 실행
                if (selectAttack == 1)
                    StartCoroutine(Attack2()); // 플레이어가 여전히 공격 범위 내에 있다면 어택 실행
                if (selectAttack == 2)
                    StartCoroutine(Attack3()); // 플레이어가 여전히 공격 범위 내에 있다면 어택 실행
            }

                
            
            yield return new WaitForSeconds(3.0f); // 2초 동안 대기
        }
        if (!isPlayerInAttackRange)
        {
            StartMoving();
        }

        isAttackCoroutineRunning = false;
    }
    private IEnumerator Attack1()
    {
        Debug.Log("a1");
        StopMoving();
        Vector3 direction = (player.transform.position - transform.position).normalized;
        bool isFacingRight = direction.x > 0;
        // 플립 설정
        if (isFacingRight)
        {
            transform.localScale = new Vector3(-1, 1, 1); // 우측 방향
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1); // 좌측 방향
        }
        BossAttack.tag = "BossAttack1";
        animator.SetTrigger("Attack");
        animator.SetFloat("NormalState", 0);
        BloodRange.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        BloodRange.SetActive(false);
        Attackanimator.Play("boss_blood_slash1");
        yield return new WaitForSeconds(2.0f);
    }
    private IEnumerator Attack2()
    {

        Debug.Log("a2");
        StopMoving();
        BossAttack.tag = "BossAttack2";
        animator.SetTrigger("Attack");
        animator.SetFloat("NormalState", 0.5f);
        batleSparksRange.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        batleSparksRange.SetActive(false);
        Attackanimator.Play("batleSparks");
        yield return new WaitForSeconds(2.5f);
    }
    private IEnumerator Attack3()
    {
        Debug.Log("a3");
        StopMoving();
        BossAttack.tag = "BossAttack3";
        animator.SetTrigger("Attack");
        animator.SetFloat("NormalState", 0f);
        BossAttack3Range.SetActive(true);
        bossAttackScript.moveA3();
        yield return new WaitForSeconds(0.35f);
        BossAttack3Range.SetActive(false);
        BossAttack3Anim.Play("Slash_baff");
        yield return new WaitForSeconds(0.7f);
        bossAttackScript.moveReturn();
    }

    // 이동 시작 함수
    public void StartMoving()
    {
        if (!is2page) {isMoving = true; }
        
    }

    // 이동 종료 함수
    public void StopMoving()
    {
        if (!is2page)
        {
        animator.SetFloat("RunState", 0);
        isMoving = false;
        }
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
    bool is2page=false;
    public IEnumerator moveCenter() {

        animator.SetFloat("RunState", 0.5f);
        Vector3 direction = (Center.transform.position - transform.position).normalized;

        // 보스를 플레이어 방향으로 이동
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        bool isFacingRight = direction.x > 0;

        // 플립 설정
        if (isFacingRight)
        {
            transform.localScale = new Vector3(-1, 1, 1); // 우측 방향
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1); // 좌측 방향
        }
        yield return new WaitForSeconds(timeToReachTarget);
        is2page = false;
        StopMoving();

    }
    float distanceToTarget; float timeToReachTarget;
    public void isCenterTure()
    {
        distanceToTarget = Vector3.Distance(transform.position, Center.transform.position);
        timeToReachTarget = distanceToTarget / moveSpeed;
        is2page = true;
    }

}
