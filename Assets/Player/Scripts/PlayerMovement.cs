using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{

  [SerializeField] float speed = 2f;
  [SerializeField] float maxSpeed = 5f;
  [SerializeField] float jumpForce = 400f;
  [SerializeField] float dashForce = 5f;
  [SerializeField] float maxDashTime = 0.2f;
  [SerializeField] LayerMask groundLayer;
  [SerializeField] Transform groundCheck;
  [SerializeField] Transform wallCheck;
  public bool isFacingRight = true;
  bool isGrounded = false;
  bool isFacingWall = false;
  bool isWallSliding = false;
  bool isDashing = false;
  float timeDashing = 0f;
  Rigidbody2D rb;
  Animator anim;


  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
  }

  void FixedUpdate()
  {
    MoveHorizontaly();
    CheckIfGrounded();
    CheckIfIsAtWall();
    HandleJump();
    HandleWallJump();
    HandleWallSlide();
    HandleDash();
  }

  void MoveHorizontaly()
  {
    float h = CrossPlatformInputManager.GetAxis("Horizontal");
    float xOffset = h * speed;
    float newXVelocity = isDashing ?
    Mathf.Clamp(rb.velocity.x + xOffset, -maxSpeed - dashForce, maxSpeed + dashForce) :
    Mathf.Clamp(rb.velocity.x + xOffset, -maxSpeed, maxSpeed);
    rb.velocity = new Vector2(newXVelocity, rb.velocity.y);
    if (rb.velocity.x > 0.5f && h > 0 && !isFacingRight && !isWallSliding) { Flip("right"); }
    if (rb.velocity.x < -0.5f && h < 0 && isFacingRight && !isWallSliding) { Flip("left"); }
    if (h == 0f)
    {
      anim.SetBool("isMoving", false);
    }
    else
    {
      anim.SetBool("isMoving", true);
    }
  }

  void HandleJump()
  {
    if (CrossPlatformInputManager.GetButtonDown("Jump") && isGrounded)
    {
      anim.SetBool("isGrounded", false);
      rb.AddForce(Vector2.up * jumpForce);
    }
    if (CrossPlatformInputManager.GetButton("Jump") && !isGrounded)
    {
      rb.AddForce(Vector2.up * jumpForce / 25);
    }
  }

  void HandleWallJump()
  {
    if (isWallSliding && !isGrounded && CrossPlatformInputManager.GetButtonDown("Jump"))
    {
      if (isFacingRight)
      {
        rb.AddForce(new Vector2(0.5f, 1f) * jumpForce);
      }
      else
      {
        rb.AddForce(new Vector2(-0.5f, 1f) * jumpForce);
      }
    }
    if (isWallSliding && isGrounded && isFacingWall && CrossPlatformInputManager.GetButtonDown("Jump"))
    {
      Flip();
    }
  }

  void HandleWallSlide()
  {
    if (isWallSliding)
    {
      if (rb.velocity.y <= 0f)
      {
        Vector2 newVelocity = rb.velocity;
        newVelocity.y = -0.25f;
        rb.velocity = newVelocity;
      }
    }
  }

  void CheckIfGrounded()
  {
    isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    anim.SetBool("isGrounded", isGrounded);
  }

  void CheckIfIsAtWall()
  {
    isFacingWall = Physics2D.OverlapCircle(wallCheck.position, 0.1f, groundLayer);
    anim.SetBool("isAtWall", isFacingWall);
  }

  void Flip(string side = null)
  {
    if (side == null)
    {
      isFacingRight = !isFacingRight;

      Vector3 newScale = transform.localScale;
      newScale.x *= -1;
      transform.localScale = newScale;
    }
    else if (side == "right")
    {
      isFacingRight = true;

      Vector3 newScale = transform.localScale;
      newScale.x = Mathf.Abs(newScale.x);
      transform.localScale = newScale;
    }
    else if (side == "left")
    {
      isFacingRight = false;

      Vector3 newScale = transform.localScale;
      newScale.x = -Mathf.Abs(newScale.x);
      transform.localScale = newScale;
    }
  }

  void OnNearWallEnter()
  {
    if (!isGrounded)
    {
      Flip();
      isWallSliding = true;
    }
    else
    {
      isWallSliding = true;
    }
    anim.SetBool("isWallSliding", isWallSliding);
  }

  void OnNearWallExit()
  {
    isWallSliding = false;
    anim.SetBool("isWallSliding", isWallSliding);
  }

  void HandleDash()
  {
    if (CrossPlatformInputManager.GetButton("Dash"))
    {
      timeDashing += Time.deltaTime;
      if (timeDashing < maxDashTime)
      {
        isDashing = true;
        Vector2 newVelocity = rb.velocity;
        if (isFacingRight) { newVelocity.x = maxSpeed + dashForce; } else { newVelocity.x = -maxSpeed - dashForce; }
        rb.velocity = newVelocity;
      }
      else if (isGrounded || isWallSliding) { isDashing = false; }
    }
    else if (isGrounded || isWallSliding) { isDashing = false; timeDashing = 0f; }
    anim.SetBool("isDashing", isDashing);
  }
}
