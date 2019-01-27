using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluteDialog : MonoBehaviour
{
    private ModalPanel modalPanel;
    private readonly string[] dialogSequence = { "*You play the flute", "*The Frog allows you to pick it up.", "Ribbit." };
    private bool isActive = true;

    private void Awake()
    {
        modalPanel = ModalPanel.Instance();
    }

    public void Flute()
    {
        if (isActive)
        {
            isActive = false;
            modalPanel.SetDialog("Flute", dialogSequence);
        }
    }
}
