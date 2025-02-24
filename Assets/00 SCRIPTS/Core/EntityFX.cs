using System.Collections;
using Spine.Unity;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private SkeletonAnimation skeletonAnimation;

    [Header("Flash FX")]
    [SerializeField] private float flashDuration;
    [SerializeField] private Material hitMaterial;
    private Material originalMaterial;
    private Material instanceMaterial;
    private Color skeletonColor;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        skeletonAnimation = GetComponentInChildren<SkeletonAnimation>();

        if (spriteRenderer != null)
        {
            originalMaterial = spriteRenderer.material;
        }
        else if (skeletonAnimation != null)
        {
            skeletonColor = skeletonAnimation.skeleton.GetColor();
        }
    }

    public IEnumerator FlashFX()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.material = hitMaterial;
            Color currentColor = spriteRenderer.color;
            spriteRenderer.color = Color.white;

            yield return new WaitForSeconds(flashDuration);

            spriteRenderer.color = currentColor;
            spriteRenderer.material = originalMaterial;
        }
        else if (skeletonAnimation != null)
        {
            skeletonAnimation.skeleton.SetColor(Color.red);

            yield return new WaitForSeconds(flashDuration);

            skeletonAnimation.skeleton.SetColor(Color.white);
        }
    }

    public void RedColorBlink()
    {

        if (spriteRenderer != null)
        {
            spriteRenderer.color = (spriteRenderer.color != Color.white) ? Color.white : Color.red;
        }
        else if (skeletonAnimation != null)
        {
            Color currentColor = skeletonAnimation.skeleton.GetColor();
            skeletonAnimation.skeleton.SetColor(currentColor != Color.white ? Color.white : Color.red);
        }
    }

    public void CancelColorChange()
    {
        CancelInvoke();

        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white;
        }
        else if (skeletonAnimation != null)
        {
            skeletonAnimation.skeleton.SetColor(Color.white);
        }
    }
}
