using Interface;
using Tools;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehaviour : MonoBehaviour
{
    public IHasHealth m_target; // Référence au script PlayerBehaviour ou PhantomBehaviour
    private Image _componentImage;
    [SerializeField]
    //private GameObject _healthTarget;
    private MonoBehaviour _healthTarget;


    private void Awake()
    {
        if (_healthTarget != null)
        {
            m_target = _healthTarget as IHasHealth;
        }
        if (_healthTarget == null)
        {
            var Interface = GetComponentInParent<IHasHealth>();
            _healthTarget = Interface.GetGameObjectWithHealth();
            m_target = _healthTarget.GetComponentInChildren<IHasHealth>();
        }
        
        _componentImage = GetComponent<Image>();

        
    }

    private void Update()
    {
        if (_healthTarget == null) return;

        float normalizedHealth = Mathf.Clamp01(m_target.CurrentHealth / m_target.MaxHealth);
        _componentImage.fillAmount = normalizedHealth;
    }
}