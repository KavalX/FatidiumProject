using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider _staminaBarSlider;

    public void Start()
    {
        _staminaBarSlider = GetComponent<Slider>();
    }

    public void SetStamina(float health)
    {
        _staminaBarSlider.value = health;
    }

}
