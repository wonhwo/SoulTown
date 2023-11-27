using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class player : MonoBehaviour
{
    [SerializeField]
    private SPUM_SpriteList spriteList;
    [SerializeField]
    private DivideSpace divideSpace = new DivideSpace();
    [SerializeField]
    public Spawner Spawner;
    [SerializeField]
    public EnemyBoxcontroller enemyBoxcontroller;
    [SerializeField]
    private Weapon weapon;
    [SerializeField]
    private camara camara;
    public GameManager gamemanager;
    public Animator animation;
    public float Speed;
    GameObject scanObject;
    Rigidbody2D rigid;
    float h;
    float v;
    bool isHorizonMove;
    bool isEventing = false;
    public bool isHurt = false;
    // 클래스 내에 배열 선언
    private List<int> extractedNumbers = new List<int>();
    //HP
    [SerializeField]
    public Image HPbar;
    private int HP = 100;
    //데미지
    private int damage;
    string currentSceneName;
    [SerializeField]
    private GameObject shield;
    void Awake()
    {
        gamemanager = FindObjectOfType<GameManager>();
        currentSceneName = SceneManager.GetActiveScene().name;
        rigid = GetComponent<Rigidbody2D>();
        animation = GetComponent<Animator>();
    }
    void Update()
    {
            h = gamemanager.isAction ? 0 : Input.GetAxisRaw("Horizontal");
            v = gamemanager.isAction ? 0 : Input.GetAxisRaw("Vertical");

        Animations();
        if(Input.GetButtonDown("Jump") && scanObject!=null)
        {
            Debug.Log(scanObject);
            gamemanager.Action(scanObject);
        }

        //if(currentSceneName.Equals("Dungeon"))
            //enemyBoxcontroller.findEnemy();
        if (Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(ActivateShieldForDuration(1.0f));
        }
    }
    private bool isShieldActive = false;
    IEnumerator ActivateShieldForDuration(float duration)
    {
        rigid.constraints = RigidbodyConstraints2D.FreezeRotation;

        if (!isShieldActive)
        {
            shield.SetActive(true);
            isShieldActive = true;

            yield return new WaitForSeconds(duration);

            shield.SetActive(false);
            isShieldActive = false;
        }
    }
    public bool IsMovingRight()
    {
        return transform.localScale.x < 0;
    }
    bool isEnemy;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Object")
        {
            scanObject = collision.gameObject;
            isEventing = true;

        }
        if (collision.CompareTag("Portal"))
        {
            transform.position = (Vector2)divideSpace.spaceList[0].Center();

        }
        if (collision.CompareTag("Portal1"))
        {
            transform.position = new Vector2(-33.87f, -191.54f);
        }
        if (collision.CompareTag("Portal2"))
        {
            transform.position = new Vector2(-20f, -134.4f);
            gamemanager.showBoss();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Rectangle"))
        {
            isEnemy = true;
            string name = collision.name;
            int lastCharacter = -1; // 기본값 설정
            if (!string.IsNullOrEmpty(name))
            {
                int lastIndex = name.Length - 1;
                // 마지막 글자부터 시작해서 숫자를 찾음
                while (lastIndex >= 0 && char.IsDigit(name[lastIndex]))
                {
                    lastIndex--;
                }
                if (lastIndex < name.Length - 1)
                {
                    // 숫자를 찾았을 경우, 해당 숫자를 추출하고 정수로 변환
                    lastCharacter = int.Parse(name.Substring(lastIndex + 1));
                }
            }
            // lastCharacter가 유효한 값을 가지고 있을 때만 실행
            if (lastCharacter >= 0)
            {
                // 배열에 이미 있는지 확인
                if (extractedNumbers.Contains(lastCharacter))
                {
                    // 이미 추출한 숫자와 동일한 경우 함수를 호출하지 않음
                    return;
                }

                // lastCharacter를 배열에 추가
                extractedNumbers.Add(lastCharacter);

                // Spawner.Return_RandomPosition 함수 호출
                StartCoroutine(SpawnMonsterWithDelay(lastCharacter));
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Object")
        {
                scanObject = null;
        }
        if(other.gameObject.tag== "Rectangle")
        {
            isEnemy = false;
        }
    }
    IEnumerator SpawnMonsterWithDelay(int lastCharacter)
    {
        yield return new WaitForSeconds(1.0f); // 1초 대기
        if (isEnemy)
        {
            StartCoroutine(Spawner.SpawnEnemiesWithDelay(lastCharacter)); // 몬스터 소환

        }
        else
        {
            extractedNumbers.Remove(lastCharacter);
            isEnemy = true;
        }
    }
    public IEnumerator HurtDelay(float delay,int damage)
    {
        isHurt = true;
        spriteList.ToggleTransparency();
        Physics2D.IgnoreLayerCollision(10, 6, true);
        //Physics2D.IgnoreLayerCollision(10, 26, true);
        HP = HP - damage;
        HPbar.fillAmount = (float)HP / 100;
        camara.ShakeCamera();
        StartCoroutine(stopPlayer(0.5f));
        yield return new WaitForSeconds(delay);

        isHurt = false;
        Physics2D.IgnoreLayerCollision(10, 6, false);
        //Physics2D.IgnoreLayerCollision(10, 26, false);

        spriteList.ToggleTransparency(); 
    }
    private bool isStopPlayerCooldown = false;

    public IEnumerator stopPlayer(float delay)
    {
        if (!isStopPlayerCooldown)
        {
            isStopPlayerCooldown = true;

            animation.SetTrigger("Stun");
            // rigid.constraints를 사용하여 플레이어의 움직임을 제한
            rigid.constraints = RigidbodyConstraints2D.FreezePosition;

            yield return new WaitForSeconds(delay);

            // 제한을 해제하여 다시 움직일 수 있도록 함
            rigid.constraints = RigidbodyConstraints2D.None;
            isStopPlayerCooldown = false;
            rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
    private void Animations()
    {
        transform.localScale = new Vector3((h < 0) ? 1 : ((h > 0) ? -1 : transform.localScale.x), 1, 1);
        if (h != 0 || v != 0)
        {
            animation.SetFloat("RunState", 1.0f);
        }
        else
        {
            animation.SetFloat("RunState", 0);
        }
    }
    private void FixedUpdate()
    {
        if (rigid != null)
        {
            Vector2 moveVec = new Vector2(h, v);
            rigid.velocity = moveVec.normalized * Speed;
        }
        
    }
    public IEnumerator LowerSpeedForDuration()
    {
        // 스피드를 낮추는 로직
        Speed = 5.0f;

        // 일정 시간 기다림
        yield return new WaitForSeconds(3.0f);

        // 원래 스피드로 복구
        Speed = 8.0f;
    }
    public void healHP(int index)
    {
        int healAmount = 0;
        if (index == 1)
        {
            healAmount = 20;
        }
        else if (index == 2)
        {

            healAmount = 50;
        }
        // HP를 증가시키고 100을 넘지 않도록 제한
        HP = Mathf.Clamp(HP + healAmount, 0, 100);

        // HP바 갱신
        HPbar.fillAmount = (float)HP / 100;

    }
}
