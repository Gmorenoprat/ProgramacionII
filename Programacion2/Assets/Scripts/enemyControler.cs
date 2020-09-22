using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyControler : MonoBehaviour
{
    public Character_Controler owner;

    Rigidbody2D rb;

    public float speed;
    public int EnemyLife;
    private Renderer render;

    // Start is called before the first frame update
    void Start()
    {
        owner = this.GetComponent<Character_Controler>();

        render = GetComponent<Renderer>();


    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TakeDamage(int Damage)
    {
        EnemyLife -= Damage;
        GetComponent<Animator>().SetBool("gethit", true);
        Debug.Log("damage taken");

        if (EnemyLife <= 1)
        {
            render.material.color = Color.red;
        }

        if (EnemyLife <= 0)
        {
            GetComponent<Animator>().SetBool("isdead", true);
            Destroy(this.gameObject);
        }

    }

} 
