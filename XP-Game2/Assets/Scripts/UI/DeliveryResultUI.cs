using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour
{
    private const string POPUP = "Popup";

    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Color successColor;
    [SerializeField] private Color failedColor;
    [SerializeField] private Sprite successSprite;
    [SerializeField] private Sprite failedSprite;
    
    [Header("Mickey Particles")]
    [SerializeField] private ParticleSystem mickeyFaceParticleSystem;
    [SerializeField] private ParticleSystem mickeyHandParticleSystem; 
    [SerializeField] private Material mickeySuccessFaceMaterial;
    [SerializeField] private Material mickeyFailedFaceMaterial;
    [SerializeField] private Material mickeySuccessHandMaterial;
    [SerializeField] private Material mickeyFailedHandMaterial;

    
    private Animator animator;

    private void Awake() 
    {
        animator = GetComponent<Animator>();    
    }
    
    private void Start() 
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;

        gameObject.SetActive(false); 
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true); 
        /* animator.SetTrigger(POPUP);
        backgroundImage.color = failedColor;
        iconImage.sprite = failedSprite;
        messageText.text = "DELIVERY\nFAILED"; */
        
        ParticleSystemRenderer mickeyFaceParticleSystemRenderer = mickeyFaceParticleSystem.ConvertTo<ParticleSystemRenderer>();
        ParticleSystemRenderer mickeyHandParticleSystemRenderer = mickeyHandParticleSystem.ConvertTo<ParticleSystemRenderer>();
        
        mickeyFaceParticleSystemRenderer.material = mickeyFailedFaceMaterial;
        mickeyHandParticleSystemRenderer.material = mickeyFailedHandMaterial;

        mickeyFaceParticleSystem.Play();
        mickeyHandParticleSystem.Play();
    }
    
    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        /* animator.SetTrigger(POPUP);
        backgroundImage.color = successColor;
        iconImage.sprite = successSprite;
        messageText.text = "DELIVERY\nSUCCESS"; */

        ParticleSystemRenderer mickeyFaceParticleSystemRenderer = mickeyFaceParticleSystem.ConvertTo<ParticleSystemRenderer>();
        ParticleSystemRenderer mickeyHandParticleSystemRenderer = mickeyHandParticleSystem.ConvertTo<ParticleSystemRenderer>();
        
        mickeyFaceParticleSystemRenderer.material = mickeySuccessFaceMaterial;
        mickeyHandParticleSystemRenderer.material = mickeySuccessHandMaterial;

        mickeyFaceParticleSystem.Play();
        mickeyHandParticleSystem.Play();
    }
    
    
}
