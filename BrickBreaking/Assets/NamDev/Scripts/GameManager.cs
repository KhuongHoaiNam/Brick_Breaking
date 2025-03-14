using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NamDev.BrickBreak
{
    public class GameManager : MonoBehaviour, ISingleton
    {
        public static GameManager instance;

        public void MakeSingleton()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}