using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartsHealthVisual : MonoBehaviour 
{
    [SerializeField] private Sprite _heart0Sprite;
    [SerializeField] private Sprite _heart1Sprite;
    [SerializeField] private Sprite _heart2Sprite;
    [SerializeField] private Sprite _heart3Sprite;
    [SerializeField] private Sprite _heart4Sprite;
    public Sprite heart0Sprite =>   _heart0Sprite;
    public Sprite heart1Sprite =>   _heart1Sprite;
    public Sprite heart2Sprite =>   _heart2Sprite;
    public Sprite heart3Sprite =>   _heart3Sprite;
    public Sprite heart4Sprite =>   _heart4Sprite;

    [SerializeField]
    private CharacterStats characterStats;
    private HeartsHealthSystem heartsHealthSystem;

    private List<HeartImage> heartImageList;

    public AnimationClip heartFullAnimationClip;
    //private bool isHealing;

    private void Awake() {
        heartImageList = new List<HeartImage>();
    }

    private void Start() {
        //FunctionPeriodic.Create(HealingAnimatedPeriodic, .05f);
        SetHeartsHealthSystem(characterStats.GetHealthSystem());
    }

    public void SetHeartsHealthSystem(HeartsHealthSystem heartsHealthSystem) {
        this.heartsHealthSystem = heartsHealthSystem;

        List<Heart> heartList = heartsHealthSystem.GetHeartList();
        int row = 0;
        int col = 0;
        int colMax = 10;
        float rowColSize = 30f;

        for (int i = 0; i < heartList.Count; i++) {
            Heart heart = heartList[i];
            Vector2 heartAnchoredPosition = new Vector2(col * rowColSize, -row * rowColSize);
            CreateHeartImage(heartAnchoredPosition).SetHeartFraments(heart.GetFragmentAmount());

            col++;
            if (col >= colMax) {
                row++;
                col = 0;
            }
        }

        heartsHealthSystem.OnDamaged += HeartsHealthSystem_OnDamaged;
        heartsHealthSystem.OnHealed += HeartsHealthSystem_OnHealed;
        heartsHealthSystem.OnDead += HeartsHealthSystem_OnDead;
    }

    private void HeartsHealthSystem_OnDead(object sender, System.EventArgs e) {
        Debug.Log("Player is Dead!");
    }

    private void HeartsHealthSystem_OnHealed(object sender, System.EventArgs e) {
        RefreshAllHearts();
        //Debug.Log("+: " + heartsHealthSystem.GetCurrentHP());
        //isHealing = true;
    }

    private void HeartsHealthSystem_OnDamaged(object sender, System.EventArgs e) {
        RefreshAllHearts();
        //Debug.Log("-: " + heartsHealthSystem.GetCurrentHP());
    }

    private void RefreshAllHearts() {
        List<Heart> heartList = heartsHealthSystem.GetHeartList();
        for (int i = 0; i < heartImageList.Count; i++) {
            heartImageList[i].SetHeartFraments(heartList[i].GetFragmentAmount());
        }
    }

    // private void HealingAnimatedPeriodic() {
    //     if (isHealing) {
    //         bool fullyHealed = true;
    //         List<Heart> heartList = heartsHealthSystem.GetHeartList();
    //         for (int i = 0; i < heartList.Count; i++) {
    //             HeartImage heartImage = heartImageList[i];
    //             Heart heart = heartList[i];
    //             if (heartImage.GetFragmentAmount() != heart.GetFragmentAmount()) {
    //                 // Visual is different from logic
    //                 heartImage.AddHeartVisualFragment();
    //                 if (heartImage.GetFragmentAmount() == HeartsHealthSystem.MAX_FRAGMENT_AMOUNT) {
    //                     // This heart was fully healed
    //                     heartImage.PlayHeartFullAnimation();
    //                 }
    //                 fullyHealed = false;
    //                 break;
    //             }
    //         }
    //         if (fullyHealed) {
    //             isHealing = false;
    //         }
    //     }
    // }

    private HeartImage CreateHeartImage(Vector2 anchoredPosition) {
        // Create Game Object
        GameObject heartGameObject = new GameObject("Heart", typeof(Image), typeof(Animation));

        // Set as child of this transform
        //heartGameObject.transform.parent = transform;
        heartGameObject.transform.SetParent(transform, false);
        heartGameObject.transform.localPosition = Vector3.zero;
        heartGameObject.transform.localScale = Vector3.one;

        // Locate and Size heart
        heartGameObject.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
        heartGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(35, 35);

        heartGameObject.GetComponent<Animation>().AddClip(heartFullAnimationClip, "HeartFull");

        // Set heart sprite
        Image heartImageUI = heartGameObject.GetComponent<Image>();
        heartImageUI.sprite = heart4Sprite;

        HeartImage heartImage = new HeartImage(this, heartImageUI, heartGameObject.GetComponent<Animation>());
        heartImageList.Add(heartImage);

        return heartImage;
    }
}