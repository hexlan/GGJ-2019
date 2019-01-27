using UnityEngine;

public class TaskDialog : MonoBehaviour
{
    private ModalPanel modalPanel;
    private readonly string[] dialogSequence = { "Greetings", "Go north to continue.", "I saw a frog by the lake." };
    private bool isActive = true;

    private void Awake()
    {
        modalPanel = ModalPanel.Instance();
    }

    public void Task()
    {
        if (isActive)
        {
            isActive = false;
            modalPanel.SetDialog("Task Giver", dialogSequence);
        }
    }
}
