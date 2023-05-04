using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject sliderParent;
    public Slider healthSlider;

    public float percent = 1.0f;

    private void Start()
    {
        UpdateVisuals();
    }
    public void SetPercent(float amount)
    {
        percent = amount;

        percent = Mathf.Clamp01(percent);

        UpdateVisuals();
    }
    private void UpdateVisuals()
    {
        healthSlider.value = percent;

        bool active = (percent == 1.0f) ? false : true;

        sliderParent.SetActive(active);
    }
}
