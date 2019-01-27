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
    bool start = true;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "End")
        {
            var levelChanger = GameObject.FindGameObjectWithTag("LevelChanger");
            levelChanger.GetComponent<LevelChanger>().FadeToLevel(3);
        }
        else if (other.gameObject.tag == "Barrier_Down")
        {
            Destroy(other.gameObject);

            DialogSnippet[] snippets ={
                    new DialogSnippet("Wisp", "How terrible!"),
                    new DialogSnippet("Wisp", "Although this may be fortunate for you."),
                    new DialogSnippet("Kid", "Why's that?"),
                    new DialogSnippet("Wisp", "There is a lantern that can grant what the holder desires."),
                    new DialogSnippet("Wisp", "However, it only works if it has enough power within."),
                    new DialogSnippet("Kid", "Let's go find it then!")
            };
            dialog.StartDialog(snippets);

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            DialogSnippet[] snippets = {
            new DialogSnippet("Kid", "Finally caught up to you!"),
            new DialogSnippet("Kid", "..."),
            new DialogSnippet("Kid", "Wait where's the path?"),
            new DialogSnippet("Kid", "Oh no! I'm lost!"),
            new DialogSnippet("???", "No need to worry."),
            new DialogSnippet("Kid", "You can speak?!"),
            new DialogSnippet("???", "Of course!"),
            new DialogSnippet("???", "Most spirits can."),
            new DialogSnippet("Kid", "Spirit?"),
            new DialogSnippet("Spirit", "Yep!"),
            new DialogSnippet("Spirit", "You've stumbled into the spirit world."),
            new DialogSnippet("Kid", "I'm sorry spirit."),
            new DialogSnippet("Kid", "But I can't be visiting right now."),
            new DialogSnippet("Kid", "I wasn't supposed to leave my home!"),
            new DialogSnippet("Spirit", "Well maybe I can help."),
            new DialogSnippet("Spirit", "You can't leave the same way you came."),
            new DialogSnippet("Spirit", "However, I know a spirit that should be able to help."),
            new DialogSnippet("Spirit", "Lets go find the Hag!"),
            new DialogSnippet("Spirit", "Oh!"),
            new DialogSnippet("Wisp", "By the way I'm Wisp!")
        };

            dialog.StartDialog(snippets);
            start = false;
        }

        if (removeEyeSore && !mouse.disabled)
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
                            GameObject.FindGameObjectWithTag("Flute").GetComponent<AudioSource>().Play();
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
                    new DialogSnippet("Wisp", "You should speak to the Hag, she might know what to do.")
                };
                break;
            case 1:
                snippets = new DialogSnippet[]{
                    new DialogSnippet("Wisp", "The Hag told us to head north.")
                };
                break;
            case 2:
                snippets = new DialogSnippet[]{
                    new DialogSnippet("Wisp", "Doesn't seem like the Eyeball is going to move without his frog."),
                    new DialogSnippet("Wisp", "We should see if we can catch it.")
                };
                break;
            case 3:
                snippets = new DialogSnippet[]{
                    new DialogSnippet("Wisp", "The Eyeball said the frog loves music. We should speak to Hag, she may be able to help.")
                };
                break;
            case 4:
                snippets = new DialogSnippet[]{
                    new DialogSnippet("Wisp", "Use the flute the Hag gave you catch the Eyeball's frog.")
                };
                break;
            case 5:
                snippets = new DialogSnippet[]{
                    new DialogSnippet("Wisp", "We caught the frog! We should return him to the Eyeball.")
                };
                break;
            case 6:
                snippets = new DialogSnippet[]{
                    new DialogSnippet("Wisp", "The path is clear for us to continue.")
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
                    new DialogSnippet("Hag", "The path home lies to the north, but you may find obstacles on your journey.")
                };
                break;
            case 1:
                snippets = new DialogSnippet[]{
                    new DialogSnippet("Hag", "Head north if home is what you seek.")
                };
                break;
            case 2:
                snippets = new DialogSnippet[]{
                    new DialogSnippet("Hag", "The Eyeball lost his frog again and is blocking the way?."),
                    new DialogSnippet("Hag", "Well you may have to catch it yourself if you want to return home.")
                };
                break;
            case 3:
                snippets = new DialogSnippet[]{
                    new DialogSnippet("Hag", "So the frog enjoys music? I have just the thing!"),
                    new DialogSnippet("Hag", "Take this flute and play it for the frog.")
                };
                state = 4;
                break;
            case 4:
                snippets = new DialogSnippet[]{
                    new DialogSnippet("Hag", "Play the flute for the frog to lure him back.")
                };
                break;
            case 5:
                snippets = new DialogSnippet[]{
                    new DialogSnippet("Hag", "You caught the frog! Now be sure to return it to its rightful owner.")
                };
                break;
            case 6:
                snippets = new DialogSnippet[]{
                    new DialogSnippet("Hag", "The path is clear, head north to return home.")
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
                    new DialogSnippet("Eyeball", "Go away. I'm not in the mood to deal with strangers.")
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
                    new DialogSnippet("Eyeball", "Have you retrieved Froggy yet?"),
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
                    new DialogSnippet("Eyeball", "Where's Froggy?"),
                    new DialogSnippet("Kid", "We haven't got him yet."),
                    new DialogSnippet("Eyeball", "Well hurry up would you?")
                };
                dialog.StartDialog(snippets);
                break;
            case 5:
                snippets = new DialogSnippet[]{
                    new DialogSnippet("Eyeball", "You found my frog! Thank you!"),
                    new DialogSnippet("Fire Spirit", "Bwahahahahahahahahahahahahahahahahahaha"),
                    new DialogSnippet("Eyeball", "Oh no! It's the Fire Spirit!"),
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
