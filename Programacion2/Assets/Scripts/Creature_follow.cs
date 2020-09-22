using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Creature_follow : MonoBehaviour
{
    public Transform ObjetToFollow;
    public float speed;
    Vector3 Direction;
    const float EPSILON= 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // Direction = (ObjetToFollow.position - transform.position).normalized;
        
       // if((transform.position - ObjetToFollow.position).magnitude >EPSILON)
        {
            //transform.Translate(Direction * Time.deltaTime * speed);
        }
    }
}
