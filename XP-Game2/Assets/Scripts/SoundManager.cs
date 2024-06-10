using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";

    public static SoundManager Instance;


    [SerializeField] private TutorialUI tutorialUI;
    [SerializeField] private RecipeUI recipeUI;
    [SerializeField] private GamePauseUI gamePauseUI;
    [SerializeField] private OptionsUI optionsUI;
    [SerializeField] private GameOverUI gameOverUI;

    public AudioClipRefsSO audioClipRefsSO;
    public float volume = 1f;

    private void Awake() 
    {
        Instance = this;    
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
    }

    private void Start() 
    {
        tutorialUI.OnButtonClick += TutorialUI_OnButtonClick;
        recipeUI.OnButtonClick += RecipeUI_OnButtonClick;
        gamePauseUI.OnButtonClick += GamePauseUI_OnButtonClick;
        optionsUI.OnButtonClick += OptionsUI_OnButtonClick;
        gameOverUI.OnButtonClick += GameOverUI_OnButtonClick;

        GameInput.Instance.OnSumSkinSelectAction += GameInput_OnSumSkinSelectAction;
        GameInput.Instance.OnSubSkinSelectAction += GameInput_OnSubSkinSelectAction;
        GameInput.Instance.OnSumSkinSelectAction_2 += GameInput_OnSumSkinSelectAction_2;
        GameInput.Instance.OnSubSkinSelectAction_2 += GameInput_OnSubSkinSelectAction_2;

        KitchenGameManager.Instance.OnGetReady += KitchenGameManager_OnGetReady;

        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
        PlayerTwo.Instance.OnPickedSomething += PlayerTwo_OnPickedSomething;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
        
    }

    private void TutorialUI_OnButtonClick(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.buttonClick, Vector3.zero, volume);
    }
    private void RecipeUI_OnButtonClick(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.buttonClick, Vector3.zero, volume);
    }
    private void GamePauseUI_OnButtonClick(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.buttonClick, Vector3.zero, volume);
    }
    private void OptionsUI_OnButtonClick(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.buttonClick, Vector3.zero, volume);
    }
    private void GameOverUI_OnButtonClick(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.buttonClick, Vector3.zero, volume);
    }
    private void KitchenGameManager_OnGetReady(object sender, System.EventArgs e)
    {
        if(KitchenGameManager.Instance.GetIsPlayerReady())
        {
            PlaySound(audioClipRefsSO.checkmark, Vector3.zero, volume);
        }
        else
        {
            PlaySound(audioClipRefsSO.checkmark, Vector3.zero, volume);
        }
        if(KitchenGameManager.Instance.GetIsPlayerTwoReady())
        {
            PlaySound(audioClipRefsSO.checkmark, Vector3.zero, volume);
        }
        else
        {
            PlaySound(audioClipRefsSO.checkmark, Vector3.zero, volume);
        }
    }
    private void GameInput_OnSumSkinSelectAction(object sender, System.EventArgs e)
    {
        if(!KitchenGameManager.Instance.IsSkinSelectionActive()) return;
        PlaySound(audioClipRefsSO.sumSkinSelection, Vector3.zero, volume);
    }
    private void GameInput_OnSubSkinSelectAction(object sender, System.EventArgs e)
    {
        if(!KitchenGameManager.Instance.IsSkinSelectionActive()) return;
        PlaySound(audioClipRefsSO.subSkinSelection, Vector3.zero, volume);
    }
    private void GameInput_OnSumSkinSelectAction_2(object sender, System.EventArgs e)
    {
        if(!KitchenGameManager.Instance.IsSkinSelectionActive()) return;
        PlaySound(audioClipRefsSO.sumSkinSelection, Vector3.zero, volume);
    }
    private void GameInput_OnSubSkinSelectAction_2(object sender, System.EventArgs e)
    {
        if(!KitchenGameManager.Instance.IsSkinSelectionActive()) return;
        PlaySound(audioClipRefsSO.subSkinSelection, Vector3.zero, volume);
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipRefsSO.trash, trashCounter.transform.position);
    }
    
    private void BaseCounter_OnAnyObjectPlacedHere(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipRefsSO.objectDrop, baseCounter.transform.position);
    }

    private void Player_OnPickedSomething(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.objectPickup, Player.Instance.transform.position);
    }
    private void PlayerTwo_OnPickedSomething(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.objectPickup, PlayerTwo.Instance.transform.position);
    }
    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipRefsSO.chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliverySuccess, deliveryCounter.transform.position);
    }
    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliveryFail, deliveryCounter.transform.position);
    }


    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    public void PlayFootstepSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipRefsSO.footstep, position, volumeMultiplier * volume);
    }
    public void PlayDashSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipRefsSO.dash, position, volumeMultiplier * volume);
    }

    public void PlayCountdownSound()
    {
        PlaySound(audioClipRefsSO.warning, Vector3.zero);
    }
    public void PlayMickeySound()
    {
        PlaySound(audioClipRefsSO.mickeyMiska, Vector3.zero);
    }

    public void PlayWarningSound(Vector3 position)
    {
        PlaySound(audioClipRefsSO.warning, position);
    }

    public void ChangeVolume()
    {
        volume += .1f;
        if(volume > 1f)
        {
            volume = 0f;
        }

        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return volume;
    }
}

