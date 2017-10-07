using System;
using System.Collections;
using System.IO;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Diagnostics;

    public class Screenshot : MonoBehaviour
    {
        private int count = 0;
        public static int width=1920;
        public static int height=1920;
        private RenderTexture renderTexture;
        private float targetFrameRate=1.0f;
        bool IsRecording;
        Texture2D m_texScreenshot;
        Rect rect;

        void Start()
        {
            renderTexture = RenderTexture.GetTemporary(width, height, 24, RenderTextureFormat.ARGB32);
            GetComponent<Camera>().targetTexture = renderTexture;
            rect = new Rect(0, 0, width, height);
            m_texScreenshot = new Texture2D(width, height, TextureFormat.RGBA32, false);
           
        }
    void startrecord() {
        UnityEngine.Debug.Log("开始录制");
        StartCoroutine(Recording());
    }
    void stoprecord() {
        UnityEngine.Debug.Log("结束录制");
        count = 0;
        StopAllCoroutines();
    }
        IEnumerator Recording()
        {
            IsRecording = true;
            while (IsRecording)
            {
                yield return new WaitForEndOfFrame();
                count++;
                RenderTexture.active = renderTexture;
                m_texScreenshot.ReadPixels(rect, 0, 0);
               byte[] bytes= m_texScreenshot.EncodeToJPG();
               File.WriteAllBytes(Application.dataPath + "/savepic/"+count+".jpg", bytes);
               UnityEngine.Debug.Log(Application.dataPath);
               float time = 1.0f / targetFrameRate;
                yield return new WaitForSeconds(time);
            }
            UnityEngine.Debug.Log("Complete");

        }
    }
