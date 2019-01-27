using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseManager : MonoBehaviour
{

    public LayerMask triggers;
    public EventVector3 OnClickEnvironment;
    private bool disabled = false;

    // Update is called once per frame
    void Update()
    {
        if (!disabled)
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

    public void DisableInput(bool value)
    {
        disabled = value;
    }
}

[System.Serializable]
public class EventVector3 : UnityEvent<Vector3> { }