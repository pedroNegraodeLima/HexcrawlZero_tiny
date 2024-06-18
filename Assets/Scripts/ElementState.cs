using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementState : MonoBehaviour
{
    public float timeMultiplier = 1f;
    float timeToMaintainace = 1f;
    public float maintanaceTarget = 30f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MantainanceTime();
    }

    void MantainanceTime()
    {
        // Cria uma contador que vai enxendo com o passar do tempo + variavel randomica até atingir um numero maximo
        float runningTime = timeMultiplier * Time.deltaTime;
        float randomAspect = Random.value * Time.deltaTime; // Random.value devolve valores randomicos em C#!!
        

        timeToMaintainace += runningTime + randomAspect;

        if (timeToMaintainace >= maintanaceTarget)
        {
            //chama o evento de exclamação na UI
            timeToMaintainace = 0f;
        }
        //Debug.Log("timeToMaintainace : " + timeToMaintainace);
       

    }

    
}
