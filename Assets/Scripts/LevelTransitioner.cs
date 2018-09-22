using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransitioner : MonoBehaviour {

    protected Animator _anim;

    protected bool _animIsPlaying = false;
    public bool AnimIsPlaying { get { return _animIsPlaying; } }

    protected virtual void Start()
    {
        _anim = gameObject.GetComponent<Animator>();
    }

    public virtual void TransitionScreen(bool open)
    {
        if(_anim)
        {
            if(_anim.GetBool("open") != open)
            {
                _animIsPlaying = true;
                _anim.SetBool("open", open);
            }
        }
    }

    protected virtual void AnimDonePlaying()
    {
        _animIsPlaying = false;
    }
}
