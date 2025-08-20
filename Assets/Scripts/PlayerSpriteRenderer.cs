using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerSpriteRenderer : MonoBehaviour
{
    [SerializeField]private PlayerMovement movement;
    public SpriteRenderer spriteRenderer { get; private set; }
    public Sprite idle;
    public Sprite jump;
    public Sprite slide;
    public AnimatedSprite run;

    private void Awake()
    {
        movement = GetComponentInParent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        if (movement.state == 1) //walking
        {
            run.framerate = 1 / 10f;
            run.enabled = true;
        }
        else if (movement.state == 2) //running
        {
            run.framerate = 1 / 20f;
            run.enabled = true;
        }
        else
        {
            run.enabled = false;
        }

        if (movement.state == 4)
        {
            spriteRenderer.sprite = jump;
        }
        else if (movement.state == 3)
        {
            spriteRenderer.sprite = slide;
        }
        else if (movement.state == 0)
        {
            spriteRenderer.sprite = idle;
        }
    }

    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        spriteRenderer.enabled = false;
        run.enabled = false;
    }

}
