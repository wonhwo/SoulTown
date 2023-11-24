using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkControllor : MonoBehaviour
{
    [SerializeField]
    private GameObject Pattern1;
    [SerializeField]
    private GameObject Pattern2;
    [SerializeField]
    private GameObject Pattern3;
    [SerializeField]
    private GameObject Pattern4;
    private static int markCount=1;
    void Start()
    {
        StartCoroutine(OnOffMark());
    }

    IEnumerator OnOffMark()
    {
        while (true)
        {
            markOff();
            if (markCount == 1)
            {
                Pattern1.SetActive(true);
                Debug.Log("1"+markCount);
                markCount++;
            }
            else if (markCount == 2)
            {
                Pattern2.SetActive(true);
                Debug.Log("2" + markCount);
                markCount++;
            }
            else if (markCount == 3)
            {
                Pattern3.SetActive(true);
                Debug.Log("3" + markCount);
                markCount++;
            }
            else if (markCount == 4)
            {
                Pattern4.SetActive(true);
                Debug.Log("4" + markCount);
                markCount = 1;
            }
            yield return new WaitForSeconds(5.0f);
        }
        
    }
    private void markOff()
    {
        Pattern1.SetActive(false);
        Pattern2.SetActive(false);
        Pattern3.SetActive(false);
        Pattern4.SetActive(false);

    }
}
