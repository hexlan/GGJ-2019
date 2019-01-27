﻿using UnityEngine;

public class WispDialog : MonoBehaviour
{
    private ModalPanel modalPanel;
    private readonly string[] dialogSequence = { "Hi", "How are you", "Glad you followed."};
    private bool isActive = true;

    private void Awake()
    {
        modalPanel = ModalPanel.Instance();
    }

    public void Wisp()
    {
        if (isActive)
        {
            isActive = false;
            modalPanel.SetDialog("Wisp", dialogSequence);
        }
    }
}
