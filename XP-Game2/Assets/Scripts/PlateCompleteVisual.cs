using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }

    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSOGameObjectList;
    [SerializeField] private GameObject gambiarraAcaiFruitGameObject;

    private void Start() 
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
        plateKitchenObject.OnIngredientRemoved += PlateKitchenObject_OnIngredientRemoved;

        foreach (KitchenObjectSO_GameObject kitchenObjectSOGameObject in kitchenObjectSOGameObjectList)
        {
            kitchenObjectSOGameObject.gameObject.SetActive(false);
        }
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        
        foreach (KitchenObjectSO_GameObject kitchenObjectSOGameObject in kitchenObjectSOGameObjectList)
        {
            if(kitchenObjectSOGameObject.kitchenObjectSO == e.kitchenObjectSO)
            {
                kitchenObjectSOGameObject.gameObject.SetActive(true);

                AudioClip[] audioClipArray = SoundManager.Instance.audioClipRefsSO.objectDrop;
                AudioSource.PlayClipAtPoint(audioClipArray[UnityEngine.Random.Range(0, audioClipArray.Length)], SoundManager.Instance.transform.position, SoundManager.Instance.volume);
            }
        }
    }

    public void AddPlateToAcai()
    {
        gambiarraAcaiFruitGameObject.SetActive(true);
    }

    private void PlateKitchenObject_OnIngredientRemoved(object sender, PlateKitchenObject.OnIngredientRemovedEventArgs e)
    {
        gambiarraAcaiFruitGameObject.SetActive(false);
        
        foreach (KitchenObjectSO_GameObject kitchenObjectSOGameObject in kitchenObjectSOGameObjectList)
        {
            if(kitchenObjectSOGameObject.kitchenObjectSO == e.kitchenObjectSO)
            {
                kitchenObjectSOGameObject.gameObject.SetActive(false);

                AudioClip[] audioClipArray = SoundManager.Instance.audioClipRefsSO.chop;
                AudioSource.PlayClipAtPoint(audioClipArray[UnityEngine.Random.Range(0, audioClipArray.Length)], SoundManager.Instance.transform.position, SoundManager.Instance.volume);
            }
        }
    }
}
