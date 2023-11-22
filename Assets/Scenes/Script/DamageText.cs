using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class DamageText : MonoBehaviour
{
    public float moveSpeed = 1.0f;

    private void Update()
    {
        //transform.position += Vector3.up * moveSpeed * Time.deltaTime;
    }
    public void DestroyEvent()
    {
        Destroy(gameObject);
    }
}
