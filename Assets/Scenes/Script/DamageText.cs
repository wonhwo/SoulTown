using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DamageText : MonoBehaviour
{
    public float moveSpeed; //텍스트 이동속도
    public float alphaSpeed;//투명도 변환속도
    public float destrotTime;
    TextMeshPro text;
    Color alpha;
    // Start is called before the first frame update
    void Start()
    {
        //text = GetComponent<TextMeshPro>();
        //alpha = text.color;
        StartCoroutine(DestroyTextWithDelay(destrotTime));
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));
        //alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);
        //text.color = alpha;
    }
    private IEnumerator DestroyTextWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
