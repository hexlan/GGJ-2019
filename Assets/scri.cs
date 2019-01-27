using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scri : MonoBehaviour
{
    void Update()
    {
        if(Input.anyKeyDown) {
            var levelChanger = GameObject.FindGameObjectWithTag("LevelChanger");
            levelChanger.GetComponent<LevelChanger>().FadeToLevel(1);
        }
    }
}
