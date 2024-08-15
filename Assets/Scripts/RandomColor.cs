using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : NetworkBehaviour
{
    public override void OnStartServer()
    {
        Debug.Log("RandomColor>> OnStartServer");
        color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    // Color32 packs to 4 bytes
    [SyncVar(hook = nameof(SetColor))]
    public Color32 color = Color.black;

    // Unity clones the material when GetComponent<Renderer>().material is called
    // Cache it here and destroy it in OnDestroy to prevent a memory leak
    Material cachedMaterial;

    void SetColor(Color32 _, Color32 newColor)
    {
        Debug.Log("RandomColor>> SetColor");
        if (cachedMaterial == null) cachedMaterial = GetComponentInChildren<Renderer>().material;
        cachedMaterial.color = newColor;
    }

    void OnDestroy()
    {
        Debug.Log("RandomColor>> OnDestroy");
        Destroy(cachedMaterial);
    }
}
