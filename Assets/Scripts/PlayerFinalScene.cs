using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFinalScene : MonoBehaviour
{
    public DialogManager manager;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "End")
        {
            var levelChanger = GameObject.FindGameObjectWithTag("LevelChanger");
            levelChanger.GetComponent<LevelChanger>().FadeToLevel(0);
        }
        else if (other.gameObject.tag == "Dialog_1")
        {
            Destroy(other.gameObject);

            DialogSnippet[] dialog =
            {
                new DialogSnippet("Mom", "Pumpkin, it's time to come inside."),
                new DialogSnippet("Dad", "You have school tomorrow, so lets get you to bed.")
            };

            manager.StartDialog(dialog);
        }
    }
}
