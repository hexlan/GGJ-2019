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
                new DialogSnippet("Wisp", "Last time to talk before you go in there...")
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
                new DialogSnippet("Boy", "I is winner...")
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

                        DialogSnippet[] snippets = new DialogSnippet[] { new DialogSnippet("Wisp", "You found the lantern...") };
                        dialog.StartDialog(snippets);
                    }
                }
            }
        }
    }

    public void finalWords()
    {
        DialogSnippet[] snippets = new DialogSnippet[] { new DialogSnippet("Wisp", "You have won!") };
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
                    new DialogSnippet("Mushroom", "No heading this way without the lantern of light.")
                };
                break;
            case 1:
                snippets = new DialogSnippet[]{
                    new DialogSnippet("Mushroom", "You found the lantern, go ahead.")
                };

                GameObject.FindGameObjectWithTag("PathBlocker").SetActive(false);
                break;
        }

        dialog.StartDialog(snippets);

        mouse.mushroom = false;
    }
}