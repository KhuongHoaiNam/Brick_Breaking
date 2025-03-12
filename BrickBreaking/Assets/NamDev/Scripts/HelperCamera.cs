using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NamDev.BrickBreak
{
    public static class HelperCamera 
    {
     
        public static Vector2 Get2dCamSize( Camera camera)
        {
            if(camera != null && camera.orthographic)
            {
                float height = camera.orthographicSize * 2;
                float width = height * camera.aspect;
                return new Vector2 (width, height);
            }
            else
            {
                return Vector2.zero;
            }
        }
    }
}