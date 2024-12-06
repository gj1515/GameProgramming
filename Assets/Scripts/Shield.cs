using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Shield : MonoBehaviour
{
    Renderer _renderer;
    [SerializeField] AnimationCurve _DisplacementCurve;
    [SerializeField] float _DisplacementMagnitude;
    [SerializeField] float _LerpSpeed;
    [SerializeField] float _DisolveSpeed;
    bool _shieldOn;
    Coroutine _disolveCoroutine;
    SphereCollider _sphereCollider;

    [SerializeField] private float _shieldActivationDelay;

    Animator animator;

    void Start()
    {
        _renderer = GetComponent<Renderer>();

        _sphereCollider = GetComponent<SphereCollider>();
        if (_sphereCollider == null)
        {
            Debug.LogError("SphereCollider is missing!");
        }

        animator = GetComponentInParent<Animator>();
    }

    void Update()
    {
        if (Mouse.current.middleButton.isPressed)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                HitShield(hit.point);
            }
        }
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            OpenCloseShield();
        }
    }

    public void HitShield(Vector3 hitPos)
    {
        _renderer.material.SetVector("_HitPos", hitPos);
        StopAllCoroutines();
        StartCoroutine(Coroutine_HitDisplacement());
    }

    public void OpenCloseShield()
    {
        float target = 1;

        if (_shieldOn)
        {
            target = 0;
            StartCoroutine(DelayedShieldActivation(target, _shieldActivationDelay));
        }
        else
        {
            if (_disolveCoroutine != null)
            {
                StopCoroutine(_disolveCoroutine);
            }
            _disolveCoroutine = StartCoroutine(Coroutine_DisolveShield(target));
            if (_sphereCollider != null)
            {
                _sphereCollider.enabled = false;
            }
        }

        _shieldOn = !_shieldOn;

        if (animator != null)
        {
            animator.SetBool("Shield", !_shieldOn);
        }
    }

    IEnumerator Coroutine_HitDisplacement()
    {
        float lerp = 0;
        while (lerp < 1)
        {
            _renderer.material.SetFloat("_DisplacementStrength", _DisplacementCurve.Evaluate(lerp) * _DisplacementMagnitude);
            lerp += Time.deltaTime * _LerpSpeed;
            yield return null;
        }
    }

    IEnumerator Coroutine_DisolveShield(float target)
    {
        float start = _renderer.material.GetFloat("_Disolve");
        float lerp = 0;
        while (lerp < 1)
        {
            _renderer.material.SetFloat("_Disolve", Mathf.Lerp(start, target, lerp));
            lerp += Time.deltaTime * _DisolveSpeed;
            yield return null;
        }
    }

    IEnumerator DelayedShieldActivation(float target, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (_disolveCoroutine != null)
        {
            StopCoroutine(_disolveCoroutine);
        }
        _disolveCoroutine = StartCoroutine(Coroutine_DisolveShield(target));
        if (_sphereCollider != null)
        {
            _sphereCollider.enabled = true;
        }
    }
}
