using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttonmanager : MonoBehaviour
{
    public GameObject Workscreen;
    public GameObject ShopScreen;
    public GameObject SpecialScreen;
    public GameObject InvestScreen;
    public GameObject Profile;
    public TMP_Text enpricevisual;
    public TMP_Text NormalInfo;
    public TMP_Text EnAmount;

    public GameObject SR1;
    public GameObject SR2;
    public GameObject SR3;

    public GameObject Letter;
    public Player plr;

    public List<Sprite> Specialsprites;
    // Start is called before the first frame update
    void Start()
    {
        plr = GameObject.FindAnyObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        enpricevisual.text = "EnergyDrink for " + GameManager.Instance.energydrinkprice + ":";
        NormalInfo.text = "Coins: " + GameManager.Instance.coins + "\nStamina: " + GameManager.Instance.stamina + "\nDebt: 500" + "\nDays left: " + GameManager.Instance.dayleft + "\nItem1: " + GetItem(GameManager.Instance.HeldItem[0])+"\nItem2: "+GetItem(GameManager.Instance.HeldItem[1])+"\nSpecial: "+GetSpecial(GameManager.Instance.SpecialId);
        EnAmount.text = "EnergyDrink("+GameManager.Instance.energydrinkamount + "):";
    }

    public void Home()
    {
        Workscreen.SetActive(false);
        ShopScreen.SetActive(false);
        SpecialScreen.SetActive(false);
        Profile.SetActive(false);
        InvestScreen.SetActive(false);
    }
    public void Work()
    {
        Workscreen.SetActive(true);
        //Debug.Log("check");
    }
    public void Work1()
    {
        SceneManager.LoadScene("Work1");
    }
    public void Shop()
    {
        ShopScreen.SetActive(true);
    }
    public void Profilecheck()
    {
        Profile.SetActive(true);
    }
    public void SpeicalOpen()
    {
        SpecialScreen.SetActive(true);
    }
    public void Invest()
    {
        GameManager.Instance.coins -= 5;
    }
    public void SInvest()
    {
        InvestScreen.SetActive(true);
    }

    public void BuyEnergyDrinks()
    {
        if (GameManager.Instance.coins >= GameManager.Instance.energydrinkprice)
        {
            GameManager.Instance.coins -= GameManager.Instance.energydrinkprice;
            GameManager.Instance.energydrinkamount += 1;
            GameManager.Instance.energydrinkprice += 1;
        }
    }
    public void EquipEndrink()
    {
        if ((GameManager.Instance.HeldItem[0] == 0 || GameManager.Instance.HeldItem[1] == 0) && GameManager.Instance.energydrinkamount >= 1)
        {
            if (GameManager.Instance.HeldItem[0] == 0)
            {
                GameManager.Instance.HeldItem[0] = 1;
            }
            else if (GameManager.Instance.HeldItem[1] == 0)
            {
                GameManager.Instance.HeldItem[1] = 1;
            }
            GameManager.Instance.energydrinkamount -= 1;

        }
    }
    public void Roll()
    {
        if (GameManager.Instance.coins >= 5)
        { 
            GameManager.Instance.coins -= 5;
            int R1 = Random.Range(1, Specialsprites.Count);
            int R2 = Random.Range(1, Specialsprites.Count);
            int R3 = Random.Range(1, Specialsprites.Count);

            SR1.GetComponent<Image>().sprite = Specialsprites[R1];
            SR2.GetComponent<Image>().sprite = Specialsprites[R2];
            SR3.GetComponent<Image>().sprite = Specialsprites[R3];

            if (R1 == R2 && R2 == R3)
            {
                GameManager.Instance.SpecialId = R1;
            }
            else GameManager.Instance.SpecialId = 0;
        }

    } 

    private string GetItem(int index)
    {
        switch (index)
        {
            case 0: return "None";
            case 1: return "EnergyDrink";
            default: return "IDK";
        }

    } 
    private string GetSpecial(int index)
    {
        switch (index)
        {
            case 0: return "None";
            case 1: return "TrashBanner";
            case 2: return "Speed up";
            case 3: return "PerfectShot";
            case 4: return "Tactician";
            default: return "IDK";
        }

    } 

    public void ReadDoneStory1()
    {

        plr.PlrLock = false;
        Letter.SetActive(false);

        if (GameManager.Instance.punlock == false)
        {
            plr.Story1();
        }
    } 


}
