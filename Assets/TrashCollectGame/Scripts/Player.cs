using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Profiling;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public Animator anim;

    public GameObject phonebase;
    public GameObject Workscreen;
    public GameObject InvestScreen;
    public GameObject ShopScreen;
    public GameObject SpecialScreen;
    public GameObject Profile;

    public GameObject NearTrash;
    public GameObject HoldTrash;
    public GameObject NearBin;

    public TimeManager TimeManager;

    public bool NearBed;
    public bool PlrLock;

    public bool Powering;
    public int MaxPower;
    public int MinPower;
    public int power;
    public int powaccel;
    public float delay;
    public GameObject PowerBar;
    public List<Sprite> powerbar;

    public float specialcounter;
    public GameObject TrashBanner;
    public GameObject CurTrashBanner;

    public bool NearPost;
    public bool NearTend;
    public GameObject Letter;
    public TMP_Text LText;
    public Transform Cut1End;

    public Transform Zarrow;

    public GameObject End1;
    public GameObject End2;
    // Start is called before the first frame update
    IEnumerator ThrowPower()
    {
        power = 1;
        powaccel = 1;
        PowerBar.GetComponent<SpriteRenderer>().sprite = powerbar[0];
        delay = UnityEngine.Random.Range((float)0,(float)0.5);
        while (Powering == true)
        {
            yield return new WaitForSeconds(delay);
            power += powaccel;
            if (power >= MaxPower || power <= MinPower) powaccel *= -1;
            PowerBar.GetComponent<SpriteRenderer>().sprite = powerbar[power];


        }
    }

    IEnumerator IEStory1()
    {
        anim.SetInteger("Facing", 3);
        transform.localScale = new Vector3(Math.Abs(transform.localScale.x), transform.localScale.y);
        yield return new WaitForSeconds(1);
        anim.SetBool("IsWalking", true);
        while (transform.position.x != Cut1End.position.x)
        {
            transform.position = Vector3.MoveTowards(new Vector3(transform.position.x,transform.position.y,transform.position.z),new Vector3(Cut1End.position.x,transform.position.y,transform.position.z), (float)1* Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        anim.SetBool("IsWalking", false);
        yield return new WaitForSeconds(1);
        anim.SetInteger("Facing", 2);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Tutorial");

    }
    IEnumerator IEEnd()
    {
        anim.SetInteger("Facing", 2);
        yield return new WaitForSeconds(1);
        anim.SetBool("IsWalking", true);
        while (transform.position.y != Cut1End.position.y)
        {
            transform.position = Vector3.MoveTowards(new Vector3(transform.position.x,transform.position.y,transform.position.z),new Vector3(transform.position.x,Cut1End.position.y,transform.position.z), (float)1* Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(2);
        if (GameManager.Instance.coins >= 500)
        {
            End1.SetActive(true);
        }
        else End2.SetActive(true);

    }
    void Start()
    {
        
        MinPower = 1;
        MaxPower = 6;
        if (GameManager.Instance.stamwarn)
        {
            Zarrow.gameObject.SetActive(true);
            LText.text = GetLetter($"Day {8 - GameManager.Instance.dayleft}", "Room 9, Zest Hotel", "Julian Devlin", "Hello Mr.Julian,\n\nWe found you unconscious while you were working so we'll charge you some coins for the heal.", "Dr.Smurf");
            GameManager.Instance.stamwarn = false;
            Debug.Log("Get Good");
        }
        if (GameManager.Instance.trashleftoverwarn)
        {
            Zarrow.gameObject.SetActive(true);
            LText.text = GetLetter($"Day {8 - GameManager.Instance.dayleft}", "Room 9, Zest Hotel", "Julian Devlin", "Hello Mr.Julian,\n\nWe found some leftover trashes so we'll have to fine you some money for that.", "Manager");
            GameManager.Instance.trashleftoverwarn = false;
            Debug.Log("Get Good");
        }

        TimeManager = GameObject.FindAnyObjectByType<TimeManager>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        PlrLock = false;
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("End"))
        {
            anim.SetBool("IsWalking", false);
            rb.velocity = new Vector3(0, 0);
            PlrLock = true;
            StartCoroutine(IEEnd());
        }
    }

    // Update is called once per frame
    void Update()
    {
        float _hor = Input.GetAxisRaw("Horizontal");
        float _ver = Input.GetAxisRaw("Vertical");


        if (PlrLock == false)
        {

            rb.velocity = new Vector3(_hor * speed, _ver * speed);

            if (_ver == 1 && (_hor == 0 || anim.GetBool("IsWalking") == false))
            {
                anim.SetInteger("Facing", 2);
            }
            else if (_ver == -1 && (_hor == 0 || anim.GetBool("IsWalking") == false))
            {
                anim.SetInteger("Facing", 1);
            }
            else if (_hor == -1 && (_ver == 0 || anim.GetBool("IsWalking") == false))
            {
                anim.SetInteger("Facing", 3);
                if (transform.localScale.x > 0) transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            else if (_hor == 1 && (_ver == 0 || anim.GetBool("IsWalking") == false))
            {
                anim.SetInteger("Facing", 3);
                if (transform.localScale.x < 0) transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            if (rb.velocity.sqrMagnitude > 0) anim.SetBool("IsWalking", true);
            else anim.SetBool("IsWalking", false);
        }
        



        if (Input.GetKeyDown(KeyCode.M) && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("PlrRoom") && GameManager.Instance.punlock == true) 
        {
            if (phonebase.activeSelf == false)
            {
                anim.SetBool("IsWalking", false);
                phonebase.SetActive(true);
                rb.velocity = new Vector3(0, 0);
                anim.SetInteger("Facing", 1);
                anim.SetTrigger("Phone");
                anim.SetBool("UsingPhone",true);
                PlrLock = true;
            }
            else
            {
                phonebase.SetActive(false);
                Workscreen.SetActive(false);
                ShopScreen.SetActive(false);
                InvestScreen.SetActive(false);
                Profile.SetActive(false);
                SpecialScreen.SetActive(false);
                anim.ResetTrigger("Phone");
                anim.SetBool("UsingPhone",false);
                PlrLock = false;
            }
        }

        if (NearTrash != null && HoldTrash == null)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                HoldTrash = NearTrash;
                HoldTrash.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                HoldTrash.transform.SetParent(gameObject.transform, true);
            }
        }
        else if (HoldTrash != null)
        {
            if (Input.GetKeyDown(KeyCode.Z) && NearBin != null)
            {
                if (HoldTrash.GetComponent<TrashManager>().type == NearBin.GetComponent<TrashManager>().type)
                {
                    TimeManager.trashcollected++;
                    Destroy(HoldTrash);
                    TimeManager.trashleft -= 1;
                }
                else
                {
                    Debug.Log("wrong");
                    Destroy(HoldTrash);
                    TimeManager.trashleft -= 1;
                }

            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                PowerBar.SetActive(true);
                Powering = true;
                StartCoroutine(ThrowPower());
            }
            if (Input.GetKeyUp(KeyCode.F))
            {
                HoldTrash.transform.SetParent(null);
                PowerBar.SetActive(false);
                Powering = false;
                switch (anim.GetInteger("Facing"))
                {
                    case 1: HoldTrash.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -power); break;
                    case 2: HoldTrash.GetComponent<Rigidbody2D>().velocity = new Vector2(0, power); break;
                    case 3: HoldTrash.GetComponent<Rigidbody2D>().velocity = new Vector2(power * Mathf.Sign(transform.localScale.x), 0); break;
                }
                HoldTrash = null;
            }
            
        }
        if (HoldTrash == null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (GameManager.Instance.SpecialId == 1)
                {

                    if (CurTrashBanner == null) 
                    { 
                        CurTrashBanner = Instantiate(TrashBanner, TrashBanner.transform.position, Quaternion.identity); 
                        CurTrashBanner.SetActive(true);
                    }
                   

                }
                if (GameManager.Instance.SpecialId == 2)
                {
                    if (specialcounter <= 0)
                    { 
                    speed += 1;
                    specialcounter = 10;
                    }
                }
            }
        }
        if (NearBed == true)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                SceneManager.LoadScene("DaySwitch");
            }
        }
        if (specialcounter > 0)
        {
            specialcounter -= Time.deltaTime;
        }

        if (NearPost)
        {
            if (Input.GetKeyDown (KeyCode.Z))
            {
                anim.SetBool("IsWalking", false);
                rb.velocity = new Vector3(0, 0);
                PlrLock = true;
                Letter.SetActive(true);
                if (GameManager.Instance.punlock == false) LText.text = GetLetter($"Day {8 - GameManager.Instance.dayleft}","Room 9, Zest Hotel","Julian Devlin", "Yo Julian,\n\nWe've considered your request to extend the deadline. We'll give you 7 more days to find what you owe. Goodluck with that.", "Creditor");
                Zarrow.gameObject.SetActive(false);
            }
        }
        if (NearTend)
        {
            if (Input.GetKeyDown (KeyCode.Z))
            {
                GameManager.Instance.SpecialId = UnityEngine.Random.Range(1, 4);
                GameManager.Instance.punlock = true;
                SceneManager.LoadScene("PlrRoom");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Trash")
        {
            NearTrash = collision.gameObject;
        }
        if (collision.gameObject.tag == "Bin")
        {
            NearBin = collision.gameObject;
        }
        if (collision.gameObject.tag == "Mailbox")
        {
            NearPost = true;
        }
        if (collision.gameObject.tag == "TutorEnd")
        {
            NearTend = true;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Trash")
        {
            NearTrash = null;
        }
        if (collision.gameObject.tag == "Bin")
        {
            NearBin = null;
        }
        if (collision.gameObject.tag == "Mailbox")
        {
            NearPost = false;
        }
        if (collision.gameObject.tag == "TutorEnd")
        {
            NearTend = true;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bin")
        {
            NearBin = collision.gameObject;
        }
        if (collision.gameObject.tag == "Trash")
        {
            NearTrash = collision.gameObject;
        }
        if (collision.gameObject.name == "Bed")
        {
            NearBed = true;
        }
        if (collision.gameObject.tag == "Mailbox")
        {
            NearPost = true;
        }
        if (collision.gameObject.tag == "TutorEnd")
        {
            NearTend = true;
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bin")
        {
            NearBin = null;
        }
        if (collision.gameObject.tag == "Trash")
        {
            NearTrash = null;
        }
        if (collision.gameObject.name == "Bed")
        {
            NearBed = false;
        }
        if (collision.gameObject.tag == "Mailbox")
        {
            NearPost = false;
        }
        if (collision.gameObject.tag == "TutorEnd")
        {
            NearTend = true;
        }


    }
    public void Story1()
    {
        rb.velocity = new Vector3(0, 0);
        PlrLock = true;
        StartCoroutine(IEStory1());
    }

    public static string GetLetter(
        string date,
        string recipientAddress,
        string recipientName,
        string bodyText,
        string senderName)
    {
        return $"{date}\n\n" +
               $"{recipientAddress}\n\n" +
               $"Dear {recipientName},\n\n" +
               $"{bodyText}\n\n" +
               $"Sincerely,\n" +
               $"{senderName}";
    }

}
