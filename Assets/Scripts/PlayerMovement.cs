using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    const float MIN_WALK = 0.2783203125f;
    const float MAX_WALK = 5.859375f;
    const float MAX_RUN = 9.609375f;
    const float ACC_WALK = 8.349609375f;
    const float ACC_RUN = 12.5244140625f;
    const float DEC_REL = 11.42578125f;
    const float DEC_SKID = 22.8515625f;
    const float MIN_SKID = 2.109375f;

    const float STOP_FALL = 98.4375f;
    const float WALK_FALL = 112.5f;
    const float RUN_FALL = 126.5625f;
    const float STOP_FALL_A = 28.125f;
    const float WALK_FALL_A = 26.3671875f;
    const float RUN_FALL_A = 35.15625f;

    const float MAX_FALL = 16.875f;

    int size = 0; // 0 = little, 1 = big, 2 = super, 3 = little invincible, 4 = big invincible, 5 = super invincible
    bool facingRight = true; // true = right, false = left
    public int state = 0; // 0 = idle, 1 = walking, 2 = running, 3 = skidding, 4 = jumping/falling, 5 = ducking, 6 = pulling flag

    [SerializeField] bool isGrounded = true;

    float fallAcc = 562.5f;

    [SerializeField] int Z = 0;
    [SerializeField] int X = 0;
    int left = 0;
    [SerializeField] int right = 0;
    int up = 0;
    int down = 0;

    [SerializeField] private LayerMask platformLayerMask;
    private Rigidbody2D rb;
    private AudioClip smallJump;
    private AudioClip superJump;
    private AudioSource audioSource;
    private BoxCollider2D boxCollider2d;
    private new Camera camera;

    public void setZ(int value)
    {
        Z = value;
    }

    public void setX(int value)
    {
        X = value;
    }

    public void setRight(int value)
    {
        right = value;
    }

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2d = GetComponent<BoxCollider2D>();
        smallJump = Resources.Load<AudioClip>("Audio/small-jump");
        superJump = Resources.Load<AudioClip>("Audio/super-jump");
        audioSource = GetComponent<AudioSource>();
        camera = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*Z = Input.GetKey(KeyCode.Z);
        X = Input.GetKey(KeyCode.X);
        left = Input.GetKey(KeyCode.LeftArrow);
        right = Input.GetKey(KeyCode.RightArrow);
        up = Input.GetKey(KeyCode.UpArrow);
        down = Input.GetKey(KeyCode.DownArrow);*/

        /*if (Input.GetKey(KeyCode.Z))
        {
            Z = 1;
        }
        else
        {
            Z = 0;
        }

        if (Input.GetKey(KeyCode.X))
        {
            X = 1;
        }
        else
        {
            X = 0;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            left = 1;
        }
        else
        {
            left = 0;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            right = 1;
        }
        else
        {
            right = 0;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            up = 1;
        }
        else
        {
            up = 0;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            down = 1;
        }
        else
        {
            down = 0;
        }*/

        if (state != 4)
        {
            if (Mathf.Abs(rb.velocity.x) < MIN_WALK)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                state = 0;
                //if (left && !down)
                if (left == 1 && down == 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x - MIN_WALK, rb.velocity.y);
                }
                //if (right && !down)
                if (right == 1 && down == 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x + MIN_WALK, rb.velocity.y);
                }
            }
            else if (Mathf.Abs(rb.velocity.x) >= MIN_WALK)
            {
                if (facingRight == true)
                {
                    //if (right && !left && !down)
                    if (right == 1 && left == 0 && down == 0)
                    {
                        //if (X)
                        if (X == 1)
                        {
                            rb.velocity = new Vector2(rb.velocity.x + (ACC_RUN * Time.deltaTime), rb.velocity.y);
                        }
                        else
                        {
                            rb.velocity = new Vector2(rb.velocity.x + (ACC_WALK * Time.deltaTime), rb.velocity.y);
                        }
                    }
                    //else if (left && !right && !down)
                    else if (left == 1 && right == 0 && down == 0)
                    {
                        rb.velocity = new Vector2(rb.velocity.x - (DEC_SKID * Time.deltaTime), rb.velocity.y);
                        state = 3;
                    }
                    else
                    {
                        rb.velocity = new Vector2(rb.velocity.x - (DEC_REL * Time.deltaTime), rb.velocity.y);
                    }
                }
                if (facingRight == false)
                {
                    //if (left && !right && !down)
                    if (left == 1 && right == 0 && down == 0)
                    {
                        //if (X)
                        if (X == 1)
                        {
                            rb.velocity = new Vector2(rb.velocity.x - (ACC_RUN * Time.deltaTime), rb.velocity.y);
                        }
                        else
                        {
                            rb.velocity = new Vector2(rb.velocity.x - (ACC_WALK * Time.deltaTime), rb.velocity.y);
                        }
                    }
                    //else if (right && !left && !down)
                    else if (right == 1 && left == 0 && down == 0)
                    {
                        rb.velocity = new Vector2(rb.velocity.x + (DEC_SKID * Time.deltaTime), rb.velocity.y);
                        state = 3;
                    }
                    else
                    {
                        rb.velocity = new Vector2(rb.velocity.x + (DEC_REL * Time.deltaTime), rb.velocity.y);
                    }
                }
            }
            /////////////////////////////////////rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - (fallAcc * Time.deltaTime));

            //if (Z) // jump
            if (Z > 0) // jump
            {
                if (Mathf.Abs(rb.velocity.x) < 1)
                {
                    rb.velocity = new Vector2(rb.velocity.x, 20f);
                    fallAcc = STOP_FALL;
                }
                else if (Mathf.Abs(rb.velocity.x) < 2.5f)
                {
                    rb.velocity = new Vector2(rb.velocity.x, 20f);
                    fallAcc = WALK_FALL;
                }
                else
                {
                    rb.velocity = new Vector2(rb.velocity.x, 20f);
                    fallAcc = RUN_FALL;
                }
                state = 4;

                /*if (size == 0)
                {
                    audioSource.PlayOneShot(smallJump);
                }
                else
                {
                    audioSource.PlayOneShot(superJump);
                }*/
            }
        }
        else
        {
            // air physics
            // vertical physics
            //if (rb.velocity.y > 0 && Z)
            if (rb.velocity.y > 0 && Z > 0)
            { // holding A while jumping jumps higher
                if (fallAcc == STOP_FALL)
                {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + ((STOP_FALL - STOP_FALL_A) * Time.deltaTime));
                }
                if (fallAcc == WALK_FALL)
                {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + ((WALK_FALL - WALK_FALL_A) * Time.deltaTime));
                }
                if (fallAcc == RUN_FALL)
                {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + ((RUN_FALL - RUN_FALL_A) * Time.deltaTime));
                }
            }

            // horizontal physics
            //if (right && !left)
            if (right == 1 && left == 0)
            {
                if (Mathf.Abs(rb.velocity.x) > MAX_WALK)
                {
                    rb.velocity = new Vector2(rb.velocity.x + (ACC_RUN * Time.deltaTime), rb.velocity.y);

                }
                else
                {
                    rb.velocity = new Vector2(rb.velocity.x + (ACC_WALK * Time.deltaTime), rb.velocity.y);
                }
            }
            //else if (left && !right)
            else if (left == 1 && right == 0)
            {
                if (Mathf.Abs(rb.velocity.x) > MAX_WALK)
                {
                    rb.velocity = new Vector2(rb.velocity.x - (ACC_RUN * Time.deltaTime), rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(rb.velocity.x - (ACC_WALK * Time.deltaTime), rb.velocity.y);
                }
            }
            else
            {
                // do nothing
            }
        }


        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - (fallAcc * Time.deltaTime));

        // max speed calculation
        if (rb.velocity.y >= MAX_FALL)
        {
            rb.velocity = new Vector2(rb.velocity.x, MAX_FALL);
        }
        if (rb.velocity.y <= -MAX_FALL)
        {
            rb.velocity = new Vector2(rb.velocity.x, -MAX_FALL);
        }
        if (rb.velocity.x >= MAX_RUN)
        {
            rb.velocity = new Vector2(MAX_RUN, rb.velocity.y);
        }
        if (rb.velocity.x <= -MAX_RUN)
        {
            rb.velocity = new Vector2(-MAX_RUN, rb.velocity.y);
        }
        //if (rb.velocity.x >= MAX_WALK && !X)
        if (rb.velocity.x >= MAX_WALK && X == 0)
        {
            rb.velocity = new Vector2(MAX_WALK, rb.velocity.y);
        }
        //if (rb.velocity.x <= -MAX_WALK && !X)
        if (rb.velocity.x <= -MAX_WALK && X == 0)
        {
            rb.velocity = new Vector2(-MAX_WALK, rb.velocity.y);
        }

        // update state
        if (state != 4 && state != 6)
        {
            //if (down)
            if (down == 1)
            {
                state = 5;
            }
            else if (Mathf.Abs(rb.velocity.x) > MAX_WALK)
            {
                state = 2;
            }
            else if (Mathf.Abs(rb.velocity.x) >= MIN_WALK)
            {
                state = 1;
            }
            else
            {
                state = 0;
            }
        }
        else
        {

        }

        // update direction

        if (rb.velocity.x > 0 && !facingRight)
        {
            Flip();
            //facingRight = true;
        }
        else if (rb.velocity.x < 0 && facingRight)
        {
            Flip();
            //facingRight = false;
        }

        IsGrounded();

        if (isGrounded)
        {
            if (rb.velocity.y < 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                if (state == 4)
                {
                    state = 0;
                }
            }
        }
        /*Vector2 leftEdge = camera.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightEdge = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        rb.position = new Vector3(Mathf.Clamp(rb.position.x, leftEdge.x + 0.5f, rightEdge.x - 0.5f), rb.position.y);*/
    }

    void IsGrounded()
    {
        float extraHeightText = 0.13375f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(new Vector2(boxCollider2d.bounds.center.x, boxCollider2d.bounds.center.y - 0.25f), new Vector2(boxCollider2d.bounds.size.x - 0.3f, boxCollider2d.bounds.size.y / 4), 0f, Vector2.down, extraHeightText, platformLayerMask);
        isGrounded = raycastHit.collider;// != null;
    }

    void Flip()
    {
        Vector2 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }
}
