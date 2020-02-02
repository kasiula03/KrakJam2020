using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Milenial : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private EnemyMovement em;

    void Update()
    {
        sr.flipX = !em.facingRight;
    }

    void Reset()
    {
        sr = GetComponent<SpriteRenderer>();
        em = GetComponent<EnemyMovement>();
    }
}
