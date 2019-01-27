using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseManager : MonoBehaviour
{

    public LayerMask triggers;

    public EventVector3 OnClickEnvironment;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Clickable"))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    OnClickEnvironment.Invoke(hit.point);
                }
            }
        }
    }
}

[System.Serializable]
public class EventVector3 : UnityEvent<Vector3> { }