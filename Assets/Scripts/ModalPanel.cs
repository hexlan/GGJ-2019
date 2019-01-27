using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModalPanel : MonoBehaviour
{
    public Text characterName;
    public Text dialogText;
    public GameObject modalPanelObject;
    public bool isFinished;
    private int currentDialog;
    private string[] dialogSequence;
    private static ModalPanel modalPanel;

    public static ModalPanel Instance()
    {
        if (!modalPanel)
        {
            modalPanel = FindObjectOfType(typeof(ModalPanel)) as ModalPanel;
            if (!modalPanel)
            {
                Debug.LogError("There needs to be one active ModalPanel script on a GameObject in your scene.");
            }
        }
        return modalPanel;
    }
    // Update is called once per frame
    void Update()
    {
        if (modalPanelObject.activeInHierarchy)
        {
            if (Input.GetMouseButtonUp(1))
            {
                if (isFinished)
                {
                    ClosePanel();
                }
                else
                {
                    AdvanceDialog();
                }
            }
        }
    }

    private void AdvanceDialog()
    {
        this.dialogText.text = dialogSequence[currentDialog];
        if (!IsDialogEnd(dialogSequence))
        {
            currentDialog++;
        }
        else
        {
            isFinished = true;
        }
    }

    public void SetDialog (string characterName, string[] dialogText)
    {
        isFinished = false;
        modalPanelObject.SetActive(true);
        currentDialog = 0;
        dialogSequence = dialogText;
        this.characterName.text = characterName;
        AdvanceDialog();
    }

    private void ClosePanel()
    {
        modalPanelObject.SetActive(false);
    }

    private bool IsDialogEnd(string[] dialogSequence)
    {
        return dialogSequence.Length == currentDialog + 1;
    }
}
