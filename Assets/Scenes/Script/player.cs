using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
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
    void Awake()
    {
        gamemanager = FindObjectOfType<GameManager>();
        currentSceneName = SceneManager.GetActiveScene().name;
        rigid = GetComponent<Rigidbody2D>();
        animation = GetComponent<Animator>();
        
    }
    void Update()
    {
        if (isHurt)
        {
            h = 0;
            v = 0;
        }
        else
        {
            h = gamemanager.isAction ? 0 : Input.GetAxisRaw("Horizontal");
            v = gamemanager.isAction ? 0 : Input.GetAxisRaw("Vertical");
        }


        Animations();
        if(Input.GetButtonDown("Jump") && scanObject!=null)
        {
            Debug.Log(scanObject);
            gamemanager.Action(scanObject);
        }
        if(currentSceneName.Equals("Map"))
            enemyBoxcontroller.findEnemy();

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
        Debug.Log(lastCharacter+","+isEnemy);
        if (isEnemy)
        {
            Spawner.Return_RandomPosition(lastCharacter); // 몬스터 소환

        }
        else
        {
            extractedNumbers.Remove(lastCharacter);
            isEnemy = true;
        }
    }
    public IEnumerator HurtDelay(float delay)
    {
        
        HP = HP - 20;
        HPbar.fillAmount =(float)HP / 100;
        animation.SetTrigger("Stun");
        camara.ShakeCamera();
        Debug.Log("test");
        isHurt = true; // 피격 상태로 설정
        yield return new WaitForSeconds(delay);
        isHurt = false; // 일정 시간 후 피격 상태 해제
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
            Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
            rigid.velocity = new Vector2(h, v) * Speed;
        }
    }
}
