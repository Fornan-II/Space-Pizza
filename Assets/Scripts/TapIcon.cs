using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapIcon : MonoBehaviour {

    public Color normalColor;
    public Color brakingColor;
    protected Animator _anim;
    protected SpriteRenderer _sr;

    public float tapTimeOut = 0.3f;

    protected float _tapTimeOutTimer = 0.0f;

    private void Start()
    {
        _anim = gameObject.GetComponent<Animator>();
        _sr = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(_tapTimeOutTimer > 0.0f)
        {
            _tapTimeOutTimer -= Time.deltaTime;
            if(_tapTimeOutTimer <= 0.0f && _anim)
            {
                _anim.SetBool("Active", false);
            }
        }
    }

    public void IndicateTapAt(Vector2 position, bool isBraking)
    {
        if(Time.timeScale == 0.0f)
        {
            return;
        }

        transform.position = new Vector3(position.x, position.y, transform.position.z);
        if (_anim)
        {
            _anim.SetBool("Active", true);
        }
        _tapTimeOutTimer = tapTimeOut;
        if (_sr)
        {
            if (isBraking)
            {
                _sr.color = brakingColor;
            }
            else
            {
                _sr.color = normalColor;
            }
        }
    }
}
