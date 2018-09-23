using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransitioner : MonoBehaviour {

    protected Animator _anim;

    public enum TransitionState
    {
        OPEN,
        CLOSING,
        CLOSED,
        OPENNING
    }
    protected TransitionState _currentState;
    public TransitionState CurrentState { get { return _currentState; } }

    public bool AnimIsPlaying { get { return (_currentState == TransitionState.OPENNING) || (_currentState == TransitionState.CLOSING); } }
    public bool IsOpenOrOpenning { get { return (_currentState == TransitionState.OPEN) || (_currentState == TransitionState.OPENNING); } }
    public bool IsClosedOrClosing { get { return (_currentState == TransitionState.CLOSED) || (_currentState == TransitionState.CLOSING); } }

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
                _anim.SetBool("open", open);
                if(open)
                {
                    _currentState = TransitionState.OPENNING;
                }
                else
                {
                    _currentState = TransitionState.CLOSING;
                }
            }
        }
    }

    protected virtual void AnimDonePlaying()
    {
        if(_currentState == TransitionState.OPENNING)
        {
            _currentState = TransitionState.OPEN;
        }
        else if(_currentState == TransitionState.CLOSING)
        {
            _currentState = TransitionState.CLOSED;
        }
    }
}
