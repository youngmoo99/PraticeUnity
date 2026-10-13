using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{   
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    
    private Rigidbody rb;
    private Vector3 moveDirection;

    private bool isGrounded = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveInput();
        JumpInput();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void MoveInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");// X축 (A D)
        float vertical = Input.GetAxisRaw("Vertical"); // z축 (W S)
        moveDirection = new Vector3(horizontal, 0f, vertical).normalized; // 키보드에서 입력받은 방향 저장
    }

    void Move()
    {
        Vector3 velocity = moveDirection * moveSpeed;

        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime); // rigidbody 이용해 캐릭터 이동
    }
    private void JumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        isGrounded = false;
        rb.AddForce(
            Vector3.up * jumpForce,
            ForceMode.Impulse
        );
        Debug.Log("점프성공!");
    }

    void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                if (Vector3.Dot(contact.normal, Vector3.up) > 0.5f)
                {
                    isGrounded = true;
                    break;
                }
            }
        }
    }
}
