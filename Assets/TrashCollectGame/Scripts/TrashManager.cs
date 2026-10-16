using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrashManager : MonoBehaviour
{
    public int type;

    public Rigidbody2D rb;
    public bool still;
    public TimeManager timeManager;

    public float vx;
    public float vy;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "Trash")
        {
            rb = GetComponent<Rigidbody2D>();
        }
        timeManager = GameObject.FindAnyObjectByType<TimeManager>();
        if (rb != null)
        {
            rb.velocity = new Vector2(vx, vy);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "Trash")
        {
            rb.velocity = Vector3.MoveTowards(rb.velocity, new Vector3(0,0), 1 * Time.deltaTime);
            if (rb.velocity.sqrMagnitude == 0 )
            {
                still = true;
            }
            else
            {
                still = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.tag == "Trash")
        {
            if (collision.gameObject.tag == "Bin" && (still == true || GameManager.Instance.SpecialId == 3 || SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Tutorial")))
            {
                if (type == collision.gameObject.GetComponent<TrashManager>().type)
                {
                    timeManager.trashleft -= 1;
                    timeManager.trashcollected++;
                    Destroy(gameObject);
                }
                else 
                {
                    if (still) rb.velocity = new Vector3(0, -1, 0);
                    else
                    {
                        if (rb.velocity.y == 0) rb.velocity = new Vector2(0, -Mathf.Abs(rb.velocity.x));
                        rb.velocity += new Vector2(0, (-Mathf.Abs(rb.velocity.y) - Mathf.Abs(rb.velocity.x) - 1) * Time.deltaTime);
                    }
                }
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (gameObject.tag == "Trash")
        {
            if (collision.gameObject.tag == "Bin" && (still == true || GameManager.Instance.SpecialId == 3 || SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Tutorial")))
            {
                if (type == collision.gameObject.GetComponent<TrashManager>().type)
                {
                    timeManager.trashleft -= 1;
                    timeManager.trashcollected++;
                    Destroy(gameObject);
                }
                else
                {
                    if (still)  rb.velocity = new Vector3(0, -1, 0);
                    else
                    {
                        if (rb.velocity.y == 0) rb.velocity = new Vector2(0, -Mathf.Abs(rb.velocity.x));
                        rb.velocity += new Vector2(0, (-Mathf.Abs(rb.velocity.y) - Mathf.Abs(rb.velocity.x) - 1) * Time.deltaTime);
                    }
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "Trash")
        {
            if (collision.gameObject.tag == "Bin" && (still == true || GameManager.Instance.SpecialId == 3 || SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Tutorial")))
            {
                if (type == collision.gameObject.GetComponent<TrashManager>().type)
                {
                    timeManager.trashleft -= 1;
                    timeManager.trashcollected++;
                    Destroy(gameObject);
                }
                else 
                {
                    if (still) rb.velocity = new Vector3(0, -1, 0);
                    else
                    {
                        if (rb.velocity.y == 0) rb.velocity = new Vector2(0, -Mathf.Abs(rb.velocity.x));
                        rb.velocity += new Vector2(0, (-Mathf.Abs(rb.velocity.y) - Mathf.Abs(rb.velocity.x) - 1) * Time.deltaTime);
                    }
                }
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (gameObject.tag == "Trash")
        {
            if (collision.gameObject.tag == "Bin" && (still == true || GameManager.Instance.SpecialId == 3 || SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Tutorial")))
            {
                if (type == collision.gameObject.GetComponent<TrashManager>().type)
                {
                    timeManager.trashleft -= 1;
                    timeManager.trashcollected++;
                    Destroy(gameObject);
                }
                else
                {
                    if (still)  rb.velocity = new Vector3(0, -1, 0);
                    else
                    {
                        if (rb.velocity.y == 0) rb.velocity = new Vector2(0, -Mathf.Abs(rb.velocity.x));
                        rb.velocity += new Vector2(0, (-Mathf.Abs(rb.velocity.y) - Mathf.Abs(rb.velocity.x) - 1) * Time.deltaTime);
                    }
                }
            }
            if (collision.gameObject.tag == "TrashBanner")
            {
                timeManager.trashleft -= 1;
                timeManager.trashcollected++;
                Destroy (gameObject);
            }
        }
    }

}