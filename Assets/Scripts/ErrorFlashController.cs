using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorFlashController : MonoBehaviour
{
    private float duration;
    private void OnEnable()
    {
        GetComponent<CanvasGroup>().alpha = 1;
        duration = 1.0f;
    }
    private void Update()
    {
        duration -= Time.deltaTime;
        GetComponent<CanvasGroup>().alpha = Mathf.Lerp(0f, 1.0f, duration/1);
        if(duration <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
