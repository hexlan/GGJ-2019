using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelThreePlayer : MonoBehaviour
{
    public DialogManager dialog;
    public MouseManager mouse;

    public GameObject lantern;

    int state = 0;

    bool suckFire = false;
    bool end = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Dialog_1")
        {
            var lookPos = GameObject.FindGameObjectWithTag("Wisp").transform.position - transform.position;
            lookPos.y = 0;
            transform.rotation = Quaternion.LookRotation(lookPos);
            Destroy(other.gameObject);

            GameObject.FindGameObjectWithTag("Wisp").GetComponent<WispFollow>().enabled = false;

            DialogSnippet[] snippet =
            {
                new DialogSnippet("Wisp", "This is where I must leave, for spirits cannot pass through the spirit gate."),
                new DialogSnippet("Wisp", "Remember all you need to do is pass through the gate and use the latern."),
                new DialogSnippet("Kid", "And then I'll be home?"),
                new DialogSnippet("Wisp", "And you'll be home soon enough... we all will be."),
                new DialogSnippet("Kid", "Goodbye Mr. Wisp.")
            };

            dialog.StartDialog(snippet);
        }
        else if (other.gameObject.tag == "End")
        {
            var lookPos = GameObject.FindGameObjectWithTag("FireSpirit").transform.position - transform.position;
            lookPos.y = 0;
            transform.rotation = Quaternion.LookRotation(lookPos);

            DialogSnippet[] snippet =
            {
                new DialogSnippet("Kid", "Ahhhhh!"),
                new DialogSnippet("Fire Spirit", "Bwahahaha. What meek creature is it that wanders into my den?"),
                new DialogSnippet("Fire Spirit", "A human child? Is this the best those lowly spirits can muster?"),
                new DialogSnippet("Kid", "Mr. Wisp said I just need to take use the lantern to get home. Here we go?"),
                new DialogSnippet("Fire Spirit", "The Lantern?! NO!")
            };

            dialog.StartDialog(snippet);
            suckFire = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(suckFire && !mouse.disabled)
        {
            GameObject.Instantiate(lantern, transform.position + new Vector3(-1f, 0.5f, 1f), Quaternion.Euler(-90, 0, -53));
            suckFire = false;
            GameObject.FindGameObjectWithTag("FireSpirit").GetComponent<FireDeath>().start = true;
        }
        else if(end && !mouse.disabled)
        {
            var levelChanger = GameObject.FindGameObjectWithTag("LevelChanger");
            levelChanger.GetComponent<LevelChanger>().FadeToLevel(4);
        }

        if (!GetComponent<NavMeshAgent>().pathPending)
        {
            if (GetComponent<NavMeshAgent>().remainingDistance <= GetComponent<NavMeshAgent>().stoppingDistance)
            {
                if (!GetComponent<NavMeshAgent>().hasPath || GetComponent<NavMeshAgent>().velocity.sqrMagnitude == 0f)
                {
                    if (mouse.mushroom)
                    {
                        mushroom();
                    }
                    else if (mouse.lantern)
                    {
                        Destroy(GameObject.FindGameObjectWithTag("Lantern"));
                        state = 1;
                        mouse.lantern = false;

                        var lookPos = GameObject.FindGameObjectWithTag("Wisp").transform.position - transform.position;
                        lookPos.y = 0;
                        transform.rotation = Quaternion.LookRotation(lookPos);

                        DialogSnippet[] snippets = new DialogSnippet[] {
                            new DialogSnippet("Wisp", "You did it! You found the fire lantern. You are almost home. All that is left is passing through the spirit gate."),
                            new DialogSnippet("Kid", "And that is all I have to do?"),
                            new DialogSnippet("Wisp", "Yes, yes. Let's hurry now.")
                        };
                        dialog.StartDialog(snippets);
                    }
                }
            }
        }
    }

    public void finalWords()
    {
        DialogSnippet[] snippets = new DialogSnippet[] {
            new DialogSnippet("Eyeball", "You did it! You saved us, even Froggy!"),
            new DialogSnippet("Frog", "Ribbit"),
            new DialogSnippet("Hag", "Thank you child, we are eternally grateful?"),
            new DialogSnippet("Kid", "Did I do good? Do I get to go home?"),
            new DialogSnippet("Hag", "Yes child, go home.")
        };
        dialog.StartDialog(snippets);
        end = true;
    }

    private void mushroom()
    {
        var lookPos = GameObject.FindGameObjectWithTag("Mushroom").transform.position - transform.position;
        lookPos.y = 0;
        transform.rotation = Quaternion.LookRotation(lookPos);

        DialogSnippet[] snippets = new DialogSnippet[] { new DialogSnippet("Mushroom", "This dialog, should not happen...") };
        switch (state)
        {
            case 0:
                snippets = new DialogSnippet[]{
                    new DialogSnippet("Mushroom", "The only thing in this direction is the Fire Spirit's den. Are you sure you want to continue?"),
                    new DialogSnippet("Wisp", "We should look for the fire lantern before we go this way")
                };
                break;
            case 1:
                snippets = new DialogSnippet[]{
                    new DialogSnippet("Mushroom", "The only thing in this direc..."),
                    new DialogSnippet("Wisp", "Is the Fire Spirit's den, we know. But we have the fire lantern."),
                    new DialogSnippet("Mushroom", "Then go if you must. And take care."),
                };

                GameObject.FindGameObjectWithTag("PathBlocker").SetActive(false);
                break;
        }

        dialog.StartDialog(snippets);

        mouse.mushroom = false;
    }
}