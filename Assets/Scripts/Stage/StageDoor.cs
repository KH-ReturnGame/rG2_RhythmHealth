using UnityEngine;

public class StageDoor : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    PolygonCollider2D polCollider2d;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        polCollider2d = GetComponent<PolygonCollider2D>();
        int index = PlayerPrefs.GetInt("DialogIndex");
        if(index == 4)
        {
            polCollider2d.enabled = false;
            spriteRenderer.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
