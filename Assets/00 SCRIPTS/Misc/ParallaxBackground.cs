using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private float parallaxEffect;
    private float xPosition;
    private float widthImage;

    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
        xPosition = transform.position.x;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Texture image = spriteRenderer.sprite.texture;
        widthImage = image.width / spriteRenderer.sprite.pixelsPerUnit;
    }

    private void Update()
    {
        float distanceToMove = mainCam.transform.position.x * parallaxEffect;
        float loopThreshold = mainCam.transform.position.x * (1 - parallaxEffect);

        transform.position = new Vector3(xPosition + distanceToMove, transform.position.y);

        if (loopThreshold > xPosition + widthImage)
            xPosition += widthImage;
        else if (loopThreshold < xPosition - widthImage)
            xPosition -= widthImage;
    }
}
