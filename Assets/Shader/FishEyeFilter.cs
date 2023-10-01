using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishEyeFilter : MonoBehaviour
{
    [SerializeField]
    private float _pow;
    [SerializeField] private Shader _shader;

    protected Material _mat;

    private bool _useFilter = true;

    private void Awake()
    {
        _mat = new Material(_shader);
        Init();
    }

    protected virtual void Init()
    {
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            _useFilter = !_useFilter;
        }

        OnUpdate();
    }


    private void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        if (_useFilter)
        {
            UseFilter(src, dst);
        }
        else
        {
            Graphics.Blit(src, dst);
        }
    }

    protected virtual void UseFilter(RenderTexture src, RenderTexture dst)
    {
        Graphics.Blit(src, dst, _mat);
    }

    protected void OnUpdate()
    {
        _mat.SetFloat("_BarrelPower", _pow);
    }
}