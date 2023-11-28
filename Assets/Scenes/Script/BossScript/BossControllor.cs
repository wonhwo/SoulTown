using DG.Tweening;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform�� �����ϱ� ���� ����
    public float moveSpeed = 5f; // ���� �̵� �ӵ�

    public bool isMoving = false; // �̵� ������ ���θ� ��Ÿ���� ����
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
            // �÷��̾� �������� �̵�
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
        // �ڽ� �ݸ����� ��� ����
        if (collision.transform.IsChildOf(transform))
        {
            return;
        }

        // �θ� �ݸ��������� �̺�Ʈ ó��
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



            yield return new WaitForSeconds(2.5f); // 2�� ���� ���
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
        // �ø� ����
        if (isFacingRight)
        {
            transform.localScale = new Vector3(-1, 1, 1); // ���� ����
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1); // ���� ����
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

    // �̵� ���� �Լ�
    public void StartMoving()
    {
        if (!is2page && isMovingAllowed) { isMoving = true; }

    }

    // �̵� ���� �Լ�
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
        // �÷��̾ ���� �̵� ���� ���
        Vector3 direction = (player.position - transform.position).normalized;

        // ������ �÷��̾� �������� �̵�
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        // �̵� ������ X ������ Ȯ���Ͽ� �ø� ���� ����
        bool isFacingRight = direction.x > 0;

        // �ø� ����
        if (isFacingRight)
        {
            transform.localScale = new Vector3(-1, 1, 1); // ���� ����
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1); // ���� ����
        }
    }
    bool is2page = false;
    public IEnumerator moveCenter() {
        isMovingAllowed = false; // �̵��� ������� ����
        animator.SetFloat("RunState", 0.5f);
        Vector3 direction = (Center.transform.position - transform.position).normalized;

        // ������ �÷��̾� �������� �̵�
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        bool isFacingRight = direction.x > 0;

        // �ø� ����
        if (isFacingRight)
        {
            transform.localScale = new Vector3(-1, 1, 1); // ���� ����
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1); // ���� ����
        }
        yield return new WaitForSeconds(timeToReachTarget);
        is2page = false;
        StopMoving();
        isMovingAllowed = true; // �̵��� �ٽ� �����

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

        // 2�� �Ŀ� gameClearGUI�� Ȱ��ȭ
        Invoke("ActivateGameClearGUI", 2f);
    }

    void ActivateGameClearGUI()
    {
        gameClearGUI.SetActive(true);

        // �߰��� ���ϴ� �۾��� ������ �� �ֽ��ϴ�.
        // ��: gameClearGUI �ִϸ��̼� ���� ��
    }
}
