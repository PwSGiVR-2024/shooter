using System.Collections;
using UnityEngine;

public class GhoulDropSpawner : MonoBehaviour
{
    public void Spawn(float delay)
    {
        StartCoroutine(DelayDrop(delay));
    }
    IEnumerator DelayDrop(float time)
    {
        yield return new WaitForSeconds(time);
        int ghoulRemainsCount = Random.Range(0, 3);
        for (int i = 0; i < ghoulRemainsCount; i++)
        {
            Vector3 offset = new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
            Instantiate(GameObject.Find("ItemPrefabs").GetComponent<ItemPrefabs>().GhoulRemains, transform.position + offset, transform.rotation);
        }

        int rottenApplesCount = Random.Range(0, 4);
        for (int i = 0; i < rottenApplesCount; i++)
        {
            Vector3 offset = new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
            Instantiate(GameObject.Find("ItemPrefabs").GetComponent<ItemPrefabs>().RottenApple, transform.position + offset, transform.rotation);
        }
    }
}
