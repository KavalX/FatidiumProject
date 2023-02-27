
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Slider _healthBarSlider;

    public void Start()
    {
        _healthBarSlider = GetComponent<Slider>();
    }

    public void SetHealth(float health)
    {
        _healthBarSlider.value = health;
    }
    
}