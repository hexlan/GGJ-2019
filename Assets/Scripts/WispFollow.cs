using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WispFollow : MonoBehaviour
{
    public EventVector3 OnClickEnvironment;

    // Update is called once per frame
    void Update()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        var heading = player.transform.position - transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance;

        if (distance > 8)
        {
            OnClickEnvironment.Invoke(transform.position + direction * 7);
        }
    }
}
