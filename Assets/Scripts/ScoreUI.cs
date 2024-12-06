using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TMPro.Examples;

public class ScoreUI : MonoBehaviour
{
    TMP_Text text;
    public ScoreManager ScoreManager;
    
    void Awake()
    {
        text = GetComponent<TMP_Text>();
    }
    
    void Update()
    {
        text.text = "Score: " + ScoreManager.instance.amount;
    }
}
