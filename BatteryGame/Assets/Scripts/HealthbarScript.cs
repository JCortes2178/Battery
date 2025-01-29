using UnityEngine;
using UnityEngine.UI;
public class HealthbarScript : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    
    public void SetMaxHealth(float maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        fill.color = gradient.Evaluate(1f); //maxHealth
    }

    public void SetHealth(float health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
