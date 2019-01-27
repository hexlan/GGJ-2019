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
            levelChanger.GetComponent<LevelChanger>().FadeToLevel(2);
        }
        else if (other.gameObject.tag == "Begining_Dialog")
        {
            Destroy(other.gameObject);

            DialogSnippet[] dialog =
            {
                new DialogSnippet("Kid", "Oooh, maybe I can find a cool bug tonight!"),
                new DialogSnippet("Kid", "I should look around!")
            };

            manager.StartDialog(dialog);
        }
        else if (other.gameObject.tag == "Dialog_1")
        {
            Destroy(other.gameObject);

            DialogSnippet[] dialog =
            {
                new DialogSnippet("Dad", "Don't go wandering too far, it's getting late."),
                new DialogSnippet("Mom", "And remember to stay out of the woods!"),
                new DialogSnippet("Kid", "I'll stay on the path!")
            };

            manager.StartDialog(dialog);
        }
        else if (other.gameObject.tag == "Dialog_2")
        {
            Destroy(other.gameObject);

            DialogSnippet[] dialog =
            {
                new DialogSnippet("Kid", "What's that!?"),
                new DialogSnippet("Kid", "Wow!"),
                new DialogSnippet("Kid", "Is that a firefly?"),
                new DialogSnippet("Kid", "I've never seen one before!"),
                new DialogSnippet("Kid", "Maybe I can catch it.")
            };

            transform.LookAt(GameObject.FindGameObjectWithTag("Wisp").transform);

            manager.StartDialog(dialog);
        }
        else if (other.gameObject.tag == "Dialog_3")
        {

            Destroy(other.gameObject);
            DialogSnippet[] dialog =
            {
                new DialogSnippet("Kid", "It sure is quick. "),
                new DialogSnippet("Kid", "I can't let it get away now!")
            };

            manager.StartDialog(dialog);
        }
    }
}
