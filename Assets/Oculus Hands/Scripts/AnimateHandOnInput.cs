using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class AnimateHandOnInput : MonoBehaviour
{
    [SerializeField] private NetworkObject m_networkObject = null;
    [SerializeField] private Animator m_handAnimator = null;

    // Update is called once per frame
    void Update()
    {
        if (!m_networkObject.HasStateAuthority)
            return;

        if(tag == "LeftHand")
        {
            float triggerValue = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.LTouch);
            m_handAnimator.SetFloat("Trigger", triggerValue);

            float gripValue = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch);
            m_handAnimator.SetFloat("Grip", gripValue);
        }
        else if (tag == "RightHand")
        {
            float triggerValue = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch);
            m_handAnimator.SetFloat("Trigger", triggerValue);

            float gripValue = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch);
            m_handAnimator.SetFloat("Grip", gripValue);
        }

    }
}