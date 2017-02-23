namespace Examples
{
    using System.Collections;
    using UnityEngine;
    using VRTK;

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
            InitController();
        }

        private void InitController()
        {
            GameObject hand = VRTK_SDKManager.instance.actualBoundaries.transform.FindChild("Hand").gameObject;
            if (!VRInputDefined.isHMDConnected)
            {
                Transform tmpHead = VRTK_SDKManager.instance.actualHeadset.transform.parent;
                VRTK_SDKManager.instance.actualHeadset.transform.SetParent(VRTK_SDKManager.instance.actualBoundaries.transform, false);
                tmpHead.SetParent(VRTK_SDKManager.instance.actualHeadset.transform, false);
                tmpHead.GetChild(0).SetParent(VRTK_SDKManager.instance.actualHeadset.transform, false);
                tmpHead.gameObject.SetActive(false);
                GameObject leftHand = Instantiate(hand, VRTK_SDKManager.instance.actualLeftController.transform, false) as GameObject;
                leftHand.name = "Hand";
                GameObject rightHand = Instantiate(hand, VRTK_SDKManager.instance.actualRightController.transform, false) as GameObject;
                rightHand.name = "Hand";

                VRTK_SDKManager.instance.actualLeftController.AddComponent<SDK_ControllerSim>();
                VRTK_SDKManager.instance.actualRightController.AddComponent<SDK_ControllerSim>();
                VRTK_SDKManager.instance.actualBoundaries.AddComponent<Examples.SDK_InputSimulator>();
            }
            DestroyImmediate(hand);
        }

        #endregion Init


    }
}