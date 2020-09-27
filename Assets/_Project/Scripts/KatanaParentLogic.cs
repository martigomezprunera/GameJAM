using System;
using System.Collections.Generic;
using UnityEngine;

public class KatanaParentLogic : MonoBehaviour
{
    [SerializeField] private Transform _katanaTransform = null;

    [SerializeField] private Transform _handBone = null;
    [SerializeField] private Transform _hipBone = null;

    [SerializeField] private CharacterAnimations _characterAnimations = null;

    private void Awake()
    {
        if (_katanaTransform == null)
            throw new NullReferenceException("_katanaTransform has not been assigned at" + GetType());
        if (_handBone == null)
            throw new NullReferenceException("_handBone has not been assigned at" + GetType());
        if (_hipBone == null)
            throw new NullReferenceException("_hipBone has not been assigned at" + GetType());
        if (_characterAnimations == null)
            throw new NullReferenceException("_characterAnimations has not been assigned at" + GetType());

        _characterAnimations.OnUnsheathe += Unsheathe;
    }

    private void Start()
    {
        Sheathe();
    }

    public void Sheathe()
    {
        _katanaTransform.SetParent(_hipBone, false);
    }
    public void Unsheathe()
    {
        _katanaTransform.SetParent(_handBone, false);
    }
}
