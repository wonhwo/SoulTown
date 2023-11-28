using DG.Tweening;
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
    [SerializeField]private GameObject Barrier;
    [SerializeField] private GameObject SpawnerBox;
    [SerializeField] protected GameObject gameClearGUI;
    public AudioSource h1; public AudioSource h2; public AudioSource lBGM; public AudioSource slashBGM; public AudioSource windBGM;
    public AudioSource deadBGM;
    private Animator BossAttack3Anim;
    private BossAttack3 bossAttackScript;
    private void Start()
    {
        bossAttackScript = BossAttack3.GetComponent<BossAttack3>();
        BossAttack3Anim = BossAttack3.GetComponent<Animator>();
        animator = GetComponent<Animator>();
        animator.SetFloat("RunState", 0);
    }
    private bool isMovingAllowed = true;
    private void Update()
    {
        if (isMoving && player != null && !is2page && isMovingAllowed)
        {
            // 플레이어 방향으로 이동
            MoveTowardsPlayer();
        }
        if (is2page)
        {
            StartCoroutine(moveCenter());
        }
        if (Barrier.activeSelf)
        {
            isbr=true;
        }
        else { isbr = false; }
    }
    private bool isbr = false;
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
            if (is2page || !isMovingAllowed || isdead|| isbr)
            {

                yield break;
            }
            int totalProbability = 100;
            int randomValue = Random.Range(1, totalProbability + 1);

            int selectAttack = randomValue <= 70 ? 0 : randomValue <= 90 ? 1 : 2;

            if (isPlayerInAttackRange)
            {
                switch (selectAttack)
                {
                    case 0:
                        StartCoroutine(Attack3());

                        break;
                    case 1:
                        StartCoroutine(Attack1());

                        break;
                    case 2:
                        StartCoroutine(Attack2());
                        break;
                }
            }



            yield return new WaitForSeconds(2.5f); // 2초 동안 대기
        }
        if (!isPlayerInAttackRange && !is2page)
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
        h1.Play();
        animator.SetFloat("NormalState", 0);
        BloodRange.SetActive(true);
        yield return new WaitForSeconds(1.1f);
        BloodRange.SetActive(false);
        slashBGM.Play();
        Attackanimator.Play("boss_blood_slash1");
        yield return new WaitForSeconds(2.5f);
    }
    private IEnumerator Attack2()
    {

        Debug.Log("a2");
        StopMoving();
        BossAttack.tag = "BossAttack2";
        animator.SetTrigger("Attack");
        h2.Play();
        animator.SetFloat("NormalState", 0.5f);
        batleSparksRange.SetActive(true);
        yield return new WaitForSeconds(1.60f);
        batleSparksRange.SetActive(false);
        windBGM.Play();
        Attackanimator.Play("batleSparks");
        yield return new WaitForSeconds(2.5f);
    }
    private IEnumerator Attack3()
    {
        Debug.Log("a3");
        StopMoving();
        BossAttack.tag = "BossAttack3";
        animator.SetTrigger("Attack");
        h1.Play();
        animator.SetFloat("NormalState", 0f);
        BossAttack3Range.SetActive(true);
        bossAttackScript.moveA3();
        yield return new WaitForSeconds(0.42f);
        BossAttack3Range.SetActive(false);
        lBGM.Play();
        BossAttack3Anim.Play("Slash_baff");
        yield return new WaitForSeconds(1.0f);
        bossAttackScript.moveReturn();
    }

    // 이동 시작 함수
    public void StartMoving()
    {
        if (!is2page && isMovingAllowed) { isMoving = true; }

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
        if (Barrier.activeSelf) { return; }
        if (isdead) return;
        animator.SetFloat("RunState", 0.5f);
        // 플레이어를 향해 이동 방향 계산
        Vector3 direction = (player.position - transform.position).normalized;

        // 보스를 플레이어 방향으로 이동
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        // 이동 방향의 X 성분을 확인하여 플립 여부 결정
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
    }
    bool is2page = false;
    public IEnumerator moveCenter() {
        isMovingAllowed = false; // 이동을 허용하지 않음
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
        isMovingAllowed = true; // 이동을 다시 허용함

    }
    float distanceToTarget; float timeToReachTarget;
    public void isCenterTure()
    {
        is2page = true;
        distanceToTarget = Vector3.Distance(transform.position, Center.transform.position);
        timeToReachTarget = distanceToTarget / moveSpeed;

    }
    bool isdead = false;
    public void isDead()
    {
        isdead = true;
        StopMoving();

        deadBGM.Play();
        animator.SetTrigger("Die");
        Destroy(SpawnerBox);

        // 2초 후에 gameClearGUI를 활성화
        Invoke("ActivateGameClearGUI", 2f);
    }

    void ActivateGameClearGUI()
    {
        gameClearGUI.SetActive(true);

        // 추가로 원하는 작업을 수행할 수 있습니다.
        // 예: gameClearGUI 애니메이션 시작 등
    }
}
