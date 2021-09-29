using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    //Variable for instance of the health bar (singleton)
    public static PlayerMovement SharedInstance;

    //Variables for movement
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private float smoothTime = 0.05f;
    private Vector3 velocity;
    private float horizontalMovement;
    private float playerSpeed;

    //Variables for jump
    private bool isJumping = false;
    private bool isGrounded;
    [SerializeField] private float verticalJumpForce;

    //Variables for animation
    [SerializeField] private Animator playerAnim;
    [SerializeField] private SpriteRenderer playerSR;

    //Variables for ladders
    private bool closeLadder;
    private float verticalMovement;
    private bool isClimbing;
    [SerializeField] private float climbSpeed;
    private float ladderCenterX;

    //Variables for collider
    [SerializeField] private BoxCollider2D playerBC;

    #endregion

    #region UnityMethods
    // Start is called before the first frame update
    void Awake()
    {
        //Get the instance for player health
        SharedInstance = this;
    }

    // Update is called once per frame
    void Update()
    {
        //Get horizontal movement based on input
        horizontalMovement = Input.GetAxis("Horizontal") * PlayerStats.SharedInstance.GetPlayerMoveSpeed();

        //Get vertical movement based on input
        verticalMovement = Input.GetAxis("Vertical") * climbSpeed;

        //Jump if spacebar is pressed and if the player is on the ground 
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
        }

        if (!isJumping)
        {
            //Get player speed based on velocity
            playerSpeed = Mathf.Abs(playerRb.velocity.x);
        }

        //If player is close to a ladder and try to use arrows, set climbing to true and make platform collider crossable
        if(closeLadder && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)))
        {
            isJumping = false;
            isClimbing = true;

            SetPlayerMiddleLadder();

            //Get all platforms in level and make them crossable
            foreach(var element in GameObject.FindGameObjectsWithTag("Platform"))
            {
                element.GetComponent<PlatformCollider>().MakePlatformCrossable();
            }
        }

        //Set parameters for animation
        FlipSprite(playerRb.velocity);
        playerAnim.SetBool("isJumping", isJumping);
        playerAnim.SetBool("isClimbing", isClimbing);
        playerAnim.SetFloat("speed", playerSpeed);
    }

    //Frame-rate independent update for physics calculations
    private void FixedUpdate()
    {
        //Move the player using physics
        MovePlayer(horizontalMovement * Time.fixedDeltaTime, verticalMovement * Time.fixedDeltaTime);
    }

    #endregion

    #region Getters & Setters
    public bool GetIsClimbing()
    {
        return isClimbing;
    }
    #endregion

    #region Methods
    //Add force to rigidbody based on movement
    private void MovePlayer(float horizontalMovement, float verticalMovement)
    {
        if (!isClimbing)
        {
            //Create target velocity and add it to the player for horizontal movement
            Vector3 targetHorizontalVelocity = new Vector2(horizontalMovement, playerRb.velocity.y);
            playerRb.velocity = Vector3.SmoothDamp(playerRb.velocity, targetHorizontalVelocity, ref velocity, smoothTime);
            playerRb.gravityScale = Constants.GRAVITYSCALE;
        }
        else
        {
            //Create target velocity and add it to the player for horizontal movement
            Vector3 targetVerticalVelocity = new Vector2(0, verticalMovement);
            playerRb.velocity = Vector3.SmoothDamp(playerRb.velocity, targetVerticalVelocity, ref velocity, smoothTime);
            playerRb.gravityScale = 0;
        }

        //Add force for the jump
        if (isJumping && isGrounded)
        {
            playerRb.AddForce(new Vector2(0.0f, verticalJumpForce));
        }
    }
    private void SetPlayerMiddleLadder()
    {
        this.transform.position = new Vector3(ladderCenterX, this.transform.position.y, this.transform.position.z);
    }

    public void IncreaseSpeedByPercentage(float percentage)
    {
        PlayerStats.SharedInstance.SetPlayerMoveSpeed(PlayerStats.SharedInstance.GetPlayerMoveSpeed() * (1 + percentage / 100));
    }

    public void DecreaseSpeedByPercentage(float percentage)
    {
        PlayerStats.SharedInstance.SetPlayerMoveSpeed(PlayerStats.SharedInstance.GetPlayerMoveSpeed() / (1 + percentage / 100));
    }

    private void FlipSprite(Vector3 velocity)
    {
        //Flip sprite if player is going backward on X axis
        if (velocity.x <= -Constants.FLIPSPEEDVALUE)
        {
            playerSR.flipX = true;
        }
        else if (velocity.x >= Constants.FLIPSPEEDVALUE)
        {
            playerSR.flipX = false;
        }
    }
    public void DeactivatePlayerInteractions()
    {
        //Stop player movement
        SharedInstance.enabled = false;

        //Avoid collisions with other elements
        playerRb.bodyType = RigidbodyType2D.Kinematic;
        playerBC.enabled = false;

        //Stop any movement
        playerRb.velocity = new Vector2(0, 0);
    }

    public void ActivatePlayerInteractions()
    {
        //Restart player movement
        PlayerMovement.SharedInstance.enabled = true;

        //Enable collisions with other elements
        playerRb.bodyType = RigidbodyType2D.Dynamic;
        playerBC.enabled = true;
    }
    #endregion

    #region Collisions
    //Detect collision between Ground and Player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isJumping = false;
            isClimbing = false;
        }
    }

    //Detect that player is leaving the ground
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Detect ladder close to player
        if (collision.CompareTag("Ladders"))
        {
            closeLadder = true;
            Transform ladderTransform = collision.gameObject.transform;
            ladderCenterX = ladderTransform.position.x + ladderTransform.gameObject.GetComponent<TilemapRenderer>().bounds.size.x / 2;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Detect that ladder is far from player and stop climbing
        if (collision.CompareTag("Ladders"))
        {
            closeLadder = false;
            isClimbing = false;
        }

        //Detect that player crossed platform and make it uncrossable
        else if (collision.CompareTag("Platform"))
        {
            collision.GetComponent<PlatformCollider>().MakePlatformUncrossable();
        }
    }
    #endregion
}
