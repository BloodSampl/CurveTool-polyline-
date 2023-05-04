using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public float MaximumHeight;
    public float MinimumHeight;

    Camera m_Camera;
    Camera camera
    {
        get 
        {
            if(m_Camera == null)
            {
                m_Camera = GetComponent<Camera>();
            }
            return m_Camera;
        }
        

    }
}
