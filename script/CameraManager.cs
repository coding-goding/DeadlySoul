using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    public Camera playerCamera;
    public Camera MapCamera;
    int mode = 1;
    [SerializeField] RenderTexture ret;
    [SerializeField] GameObject m;
    // Start is called before the first frame update
    void Start()
    {
        playerCamera.enabled = true;
        MapCamera.enabled = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(mode == 1) {
            ret.Release();
            ret.width = 50;
            ret.height = 50;
            m.GetComponent<RectTransform>().anchoredPosition = new Vector3(-30, -30, 0);
            m.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
            if (m.GetComponent<RawImage>().color.a <= 1) {
                m.GetComponent<RawImage>().color = new Color(m.GetComponent<RawImage>().color.r, m.GetComponent<RawImage>().color.g, m.GetComponent<RawImage>().color.b, m.GetComponent<RawImage>().color.a + 0.02f);
            }
        }
        if(mode == 2) {
            ret.Release();
            ret.width = 100;
            ret.height = 100;
            m.GetComponent<RectTransform>().anchoredPosition = new Vector3(-55, -55, 0);
            m.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
        }
        if(mode == 3) {
            if (m.GetComponent<RawImage>().color.a >= 0)
            {
                m.GetComponent<RawImage>().color = new Color(m.GetComponent<RawImage>().color.r, m.GetComponent<RawImage>().color.g, m.GetComponent<RawImage>().color.b, m.GetComponent<RawImage>().color.a - 0.02f);
            }
        }
        if (Input.GetKeyDown(KeyCode.P)) {
            if(mode != 3) {
                mode++;
            }
            else {
                mode = 1;
            }
        }
    }
}
