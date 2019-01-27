using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelTwoPlayer : MonoBehaviour
{
    public DialogManager dialog;
    public MouseManager mouse;

    int state = 0;
    // 0 - Talk to Hag
    // 1 - Talk to Eyeball
    // 2 - Frog 1
    // 3 - Need music
    // 4 - Got Flute
    // 5 - Got Frog
    // 6 - Continue Onwards

    bool frogIsOnBeach = true;

    private void Start()
    {
        DialogSnippet[] snippets = {
            new DialogSnippet("Boy", "I'm lost!"),
            new DialogSnippet("???", "Hi lost, I'm wisp"),
            new DialogSnippet("Boy", "Hardee-har-har"),
            new DialogSnippet("???", "Dialog"),
            new DialogSnippet("Boy", "Dialog"),
            new DialogSnippet("???", "Dialog"),
            new DialogSnippet("Boy", "Dialog"),
            new DialogSnippet("???", "Dialog"),
            new DialogSnippet("Boy", "Dialog"),
            new DialogSnippet("???", "Go speak with the hag, she'll know what to do.")
        };

        dialog.StartDialog(snippets);
    }

    // Update is called once per frame
    void Update()
    {
        var frogPosition = new Vector3(7.7f, 11.59f, 9.01f);

        var distance = Vector3.Distance(transform.position, frogPosition);
        if (state < 5)
        {
            if (distance < 4 && frogIsOnBeach)
            {
                GameObject.FindGameObjectWithTag("Frog").GetComponent<Animator>().Play("FrogJump");
                frogIsOnBeach = false;
                if (state == 2) { state = 3; }
            }
            else if (distance > 4 && !frogIsOnBeach)
            {
                GameObject.FindGameObjectWithTag("Frog").GetComponent<Animator>().Play("FrogReverseJump");
                frogIsOnBeach = true;
            }
        }

        if (!GetComponent<NavMeshAgent>().pathPending)
        {
            if (GetComponent<NavMeshAgent>().remainingDistance <= GetComponent<NavMeshAgent>().stoppingDistance)
            {
                if (!GetComponent<NavMeshAgent>().hasPath || GetComponent<NavMeshAgent>().velocity.sqrMagnitude == 0f)
                {
                    if (mouse.hag)
                    {
                        hag();
                    }
                    else if (mouse.wisp)
                    {
                        wisp();
                    }
                    else if (mouse.eyeball)
                    {
                        eyeball();
                    }
                    else if (mouse.frog)
                    {
                        var lookPos = GameObject.FindGameObjectWithTag("Frog").transform.position - transform.position;
                        lookPos.y = 0;
                        transform.rotation = Quaternion.LookRotation(lookPos);

                        DialogSnippet[] snippets = new DialogSnippet[] { new DialogSnippet("Frog", "Ribbit") };
                        if (state == 4)
                        {
                            snippets = new DialogSnippet[]{
                                new DialogSnippet("Boy", "*Plays Flute*"),
                                new DialogSnippet("Frog", "Ribbit Ribbit")
                            };

                            dialog.StartDialog(snippets);
                            Destroy(GameObject.FindGameObjectWithTag("Frog"));
                            state = 5;
                        }
                        else
                        {
                            dialog.StartDialog(snippets);
                        }

                        mouse.frog = false;
                    }
                }
            }
        }
    }

    private void wisp()
    {
        DialogSnippet[] snippets = new DialogSnippet[] { new DialogSnippet("Wisp", "This dialog, should not happen...") };
        switch (state)
        {
            case 0:
                snippets = new DialogSnippet[]{
                    new DialogSnippet("Wisp", "Speak with the hag.")
                };
                break;
            case 1:
                snippets = new DialogSnippet[]{
                    new DialogSnippet("Wisp", "Head north.")
                };
                break;
            case 2:
                snippets = new DialogSnippet[]{
                    new DialogSnippet("Wisp", "Retrieve the eyeball's frog.")
                };
                break;
            case 3:
                snippets = new DialogSnippet[]{
                    new DialogSnippet("Wisp", "We may need to play some music, check with the hag.")
                };
                break;
            case 4:
                snippets = new DialogSnippet[]{
                    new DialogSnippet("Wisp", "You have the flute, go play for the frog.")
                };
                break;
            case 5:
                snippets = new DialogSnippet[]{
                    new DialogSnippet("Wisp", "Take the from back to the eyeball so we can go North.")
                };
                break;
            case 6:
                snippets = new DialogSnippet[]{
                    new DialogSnippet("Wisp", "Make your way North.")
                };
                break;
        }

        dialog.StartDialog(snippets);
        mouse.wisp = false;
    }

    private void hag()
    {
        var lookPos = GameObject.FindGameObjectWithTag("Hag").transform.position - transform.position;
        lookPos.y = 0;
        transform.rotation = Quaternion.LookRotation(lookPos);

        DialogSnippet[] snippets = new DialogSnippet[] { new DialogSnippet("Hag", "This dialog, should not happen...") };
        switch (state)
        {
            case 0:
                state = 1;
                snippets = new DialogSnippet[]{
                    new DialogSnippet("Hag", "Head North to get home.")
                };
                break;
            case 1:
                snippets = new DialogSnippet[]{
                    new DialogSnippet("Hag", "Head North to get home.")
                };
                break;
            case 2:
                snippets = new DialogSnippet[]{
                    new DialogSnippet("Hag", "There's an eyeball blocking the way? Better retrieve his frog.")
                };
                break;
            case 3:
                snippets = new DialogSnippet[]{
                    new DialogSnippet("Hag", "The frog enjoys music? Well here, take my flute.")
                };
                state = 4;
                break;
            case 4:
                snippets = new DialogSnippet[]{
                    new DialogSnippet("Hag", "Go play the flute for the frog.")
                };
                break;
            case 5:
                snippets = new DialogSnippet[]{
                    new DialogSnippet("Hag", "Return the frog to the eyeball and make your way North.")
                };
                break;
            case 6:
                snippets = new DialogSnippet[]{
                    new DialogSnippet("Hag", "Make your way North.")
                };
                break;
        }

        dialog.StartDialog(snippets);

        mouse.hag = false;
    }

    private void eyeball()
    {
        var lookPos = GameObject.FindGameObjectWithTag("Eyeball").transform.position - transform.position;
        lookPos.y = 0;
        transform.rotation = Quaternion.LookRotation(lookPos);

        DialogSnippet[] snippets = new DialogSnippet[] { new DialogSnippet("Eyeball", "This dialog, should not happen...") };
        switch (state)
        {
            case 0:
                snippets = new DialogSnippet[]{
                    new DialogSnippet("Eyeball", "Go Away.")
                };
                break;
            case 1:
                snippets = new DialogSnippet[]{
                    new DialogSnippet("Eyeball", "I've lost my pet frog and I'm not moving until I get him back."),
                    new DialogSnippet("Boy", "I'll get him back for you."),
                    new DialogSnippet("Eyeball", "You'd do that? He's shy, but will come to you if you play music.")
                };
                state = 2;
                break;
            case 2:
                snippets = new DialogSnippet[]{
                    new DialogSnippet("Eyeball", "Do you have my frog yet?"),
                    new DialogSnippet("Boy", "Not yet."),
                    new DialogSnippet("Eyeball", "Well hurry up would you?")
                };
                break;
            case 3:
                snippets = new DialogSnippet[]{
                    new DialogSnippet("Eyeball", "Do you have my frog yet?"),
                    new DialogSnippet("Boy", "Not yet."),
                    new DialogSnippet("Eyeball", "Well hurry up would you?")
                };
                break;
            case 4:
                snippets = new DialogSnippet[]{
                    new DialogSnippet("Eyeball", "Do you have my frog yet?"),
                    new DialogSnippet("Boy", "Not yet."),
                    new DialogSnippet("Eyeball", "Well hurry up would you?")
                };
                break;
            case 5:
                snippets = new DialogSnippet[]{
                    new DialogSnippet("Eyeball", "You found my frog! Thank you!")
                };
                state = 6;
                break;
        }

        dialog.StartDialog(snippets);

        mouse.eyeball = false;
    }
}
