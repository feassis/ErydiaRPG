using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarGraphics : MonoBehaviour
{
    [SerializeField] private Image backFill;
    [SerializeField] private Image fill;
    [SerializeField] private float fillSpeed = 0.2f;

    private float targetFillAmount = 1;

    private void Update()
    {
        backFill.fillAmount = Mathf.Lerp(backFill.fillAmount, targetFillAmount,
            Time.deltaTime * fillSpeed);
    }

    public void UpdateHPBar(float fillAmount)
    {
        targetFillAmount = fillAmount;
        fill.fillAmount = fillAmount;
    }
}
