﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;

    
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();    
    }

    public void Move(float move)
    {
        _anim.SetFloat("Move", move);
    }
}
