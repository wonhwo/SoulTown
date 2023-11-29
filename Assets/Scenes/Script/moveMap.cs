using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class moveMap : MonoBehaviour
{
    public string nextSceneName; // 이동할 다음 씬의 이름
    private GameManager gameManager;
    [SerializeField]
    Weapon weapon;
    private void Awake()
    {
        gameManager=FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (gameObject.name.Equals("move1Warp"))
            {
                LoadingMnager.LoadScene("Town");
            }
            if (gameObject.name.Equals("move2Warp"))
            {
                LoadingMnager.LoadScene("Town2");
            }
            if (gameObject.name.Equals("move3Warp"))
            {
                LoadingMnager.LoadScene("Dungeon");
            }
        }
    }
}
