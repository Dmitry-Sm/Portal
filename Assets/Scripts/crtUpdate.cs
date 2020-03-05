using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crtUpdate : MonoBehaviour
{
    [SerializeField]
    private CustomRenderTexture crt;
    [SerializeField]
    private int que;

    private int ticker = 0;

    private void LateUpdate() {
            crt.Update();
        if ((++ticker + que) % 8 == 0)
        {
        }
        
    }

    private void OnPreRender() {
        // crt.Update();
    }


    void Update()
    {

        if ((++ticker + que) % 4 == 0)
        {
            // crt.Update();
        }
    }
}
