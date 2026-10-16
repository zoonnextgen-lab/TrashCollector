using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Bot : MonoBehaviour
{
    public float speed;
    public int drop;
    public GameObject Paper;
    public Rigidbody2D rb;
    public Animator anim;

    public TimeManager timeManager;
    IEnumerator DropCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            if (Random.Range(0, 3) == 1 && timeManager.TimeLeft >= timeManager.RandTime/2)
            {
                drop = 1;
                anim.SetTrigger("DropPaper");
                timeManager.trashleft++;

            }
        }
    }

    void Start()
    {
        timeManager = GameObject.FindObjectOfType<TimeManager>();
        StartCoroutine(DropCheck());
        drop = 0;
        rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (drop == 0) { rb.velocity = new Vector2(speed,rb.velocity.y); }
        else { rb.velocity = new Vector2 (0,rb.velocity.y); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "End")
        {
            Destroy(gameObject);
        }
    }

    public void spawnPaper()
    {
        GameObject NewPaper = Instantiate(Paper);
        NewPaper.transform.position = Paper.transform.position;
        NewPaper.SetActive(true);
        drop = 0;
    }
}
