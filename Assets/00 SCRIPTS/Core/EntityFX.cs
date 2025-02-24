using System.Collections;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private MeshRenderer meshRenderer;

    [Header("Flash FX")]
    [SerializeField] private Material hitMaterial;
    private Material originalMaterial;

    private void Start()
    {
        // Tìm cả SpriteRenderer và MeshRenderer
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();

        // Kiểm tra cái nào tồn tại và lưu material gốc
        if (spriteRenderer != null)
        {
            originalMaterial = spriteRenderer.material;
        }
        else if (meshRenderer != null)
        {
            originalMaterial = meshRenderer.material;
        }
    }

    public IEnumerator HitFlashFX()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.material = hitMaterial;
        }
        else if (meshRenderer != null)
        {
            meshRenderer.material = hitMaterial;
        }

        yield return new WaitForSeconds(0.2f);

        Reset();
    }

    public void Reset()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.material = originalMaterial;
        }
        else if (meshRenderer != null)
        {
            meshRenderer.material = originalMaterial;
        }
    }
}
