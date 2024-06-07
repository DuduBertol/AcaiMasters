using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI recipeNametext;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;
    [SerializeField] private Transform recipeSprite;

    

    private void Awake() 
    {
        iconTemplate.gameObject.SetActive(false);
    }

    public void SetRecipeSO(RecipeSO recipeSO)
    {
        recipeNametext.text = recipeSO.recipeName;

        recipeSprite.gameObject.GetComponent<Image>().sprite = recipeSO.recipeImage;

        foreach (Transform child in iconContainer)
        {
            if(child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOList)
        {
            Transform iconTransform = Instantiate(iconTemplate, iconContainer);
            iconTransform.gameObject.SetActive(true);
            // iconTransform.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
            iconTransform.GetChild(2).GetComponent<Image>().sprite = kitchenObjectSO.sprite;

            /* if(kitchenObjectSO.hasRequirement)
            {
                iconTransform.GetChild(0).gameObject.SetActive(true); //Requirements
                iconTransform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = kitchenObjectSO.requirementSprite; //RequirementSprite
            } */
        }
    }
    
}

