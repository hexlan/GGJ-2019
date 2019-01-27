using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierDialog : MonoBehaviour
{
    private ModalPanel modalPanel;
    private readonly string[] dialogSequence = { "Who are you?", "I'm not moving.", "I want my Frog." };
    private bool isActive = true;

    private void Awake()
    {
        modalPanel = ModalPanel.Instance();
    }

    public void Barrier()
    {
        if (isActive)
        {
            isActive = false;
            modalPanel.SetDialog("Barrier Spirit", dialogSequence);
        }
    }
}
