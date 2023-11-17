using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    private Animator animation;
    Animator parentAnimator;
    private int damage;
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
    private void weaponAnimation() {
        if (Input.GetKeyDown(KeyCode.LeftControl)&&!isSlash)
        {
            
            StartCoroutine(AnimationDelay());
            damage = 50;
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
    float delay = 0.55f;
    private string SelectfirstAttack()
    {
        string AttackName = "blood_slash";
        switch (AttackName)
        {
            case "SpeedSlash":
                AttackName = "SpeedSlash";break;
            case "SpearSlash":
                transform.localScale = new Vector2(1f, 1f); // X 스케일을 -1로 변경하여 반전
                AttackName = "SpearSlash"; break;
            case "SquadSlash":
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
        string AttackName = "SpearSlash";
        switch (AttackName)
        {
            case "SpeedSlash":
                AttackName = "SpeedSlash"; break;
            case "SpearSlash":
                transform.localScale = new Vector2(-1f, 1f); // X 스케일을 -1로 변경하여 반전
                AttackName = "SpearSlash"; break;
            case "SquadSlash":
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
