using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalSummary : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text summary;

    void Start()
    {
        summary.text = "TIME: " + GameManager.time.ToString("0.00") + "\n\n" +
                       "SCORE: " + GameManager.count;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
