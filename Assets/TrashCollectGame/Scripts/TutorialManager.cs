using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public TimeManager tm;
    public GameObject Door1;
    public Transform Plr;
    public Transform Room2;
    public GameObject Door2;
    public GameObject Door3;
    // Start is called before the first frame update
    void Start()
    {
        tm = GameObject.FindObjectOfType<TimeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tm.trashcollected >= 3)
        {
            Door1.SetActive(false);
        }
        if (Plr.transform.position.x > Room2.position.x)
        {
            Door2.SetActive(true);
        }
        if (tm.trashcollected >= 3)
        {
            Door3.SetActive(false);
        }
    }
}
