using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Parallax : MonoBehaviour
{
    [FormerlySerializedAs("BackgroundLayers")] 
    public List<BackgroundLayer> backgroundLayers;

    [FormerlySerializedAs("CameraPosition")] 
    public Transform cameraPosition;

    private Vector3 _previousCameraPosition;

    private void Start()
    {
        _previousCameraPosition = cameraPosition.position;
    }

    private void LateUpdate()
    {
        var position = cameraPosition.position;
        
        foreach (var background in backgroundLayers)
        {
            var deltaX = (position.x - _previousCameraPosition.x) * background.speed;
            
            background.transform.Translate(new Vector3(deltaX, 0));
            var moveAmount = position.x * (1 - background.speed);
            
            if (moveAmount > background.StartPosition + background.SpriteWidth)
            {
                background.transform.Translate(new Vector3(background.SpriteWidth, 0));
                background.StartPosition += background.SpriteWidth;
            }
            else if (moveAmount < background.StartPosition - background.SpriteWidth)
            {
                background.transform.Translate(new Vector3(-background.SpriteWidth, 0));
                background.StartPosition -= background.SpriteWidth;
            }
        }
        
        _previousCameraPosition = cameraPosition.position;
    }
}   