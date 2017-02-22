namespace Examples
{
    using System.Collections;
    using UnityEngine;

    public class VRInputManager : MonoBehaviour
    {
        #region SINGLETON

        private static VRInputManager instance = null;

        public static VRInputManager Instance
        {
            get
            {
                if (instance) return instance;
                else
                {
                    Debug.LogError("VRInputManager is uninitialized!");
                    return null;
                }
            }
        }

        #endregion SINGLETON

        #region Init

        private void Awake()
        {
            instance = this;
            Init();
        }

        private void Init()
        {
        }

        #endregion Init
    }
}