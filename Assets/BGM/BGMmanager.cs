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
        // �ν��Ͻ��� null�� ��� ���� ������Ʈ�� �Ҵ��ϰ�,
        // �̹� �ν��Ͻ��� �����Ѵٸ� �ߺ� ������ �����ϱ� ���� ���� ������Ʈ�� �ı��մϴ�.
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
