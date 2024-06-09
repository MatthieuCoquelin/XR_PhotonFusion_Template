using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class NetworkRig : NetworkBehaviour
{
    [SerializeField] private NetworkTransform m_playerTransform;
    [SerializeField] private NetworkTransform m_headTransform;
    [SerializeField] private NetworkTransform m_leftHandTransform;
    [SerializeField] private NetworkTransform m_rightHandTransform;

    private HardwareRig hardwareRig;

    [SerializeField] private List<MeshRenderer> m_meshList = null;
    [SerializeField] private List<SkinnedMeshRenderer> m_skinnedMeshList = null;

    public override void Spawned()
    {
        base.Spawned();

        if (!Object.HasStateAuthority)
            return;

        hardwareRig = FindObjectOfType<HardwareRig>();
        if (hardwareRig == null)
            Debug.LogError("Missing HardwareRig in the scene");
    }

    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();

        if (GetInput<RigState>(out var input))
        {
            m_playerTransform.transform.SetPositionAndRotation(input.PlayerPosition, input.PlayerRotation);
            m_headTransform.transform.SetPositionAndRotation(input.HeadsetPosition, input.HeadsetRotation);
            m_leftHandTransform.transform.SetPositionAndRotation(input.LeftHandPosition, input.LeftHandRotation);
            m_rightHandTransform.transform.SetPositionAndRotation(input.RightHandPosition, input.RightHandRotation);
        }
    }

    private void LateUpdate()
    {
        if (!Object.HasStateAuthority)
            return;

        for (int i = 0; i < m_meshList.Count; i++)
            m_meshList[i].enabled = false;

        for (int i = 0; i < m_skinnedMeshList.Count; i++)
            m_skinnedMeshList[i].enabled = false;
    }

    public override void Render()
    {
        base.Render();
        if (Object.HasStateAuthority)
        {
            m_playerTransform.transform.SetPositionAndRotation(hardwareRig.playerTransform.position, hardwareRig.playerTransform.rotation);
            m_headTransform.transform.SetPositionAndRotation(hardwareRig.headTransform.position, hardwareRig.headTransform.rotation);
            m_leftHandTransform.transform.SetPositionAndRotation(hardwareRig.leftHandTransform.position, hardwareRig.leftHandTransform.rotation);
            m_rightHandTransform.transform.SetPositionAndRotation(hardwareRig.rightHandTransform.position, hardwareRig.rightHandTransform.rotation);
        }
    }
}