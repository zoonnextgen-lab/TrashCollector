using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint : MonoBehaviour
{
    public List<Transform> Bots;

    IEnumerator spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            if (Random.Range(0,3) == 1)
            {
                int num = Random.Range(0,Bots.Count);
                Transform Bot = Instantiate(Bots[num], Bots[num].position,Quaternion.identity);
                Bot.gameObject.SetActive(true);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
