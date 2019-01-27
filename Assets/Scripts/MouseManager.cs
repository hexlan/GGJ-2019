using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseManager : MonoBehaviour
{

    public LayerMask triggers;
    public EventVector3 OnClickEnvironment;
    private bool disabled = false;

    public bool wisp = false;
    public bool hag = false;
    public bool eyeball = false;
    public bool frog = false;

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
                        if (hit.collider.gameObject.tag == "Hag")
                        {
                            hag = true;
                            OnClickEnvironment.Invoke(new Vector3(-8, 12.631f, -10));
                        }
                        else if (hit.collider.gameObject.tag == "Wisp")
                        {
                            wisp = true;
                        }
                        else if (hit.collider.gameObject.tag == "Eyeball")
                        {
                            eyeball = true;
                            OnClickEnvironment.Invoke(new Vector3(-17.5f, 12.631f, 18));
                        }
                        else if (hit.collider.gameObject.tag == "Frog")
                        {
                            frog = true;
                            OnClickEnvironment.Invoke(new Vector3(8f, 12.631f, 9));
                        }
                        else
                        {
                            wisp = false;
                            hag = false;
                            eyeball = false;
                            frog = false;
                            OnClickEnvironment.Invoke(hit.point);
                            
                        }
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