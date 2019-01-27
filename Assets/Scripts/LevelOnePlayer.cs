using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOnePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "WispTrigger")
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
    }
}
