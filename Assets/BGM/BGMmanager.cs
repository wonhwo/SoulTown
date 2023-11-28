using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMmanager : MonoBehaviour
{
    public AudioSource bgm;
    private static BGMmanager instance;

    void Awake()
    {
        // 인스턴스가 null인 경우 현재 오브젝트를 할당하고,
        // 이미 인스턴스가 존재한다면 중복 생성을 방지하기 위해 현재 오브젝트를 파괴합니다.
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name.Equals("Dungeon"))
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        bgm.Play();
    }
}
