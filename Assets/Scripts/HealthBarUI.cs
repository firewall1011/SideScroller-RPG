using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField]
    private Image foregroundImage;
    [SerializeField]
    private float updateSpeedSeconds = 0.5f;

    private void Awake()
    {
        GetComponentInParent<HealthSystem>().onHealthChange += HandleHealthChanged;

    }

    private void HandleHealthChanged(float pct)
    {
        if (pct > 0f)
            StartCoroutine(ChangeToPct(pct));
        else
            foregroundImage.fillAmount = pct;
    }

    private IEnumerator ChangeToPct(float pct)
    {
        float preChangePct = foregroundImage.fillAmount;
        float timeElapsed = 0f;

        while(timeElapsed < updateSpeedSeconds)
        {
            timeElapsed += Time.deltaTime;
            foregroundImage.fillAmount = Mathf.Lerp(preChangePct, pct, timeElapsed / updateSpeedSeconds);
            yield return null;
        }

        foregroundImage.fillAmount = pct;
    }
}
