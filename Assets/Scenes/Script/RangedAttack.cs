using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    public GameObject projectilePrefab;  // �߻�ü ������
    public float projectileSpeed = 5f;   // �߻�ü �ӵ�
    public float attackCooldown = 2f;    // ���� ��ٿ� �ð�
    private bool canAttack = true;        // ���� ���� ����
    private Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
        // �߻�ü �������� �����ؾ� �մϴ�.
        if (projectilePrefab == null)
        {
            Debug.LogError("Projectile Prefab is not set!");
        }
    }
    public void Attack(Transform targetTransform)
    {
        // �߻�ü�� �����ϰ� �÷��̾� �������� �߻�
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        // �÷��̾� �������� �߻������ ������ ���� ����
        Vector3 directionToPlayer = (targetTransform.position - transform.position).normalized;
        projectile.GetComponent<Rigidbody2D>().velocity = directionToPlayer * projectileSpeed;

        animator.SetTrigger("Attack");
        animator.SetFloat("NormalState", 1);

        // �߻� ���� ����
        canAttack = false;
        Invoke("ResetAttackCooldown", attackCooldown);
    }

    private void ResetAttackCooldown()
    {
        canAttack = true;
    }

}
