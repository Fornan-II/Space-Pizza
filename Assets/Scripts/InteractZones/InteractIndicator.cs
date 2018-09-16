using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractIndicator : MonoBehaviour {

    public Color ableToInteractColor = Color.white;
    public Color isInteractingColor = Color.green;
    public float timeToOpen = 0.3f;
    public float ableToInteractPopSize;
    public float timeToPop = 0.1f;

    protected SpriteRenderer _sr;
    protected bool _isOpen = false;
    protected bool _isInteracting = false;
    protected bool _canInteract = false;

    // Use this for initialization
    void Start ()
    {
        _sr = gameObject.GetComponent<SpriteRenderer>();
        _sr.color = ableToInteractColor;
        gameObject.transform.localScale = Vector3.zero;
	}

    public virtual void InInteractZone(bool value)
    {
        _canInteract = value;

        if(_canInteract && !_isOpen)
        {
            StopCoroutine(CloseIndicator());
            StartCoroutine(OpenIndicator());
        }
        else if(!_canInteract && _isOpen)
        {
            IsInteracting(false);
            _isInteracting = false;
            StopCoroutine(OpenIndicator());
            StartCoroutine(CloseIndicator());
        }
    }

    public virtual void IsInteracting(bool value)
    {
        if (value && !_isInteracting)
        {
            _isInteracting = true;
            StartCoroutine(PopFromInteracting());
            _sr.color = isInteractingColor;
        }
        else if(!value && _isInteracting)
        {
            _isInteracting = false;
            StopCoroutine(PopFromInteracting());
            _sr.color = ableToInteractColor;
        }
    }

    protected virtual IEnumerator OpenIndicator()
    {
        _isOpen = true;
        float timeElapsed = 0.0f;
        float newScale;

        while(timeElapsed < timeToOpen)
        {
            yield return null;
            timeElapsed += Time.deltaTime;
            newScale = Mathf.Lerp(0.0f, 1.0f, timeElapsed / timeToOpen);
            gameObject.transform.localScale = new Vector3(newScale, newScale, newScale);
        }
    }

    protected virtual IEnumerator CloseIndicator()
    {
        _isOpen = false;
        float timeElapsed = 0.0f;
        float newScale;

        while (timeElapsed < timeToOpen)
        {
            yield return null;
            timeElapsed += Time.deltaTime;
            newScale = Mathf.Lerp(1.0f, 0.0f, timeElapsed / timeToOpen);
            gameObject.transform.localScale = new Vector3(newScale, newScale, newScale);
        }
    }

    protected virtual IEnumerator PopFromInteracting()
    {
        StopCoroutine(OpenIndicator());
        gameObject.transform.localScale = Vector3.one;

        float timeElapsed = 0.0f;
        float newScale;
        float halfPop = timeToPop;

        while(timeElapsed < halfPop)
        {
            yield return null;
            timeElapsed += Time.deltaTime;
            newScale = Mathf.Lerp(1.0f, ableToInteractPopSize, timeElapsed / halfPop);
            gameObject.transform.localScale = new Vector3(newScale, newScale, newScale);
        }

        timeElapsed = 0.0f;

        while (timeElapsed < halfPop)
        {
            yield return null;
            timeElapsed += Time.deltaTime;
            newScale = Mathf.Lerp(ableToInteractPopSize, 1.0f, timeElapsed / halfPop);
            gameObject.transform.localScale = new Vector3(newScale, newScale, newScale);
        }
    }
}
