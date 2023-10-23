using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ability : MonoBehaviour
{
    int hp = 100;
    Animator animator;
    [SerializeField]
    GameObject gameObject;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp < 1)
        {
            animator.SetTrigger("Death");
            
            StartCoroutine(DestroyAfterDelay(1.0f));
        }
        
    }
    public void Hurt()
    {
        hp =hp- 20;
    }
    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        // 지연 후에 오브젝트 삭제
        Destroy(gameObject);
    }
}
