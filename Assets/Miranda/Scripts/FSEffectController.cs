using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FSEffectController : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private ScriptableRendererFeature _FSFreeze;
    [SerializeField] private Material _material;

    [Header("Stats")]
    [SerializeField] private float _vignettePowerStat;
    [SerializeField] private float _vignetteIntensityStat;
    [SerializeField] private float _voronoiPowerStat;
    [SerializeField] private float _voronoiIntensityStat;

    public Calor script;

    private int _vignetteIntensity = Shader.PropertyToID("_viginetteIntensity");
    private int _vignettePower = Shader.PropertyToID("_ViginettePower");
    private int _voronoiIntensity = Shader.PropertyToID("_VoronoiIntensity");
    private int _voronoiPower = Shader.PropertyToID("_VoronoiPower");

    private void Update()
    {
            
            _voronoiIntensityStat = 2f - (script.calor / 50);
            _material.SetFloat(_voronoiIntensity, _voronoiIntensityStat);
            _FSFreeze.SetActive(true);
            
    }
}
