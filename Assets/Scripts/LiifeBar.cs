using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiifeBar : MonoBehaviour
{
    Image image;
    public Life targetLife;
    private float initialLifeAmount;

    void Awake()
    {
        image = GetComponent<Image>();
        initialLifeAmount = targetLife.amount;
    }

    void Update()
    {
        image.fillAmount = targetLife.amount / initialLifeAmount;
    }
}