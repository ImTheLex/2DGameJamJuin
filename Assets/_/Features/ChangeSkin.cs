using Spine.Unity;
using UnityEngine;

public class ChangeSkin : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [ContextMenu("ChangeSkin")]
    private void ChangeSkinMethods()
    {
        
        _skeletonAnimation.initialSkinName = _skinName;
        _skeletonAnimation.Initialize(true);
    }
    [SerializeField]private SkeletonMecanim _skeletonAnimation;
    [SerializeField] private string _skinName;
}
