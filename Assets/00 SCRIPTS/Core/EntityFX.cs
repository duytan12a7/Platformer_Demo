using System.Collections;
using Spine.Unity;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private SkeletonAnimation skeletonAnimation;
    private SkeletonMecanim skeletonMecanim;

    [Header("Flash FX")]
    [SerializeField] private float flashDuration;
    [SerializeField] private Material hitMaterial;
    private Material originalMaterial;
    private Material instanceMaterial;
    private Color skeletonColor;


    [Header("Ailment colors")]
    [SerializeField] private Color[] igniteColor;
    [SerializeField] private Color[] chillColor;
    [SerializeField] private Color[] shockColor;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        skeletonAnimation = GetComponentInChildren<SkeletonAnimation>();
        skeletonMecanim = GetComponentInChildren<SkeletonMecanim>();


        if (skeletonAnimation != null)
        {
            skeletonColor = skeletonAnimation.skeleton.GetColor();
        }
        else if (skeletonMecanim != null)
        {
            skeletonColor = skeletonMecanim.skeleton.GetColor();
        }
        else if (spriteRenderer != null)
        {
            originalMaterial = spriteRenderer.material;
        }
    }

    public IEnumerator FlashFX()
    {
        if (skeletonAnimation != null)
        {
            skeletonAnimation.skeleton.SetColor(Color.red);

            yield return new WaitForSeconds(flashDuration);

            skeletonAnimation.skeleton.SetColor(Color.white);
        }
        else if (skeletonMecanim != null)
        {
            skeletonMecanim.skeleton.SetColor(Color.red);

            yield return new WaitForSeconds(flashDuration);

            skeletonMecanim.skeleton.SetColor(Color.white);
        }
        else if (spriteRenderer != null)
        {
            spriteRenderer.material = hitMaterial;
            Color currentColor = spriteRenderer.color;
            spriteRenderer.color = Color.white;

            yield return new WaitForSeconds(flashDuration);

            spriteRenderer.color = currentColor;
            spriteRenderer.material = originalMaterial;
        }
    }

    public void RedColorBlink()
    {
        if (skeletonAnimation != null)
        {
            Color currentColor = skeletonAnimation.skeleton.GetColor();
            skeletonAnimation.skeleton.SetColor(currentColor != Color.white ? Color.white : Color.red);
        }
        else if (skeletonMecanim != null)
        {
            Color currentColor = skeletonMecanim.skeleton.GetColor();
            skeletonMecanim.skeleton.SetColor(currentColor != Color.white ? Color.white : Color.red);
        }
        else if (spriteRenderer != null)
        {
            spriteRenderer.color = (spriteRenderer.color != Color.white) ? Color.white : Color.red;
        }
    }

    public void CancelColorChange()
    {
        CancelInvoke();

        if (skeletonAnimation != null)
        {
            skeletonAnimation.skeleton.SetColor(Color.white);
        }
        else if (skeletonMecanim != null)
        {
            skeletonMecanim.skeleton.SetColor(Color.white);
        }
        else if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white;
        }
    }


    public void IgniteFxFor(float _seconds)
    {
        InvokeRepeating(nameof(IgniteColorFx), 0, .3f);
        Invoke(nameof(CancelColorChange), _seconds);
    }

    public void ChillFxFor(float _seconds)
    {
        InvokeRepeating(nameof(ChillColorFx), 0, .3f);
        Invoke(nameof(CancelColorChange), _seconds);
    }


    public void ShockFxFor(float _seconds)
    {
        InvokeRepeating(nameof(ShockColorFx), 0, .3f);
        Invoke(nameof(CancelColorChange), _seconds);
    }

    private void IgniteColorFx()
    {
        skeletonColor = (skeletonColor == igniteColor[0]) ? igniteColor[1] : igniteColor[0];
        if (skeletonAnimation != null)
            skeletonAnimation.skeleton.SetColor(skeletonColor);
        else if (skeletonMecanim != null)
            skeletonMecanim.skeleton.SetColor(skeletonColor);
        else if (spriteRenderer != null)
            spriteRenderer.color = skeletonColor;
    }

    private void ChillColorFx()
    {
        skeletonColor = (skeletonColor == chillColor[0]) ? chillColor[1] : chillColor[0];
        if (skeletonAnimation != null)
            skeletonAnimation.skeleton.SetColor(skeletonColor);
        else if (skeletonMecanim != null)
            skeletonMecanim.skeleton.SetColor(skeletonColor);
        else if (spriteRenderer != null)
            spriteRenderer.color = skeletonColor;
    }

    private void ShockColorFx()
    {
        skeletonColor = (skeletonColor == shockColor[0]) ? shockColor[1] : shockColor[0];
        if (skeletonAnimation != null)
            skeletonAnimation.skeleton.SetColor(skeletonColor);
        else if (skeletonMecanim != null)
            skeletonMecanim.skeleton.SetColor(skeletonColor);
        else if (spriteRenderer != null)
            spriteRenderer.color = skeletonColor;
    }
}
