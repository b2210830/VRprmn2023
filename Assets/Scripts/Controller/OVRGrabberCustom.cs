using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows grabbing and throwing of objects with the OVRGrabbable component on them.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class OVRGrabberCustom : OVRGrabber
{
    private bool _counting = false;
    private float _sec = 1f;
    private float _currentSec = 0f;
    private bool _showing = false;

    public override void Update()
    {
        if(!_counting) return;

        if(_currentSec >= _sec)
        {
            if(_showing) return;

            UserGuide.Instance.Show("物をつかむ", OVRInput.Button.SecondaryHandTrigger);
            _showing = true;
        }
        _currentSec += Time.deltaTime;
    }

	protected override void OnTriggerEnter(Collider otherCollider)
	{
        // Get the grab trigger
		OVRGrabbable grabbable = otherCollider.GetComponent<OVRGrabbable>() ?? otherCollider.GetComponentInParent<OVRGrabbable>();
        if (grabbable == null) return;

        // 対象物のシェーダーを輪郭を描画するシェーダーに切り替える
        if(grabbable.AllowSwitchShader && otherCollider.gameObject.GetComponent<Renderer>())
        {
            var shader = otherCollider.gameObject.GetComponent<Renderer>().material.shader;
            var shader_ = Resources.Load<Shader>("Shader/Outline");
            if(shader != shader_)
            {
                otherCollider.gameObject.GetComponent<Renderer>().material.shader = shader_;
            }
        }

        // ガイド表示
        _counting = true;

        // Add the grabbable
        int refCount = 0;
        m_grabCandidates.TryGetValue(grabbable, out refCount);
        m_grabCandidates[grabbable] = refCount + 1;
	}

	protected override void OnTriggerExit(Collider otherCollider)
	{
		OVRGrabbable grabbable = otherCollider.GetComponent<OVRGrabbable>() ?? otherCollider.GetComponentInParent<OVRGrabbable>();
        if (grabbable == null) return;

        // 対象物のシェーダーを元に戻す
        if(grabbable.AllowSwitchShader && otherCollider.gameObject.GetComponent<Renderer>())
        {
            var shader = otherCollider.gameObject.GetComponent<Renderer>().material.shader;
            var defaultShader = grabbable._DefaultShader;
            if(shader != defaultShader)
            {
                otherCollider.gameObject.GetComponent<Renderer>().material.shader = defaultShader;
            }
        }

        // ガイド表示を破棄
        _counting = false;
        _currentSec = 0f;
        _showing = false;
        UserGuide.Instance.Hide();

        // Remove the grabbable
        int refCount = 0;
        bool found = m_grabCandidates.TryGetValue(grabbable, out refCount);
        if (!found)
        {
            return;
        }

        if (refCount > 1)
        {
            m_grabCandidates[grabbable] = refCount - 1;
        }
        else
        {
            m_grabCandidates.Remove(grabbable);
        }
	}
}