using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public int MinTime;
    public int MaxTime;
    public int RandTime;
    public float TimeLeft;

    public int predstam;

    public int diff;
    public int trashcollected;

    public int trashleft;

    public TMP_Text TacticianInfo;
    public TMP_Text NormalInfo;
    public GameObject TacticianInfoActive;

    public GameObject suptext;

    IEnumerator Timer()
    {
        while (TimeLeft > 0)
        {
            yield return new WaitForSeconds(1);
            if (TimeLeft % 2 == 0) { predstam -= diff; }
            if (TimeLeft - 1 > 0)
            {
                if (predstam <= 0)
                {
                    GameManager.Instance.WorkEndNoStam(diff, trashcollected);
                }
            }
            TimeLeft -= 1;
        }
        GameManager.Instance.WorkEnd(diff, trashcollected, RandTime, trashleft);
    }

    IEnumerator Stamup()
    {
        suptext.SetActive(true);
        yield return new WaitForSeconds(3);
        suptext.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.SpecialId == 4)
        {
            TacticianInfoActive.SetActive(true);
        }
        predstam = GameManager.Instance.stamina;
        RandTime = Random.Range(MinTime, MaxTime);
        TimeLeft = RandTime;
        trashleft = 0;
        trashcollected = 0;
        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (GameManager.Instance.HeldItem[0] != 0) { GetItemSpecial(GameManager.Instance.HeldItem[0]); GameManager.Instance.HeldItem[0] = 0; }
            else if (GameManager.Instance.HeldItem[1] != 0) { GetItemSpecial(GameManager.Instance.HeldItem[1]); GameManager.Instance.HeldItem[1] = 0; }
        }
        if (GameManager.Instance.SpecialId == 4) TacticianInfo.text = $"Tactician Special's info\nFullTime: {RandTime}\nTimeLeft: {TimeLeft}\nPredStam: {predstam}\nTrashCollected: {trashcollected}\nTrashLeft: {trashleft}";
        if (NormalInfo != null) NormalInfo.text = $"Objective:\r\nCollect all trashes\r\n\r\nTime Left: {TimeLeft}\nItem1: {GetItem(GameManager.Instance.HeldItem[0])}\nItem2: {GetItem(GameManager.Instance.HeldItem[1])}";
    }
    private void GetItemSpecial(int id)
    {
        switch (id)
        {
            default: Debug.Log("Idk"); break;
            case 1: GameManager.Instance.stamina = 100; predstam += 100; RandTime = (int)TimeLeft; StartCoroutine(Stamup()); ;  break;
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
}
