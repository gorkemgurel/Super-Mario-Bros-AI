using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPositions : MonoBehaviour
{
    [SerializeField] private Rigidbody2D mario;
    [SerializeField] private Rigidbody2D koopa;
    [SerializeField] private Rigidbody2D goomba1;
    [SerializeField] private Rigidbody2D goomba2;
    [SerializeField] private Rigidbody2D goomba3;
    [SerializeField] private Rigidbody2D goomba4;
    [SerializeField] private Rigidbody2D goomba5;
    [SerializeField] private Rigidbody2D goomba6;
    [SerializeField] private Rigidbody2D goomba7;
    [SerializeField] private Rigidbody2D goomba8;
    [SerializeField] private Rigidbody2D goomba9;
    [SerializeField] private Rigidbody2D goomba10;
    [SerializeField] private Rigidbody2D goomba11;
    [SerializeField] private Rigidbody2D goomba12;
    [SerializeField] private Rigidbody2D goomba13;
    [SerializeField] private Rigidbody2D goomba14;
    [SerializeField] private Rigidbody2D goomba15;
    [SerializeField] private Rigidbody2D goomba16;
    // Start is called before the first frame update
    public void Reset()
    {
        mario.position = new Vector3(2.5f, 2f, 0f);
        koopa.position = new Vector3(107f, 2f, 0f);
        goomba1.position = new Vector3(22f, 2f, 0f);
        goomba2.position = new Vector3(40f, 2f, 0f);
        goomba3.position = new Vector3(51f, 2f, 0f);
        goomba4.position = new Vector3(52.5f, 2f, 0f);
        goomba5.position = new Vector3(80f, 10f, 0f);
        goomba6.position = new Vector3(82f, 10f, 0f);
        goomba7.position = new Vector3(97f, 2f, 0f);
        goomba8.position = new Vector3(98.5f, 2f, 0f);
        goomba9.position = new Vector3(114f, 2f, 0f);
        goomba10.position = new Vector3(115.5f, 2f, 0f);
        goomba11.position = new Vector3(124f, 2f, 0f);
        goomba12.position = new Vector3(125.5f, 2f, 0f);
        goomba13.position = new Vector3(128f, 2f, 0f);
        goomba14.position = new Vector3(129.5f, 2f, 0f);
        goomba15.position = new Vector3(174f, 2f, 0f);
        goomba16.position = new Vector3(175.5f, 2f, 0f);

        mario.velocity = Vector3.zero;
        koopa.velocity = Vector3.zero;
        goomba1.velocity = Vector3.zero;
        goomba2.velocity = Vector3.zero;
        goomba3.velocity = Vector3.zero;
        goomba4.velocity = Vector3.zero;
        goomba5.velocity = Vector3.zero;
        goomba6.velocity = Vector3.zero;
        goomba7.velocity = Vector3.zero;
        goomba8.velocity = Vector3.zero;
        goomba9.velocity = Vector3.zero;
        goomba10.velocity = Vector3.zero;
        goomba11.velocity = Vector3.zero;
        goomba12.velocity = Vector3.zero;
        goomba13.velocity = Vector3.zero;
        goomba14.velocity = Vector3.zero;
        goomba15.velocity = Vector3.zero;
        goomba16.velocity = Vector3.zero;

        //Debug.Log("Reset");
    }
}
