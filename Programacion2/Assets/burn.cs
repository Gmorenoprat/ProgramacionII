using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class burn : MonoBehaviour
{
    public int damage;


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("me quemo");
        if (other.gameObject.layer == 9)
        {

            other.gameObject.GetComponent<Character_Controler>().Life -= damage;
        }
    }
}
