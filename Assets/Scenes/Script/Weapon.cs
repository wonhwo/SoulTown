using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private player player;

    private Animator animation;
    [SerializeField]
    Animator parentAnimator;
    private int damage;
    float delay;
    private WeaponSetting weaponSetting;
    void Awake()
    {
        animation = GetComponent<Animator>();
        weaponSetting = GetComponent<WeaponSetting>();
    }
    void Update()
    {
        setdamage();
        weaponAnimation();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // �浹�� ��ü���� ���� ����
            collision.gameObject.GetComponent<Enemy>().SetDamage(damage);
        }
    }
    int countS = 1;
    static bool isSlash = false;
    Vector3 playerTransform;
    private void weaponAnimation()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isSlash)
        {
            flipHorizontally();
            //transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 2, 0);
            StartCoroutine(FollowPlayerWithDelay());
            StartCoroutine(AnimationDelay());
        }
    }
    private IEnumerator FollowPlayerWithDelay()
    {
        
        float elapsedTime = 0.0f;
        float followDuration = 2.0f; // ���󰡴� ���� �ð�

        while (elapsedTime < followDuration)
        {
            
            // ������ ��ġ�� �÷��̾��� ��ġ�� ���ݾ� ���󰡱�
            transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x, player.transform.position.y + 2, 0), elapsedTime / followDuration);

            // ��� �ð� ������Ʈ
            elapsedTime += Time.deltaTime; 
        }
        yield return null;
    }
    public bool flipHorizontally()
    {
        bool flip = false;
        if (player.transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(-1,1,0);
            flip = true;
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 0);
        }
        return flip;
    }

    int countAttack = 1;
    private IEnumerator AnimationDelay()
    {
        parentAnimator.SetTrigger("Attack");
        if (countAttack == 1)
        {
            isSlash = true;
            sendAttack();
            animation.Play(weaponSetting.SelectfirstAttack());
            countAttack++;
        }
        else if(countAttack == 2) {
            isSlash = true;
            sendAttack();
            animation.Play(weaponSetting.Selectlastttack());
            countAttack = 1;
        }
        yield return new WaitForSeconds(delay);
        isSlash = false;
        sendAttack();
    }

    public bool sendAttack()
    {
        return isSlash;
    }
    public void setdamage()
    {
        damage=weaponSetting.damage;
        delay = weaponSetting.delay;
    }
}
