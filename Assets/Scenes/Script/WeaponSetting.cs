using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSetting : MonoBehaviour
{
    [SerializeField]
    private player player;
    public int damage;
    private int plusDamage1;
    private int plusDamage2;
    public float delay=0.55f;
    private Weapon weapon;
    private void Start()
    {
       weapon = GetComponent<Weapon>();
    }
    public void setSkill(string skillName,int setDamage)
    {
        plusDamage1 = setDamage;
        AttackName = skillName;
    }
    public void setSkill2(string skillName, int setDamage)
    {
        plusDamage2 = setDamage;
        AttackName2 = skillName;
    }
    string AttackName= "KoalaSwordSlash1";
    string AttackName2 = "KoalaSwordSlash3";
    public string SelectfirstAttack()
    {
        transform.localScale = new Vector3(1, 1, 1);
        delay = 0.55f;
        contrallX = 0f;
         contrallY = 0f;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        switch (AttackName)
        {
            case "KoalaSwordSlash1":
                damage = 20;
                normalflip();
                AttackName = "KoalaSwordSlash1"; break;
            case "SpeedSlash":
                damage = 25+plusDamage1;
                AttackName = "SpeedSlash"; break;
            case "SpearSlash":
                SpearSlashSetting(1);
                AttackName = "SpearSlash"; break;
            case "SquadSlash":
                flippp();
                damage = 30 + plusDamage1;
                AttackName = "SquadSlash"; break;
            case "SuperSlash":
                damage = 10 + plusDamage1;
                contrallX = 4;
                flip();
                AttackName = "SuperSlash"; break;
            case "blood_slash1":
                damage = 20 + plusDamage1;
                contrallX = 2;
                flip();
                AttackName = "blood_slash1"; break;
            case "clawsSlash":
                damage = 30 + plusDamage1;
                contrallX = 2;
                flip();
                AttackName = "clawsSlash"; break;
            case "SlashV4":
                damage = 20 + plusDamage1;
                AttackName = "SlashV4"; break;
            case "SlashV3":
                AttackName = "SlashV3"; break;
            case "lineSlash":
                transform.Rotate(0, 0, 90);
                transform.localScale= new Vector3(1, plusDamage1, 1);
                damage = 20 ;
                AttackName = "lineSlash"; break;
            case "LightDarkSlash":
                AttackName = "LightDarkSlash"; break;
        }
        return AttackName;
    }
    public string Selectlastttack()
    {
        transform.localScale = new Vector3(1, 1, 1);
        delay = 0.55f;
        contrallX = 0f;
         contrallY = 0f;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        switch (AttackName2)
        {
            case "KoalaSwordSlash3":
                normalflip();
                damage = 20;
                flippp();
                AttackName2 = "KoalaSwordSlash3"; break;
            case "SpeedSlash":
                damage = 25 + plusDamage2;
                AttackName2 = "SpeedSlash"; break;
            case "SpearSlash":
                SpearSlashSetting(2);
                AttackName2 = "SpearSlash"; break;
            case "SquadSlash":
                damage = 30 + plusDamage2;
                flippp();
                AttackName2 = "SquadSlash"; break;
            case "SuperSlash":
                damage = 10 + plusDamage2;
                contrallX = 4;
                flip();
                AttackName2 = "SuperSlash"; break;
            case "blood_slash2":
                damage = 50 + plusDamage2;
                contrallX = 2;
                flip();
                AttackName2 = "blood_slash2"; break;

            case "clawsSlash":
                damage = 30 + plusDamage2;
                contrallX = 2;
                flip();
                AttackName2 = "clawsSlash"; break;
            case "SlashV4":
                damage = 20 + plusDamage2;
                AttackName2 = "SlashV4"; break;
            case "SlashV3":
                AttackName2 = "SlashV3"; break;
            case "lineSlash":
                transform.Rotate(0, 0, 90);
                transform.localScale = new Vector3(1, plusDamage1, 1);
                damage = 20;
                AttackName2 = "lineSlash"; break;
            case "LightDarkSlash":
                AttackName2 = "LightDarkSlash"; break;
        }
        return AttackName2;
    }
    float contrallX;
    float contrallY;
    private void SpearSlashSetting(int atteck)
    {
        if (atteck == 1)
        {
            damage = 50 ;
            if (plusDamage1 == 1)
                delay = 0.45f;
            else if (plusDamage1 == 2)
                delay = 0.35f;
            else if (plusDamage1 >= 3)
                delay = 0.30f;

            contrallX = 2;
            flip();

            transform.rotation = Quaternion.Euler(0, 0, player.IsMovingRight() ? -90 : 90);
        }
        else
        {
            damage = 50;
            if (plusDamage2 == 1)
            {
                delay = 0.35f;
            }
            contrallX = 2;
            flip();
            transform.rotation = Quaternion.Euler(0, 0, 180);

        }
    }
    private void flip()
    {
        if (weapon.flipHorizontally())
        {
            transform.position = new Vector3(player.transform.position.x + contrallX, player.transform.position.y + 2+ contrallY, 0);
        }
        else
        {
            transform.position = new Vector3(player.transform.position.x - contrallX, player.transform.position.y + 2+ contrallY, 0);
        }
    }
    private void normalflip()
    {
        if (weapon.flipHorizontally())
        {
            transform.position = new Vector3(player.transform.position.x + contrallX, player.transform.position.y  + contrallY, 0);
        }
        else
        {
            transform.position = new Vector3(player.transform.position.x - contrallX, player.transform.position.y  + contrallY, 0);
        }
    }
    private void flippp()
    {
        if (weapon.flipHorizontally())
        {
            transform.localScale = new Vector2(1f, 1f); // X 스케일을 -1로 변경하여 반전
        }
        else
        {
            transform.localScale = new Vector2(-1f, 1f); // X 스케일을 -1로 변경하여 반전
        }
    }
}
