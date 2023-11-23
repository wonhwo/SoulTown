using System.Collections;
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
    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("RunState", 0);
    }
    private void Update()
    {
        if (isMoving && player != null)
        {
            // �÷��̾� �������� �̵�
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
                    StartCoroutine(Attack1()); // �÷��̾ ������ ���� ���� ���� �ִٸ� ���� ����
                if (selectAttack == 1)
                    StartCoroutine(Attack2()); // �÷��̾ ������ ���� ���� ���� �ִٸ� ���� ����
            }
            yield return new WaitForSeconds(2.0f); // 2�� ���� ���
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

    // �̵� ���� �Լ�
    public void StartMoving()
    {
        isMoving = true;
    }

    // �̵� ���� �Լ�
    private void StopMoving()
    {
        animator.SetFloat("RunState", 0);
        isMoving = false;
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
}
