using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    private Rigidbody2D _playerRigid;
    private SpriteRenderer spriteRenderer;
    private Collider2D _playerCollider;
    Vector2 inputVec;
    public float speed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerRigid = GetComponent<Rigidbody2D>();
         spriteRenderer = GetComponent<SpriteRenderer>();
        _playerCollider = GetComponent<Collider2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        movement();
    }

    void movement()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        _playerRigid.MovePosition(_playerRigid.position + nextVec);
    }
}
