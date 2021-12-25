using System;
using UnityEngine;
using NaughtyAttributes;

[RequireComponent(typeof(MeshRenderer))]
public class OSCSetMaterialColor : MonoBehaviour
{
    [SerializeField] private OSCHandler oscHandler;

    [SerializeField] private Material redMaterial;
    [SerializeField] private Material greenMaterial;
    [SerializeField] private MeshRenderer meshRenderer;
    
    void OnEnable()
    {
        if (oscHandler != null)
        {
            oscHandler.OnRedMaterial += SetMaterialRed;
            oscHandler.OnGreenMaterial += SetMaterialGreen;    
        }
    }

    private void OnDisable()
    {
        if (oscHandler != null)
        {
            oscHandler.OnRedMaterial -= SetMaterialRed;
            oscHandler.OnGreenMaterial -= SetMaterialGreen;
        }
    }
    
    private void Awake()
    {
        if(meshRenderer == null)
          meshRenderer = GetComponent<MeshRenderer>();
    }
    
    [Button]
    private void SetMaterialGreen()
    {
        meshRenderer.material = greenMaterial;
    }
    [Button]
    private void SetMaterialRed()
    {
        meshRenderer.material = redMaterial;
    }
}
