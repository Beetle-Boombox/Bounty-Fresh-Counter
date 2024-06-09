using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CounterUI : MonoBehaviour
{
    public TextMeshProUGUI eggCounter, chickCounter, henCounter, roosterCounter;
    public int eggCount, chickCount, henCount, roosterCount;
    
    void Start()
    {
        eggCount = 0;
        chickCount = 0;
        henCount = 0;
        roosterCount = 0;
    }

    
    void Update()
    {
        eggCounter.text = eggCount.ToString();
        chickCounter.text = chickCount.ToString();
        henCounter.text = henCount.ToString();    
        roosterCounter.text = roosterCount.ToString();
    }
}