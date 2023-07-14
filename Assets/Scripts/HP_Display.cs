using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_Display : MonoBehaviour
{
    private Coroutine update;
    [SerializeField]
    private Sprite[] HeartSprites;
    [SerializeField]
    private Image HeartPrefab;

    private List<Image> hearts;

    [SerializeField]
    private int maxHP = 5; //start with 5 hearts
    private int hp = 0; // displayed HP
    [SerializeField]
    private int targetHp = 5;//for winding down hp


    // Start is called before the first frame update
    void Awake()
    {
        hearts = new List<Image>();
        hearts.Add(this.GetComponent<Image>());
        hp = 0;
        DisplayHearts();
        update = StartCoroutine(ChangeLife());
    }

    public void UpdateHealth(int hp)
    {
        this.targetHp = hp;
        if(update != null)
        {
            StopCoroutine(update);
        }
        
        update = StartCoroutine(ChangeLife());
    }

    public void UpdateMaxHealth(int maxHp)
    {
        this.maxHP = maxHp;

        if (update != null)
        {
            StopCoroutine(update);
        }

        //update = StartCoroutine(ChangeLife());
        UpdateHearts();
        DisplayHearts();
    }

    public int getTargetHealth()
    {
        return targetHp;
    }

    public int getMaxHealth()
    {
        return maxHP;
    }

    //Using Coroutine so the hearts only update when needed instead of every frame.
    private IEnumerator ChangeLife() {

        while (targetHp != hp)
        {
            if (hp < targetHp)
            {
                hp++;
            }
            else if (hp > targetHp)
            {
                hp--;
            }

            UpdateHearts();
            yield return new WaitForSeconds(1/24.0f);
        }
        yield break;
    }

    //Creates Hearts
    private void DisplayHearts() {
        for (int i = 1; i < maxHP; i++) {
            if (hearts.Count <= i) { //Stop Duplicates
                //Instantiate Heart
                GameObject g = Instantiate<GameObject>(HeartPrefab.gameObject);
                g.GetComponent<RectTransform>().SetParent(this.GetComponent<RectTransform>());
                g.GetComponent<RectTransform>().localPosition = new Vector2(12.5f * i , 0);
                g.GetComponent<RectTransform>().localScale = Vector2.one;
                hearts.Add(g.GetComponent<Image>());
            }
        }
    }

    //Updates Displayed Hearts
    void UpdateHearts() {

        for (int i = 0; i < hearts.Count; i++) {
            //Color Correct Hearts
            if (i >= maxHP)
            {
                hearts[i].color = Color.clear;
            }
            else
            {
                hearts[i].color = Color.white;
            }

            //Full Heart
            if (hp > i) {
                hearts[i].sprite = HeartSprites[0];
            }
            else
            {
                hearts[i].sprite = HeartSprites[1];
            }
        }
    }

    private void Update()
    {
        if (targetHp > maxHP)
        {
            targetHp = maxHP;
        }
    }

}
