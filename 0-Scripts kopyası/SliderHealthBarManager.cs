using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderHealthBarManager : MonoBehaviour
{
    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }

    private void Start()
    {
        MaxHealth = 20f;
        CurrentHealth = MaxHealth;
    }
    private void Update()
    {
        
    }
}
