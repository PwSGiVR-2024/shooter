using System.Collections;
using UnityEngine;

public class GhoulDropSpawner : MonoBehaviour
{
    const float _offset = 0.5f;
    public void Spawn(float delay)
    {
        StartCoroutine(DelayDrop(delay));
    }
    IEnumerator DelayDrop(float time)
    {
        yield return new WaitForSeconds(time);
        int ghoulRemainsCount = Random.Range(0, 3);
        Debug.Log(ghoulRemainsCount);
        for (int i = 0; i < ghoulRemainsCount; i++)
        {
            Vector3 offset = new Vector3(Random.Range(-1 * _offset, _offset), _offset, Random.Range(-1 * _offset, _offset));
            Instantiate(GameObject.Find("ItemPrefabs").GetComponent<ItemPrefabs>().GhoulRemains, transform.position + offset, transform.rotation);
        }

        int rottenApplesCount = Random.Range(0, 4);
        for (int i = 0; i < rottenApplesCount; i++)
        {
            Vector3 offset = new Vector3(Random.Range(-1 * _offset, _offset), _offset, Random.Range(-1 * _offset, _offset));
            Instantiate(GameObject.Find("ItemPrefabs").GetComponent<ItemPrefabs>().RottenApple, transform.position + offset, transform.rotation);
        }
    }
}
