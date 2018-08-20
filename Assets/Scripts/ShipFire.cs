using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFire : MonoBehaviour {

    public Vector2 scaleRange = new Vector2(0.9f, 1.1f);
    public float rescalingInterval = 0.3f;

    protected Vector3 _defaultScale;
    protected float timer = 0.0f;
    protected SpriteRenderer _sr;

	// Use this for initialization
	void Start () {
        _defaultScale = transform.localScale;
        _sr = gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if(timer > rescalingInterval)
        {
            float newScaleMultiplier = Random.Range(scaleRange.x, scaleRange.y);
            transform.localScale = newScaleMultiplier * _defaultScale;
            timer = 0.0f;
        }
        timer += Time.deltaTime;
	}

    public void SetFireTo(bool state)
    {
        if(_sr)
        {
            _sr.enabled = state;
        }
    }
}
