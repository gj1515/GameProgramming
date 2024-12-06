using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class FPSDisplay : MonoBehaviour
{
    private TMP_Text fpsText;
    private float deltaTime = 0.0f;
    private float updateInterval = 0.5f;
    private float nextUpdate = 0.0f;

    private void Start()
    {
        fpsText = GetComponent<TMP_Text>();
        if (fpsText == null)
        {
            enabled = false;
            return;
        }
    }

    private void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        if (Time.unscaledTime > nextUpdate)
        {
            float fps = 1.0f / deltaTime;

            fpsText.text = $"FPS: {Mathf.Round(fps)}";

            if (fps >= 50)
                fpsText.color = Color.white;
            else if (fps >= 30)
                fpsText.color = Color.yellow;
            else
                fpsText.color = Color.red;

            nextUpdate = Time.unscaledTime + updateInterval;
        }
    }
}
