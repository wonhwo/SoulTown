using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ability : MonoBehaviour
{
    int hp = 100;
    Animator animator;
    [SerializeField]
    GameObject gameObject;
    [SerializeField]
    public Image HP;
    // ������ �ı��Ǿ��� �� ȣ��� �ݹ� ��������Ʈ
    void Start()
    {
        animator = GetComponent < Animator>();
    }

    void Update()
    {
        if (hp < 1)
        {

            StartCoroutine(DestroyAfterDelay(1.0f));
        }
    }

    public void Hurt()
    {
        hp = hp - 50;
        HP.fillAmount = (float)hp/100;
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        animator.SetTrigger("Death");
        
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);

    }

}
