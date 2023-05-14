using System;
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

    private void Awake() {
        heartImageList = new List<HeartImage>();
    }

    private void Start() {
        SetHeartsHealthSystem(characterStats.GetHealthSystem());
    }

    public void SetHeartsHealthSystem(HeartsHealthSystem heartsHealthSystem) {
        SetHeartList(heartsHealthSystem);

        heartsHealthSystem.OnDamaged += HeartsHealthSystem_OnDamaged;
        heartsHealthSystem.OnHealed += HeartsHealthSystem_OnHealed;
        heartsHealthSystem.OnDead += HeartsHealthSystem_OnDead;
        heartsHealthSystem.OnSet += HeartsHealthSystem_OnSet;
        heartsHealthSystem.OnChangeHeartAmount += HeartsHealthSystem_OnOnChangeHeartAmount;
    }

    private void SetHeartList(HeartsHealthSystem heartsHealthSystem) {
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
    }

    private void HeartsHealthSystem_OnOnChangeHeartAmount(object sender, EventArgs e) {
        ClearUI();
        heartImageList = new List<HeartImage>();
        SetHeartsHealthSystem(characterStats.GetHealthSystem());
        RefreshAllHearts();
    }

    private void HeartsHealthSystem_OnSet(object sender, EventArgs e) {
        RefreshAllHearts();
    }

    private void HeartsHealthSystem_OnDead(object sender, System.EventArgs e) {
        //
    }

    private void HeartsHealthSystem_OnHealed(object sender, System.EventArgs e) {
        RefreshAllHearts();
    }

    private void HeartsHealthSystem_OnDamaged(object sender, System.EventArgs e) {
        RefreshAllHearts();
    }

    private void RefreshAllHearts() {
        List<Heart> heartList = heartsHealthSystem.GetHeartList();
        for (int i = 0; i < heartImageList.Count; i++) {
            heartImageList[i].SetHeartFraments(heartList[i].GetFragmentAmount());
        }
    }

    private void ClearUI() {
        Transform parent = gameObject.transform;
        foreach(Transform child in parent) {
            Destroy(child.gameObject);
        }
    }

    private HeartImage CreateHeartImage(Vector2 anchoredPosition) {
        GameObject heartGameObject = new GameObject("Heart", typeof(Image), typeof(Animation));

        heartGameObject.transform.SetParent(transform, false);
        heartGameObject.transform.localPosition = Vector3.zero;
        heartGameObject.transform.localScale = Vector3.one;

        heartGameObject.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
        heartGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(35, 35);

        Image heartImageUI = heartGameObject.GetComponent<Image>();
        heartImageUI.sprite = heart4Sprite;

        HeartImage heartImage = new HeartImage(this, heartImageUI);
        heartImageList.Add(heartImage);

        return heartImage;
    }
}