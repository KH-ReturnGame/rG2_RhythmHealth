using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    private Rigidbody2D _playerRigid;
    Vector2 inputVec;
    public float speed = 5f;
    public SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerRigid = GetComponent<Rigidbody2D>();
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
        if(inputVec.x > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if(inputVec.x < 0)
        {
            spriteRenderer.flipX = false;
        }
    }
}