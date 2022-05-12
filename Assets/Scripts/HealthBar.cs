using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void updateValue(float value)
    {
        slider.value = value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
