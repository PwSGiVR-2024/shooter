using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationgirl : MonoBehaviour
{
    public int speed;
    public GameObject girl;
    void Start()
    {

    }


    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("rot"))
        {
            print("22");
            girl.transform.Rotate(0.0f, 180.0f, 0.0f, Space.World);
        }

    }
}