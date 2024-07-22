using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerBehaviour : MonoBehaviour
{
    private bool _isFacingRight = true;
    private float baseGravity = 5.0f;
    private float fallSpeedMultiplier = 2.0f;
    private float maxFallSpeed = 20.0f;
    int noOfJumps;
    [SerializeField] private int maxJumps = 2;
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float jumpPower = 5f;
    private Rigidbody2D rb2d;
    private CapsuleCollider2D playerCollider;
    [SerializeField] private LayerMask groundLayer;
    private Animator playerAnim;
    private PlayerInputHandler playerInputHandler;

    // Start is called before the first frame update
    void Awake()
    {
        playerInputHandler = GetComponent<PlayerInputHandler>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        playerAnim = gameObject.GetComponent<Animator>();
        playerCollider = gameObject.GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovementSystem();
        //JumpSystem();
        FallModifier();
    }
    void Update()
    {
        CheckGround(); //Displaying the raycasts in the editor.
        if (CheckGround())
        {
            noOfJumps = maxJumps;
        }
    }
    private void MovementSystem()
    {
        //After many trials this cunt works
        Vector2 direction = playerInputHandler.Move;
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb2d.velocity = new Vector2(direction.x * speed, rb2d.velocity.y);
        if (direction.x == 1 || direction.x == -1)
        {
            playerAnim.SetBool("isRunning", true);
        }
        else if (direction.x == 0)
        {
            playerAnim.SetBool("isRunning", false);
        }
        //Flip the Player
        if (direction.x > 0 && !_isFacingRight)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            _isFacingRight = true;
        }
        else if (direction.x < 0 && _isFacingRight)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            _isFacingRight = false;
        }
    }

    private void FallModifier()
    {
        if (rb2d.velocity.y < 0 && noOfJumps <= 0)
        {
            rb2d.gravityScale = baseGravity * fallSpeedMultiplier;
            rb2d.velocity = new Vector2(rb2d.velocity.x, Mathf.Max(rb2d.velocity.y, -maxFallSpeed));
        }
        else
        {
            rb2d.gravityScale = baseGravity;
        }
    }
    public void JumpSystem(InputAction.CallbackContext context)
    {
       //THIS ONE IS BROKEN.
        if (noOfJumps > 0)
        {
            if (playerInputHandler.Jump)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpPower);
                noOfJumps--;
            }
            if (playerInputHandler.Hop)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * 0.5f);
                noOfJumps--;
            }
        }
    }

    private bool CheckGround()
    {
        float extraHeight = 0.5f;
        RaycastHit2D hitDetect = Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0f, Vector2.down, extraHeight, groundLayer);

        Color rayColor;
        if (hitDetect.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Vector3 boxStartPos = playerCollider.bounds.center;
        Debug.DrawRay(boxStartPos + new Vector3(playerCollider.bounds.extents.x, 0), Vector2.down * (playerCollider.bounds.extents.y + extraHeight), rayColor);
        Debug.DrawRay(boxStartPos - new Vector3(playerCollider.bounds.extents.x, 0), Vector2.down * (playerCollider.bounds.extents.y + extraHeight), rayColor);
        Debug.DrawRay(boxStartPos - new Vector3(playerCollider.bounds.extents.x, playerCollider.bounds.extents.y + extraHeight), Vector2.right * (playerCollider.bounds.extents.x + 1.5f), rayColor);
        return hitDetect.collider != null;
    }


}
