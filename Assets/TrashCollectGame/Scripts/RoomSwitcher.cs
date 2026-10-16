using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomSwitcher : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DaySwitch()
    {
        GameManager.Instance.stamina = GameManager.Instance.maxstamina;
        GameManager.Instance.dayleft -= 1;
        if (GameManager.Instance.dayleft > 0) SceneManager.LoadScene("PlrRoom");
        else SceneManager.LoadScene("End");
    }
}
