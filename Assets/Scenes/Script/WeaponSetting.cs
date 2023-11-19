using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSetting : MonoBehaviour
{
    [SerializeField]
    private player player;
    public int damage;
    public float delay=0.55f;
    private Weapon weapon;
    private void Start()
    {
       weapon = GetComponent<Weapon>();
    }
    public void setSkill(string skillName)
    {
        AttackName = skillName;
    }
    public void setSkill2(string skillName)
    {
        AttackName2 = skillName;
    }
    string AttackName= "KoalaSwordSlash1";
    string AttackName2 = "KoalaSwordSlash3";
    public string SelectfirstAttack()
    {
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
                damage = 25;
                AttackName = "SpeedSlash"; break;
            case "SpearSlash":
                SpearSlashSetting(1);
                AttackName = "SpearSlash"; break;
            case "SquadSlash":
                flippp();
                damage = 30;
                AttackName = "SquadSlash"; break;
            case "SuperSlash":
                damage = 10;
                contrallX = 4;
                flip();
                AttackName = "SuperSlash"; break;
            case "blood_slash1":
                damage = 20;
                contrallX = 2;
                flip();
                AttackName = "blood_slash1"; break;
            case "clawsSlash":
                damage = 30;
                contrallX = 2;
                flip();
                AttackName = "clawsSlash"; break;
            case "SlashV4":
                damage = 20;
                AttackName = "SlashV4"; break;
            case "SlashV3":
                AttackName = "SlashV3"; break;
            case "lineSlash":
                transform.Rotate(0, 0, 90);
                damage = 20;
                AttackName = "lineSlash"; break;
            case "LightDarkSlash":
                AttackName = "LightDarkSlash"; break;
        }
        Debug.Log("at1");
        return AttackName;
    }
    public string Selectlastttack()
    {
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
                damage = 25;
                AttackName2 = "SpeedSlash"; break;
            case "SpearSlash":
                SpearSlashSetting(2);
                AttackName2 = "SpearSlash"; break;
            case "SquadSlash":
                damage = 30;
                flippp();
                AttackName2 = "SquadSlash"; break;
            case "SuperSlash":
                damage = 10;
                contrallX = 4;
                flip();
                AttackName2 = "SuperSlash"; break;
            case "blood_slash2":
                damage = 50;
                contrallX = 2;
                flip();
                AttackName2 = "blood_slash2"; break;

            case "clawsSlash":
                damage = 30;
                contrallX = 2;
                flip();
                AttackName2 = "clawsSlash"; break;
            case "SlashV4":
                damage = 20;
                AttackName2 = "SlashV4"; break;
            case "SlashV3":
                AttackName2 = "SlashV3"; break;
            case "lineSlash":
                transform.Rotate(0, 0, 90);
                damage = 20;
                AttackName2 = "lineSlash"; break;
            case "LightDarkSlash":
                AttackName2 = "LightDarkSlash"; break;
        }
        Debug.Log("at2");
        return AttackName2;
    }
    float contrallX;
    float contrallY;
    private void SpearSlashSetting(int atteck)
    {
        if (atteck == 1)
        {
            damage = 50;
            contrallX = 2;
            flip();

            transform.rotation = Quaternion.Euler(0, 0, player.IsMovingRight() ? -90 : 90);
        }
        else
        {
            damage = 50;
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
