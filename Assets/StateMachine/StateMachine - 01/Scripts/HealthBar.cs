using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{

  private Unit unitReference;

  private RectTransform rectTransform;

  private float currentHealth;
  private float maxHealth;

  private Slider healthSlider;

  private void Awake()
  {
    rectTransform = GetComponent<RectTransform>();
    healthSlider = GetComponent<Slider>();
  }

  public void SetUnitReference(Unit unit)
  {
    unitReference = unit;
    currentHealth = unit.Health;
    maxHealth = unit.MaxHealth;
    healthSlider.maxValue = maxHealth;
    healthSlider.minValue = 0;
  }

  public void UpdateHealth(int updatedHealth)
  {
    currentHealth = updatedHealth;
    healthSlider.value = currentHealth;
  }

}
