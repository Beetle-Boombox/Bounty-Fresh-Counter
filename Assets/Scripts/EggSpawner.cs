using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EggSpawner : MonoBehaviour
{
    public GameObject Egg, Chick, Hen, CounterUI, HenMechanics;
    public float minX, maxX, minY, maxY;
    public GameObject hatchTimerPrefab;

    public void SpawnEgg()
    {
        CounterUI = FindAnyObjectByType<CounterUI>().gameObject;
        StartCoroutine(PrimordialEgg());
    }

    //First egg, only hens hatch
    IEnumerator PrimordialEgg()
    {
        Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), transform.position.z);

        GameObject newEgg = Instantiate(Egg, randomPosition, Quaternion.identity);
        CounterUI.GetComponent<CounterUI>().eggCount++;
        Debug.Log("An egg has spawned!");

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

        GameObject newChick = Instantiate(Chick, randomPosition, Quaternion.identity);
        CounterUI.GetComponent<CounterUI>().chickCount++;
        
        yield return new WaitForSeconds(30);
        Destroy(newChick);
        CounterUI.GetComponent<CounterUI>().chickCount--;
        GameObject newHen = Instantiate(Hen, randomPosition, Quaternion.identity);
        CounterUI.GetComponent<CounterUI>().henCount++;
        
        yield return new WaitForSeconds(30);
        HenMechanics.GetComponent<HenMechanics>().HereComesTheHen();

        //Original hens die
        yield return new WaitForSeconds(10);
    
        Destroy(newHen);
        CounterUI.GetComponent<CounterUI>().henCount--;
    }
}
