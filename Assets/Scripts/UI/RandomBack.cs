using UnityEngine;
using UnityEngine.UI;

public class RandomBack : MonoBehaviour
{
    public Sprite[] sprites;
    public Image backgroundImage;
    void Awake()
    {
        backgroundImage.sprite = sprites[Random.Range(0, sprites.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
