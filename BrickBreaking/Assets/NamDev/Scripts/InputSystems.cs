using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NamDev.BrickBreak
{
    public class InputSystems : MonoBehaviour, ISingleton
    {
        public static InputSystems instance;
        public bool isMoblie;


        public void InputOnPC()
        {

        }



        private void Awake()
        {
            MakeSingleton();
        }


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