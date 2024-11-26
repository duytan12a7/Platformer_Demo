using System.Collections;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [Header("Flash FX")]
    [SerializeField] private Material hitMaterial;
    private Material orignalMaterial;


    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        orignalMaterial = spriteRenderer.material;
    }

    public IEnumerator HitFlashFX()
    {
        spriteRenderer.material = hitMaterial;

        yield return new WaitForSeconds(0.2f);

        Reset();
    }

    public void Reset()
    {
        spriteRenderer.material = orignalMaterial;
    }
}