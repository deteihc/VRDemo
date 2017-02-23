namespace Examples
{
    using System.Collections;
    using UnityEngine;

    public class VRInputMovement : MonoBehaviour
    {
        private Hand controllerHand = Hand.LEFT;

        private void OnEnable()
        {
            GlobalEvent.register("OnTouchpadPressed", this, "StartMove");
            GlobalEvent.register("OnTouchpadReleased", this, "StopMove");
            GlobalEvent.register("OnTouchpadAxisChanged", this, "ChangeMove");
        }

        private void OnDisable()
        {
            GlobalEvent.deregister(this);
        }

        public void StartMove(VRControllerEventArgs e)
        {
            if (e.hand == controllerHand)
            {
                GlobalEvent.fire("OnStartMove", e.touchpadAxis);
            }
        }

        public void StopMove(VRControllerEventArgs e)
        {
            if (e.hand == controllerHand)
            {
                GlobalEvent.fire("OnStopMove");
            }
        }

        public void ChangeMove(VRControllerEventArgs e)
        {
            if (e.hand == controllerHand)
            {
                GlobalEvent.fire("OnChangeMove", e.touchpadAxis);
            }
        }

        private void Update()
        {
            UpdateSimKey();
        }

        private Vector3 moveAxis = Vector3.zero;

        private void UpdateSimKey()
        {
            Vector3 axis = Vector3.zero;

            if (Input.GetKey(KeyCode.W))
            {
                axis.z = 1;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                axis.z = -1;
            }

            if (Input.GetKey(KeyCode.A))
            {
                axis.x = -1;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                axis.x = 1;
            }

            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)
            || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                if (axis == Vector3.zero)
                {
                    //StopMove
                    StopMove(VRInputDefined.MakeEventArgs(controllerHand, Vector2.zero));
                }
                else if (axis != moveAxis)
                {
                    //ChangeMove
                    ChangeMove(VRInputDefined.MakeEventArgs(controllerHand, new Vector2(axis.x, axis.z)));
                }
            }

            if (moveAxis == Vector3.zero && axis != Vector3.zero)
            {
                //StartMove
                StartMove(VRInputDefined.MakeEventArgs(controllerHand, new Vector2(axis.x, axis.z)));
            }

            moveAxis = axis;
        }
    }
}