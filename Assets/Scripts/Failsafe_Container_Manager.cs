using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Failsafe_Container_Manager : MonoBehaviour
{
    public Sprite Failsafes_3;
    public Sprite Failsafes_2;
    public Sprite Failsafes_1;
    public Sprite Failsafes_0;
    public Sprite Failsafes_Broken;
    public List<Sprite> spriteList = new List<Sprite>();
    int failsafesRemaining;

    public void Start()
    {
        failsafesRemaining = 3;
        spriteList.Add(Failsafes_3);
        spriteList.Add(Failsafes_2);
        spriteList.Add(Failsafes_1);
        spriteList.Add(Failsafes_0);
        spriteList.Add(Failsafes_Broken);
    }
    public void SpendFailsafe()
    {
        failsafesRemaining--;
        gameObject.GetComponent<Image>().sprite = spriteList[3-failsafesRemaining];
    }
}
