using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelTwoPlayer : MonoBehaviour
{
    public DialogManager dialog;
    public MouseManager mouse;

    public GameObject firspirt;

    int state = 0;
    // 0 - Talk to Hag
    // 1 - Talk to Eyeball
    // 2 - Frog 1
    // 3 - Need music
    // 4 - Got Flute
    // 5 - Got Frog
    // 6 - Continue Onwards

    bool removeEyeSore = false;

    bool frogIsOnBeach = true;

    private void Start()
    {
        DialogSnippet[] snippets = {
            new DialogSnippet("Kid", "Finally caught up to you!"),
            new DialogSnippet("Kid", "..."),
            new DialogSnippet("Kid", "Wait wheres the path?"),
            new DialogSnippet("Kid", "Oh no! I'm lost!"),
            new DialogSnippet("???", "No need to worry."),
            new DialogSnippet("Kid", "!"),
            new DialogSnippet("Kid", "You can speeak?!"),
            new DialogSnippet("???", "Of course!"),
            new DialogSnippet("???", "Any spirt can."),
            new DialogSnippet("Kid", "Spirit?"),
            new DialogSnippet("Spirit", "Yep!"),
            new DialogSnippet("Spirit", "You've found your way to one the homes of the spirits."),
            new DialogSnippet("Spirit", "The Whispering Woods."),
            new DialogSnippet("Kid", "I'm sorry spirit."),
            new DialogSnippet("Kid", "But I shouldn't be visitning someone elses home now."),
            new DialogSnippet("Kid", "I wasn't supposed to leave my home!"),
            new DialogSnippet("Spirit", "Well mabey I can help."),
            new DialogSnippet("Spirit", "You can't leave the same way you came."),
            new DialogSnippet("Spirit", "However, I think I know a spirit that can find the path you need."),
            new DialogSnippet("Spirit", "Lets go find the Hag!"),
            new DialogSnippet("Spirit", "Oh!"),
            new DialogSnippet("Wisp", "By the way I'm Wisp!")
        };

        dialog.StartDialog(snippets);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "End")
        {
            var levelChanger = GameObject.FindGameObjectWithTag("LevelChanger");
            levelChanger.GetComponent<LevelChanger>().FadeToLevel(3);
        }else if(other.gameObject.tag == "Barrier_Down")
        {            
            Destroy(other.gameObject);

            DialogSnippet[] snippets ={
                    new DialogSnippet("Wisp", "How terrible!"),
                    new DialogSnippet("Wisp", "Although this may be fortunate for you."),
                    new DialogSnippet("Kid", "Why would that be?"),
                    new DialogSnippet("Wisp", "There is a lantern that can grant what the holder deasires."),
                    new DialogSnippet("Wisp", "However, it only works if it has enough power within."),
                    new DialogSnippet("Wisp", "If you capture that fire spirit it may reveal the path to lead you home."),
                    new DialogSnippet("Kid", "Let's go find it then!")
            };
            dialog.StartDialog(snippets);

        }
    }

        // Update is called once per frame
        void Update()
    {
        if(removeEyeSore && !mouse.disabled)
        {
            Destroy(GameObject.FindGameObjectWithTag("Eyeball"));
            Destroy(GameObject.FindGameObjectWithTag("FireSpirit"));
        }

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
                                new DialogSnippet("Kid", "*Plays Flute*"),
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
                dialog.StartDialog(snippets);
                break;
            case 1:
                snippets = new DialogSnippet[]{
                    new DialogSnippet("Eyeball", "I've lost my pet frog and I'm not moving until I get him back."),
                    new DialogSnippet("Kid", "I'll get him back for you."),
                    new DialogSnippet("Eyeball", "You'd do that? He's shy, but will come to you if you play music.")
                };
                dialog.StartDialog(snippets);
                state = 2;
                break;
            case 2:
                snippets = new DialogSnippet[]{
                    new DialogSnippet("Eyeball", "Do you have my frog yet?"),
                    new DialogSnippet("Kid", "Not yet."),
                    new DialogSnippet("Eyeball", "Well hurry up would you?")
                };
                dialog.StartDialog(snippets);
                break;
            case 3:
                snippets = new DialogSnippet[]{
                    new DialogSnippet("Eyeball", "Do you have my frog yet?"),
                    new DialogSnippet("Kid", "Not yet."),
                    new DialogSnippet("Eyeball", "Well hurry up would you?")
                };
                dialog.StartDialog(snippets);
                break;
            case 4:
                snippets = new DialogSnippet[]{
                    new DialogSnippet("Eyeball", "Do you have my frog yet?"),
                    new DialogSnippet("Kid", "Not yet."),
                    new DialogSnippet("Eyeball", "Well hurry up would you?")
                };
                dialog.StartDialog(snippets);
                break;
            case 5:
                snippets = new DialogSnippet[]{
                    new DialogSnippet("Eyeball", "You found my frog! Thank you!"),
                    new DialogSnippet("Eyeball", "Oh no! Fire Spirit!"),
                    new DialogSnippet("Fire Spirit", "Bwahahahahahahahahahahahahahahahahahaha"),
                    new DialogSnippet("Fire Spirit", "Your power is now mine!"),
                    new DialogSnippet("Fire Spirit", "Mwahahaha!")
                };
                state = 6;

                GameObject.FindGameObjectWithTag("PathBlocker").SetActive(false);
                GameObject.Instantiate(firspirt, new Vector3(-20, 11, 27), Quaternion.Euler(0, 145, 0));
                dialog.StartDialog(snippets);
                removeEyeSore = true;

                break;
            default:
                dialog.StartDialog(snippets);
                break;
        }

        mouse.eyeball = false;
    }
}
