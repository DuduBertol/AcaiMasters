using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepByStepTutorialUI : MonoBehaviour
{
    [SerializeField] Transform AcaiContainerPos;
    [SerializeField] Transform BowlPos;
    [SerializeField] Transform FrezerPos;
    [SerializeField] GameObject AcaiFruitUI;
    [SerializeField] GameObject AcaiFruitBowlUI;
    [SerializeField] GameObject AcaiFruitBowlFreezeUI;
    [SerializeField] PlatesCounter platesCounter;
    [SerializeField] StoveCounter stoveCounterOne;
    [SerializeField] StoveCounter stoveCounterTwo;
    [SerializeField] StoveCounter stoveCounterThree;

    private void Start() 
    {
        transform.position = AcaiContainerPos.position;
        AcaiFruitUI.SetActive(true);
        AcaiFruitBowlUI.SetActive(false);
        AcaiFruitBowlFreezeUI.SetActive(false);

        Player.Instance.OnFirstInteractAcaiFruit += Player_OnFirstInteractAcaiFruit;
        PlayerTwo.Instance.OnFirstInteractAcaiFruit += PlayerTwo_OnFirstInteractAcaiFruit;

        platesCounter.OnFirstPlateRemoved += PlatesCounter_OnFirstPlateRemoved;
        
        stoveCounterOne.OnFirstInteractFreezer += StoveCounterOne_OnFirstInteractFreezer;
        stoveCounterTwo.OnFirstInteractFreezer += StoveCounterTwo_OnFirstInteractFreezer;
        stoveCounterThree.OnFirstInteractFreezer += StoveCounterThree_OnFirstInteractFreezer;
        
    }

    private void StoveCounterOne_OnFirstInteractFreezer(object sender, System.EventArgs e)
    {
        AcaiFruitBowlFreezeUI.SetActive(false);
    }
    private void StoveCounterTwo_OnFirstInteractFreezer(object sender, System.EventArgs e)
    {
        AcaiFruitBowlFreezeUI.SetActive(false);
    }
    private void StoveCounterThree_OnFirstInteractFreezer(object sender, System.EventArgs e)
    {
        AcaiFruitBowlFreezeUI.SetActive(false);
    }
    private void PlatesCounter_OnFirstPlateRemoved(object sender, System.EventArgs e)
    {
        LeanTween.moveX(gameObject, FrezerPos.position.x, 1f).setEaseInOutExpo();
        LeanTween.moveZ(gameObject, FrezerPos.position.z, 1f).setEaseInOutExpo();
        
        AcaiFruitBowlUI.SetActive(false);
        AcaiFruitBowlFreezeUI.SetActive(true);
    }

    private void Player_OnFirstInteractAcaiFruit(object sender, System.EventArgs e)
    {
        LeanTween.moveX(gameObject, BowlPos.position.x, 1f).setEaseInOutExpo();
        LeanTween.moveZ(gameObject, BowlPos.position.z, 1f).setEaseInOutExpo();
        
        AcaiFruitUI.SetActive(false);
        AcaiFruitBowlUI.SetActive(true);
    }
    private void PlayerTwo_OnFirstInteractAcaiFruit(object sender, System.EventArgs e)
    {
        LeanTween.moveX(gameObject, BowlPos.position.x, 1f).setEaseInOutExpo();
        LeanTween.moveZ(gameObject, BowlPos.position.z, 1f).setEaseInOutExpo();
        
        AcaiFruitUI.SetActive(false);
        AcaiFruitBowlUI.SetActive(true);
    }
}
