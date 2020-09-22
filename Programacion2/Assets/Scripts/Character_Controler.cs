using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class Character_Controler : MonoBehaviour
{
    public Animator MainAnimation;
  
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

    [Header("Attack")]
    public float AttackRange;
    public int Damage;
    private float TimeToAttack;
    public float FirstTimeToAttack;
    public Transform attackPoint;
    public LayerMask DefineEnemy;




    // Start is called before the first frame update
    void Start()
    {
        MainAnimation = this.GetComponent<Animator>();
        MainAnimation.SetInteger("life", Life);
  
        Body = this.GetComponent<Rigidbody>();
        NumberJump = MaxJump;
        playersound = GetComponent<sound>();

    }

    // Update is called once per frame
    void Update()
    {
               

        //atack
        if (TimeToAttack <= 0)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                MainAnimation.SetTrigger("attackmovement");

                playersound.SoundPlay(playersound.clips[2]); //audio

                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPoint.position, AttackRange, DefineEnemy);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemyControler enemy = enemiesToDamage[i].GetComponent<enemyControler>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(Damage);
                    }
                    else
                    {
                        // proximamente boss

                       // bossLife boss = enemiesToDamage[i].GetComponent<bossLife>();
                        //if (boss != null)
                        {
                           // boss.TakeDamage(Damage);
                        }
                    }

                }
            }
            TimeToAttack = FirstTimeToAttack;
        }
        else
        {
            TimeToAttack -= Time.deltaTime;
        }
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

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        { return; }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, AttackRange);
    }



    
    public void Jump()
    {
        playersound.SoundPlay(playersound.clips[1]);
        if (NumberJump > 0)
        {
            MainAnimation.Play("jump");


            Body.velocity = Vector3.zero;
            Body.AddForce(Vector3.up * JumpForce  * Body.mass, ForceMode.Impulse);
            NumberJump--;
        }
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
