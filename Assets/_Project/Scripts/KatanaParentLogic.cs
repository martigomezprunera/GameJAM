using System;
using System.Collections.Generic;
using UnityEngine;
using XftWeapon;

public class KatanaParentLogic : MonoBehaviour
{
    [SerializeField] private MeshRenderer _katanaHand = null;
    [SerializeField] private MeshRenderer _katanaHip = null;

    [SerializeField] private GameObject _trail = null;


    [SerializeField] private CharacterAnimations _characterAnimations = null;

    private void Awake()
    {
        if (_katanaHand == null)
            throw new NullReferenceException("_katanaHand has not been assigned at" + GetType());
        if (_katanaHip == null)
            throw new NullReferenceException("_katanaHip has not been assigned at" + GetType());

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
        _katanaHand.enabled = false;
        _katanaHip.enabled = true;

        //_trail.MyColor = new Color(0, 0, 0, 1);
        _trail.SetActive(false);
    }
    public void Unsheathe()
    {
        _katanaHand.enabled = true;
        _katanaHip.enabled = false;

        //_trail.MyColor = new Color(1, 1, 1, 0.2f);
        _trail.SetActive(true);
    }
}
