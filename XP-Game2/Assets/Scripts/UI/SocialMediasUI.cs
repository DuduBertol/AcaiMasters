using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SocialMediasUI : MonoBehaviour
{
    [SerializeField] Button itchIoButton;
    [SerializeField] Button linkedinButton;
    [SerializeField] Button instagramButton;
    [SerializeField] Button twitterButton;
    
    private void Awake() 
    {
        itchIoButton.onClick.AddListener(() => {
            Application.OpenURL("https://dudubertoldev.itch.io");
        });    
        linkedinButton.onClick.AddListener(() => {
            Application.OpenURL("https://www.linkedin.com/in/eduardo-bertol/");
        });    
        instagramButton.onClick.AddListener(() => {
            Application.OpenURL("https://www.instagram.com/dudubertoldev/");
        });    
        twitterButton.onClick.AddListener(() => {
            Application.OpenURL("https://twitter.com/dudubertoldev");
        });   
    }
}
