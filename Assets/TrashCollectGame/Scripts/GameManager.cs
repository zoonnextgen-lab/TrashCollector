using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int stamina;
    public int maxstamina;
    public int coins;
    public bool stamwarn;
    public bool trashleftoverwarn;
    public int warncount;
    public int dayleft;
    public int energydrinkprice;
    public int energydrinkamount;

    public List<int> HeldItem;

    public int SpecialId;

    public bool punlock;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        HeldItem[0] = 0;
        HeldItem[1] = 0;
        energydrinkprice = 5;
        energydrinkamount = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void WorkEnd(int diff,int trashcollected, int Time, int trashleft)
    {
        if (trashleft > 0)
        {
            trashleftoverwarn = true;
        }
        coins += trashcollected * diff;
        coins -= trashleft * diff * 10;
        stamina -= Time / 2 * diff;
        SceneManager.LoadScene("PlrRoom");
    }
    public void WorkEndNoStam(int diff,int trashcollected)
    {
        warncount++;
        coins += trashcollected * diff;
        stamina = 100;
        coins -= 100 * warncount;
        stamwarn = true;
        SceneManager.LoadScene("PlrRoom");
        dayleft -= 1;
    }
}
