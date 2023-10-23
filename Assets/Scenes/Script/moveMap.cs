using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class moveMap : MonoBehaviour
{
    public string nextSceneName; // �̵��� ���� ���� �̸�

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("town"))
        {
            SceneManager.LoadScene("town2");
        }
        if (collision.gameObject.CompareTag("town2"))
        {
            SceneManager.LoadScene("Map");
        }
    }
}
