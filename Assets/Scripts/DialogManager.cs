using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Image dialogBackground;
    private DialogSnippet[] dialog;
    private int dialogIndex = 0;
    public MouseManager input;
    public EventVector3 OnClickEnvironment;

    private void Start()
    {
        dialogBackground.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogBackground.IsActive())
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (dialogIndex + 1 < dialog.Length)
                {
                    dialogIndex++;
                    showDialog();
                }
                else
                {
                    dialogBackground.gameObject.SetActive(false);
                    input.DisableInput(false);
                }
            }
        }

    }

    void showDialog()
    {
        dialogBackground.gameObject.SetActive(true);
        dialogBackground.transform.Find("Speaker").gameObject.GetComponent<Text>().text = dialog[dialogIndex].speaker;
        dialogBackground.transform.Find("Text").gameObject.GetComponent<Text>().text = dialog[dialogIndex].line;
    }

    public void StartDialog(DialogSnippet[] newDialog)
    {
        input.DisableInput(true);
        OnClickEnvironment.Invoke(GameObject.FindGameObjectWithTag("Player").transform.position);

        dialog = newDialog;
        dialogIndex = 0;
        showDialog();
    }
}

public class DialogSnippet
{
    public string speaker;
    public string line;

    public DialogSnippet(string speaker, string line)
    {
        this.speaker = speaker;
        this.line = line;
    }
}
