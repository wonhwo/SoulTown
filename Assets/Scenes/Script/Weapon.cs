using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Weapon : MonoBehaviour
{
    public AudioSource A1; public AudioSource A2;
    [SerializeField]
    private player player;
    [SerializeField]
    DamageTextController damageTextController;
    private Animator animation;
    [SerializeField]
    Animator parentAnimator;
    private static int damage;
    float delay;
    private WeaponSetting weaponSetting;
    private string currentScene;
    void Awake()
    {
        currentScene = SceneManager.GetActiveScene().name;
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
            Vector3 pos = Camera.main.WorldToScreenPoint(collision.transform.position);
            DamageTextController.Instance.CreateDamageText(pos, damage);
            // 충돌한 객체에게 변수 전달
            collision.gameObject.GetComponent<Enemy>().SetDamage(damage);
        }
        if (collision.CompareTag("Boss"))
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(collision.transform.position);
            DamageTextController.Instance.CreateDamageText(pos, damage);
            // 충돌한 객체에게 변수 전달
            collision.gameObject.GetComponent<BossBody>().SetDamage(damage);
        }
    }
    int countS = 1;
    [SerializeField]
    public  bool isSlash=false;
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
        float followDuration = 2.0f; // 따라가는 지속 시간

        while (elapsedTime < followDuration)
        {
            
            // 무기의 위치를 플레이어의 위치로 조금씩 따라가기
            transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x, player.transform.position.y + 2, 0), elapsedTime / followDuration);

            // 경과 시간 업데이트
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
        isSlash = true;
        parentAnimator.SetTrigger("Attack");
        if (countAttack == 1)
        {
            A1.Play();
            
            sendAttack();
            animation.Play(weaponSetting.SelectfirstAttack());
            countAttack++;
        }
        else if(countAttack == 2) {
            A2.Play();
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
