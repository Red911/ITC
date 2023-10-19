using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SwitchPostProcValues : MonoBehaviour
{
    
    [SerializeField] private Material _postProcessMat;
    private int _colorMultiplier = Shader.PropertyToID("_MultiplyPower");
    private int _distortionMultiplier = Shader.PropertyToID("_MultiplyPower");
    private int _gradientTexture = Shader.PropertyToID("_Gradient");
    private int _maskTexture = Shader.PropertyToID("_Mask");
    
    [Header("Monster 1")] 
    [SerializeField] private float _monster1Intensity;
    [SerializeField] private float _monster1ColorMultiplier;
    [SerializeField] private Texture2D _monster1GradientTexture;
    [SerializeField] private Texture2D _monster1MaskTexture;
    [Space(20)]
    [Header("Monster 2")] 
    [SerializeField] private float _monster2Intensity;
    [SerializeField] private float _monster2ColorMultiplier;
    [SerializeField] private Texture2D _monster2GradientTexture;
    [SerializeField] private Texture2D _monster2MaskTexture;
    [Space(20)]
    [Header("Monster 3")] 
    [SerializeField] private float _monster3Intensity;
    [SerializeField] private float _monster3ColorMultiplier;
    [SerializeField] private Texture2D _monster3GradientTexture;
    [SerializeField] private Texture2D _monster3MaskTexture;
    [Space(20)]
    [Header("Monster 4")] 
    [SerializeField] private float _monster4Intensity;
    [SerializeField] private float _monster4ColorMultiplier;
    [SerializeField] private Texture2D _monster4GradientTexture;
    [SerializeField] private Texture2D _monster4MaskTexture;
    [Space(20)]
    [Header("Monster 5")] 
    [SerializeField] private float _monster5Intensity;
    [SerializeField] private float _monster5ColorMultiplier;
    [SerializeField] private Texture2D _monster5GradientTexture;
    [SerializeField] private Texture2D _monster5MaskTexture;


    public void SetPostProcess(int monsterNumber)
    {
        switch (monsterNumber)
        {
            case 0:
            {
                _postProcessMat.SetFloat(_colorMultiplier, _monster1Intensity);
                _postProcessMat.SetFloat(_distortionMultiplier, _monster1ColorMultiplier);
                _postProcessMat.SetTexture(_gradientTexture, _monster1GradientTexture);
                _postProcessMat.SetTexture(_maskTexture, _monster1MaskTexture);
                break;
            }
            
            case 1:
            {
                _postProcessMat.SetFloat(_colorMultiplier, _monster2Intensity);
                _postProcessMat.SetFloat(_distortionMultiplier, _monster2ColorMultiplier);
                _postProcessMat.SetTexture(_gradientTexture, _monster2GradientTexture);
                _postProcessMat.SetTexture(_maskTexture, _monster2MaskTexture);
                break;
            }
            case 2:
            {
                _postProcessMat.SetFloat(_colorMultiplier, _monster3Intensity);
                _postProcessMat.SetFloat(_distortionMultiplier, _monster3ColorMultiplier);
                _postProcessMat.SetTexture(_gradientTexture, _monster3GradientTexture);
                _postProcessMat.SetTexture(_maskTexture, _monster3MaskTexture);
                break;
            }
            case 3:
            {
                _postProcessMat.SetFloat(_colorMultiplier, _monster4Intensity);
                _postProcessMat.SetFloat(_distortionMultiplier, _monster4ColorMultiplier);
                _postProcessMat.SetTexture(_gradientTexture, _monster4GradientTexture);
                _postProcessMat.SetTexture(_maskTexture, _monster4MaskTexture);
                break;
            }
            case 4:
            {
                _postProcessMat.SetFloat(_colorMultiplier, _monster5Intensity);
                _postProcessMat.SetFloat(_distortionMultiplier, _monster5ColorMultiplier);
                _postProcessMat.SetTexture(_gradientTexture, _monster5GradientTexture);
                _postProcessMat.SetTexture(_maskTexture, _monster5MaskTexture);
                break;
            }
        }
    }
}
