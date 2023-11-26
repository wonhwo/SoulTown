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
            // �÷��̾� �������� �̵�
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
            int selectAttack =Random.Range(0, 3);
            if (isPlayerInAttackRange)
            {
                if(selectAttack==0)
                    StartCoroutine(Attack1()); // �÷��̾ ������ ���� ���� ���� �ִٸ� ���� ����
                if (selectAttack == 1)
                    StartCoroutine(Attack2()); // �÷��̾ ������ ���� ���� ���� �ִٸ� ���� ����
                if (selectAttack == 2)
                    StartCoroutine(Attack3()); // �÷��̾ ������ ���� ���� ���� �ִٸ� ���� ����
            }

                
            
            yield return new WaitForSeconds(3.0f); // 2�� ���� ���
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

    // �̵� ���� �Լ�
    public void StartMoving()
    {
        if (!is2page) {isMoving = true; }
        
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

        animator.SetFloat("RunState", 0.5f);
        // �÷��̾ ���� �̵� ���� ���
        Vector3 direction = (player.position - transform.position).normalized;

        // ������ �÷��̾� �������� �̵�
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        // �̵� ������ X ������ Ȯ���Ͽ� �ø� ���� ����
        bool isFacingRight = direction.x  > 0;

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
    bool is2page=false;
    public IEnumerator moveCenter() {

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

    }
    float distanceToTarget; float timeToReachTarget;
    public void isCenterTure()
    {
        distanceToTarget = Vector3.Distance(transform.position, Center.transform.position);
        timeToReachTarget = distanceToTarget / moveSpeed;
        is2page = true;
    }

}
