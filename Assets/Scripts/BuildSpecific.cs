using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSpecific : MonoBehaviour {

    public RuntimePlatform[] validRuntimePlatforms;

	protected virtual void Start () {
        bool allowToExist = false;

        if(validRuntimePlatforms.Length > 0)
        {
            foreach (RuntimePlatform rtp in validRuntimePlatforms)
            {
                if (rtp == Application.platform)
                {
                    allowToExist = true;
                }
            }
        }

        if(!allowToExist)
        {
            gameObject.SetActive(false);
        }
	}
}
