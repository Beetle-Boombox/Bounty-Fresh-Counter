using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HenMechanics : MonoBehaviour
{

    public GameObject Egg, Chick, Hen, Rooster, CounterUI;
    public float minX, maxX, minY, maxY;
    public GameObject hatchTimerPrefab;

    public void HereComesTheHen(){
        StartCoroutine(Eggen());
    }
    IEnumerator Eggen()
    {
        int eggLitter = Random.Range(2, 11);
        Debug.Log(eggLitter);
        
        
        //Hen laying eggs
        for (int i = 0; i < eggLitter; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), transform.position.z);

            Debug.Log(i);
            GameObject newEgg = Instantiate(Egg, randomPosition + Vector3.up * -1, Quaternion.identity);
            CounterUI.GetComponent<CounterUI>().eggCount++;

            GameObject hatchTimer = Instantiate(hatchTimerPrefab, randomPosition + Vector3.up * 0.5f, Quaternion.identity);
            float timer = 10f;

            hatchTimer.transform.SetParent(newEgg.transform);

            
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                hatchTimer.GetComponent<TextMeshPro>().text = Mathf.Ceil(timer).ToString();
                yield return null;
            }

            Destroy(hatchTimer);
            Destroy(newEgg);
            CounterUI.GetComponent<CounterUI>().eggCount--;
            
            //Hen or Rooster randomizer
            float rand = Random.Range(0, 2) == 0 ? 1 : 2;
                if (rand == 1)
                {   
                    GameObject newChick = Instantiate(Chick, randomPosition, Quaternion.identity);
                    CounterUI.GetComponent<CounterUI>().chickCount++;
                    
                    yield return new WaitForSeconds(30);
                    Destroy(newChick);
                    CounterUI.GetComponent<CounterUI>().chickCount--;

                    Instantiate(Hen, randomPosition, Quaternion.identity);
                    CounterUI.GetComponent<CounterUI>().henCount++;
                    yield return new WaitForSeconds(30);
                    yield return StartCoroutine(Eggen());
                }
                else
                {
                    GameObject newChick = Instantiate(Chick, randomPosition, Quaternion.identity);
                    CounterUI.GetComponent<CounterUI>().chickCount++;
                    
                    yield return new WaitForSeconds(30);
                    Destroy(newChick);
                    CounterUI.GetComponent<CounterUI>().chickCount--;

                    GameObject newRooster = Instantiate(Rooster, randomPosition, Quaternion.identity);
                    CounterUI.GetComponent<CounterUI>().roosterCount++;

                    yield return new WaitForSeconds(40);
                    Destroy(newRooster);
                    CounterUI.GetComponent<CounterUI>().roosterCount--;
                }
        }
    }
}
