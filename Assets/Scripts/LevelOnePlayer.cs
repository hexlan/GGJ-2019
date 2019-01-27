using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOnePlayer : MonoBehaviour
{
    public DialogManager manager;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "WispTrigger")
        {
            Destroy(other.gameObject);
            var wisp = GameObject.FindGameObjectWithTag("Wisp");
            wisp.GetComponent<LevelOneWisp>().moveWisp();
        }
        else if (other.gameObject.tag == "End")
        {
            var levelChanger = GameObject.FindGameObjectWithTag("LevelChanger");
            levelChanger.GetComponent<LevelChanger>().FadeToLevel(1);
        }
        else if (other.gameObject.tag == "Dialog_1")
        {
            Destroy(other.gameObject);

            DialogSnippet[] dialog =
            {
                new DialogSnippet("Dad", "Don't go wandering too far, it's getting late."),
                new DialogSnippet("Mom", "And remember to stay out of the woods!")
            };

            manager.StartDialog(dialog);
        }
        else if (other.gameObject.tag == "Dialog_2")
        {
            Destroy(other.gameObject);

            DialogSnippet[] dialog =
            {
                new DialogSnippet("Kid", "What's That!?")
            };

            transform.LookAt(GameObject.FindGameObjectWithTag("Wisp").transform);

            manager.StartDialog(dialog);
        }
    }
}
