using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//public enum Buff
//{
//    MELEEATK,
//    RANGEDATK,
//    HEALING,
//    SPEED
//}

public class Buffs : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> buffIcons = new List<GameObject>();

    private void UpdateText(GameObject buffIcon, float remainingTime)
    {
        // Get the Text component and update the text to display the remaining time
        Text textComponent = buffIcon.GetComponentInChildren<Text>();
        if (textComponent != null)
        {
            textComponent.text = Mathf.CeilToInt(remainingTime).ToString() + " s";
        }
    }

    
    public void StartCountdown(GameObject buffIcon, float duration)
    {
        float remainingTime = duration;
        UpdateText(buffIcon, duration);

        // Start the countdown coroutine
        StartCoroutine(CountdownCoroutine(buffIcon, remainingTime));
    }

   
    private IEnumerator CountdownCoroutine(GameObject buffIcon, float remainingTime)
    {
        Image iconImage = buffIcon.GetComponentInChildren<Image>();
        Color originalColor = iconImage.color;
        bool isBlinking = false;

        while (remainingTime > 0)
        {
            // Check if the remaining time is less than 3 seconds
            if (remainingTime < 3f)
            {
                isBlinking = !isBlinking;

                if (isBlinking)
                {
                    iconImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f); // Set the alpha to 0 (hide)
                }
                else
                {
                    iconImage.color = originalColor; // Set the alpha to the original color (show)
                }
            }

            yield return new WaitForSeconds(0.5f); // Wait for 0.5 seconds
            remainingTime -= 0.5f; // Subtract 0.5 from the remaining time
            UpdateText(buffIcon, remainingTime);
        }

        // When the countdown is complete, hide or destroy the buff icon
        buffIcon.SetActive(false); // Set the buff icon inactive
        iconImage.color = originalColor; // Reset the icon color
    }



    public void AddBuffIcon(GameObject buffIcon, float duration)
    {
        buffIcons.Add(buffIcon);
        buffIcon.SetActive(true); // Set the buff icon active
        StartCountdown(buffIcon, duration);
    }

    //public void RemoveBuffIcon(GameObject buffIcon)
    //{
    //    buffIcons.Remove(buffIcon);
    //    buffIcon.SetActive(false); // Set the buff icon inactive
    //}

    public void ClearBuffIcons()
    {
        foreach (GameObject buffIcon in buffIcons)
        {
            Destroy(buffIcon);
        }

        buffIcons.Clear();
    }

    public void InitializeBuffIcons(List<GameObject> buffList)
    {
        ClearBuffIcons();

        foreach (GameObject buffIcon in buffList)
        {
            buffIcon.SetActive(false); // Set the buff icon active
        }
    }
}
