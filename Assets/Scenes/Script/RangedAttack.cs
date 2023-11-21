using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    public GameObject projectilePrefab;  // 발사체 프리팹
    public float projectileSpeed = 5f;   // 발사체 속도
    public float attackCooldown = 2f;    // 공격 쿨다운 시간
    private bool canAttack = true;        // 공격 가능 여부
    private Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
        // 발사체 프리팹을 설정해야 합니다.
        if (projectilePrefab == null)
        {
            Debug.LogError("Projectile Prefab is not set!");
        }
    }
    public void Attack(Transform tartgetTransform)
    {
        
        // 발사체를 생성하고 플레이어 방향으로 발사
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Vector3 directionToPlayer = (tartgetTransform.position - transform.position).normalized;
        animator.SetTrigger("Attack");
        animator.SetFloat("NormalState", 1);
        projectile.GetComponent<Rigidbody2D>().velocity = directionToPlayer * projectileSpeed;

        // 발사 이후 로직
        canAttack = false;
        Invoke("ResetAttackCooldown", attackCooldown);
    }

    private void ResetAttackCooldown()
    {
        canAttack = true;
    }

}
