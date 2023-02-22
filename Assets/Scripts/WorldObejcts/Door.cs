
using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool _doorOpened = false;
    public Animator _animator;
    private BoxCollider2D _boxCollider2D;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    public void OpenDoor()
    {
        if (_doorOpened == false)
        {
            _animator.enabled = true;
            _doorOpened = true;
            _boxCollider2D.enabled = false;
        }
    }
}
