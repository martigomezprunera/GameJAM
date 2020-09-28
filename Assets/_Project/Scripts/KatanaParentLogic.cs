using System;
using System.Collections.Generic;
using UnityEngine;

public class KatanaParentLogic : MonoBehaviour
{
    [SerializeField] private GameObject _katanaHand = null;
    [SerializeField] private GameObject _katanaHip = null;

    [SerializeField] private CharacterAnimations _characterAnimations = null;

    private void Awake()
    {
        if (_katanaHand == null)
            throw new NullReferenceException("_katanaHand has not been assigned at" + GetType());
        if (_katanaHip == null)
            throw new NullReferenceException("_katanaHip has not been assigned at" + GetType());

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
        _katanaHand.SetActive(false);
        _katanaHip.SetActive(true);
    }
    public void Unsheathe()
    {
        _katanaHand.SetActive(true);
        _katanaHip.SetActive(false);
    }
}
