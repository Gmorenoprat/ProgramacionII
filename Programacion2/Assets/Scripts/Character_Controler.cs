using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class Character_Controler : MonoBehaviour
{
    public Animator MainAnimation;

    public Transform posicionDisparo;
    public GameObject flechaObj;
  
    public float Speed;
    public int Life;
    public Rigidbody Body;
   
    public sound playersound;

    [Header("Jump")]
    public float JumpForce;
    public int NumberJump;
    public int MaxJump;

    [Header("Life Components")]
    public int NumberOfHeart;
    public Image[] hearts;
    public Sprite fulHeart;
    public Sprite emptyHeart;



    // Start is called before the first frame update
    void Start()
    {
        MainAnimation = this.GetComponent<Animator>();
       
  
        Body = this.GetComponent<Rigidbody>();
        NumberJump = MaxJump;
        playersound = GetComponent<sound>();

    }

    // Update is called once per frame
    void Update()
    {
               

        //vida
        if (Life > NumberOfHeart)
        {
            Life = NumberOfHeart;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < Life)
            {
                hearts[i].sprite = fulHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i < NumberOfHeart)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
        if (Life <= 0)
        {
            Die();
        }
        //cheats

       // if (Input.GetKeyDown(KeyCode.Mouse1))
        {
           // Damage += 2;
        }
       // if (Input.GetKeyDown(KeyCode.LeftControl))
        {
           // Life += 2;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            MainAnimation.SetBool("estaApuntando", true);
            MainAnimation.Play("Apuntar");
          
        }
        if (Input.GetKeyUp(KeyCode.Mouse0) && MainAnimation.GetBool("estaApuntando"))
        {
            MainAnimation.Play("Disparar");
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            MainAnimation.SetBool("estaApuntando", false);
        }
    }
    public void TakeDamage(int damage)
    {
        Life -= damage;

        StartCoroutine(DamageAnimation());

        playersound.SoundPlay(playersound.clips[0]); //audio

        if (Life <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        MainAnimation.Play("die");
        GameObject.Destroy(this.gameObject);
        SceneManager.LoadScene("lose");
    }

  
    IEnumerator DamageAnimation()
    {
        SpriteRenderer[] srs = GetComponentsInChildren<SpriteRenderer>();

        for (int i = 0; i < 3; i++)
        {
            foreach (SpriteRenderer sr in srs)
            {
                Color c = sr.color;
                c.a = 0;
                sr.color = c;
            }

            yield return new WaitForSeconds(.1f);

            foreach (SpriteRenderer sr in srs)
            {
                Color c = sr.color;
                c.a = 1;
                sr.color = c;
            }

            yield return new WaitForSeconds(.1f);
        }
    }

   

    public void Jump()
    {
        playersound.SoundPlay(playersound.clips[1]);
        if (NumberJump > 0)
        {
            MainAnimation.Play("Jump");

            Body.velocity = Vector3.zero;
            Body.AddForce(Vector3.up * JumpForce  * Body.mass, ForceMode.Impulse);
            NumberJump--;
        }
    } 
    public void Disparar()
    {
        //playersound.SoundPlay(playersound.clips[1]);
        GameObject flecha = Instantiate(flechaObj, posicionDisparo);
        flecha.transform.forward = posicionDisparo.forward;
        Destroy(flecha, 5f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            NumberJump = MaxJump;
            MainAnimation.Play("movement");

        }


    }

}
