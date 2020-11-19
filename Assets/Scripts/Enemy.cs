using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    const float DAMAGE_TIME = 0.5f;
    float damagedTime = 0f;
    bool damaged = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (damagedTime >= DAMAGE_TIME)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            damagedTime = 0;
            damaged = false;
        }
        else if (damaged)
            damagedTime += Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        damaged = true;
        GetComponent<SpriteRenderer>().color = Color.red;
    }
}
