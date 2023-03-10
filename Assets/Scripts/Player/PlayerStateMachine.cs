using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerState : FSM
{
    protected GameObject gameObject;

    public PlayerState(GameObject gameObject) : base()
    {
        this.gameObject = gameObject;
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
    }
    public virtual void OnCollisionStay2D(Collision2D collision)
    {
    }
    public virtual void OnCollisionExit2D(Collision2D collision)
    {
    }

    protected Vector2 GetBoxColliderSize()
    {
        return gameObject.GetComponent<BoxCollider2D>().size * new Vector3(Mathf.Abs(gameObject.transform.localScale.x), gameObject.transform.localScale.y);
    }

    protected bool IsGrounded()
    {
        float distToGround = gameObject.GetComponent<Collider2D>().bounds.extents.y;
        Debug.DrawRay(gameObject.transform.position, -Vector3.up * distToGround, Color.red);
        Debug.DrawRay(gameObject.transform.position - Vector3.up * distToGround , -Vector3.up * 0.1f, Color.blue);
        ContactFilter2D filter = new ContactFilter2D();
        filter.useLayerMask = true;
        filter.useTriggers = false;
        filter.SetLayerMask(~LayerMask.GetMask("Player"));
        RaycastHit2D[] res = new RaycastHit2D[1];
        return Physics2D.BoxCast(gameObject.transform.position, GetBoxColliderSize(), 0, -Vector3.up, filter, res, 0.1f) != 0;
    }

    protected void FixVelocityWithContact()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(gameObject.GetComponent<Rigidbody2D>().transform.position, GetBoxColliderSize(), 0, gameObject.GetComponent<Rigidbody2D>().velocity.normalized, gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * Time.deltaTime, ~LayerMask.GetMask("Player") & ~LayerMask.GetMask("Enemy"));
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.isTrigger)
            {
                continue;
            }

            if (hit.rigidbody.bodyType != RigidbodyType2D.Static)
            {
                continue;
            }

            if(Mathf.Abs(hit.normal.x) < 0.1f || Mathf.Sign(hit.normal.x) == Mathf.Sign(gameObject.GetComponent<Rigidbody2D>().velocity.x))
            {
                continue;
            }

            float distToGround = gameObject.GetComponent<Collider2D>().bounds.extents.y;
            if ((hit.point.y + distToGround - hit.centroid.y) < 0.1f) {
                Vector2 pos = gameObject.GetComponent<Rigidbody2D>().transform.position;
                pos.y += 0.1f;
                gameObject.GetComponent<Rigidbody2D>().MovePosition(pos);
                continue;

            }
            Debug.Log("Collide with : " + hit.normal + hit.centroid + (hit.point + new Vector2(0, distToGround)) + gameObject.GetComponent<Rigidbody2D>().transform.position);

            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, gameObject.GetComponent<Rigidbody2D>().velocity.y);
            break;
        }
    }
}

public class PlayerStateGrounded : PlayerState
{
    private Rigidbody2D rb;

    public PlayerStateGrounded(GameObject gameObject) : base(gameObject)
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    protected override void Init()
    {
        base.Init();
        PlayerController.Instance.OnJumpCB += OnJumpPressed;
    }

    protected override void Update()
    {
        float currentSpeed = PlayerController.Instance.GetCurrentSpeed();
        rb.velocity = new Vector2(currentSpeed, rb.velocity.y);
        FixVelocityWithContact();

        if (currentSpeed < 0)
        {
            gameObject.transform.localScale = new Vector3(-Mathf.Abs(gameObject.transform.localScale.x), gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        } else if(currentSpeed > 0)
        {
            gameObject.transform.localScale = new Vector3(Mathf.Abs(gameObject.transform.localScale.x), gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        }
        
        if(!IsGrounded())
        {
            base.nextState = new PlayerStateInAir(gameObject);
        }
    }

    private void OnJumpPressed()
    {
        rb.velocity = new Vector2(rb.velocity.x, Mathf.Min(rb.velocity.y + PlayerController.Instance.JumpSpeed, PlayerController.Instance.JumpSpeed));
    }

    protected override void Exit()
    {
        base.Exit();
        PlayerController.Instance.OnJumpCB -= OnJumpPressed;
    }
}

public class PlayerStateInAir : PlayerState
{
    private Rigidbody2D rb;

    public PlayerStateInAir(GameObject gameObject) : base(gameObject)
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    protected override void Update()
    {
        float currentSpeed = PlayerController.Instance.GetCurrentSpeed();
        rb.velocity = new Vector2(currentSpeed, rb.velocity.y);
        FixVelocityWithContact();

        if (currentSpeed < 0)
        {
            gameObject.transform.localScale = new Vector3(-Mathf.Abs(gameObject.transform.localScale.x), gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        }
        else if (currentSpeed > 0)
        {
            gameObject.transform.localScale = new Vector3(Mathf.Abs(gameObject.transform.localScale.x), gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        }

        if (IsGrounded())
        {
            base.nextState = new PlayerStateGrounded(gameObject);
        }
    }
}

public class PlayerStateMachine : MonoBehaviour
{
    private PlayerState state;

    private void Start()
    {
        state = new PlayerStateGrounded(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        state = (PlayerState)state.Progress();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        state.OnCollisionEnter2D(collision);
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        state.OnCollisionStay2D(collision);
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        state.OnCollisionExit2D(collision);
    }
}
