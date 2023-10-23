using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private ability ability;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("weapon"))
        { 
            Debug.Log("test");
            animator.SetTrigger("Hurt");
            ability.Hurt();
        }

    }


}
