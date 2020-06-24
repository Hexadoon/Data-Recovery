﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
  	public Gradient gradient;
  	public Image fill;

  	public void SetMaxHealth(float health)
  	{
  		slider.maxValue = health;
  		slider.value = health;

  		fill.color = gradient.Evaluate(1f);
      disableBar();
  	}

      public void SetHealth(float health)
  	{
  		slider.value = health;

  		fill.color = gradient.Evaluate(slider.normalizedValue);
  	}

    public void enableBar() {
      gameObject.SetActive(true);
    }
    public void disableBar() {
      gameObject.SetActive(false);
    }

}
