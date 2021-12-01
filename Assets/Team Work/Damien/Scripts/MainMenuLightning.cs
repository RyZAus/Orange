using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLightning : MonoBehaviour
{
    private Light lightning;
    public int counter;
    public int timerMin = 500;
    public int timerMax = 4000;
    void Start()
    {
        lightning = gameObject.GetComponent<Light>();
        ResetCounter();
    }

    // Update is called once per frame
    void Update()
    {
        if (counter <= 0)
        {
            StartCoroutine(FlashLightning());
            ResetCounter();
        }
        else
        {
            counter--;
        }
        
    }

    void ResetCounter()
    {
        counter = Random.Range(timerMin, timerMax);
    }

    IEnumerator FlashLightning()
    {
        lightning.enabled = true;
        yield return new WaitForSeconds(1);
        lightning.enabled = false;
        
    }
}
