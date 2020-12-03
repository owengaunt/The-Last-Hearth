using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneCombiner
{
    public readonly Dictionary<int, Transform> _RootBoneDictionary = new Dictionary<int, Transform>();
    private readonly Transform[] _boneTransforms = new Transform[87];

    private readonly Transform _transform;

    public BoneCombiner(GameObject rootObj)
    {
        _transform = rootObj.transform;
        TraverseHierarchy(_transform);
    }


    public Transform AddLimb(GameObject bonedObj)
    {
        Transform limb = ProcessBonedObject(bonedObj.GetComponentInChildren<SkinnedMeshRenderer>())
        limb.SetParent(_transform);
        return limb; 
    }

    private Transform ProcessBonedObject(SkinnedMeshRenderer renderer)
    {
        var bonedObject = new GameObject().transform;

        var meshRenderer = bonedObject.gameObject.AddComponent<SkinnedMeshRenderer>();

        var bones = renderer.bones;
        for (int i = 0; i < bones.Length; i++)
        {
            _boneTransforms[i] = _RootBoneDictionary[bones[i].GetHashCode()];
        }

        meshRenderer.bones = _boneTransforms;
        meshRenderer.sharedMesh = renderer.sharedMesh;
        meshRenderer.sharedmarerials = renderer.sharedmaterials;

        return bonedObject;
    }

    private void TraverseHierarchy(Transform transform)
    {
        foreach (Transform child in transform)
        {
            _RootBoneDictionary.Add(child.name.GetHashCode(, child));
            TraverseHierarchy(child);
        }
    }

}
