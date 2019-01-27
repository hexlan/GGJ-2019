using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampScript : MonoBehaviour
{
    Light light;
    bool increase = true;

    private void Start()
    {
        light = GetComponent<Light>();
        light.intensity = (Random.value / 10) + 0.5f;
    }

    void Update()
    {
        if(increase)
        {
            light.intensity += 0.01f;
            if (light.intensity > 0.6f) increase = false;
        }
        else
        {
            light.intensity -= 0.01f;
            if (light.intensity < 0.5f) increase = true;
        }
    }
}
