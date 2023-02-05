using UnityEngine;
using UnityEngine.Serialization;

public class BackgroundLayer : MonoBehaviour
{
    [FormerlySerializedAs("Background")]  
    public GameObject background;
    
    [FormerlySerializedAs("Sprite")] 
    public float speed;

    public float SpriteWidth { get; private set; }

    public float StartPosition { get; set; }
    
    private void Start()
    {
        SpriteWidth = background.GetComponent<SpriteRenderer>().bounds.size.x;
        StartPosition = background.transform.position.x;
    }
}