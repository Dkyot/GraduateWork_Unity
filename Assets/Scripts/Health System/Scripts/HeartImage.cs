using UnityEngine;
using UnityEngine.UI;

public class HeartImage 
{

    private int fragments;
    private Image heartImage;
    private HeartsHealthVisual heartsHealthVisual;
    private Animation animation;

    public HeartImage(HeartsHealthVisual heartsHealthVisual, Image heartImage, Animation animation) {
        this.heartsHealthVisual = heartsHealthVisual;
        this.heartImage = heartImage;
        this.animation = animation;
    }

    public void SetHeartFraments(int fragments) {
        this.fragments = fragments;
        switch (fragments) {
        case 0: heartImage.sprite = heartsHealthVisual.heart0Sprite; break;
        case 1: heartImage.sprite = heartsHealthVisual.heart1Sprite; break;
        case 2: heartImage.sprite = heartsHealthVisual.heart2Sprite; break;
        case 3: heartImage.sprite = heartsHealthVisual.heart3Sprite; break;
        case 4: heartImage.sprite = heartsHealthVisual.heart4Sprite; break;
        }
    }

    public int GetFragmentAmount() {
        return fragments;
    }

    public void AddHeartVisualFragment() {
        SetHeartFraments(fragments + 1);
    }
    
    public void PlayHeartFullAnimation() {
        animation.Play("HeartFull", PlayMode.StopAll);
    }
}