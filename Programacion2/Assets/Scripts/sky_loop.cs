using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sky_loop : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
        GetComponent<MeshRenderer>().material.mainTextureOffset += Vector2.right * speed * Time.deltaTime;
    }
}
