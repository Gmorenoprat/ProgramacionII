using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

public class enemyControler : MonoBehaviour
{
    public Animator EnemyAnim;
    public Rigidbody EnemyRigid;
    public GameObject Player;
    public SphereCollider enemyCol;

    public GameObject Hand;

    public float EnemyWalkSpeed;
    public float EnemyRunSpeed;
    public float EnemyMeleeSpeed;
    public float EnemyDistance;
    public float EnemyAttackDistance;
    public float EnemyMeleeDistance;
    public float EnemyMeleeRate;

    public bool PlayerSighted;
    public bool RunToPlayer;
    public bool EnemyShoot;
    public bool MeleeAttack;
    public bool EnemyDying;
    public float EnemyDeadDelay;
    public float MeleeTimer;
    public int EnemyLife;
    public int AttackDamage;
    public Vector3 attackOffset;
    public float AttackDistance;



    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        enemyCol = GetComponent<SphereCollider>();
        EnemyAnim = GetComponent<Animator>();
        EnemyRigid = GetComponent<Rigidbody>();
        MeleeTimer = Time.time + EnemyMeleeRate;
        PlayerSighted = false;
        RunToPlayer = false;
        EnemyShoot = false;
        MeleeAttack = false;
        EnemyDying = false;

    }
    // Update is called once per frame
    void Update()
    {
        EnemyDistance = Vector3.Distance(transform.position, Player.transform.position);

        EnemyAnim.SetBool("EnemyRunOn", RunToPlayer);
        EnemyAnim.SetBool("EnemyWalkOn", EnemyShoot);
        EnemyAnim.SetBool("EnemyMeleeOn", MeleeAttack);
        EnemyAnim.SetBool("EnemyDieOn", EnemyDying);

        if (PlayerSighted == true)
        {
            PlayerFound();
        }
        if (EnemyDying == true)
        {
            EnemyDead();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioSource EnemyStartUp = GetComponent<AudioSource>();
            EnemyStartUp.Play();
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerSighted = true;
        }
    }
    void OnTrigerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerSighted = false;
            RunToPlayer = false;
            MeleeAttack = false;
            EnemyShoot = false;
        }
    }
    void PlayerFound()
    {
        Vector3 lookAtPos = Player.transform.position;
        lookAtPos.y = transform.position.y;
        transform.LookAt(lookAtPos);

        if (EnemyDistance >= EnemyAttackDistance)
        {
            transform.position += transform.forward * EnemyRunSpeed * Time.deltaTime;
            RunToPlayer = true;
            MeleeAttack = false;
            EnemyShoot = false;
        }
        if (EnemyDistance < EnemyAttackDistance && EnemyDistance > EnemyMeleeDistance)
        {
            transform.position += transform.forward * EnemyWalkSpeed * Time.deltaTime;
            RunToPlayer = false;
            MeleeAttack = false;
            EnemyShoot = true;
        }
        if (EnemyDistance <= EnemyMeleeDistance)
        {
            MeleeAttack = true;
            transform.position += transform.forward * EnemyMeleeSpeed * Time.deltaTime;
            RunToPlayer = false;           
            EnemyShoot = false;
           MeleeTimer = Time.time + EnemyMeleeRate;
            
        }
        else
        {
            MeleeAttack = false;
        }
    }
    void EnemyDead()
    {
        EnemyAnim.SetBool("EnemyDieOn", EnemyDying);
        Destroy(this.gameObject, EnemyDeadDelay);

    }
    private void OnTriggerEnter(BoxCollider collision)
    {
        
    }
    
}
