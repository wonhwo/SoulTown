using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using UnityEditor.Hardware;

public class player : MonoBehaviour
{
    [SerializeField]
    private DivideSpace divideSpace = new DivideSpace();
    [SerializeField]
    RectangleSpace rectangleSpace;
    [SerializeField]
    public MakeRandomMap makeRandom;
    public GameManager gamemanager;
    public Animator animation;
    public float Speed;
    Vector3 dirVec;
    GameObject scanObject;
    Rigidbody2D rigid;
    float h;
    float v;
    bool isHorizonMove;
    bool isJumping = false;
    bool isWalking = false;
    bool isEventing = false;
    private Animator lefthandAnimator;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animation = GetComponent<Animator>();
        lefthandAnimator = transform.Find("Sword").GetComponent<Animator>();
    }
    void Update()
    {
        h = gamemanager.isAction ?  0 :Input.GetAxisRaw("Horizontal");
        v = gamemanager.isAction ? 0 : Input.GetAxisRaw("Vertical");
        Animations();
        if (Input.GetButtonDown("Jump") && scanObject != null && isEventing)
        {
            gamemanager.Action(scanObject);
        }


    }
    String PortalText; String PortalChar; int PortalNum;char lastChar; String text;
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
            LevelManager.levelManager.spawner.Return_RandomPosition();
        }
        else
        {
            scanObject = null;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Object")
        {
            isEventing = false;
        }
    }

    int countS=1;
    static bool isSlash = false;
    private void Animations()
    {

        // �ȱ� �ִϸ��̼� ����
        if (!isJumping)
        {
            isWalking = (h != 0 || v != 0);

        }

        animation.SetBool("Walking", isWalking);

        // �ȴ� ���⿡ ���� ĳ���� ������ ����
        transform.localScale = new Vector3((h < 0) ? -1 : ((h > 0) ? 1 : transform.localScale.x), 1, 1);

        // "Jump" Ű (��: 'C' Ű) ó��
        if (Input.GetKeyDown(KeyCode.C) && !isJumping)
        {
            // "Jump" �ִϸ��̼� ����
            animation.SetTrigger("Jumping");
            isJumping = true;
            isWalking = false;
            // ���⿡ ���� ���� �߰�
        }

        // "Jump" Ű�� ���� ��
        if (Input.GetKeyUp(KeyCode.C))
        {
            // "Jump" �ִϸ��̼� ���ߵ��� ����
            animation.ResetTrigger("Jumping");
            isJumping = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl)&&!isSlash)
        {
            StartCoroutine(AnimationDelay());
            
        }
    }
    private IEnumerator AnimationDelay()
    {
        isSlash = true;
        lefthandAnimator.SetInteger("num", countS);
        lefthandAnimator.SetTrigger("Slash");
        countS++;
        if (countS > 3)
        {
            countS = 1;
        }
        yield return new WaitForSeconds(0.5f);
        isSlash = false;
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
