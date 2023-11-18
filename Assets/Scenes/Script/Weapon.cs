using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private player player;
    private Animator animation;
    Animator parentAnimator;
    private int damage;
    float delay = 0.55f;
    void Awake()
    {
        animation = GetComponent<Animator>();
        parentAnimator = transform.parent.GetComponent<Animator>();
    }
    void Update()
    {
        weaponAnimation();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // 충돌한 객체에게 변수 전달
            collision.gameObject.GetComponent<Enemy>().SetDamage(damage);
        }
    }


    int countS = 1;
    static bool isSlash = false;
    private void weaponAnimation()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isSlash)
        {
            StartCoroutine(AnimationDelay());
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            animation.Play("batleSparks");
            damage = 10;
        }
    }
    int countAttack = 1;
    private IEnumerator AnimationDelay()
    {
        parentAnimator.SetTrigger("Attack");
        if (countAttack == 1)
        {
            isSlash = true;
            sendAttack();
            animation.Play(SelectfirstAttack());
            countAttack++;
        }
        else if(countAttack == 2) {
            isSlash = true;
            sendAttack();
            animation.Play(Selectlastttack());
            //animation.Play("KoalaSwordSlash3");
            countAttack = 1;
        }
        yield return new WaitForSeconds(delay);
        isSlash = false;
        sendAttack();
    }

    private string SelectfirstAttack()
    {
        string AttackName = "SquadSlash";
        switch (AttackName)
        {
            case "KoalaSwordSlash1":
                damage = 20;
                AttackName = "KoalaSwordSlash1"; break;
            case "SpeedSlash":
                damage = 25;
                AttackName = "SpeedSlash";break;
            case "SpearSlash":
                damage = 50;
                transform.rotation = Quaternion.Euler(0, 0, player.IsMovingRight() ? -90 : 90);

                Debug.Log("at1");
                AttackName = "SpearSlash"; break;
            case "SquadSlash":
                damage = 30;
                AttackName = "SquadSlash"; break;
            case "SuperSlash":
                AttackName = "SuperSlash"; break;
            case "blood_slash":
                AttackName = "blood_slash"; break;
            case "clawsSlash":
                AttackName = "clawsSlash"; break;
            case "SlashV4":
                AttackName = "SlashV4"; break;            
            case "SlashV3":
                AttackName = "SlashV3"; break;            
            case "lineSlash":
                AttackName = "lineSlash"; break;            
            case "LightDarkSlash":
                AttackName = "LightDarkSlash"; break;
        }

        return AttackName;
    }
    private string Selectlastttack()
    {
        string AttackName = "SquadSlash";
        switch (AttackName)
        {
            case "KoalaSwordSlash3":
                damage = 20;
                transform.localScale = new Vector2(-0.5f, 0.5f); // X 스케일을 -1로 변경하여 반전
                AttackName = "KoalaSwordSlash3"; break;
            case "SpeedSlash":
                damage = 25;
                AttackName = "SpeedSlash"; break;
            case "SpearSlash":
                damage = 50;
                transform.rotation = Quaternion.Euler(0, 0, 180);
                Debug.Log("at2");
                AttackName = "SpearSlash"; break;
            case "SquadSlash":
                damage = 30;
                AttackName = "SquadSlash"; break;
            case "SuperSlash":
                AttackName = "SuperSlash"; break;
            case "blood_slash":
                AttackName = "blood_slash"; break;
            case "clawsSlash":
                AttackName = "clawsSlash"; break;
            case "SlashV4":
                AttackName = "SlashV4"; break;
            case "SlashV3":
                AttackName = "SlashV3"; break;
            case "lineSlash":
                AttackName = "lineSlash"; break;
            case "LightDarkSlash":
                AttackName = "LightDarkSlash"; break;
        }

        return AttackName;
    }
    public bool sendAttack()
    {
        return isSlash;
    }
}
