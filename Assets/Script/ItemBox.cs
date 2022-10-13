using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public GameObject Itembox;
    void Start()
    {
        
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("You can use Board with Key.Z");
            Destroy(Itembox);
        }
    }
    void Update()
    {
        
    }
}
