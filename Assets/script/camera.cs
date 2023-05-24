using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class camera : MonoBehaviour

{
    public Cinemachine.CinemachineVirtualCamera hello;
    CinemachineTransposer Offset;
    public int variableZoom=1;
    public float variableZoomTranslate=1;
    public float variableZoomSmooth= 0.05f;
    public bool zoom=true;
    // Start is called before the first frame update
    void Start()
    {
        Offset = hello.GetCinemachineComponent<CinemachineTransposer>();

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(Zoom());
            
            /*hello.GetComponent<CinemachineCameraOffset>().m_Offset = new Vector3(-0.566f, 10000, 10);*/
            


        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Zoom()
    {
        if (zoom)
        {
            while (hello.m_Lens.OrthographicSize <= variableZoom && Offset.m_FollowOffset.y <= variableZoomTranslate)
            {
                float coefficient = variableZoom / variableZoomTranslate;
                hello.m_Lens.OrthographicSize += 0.05f;
                hello.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(Offset.m_FollowOffset.x, Offset.m_FollowOffset.y + variableZoomSmooth / coefficient, Offset.m_FollowOffset.z);
                yield return new WaitForSeconds(0.01f);
            }
        }
        else
        {
            while (hello.m_Lens.OrthographicSize >= variableZoom && Offset.m_FollowOffset.y >= variableZoomTranslate)
            {
                float coefficient = variableZoom / variableZoomTranslate;
                hello.m_Lens.OrthographicSize -= 0.01f;
                hello.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(Offset.m_FollowOffset.x, Offset.m_FollowOffset.y - variableZoomSmooth / coefficient, Offset.m_FollowOffset.z);
                yield return new WaitForSeconds(0.005f);
            }


        }
    }
}
