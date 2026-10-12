using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    void Start()
    {
        Debug.Log("게임 시작!");
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");// X축 (A D)
        float vertical = Input.GetAxisRaw("Vertical"); // z축 (W S)

        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical);

        transform.position += moveDirection.normalized * moveSpeed * Time.deltaTime;
    }
}
