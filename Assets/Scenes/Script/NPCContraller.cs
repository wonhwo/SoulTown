using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class NPCContraller : MonoBehaviour
{
    public GameObject Image;
    public TextMeshProUGUI textMeshProUGUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Image.SetActive(true);

            // null üũ�� �߰��Ͽ� ���� ����
            if (textMeshProUGUI != null)
            {
                textMeshProUGUI.text = "space Ŭ��!";
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Image.SetActive(false);

            // null üũ�� �߰��Ͽ� ���� ����
            if (textMeshProUGUI != null)
            {
                textMeshProUGUI.text = gameObject.name;
            }
        }
    }
}
