using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Active : MonoBehaviour
{
    public GameObject targetObject;
    public float delayInSeconds = 5f;

    void Start()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(false);
            StartCoroutine(ActivateObjectAfterDelay());
        }
        else
        {
            Debug.LogError("Target Object is not assigned!");
        }
    }

    IEnumerator ActivateObjectAfterDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);
        targetObject.SetActive(true);
    }
}
