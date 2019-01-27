using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDeath : MonoBehaviour
{
    public bool start = false;
    bool shrinking = true;

    void Update()
    {
        if (start)
        {
            if (shrinking)
            {
                transform.Rotate(Vector3.up, 1000f * Time.deltaTime);
                transform.localScale *= 0.99f;
                //transform.position += Vector3.up * 0.02f;

                transform.position = Vector3.Lerp(transform.position, GameObject.FindGameObjectWithTag("Lantern").transform.position, 0.75f * Time.deltaTime);

                if (transform.localScale.x < 0.3f) { shrinking = false; }
            }
            else
            {
                var cages = GameObject.FindGameObjectsWithTag("Cage");

                for(int i = 0; i < cages.Length; i++)
                {
                    Destroy(cages[i]);
                }

                GameObject.FindGameObjectWithTag("Player").GetComponent<LevelThreePlayer>().finalWords();

                Destroy(gameObject);
            }
        }
    }
}
