using Microsoft.Win32.SafeHandles;
using System.Collections;
using TMPro;
using UnityEngine;

public class PushableKeyPanelCodeLocker : MonoBehaviour, Interactable
{
    public CodeSystemKeys keyAssociated;
    private CodeLocker codeSystemAssociated;
    public TextMeshProUGUI displayedkey;
    Color originalColorDisplayed;

    private void Start()
    {
        codeSystemAssociated = GetComponentInParent<CodeLocker>();
        originalColorDisplayed = displayedkey.color;
        UpdatestringDisplayed();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Interact();
        }
    }

    private void UpdatestringDisplayed()
    {
        displayedkey.text = ConvertCodeSystemKeyToSChar(keyAssociated).ToString();
    }
    private void SendInformationToCodeLockerSystem()
    {
        codeSystemAssociated.KeyActivated(this);
    }

    public CodeSystemKeys GetKeyCodeSystemAssociated()
    {
        return keyAssociated;
    }

    public void Interact()
    {
        PushedMovement();
        SendInformationToCodeLockerSystem();
    }

    private char ConvertCodeSystemKeyToSChar(CodeSystemKeys keyCode)
    {
        char keycodeCodified = new char();
        switch (keyCode)
        {
            case CodeSystemKeys.RESET:
                keycodeCodified = '*';
                break;
            case CodeSystemKeys.ENTER:
                keycodeCodified = '#';
                break;
            case CodeSystemKeys.ZERO:
                keycodeCodified = '0';
                break;
            case CodeSystemKeys.ONE:
                keycodeCodified = '1';
                break;
            case CodeSystemKeys.TWO:
                keycodeCodified = '2';
                break;
            case CodeSystemKeys.THREE:
                keycodeCodified = '3';
                break;
            case CodeSystemKeys.FOUR:
                keycodeCodified = '4';
                break;
            case CodeSystemKeys.FIVE:
                keycodeCodified = '5';
                break;
            case CodeSystemKeys.SIX:
                keycodeCodified = '6';
                break;
            case CodeSystemKeys.SEVEN:
                keycodeCodified = '7';
                break;
            case CodeSystemKeys.EIGHT:
                keycodeCodified = '8';
                break;
            case CodeSystemKeys.NINE:
                keycodeCodified = '9';
                break;
            default:
                break;
        }

        return keycodeCodified;
    }

    private void PushedMovement()
    {
        StartCoroutine(SpringAlikeMovement());
        StartCoroutine(LetterDisplayedColored());
    }

    private IEnumerator SpringAlikeMovement()
    {
        float duration = 0.2f;
        float maxDisplacement = -0.053f;
        float halfDuration = duration / 2f;
        float speed = maxDisplacement / halfDuration;
        float timer = 0;

        while(timer < duration)
        {
            if(timer<halfDuration)
            {
                if(transform.localPosition.z > maxDisplacement)
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + speed * Time.deltaTime);
            }
            else
            {
                if (transform.localPosition.z < maxDisplacement)
                    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - speed * Time.deltaTime);
            }
            timer += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0f);
        
    }

    private IEnumerator LetterDisplayedColored()
    {
        

        var pushedButtonColor = new Color();
        ColorUtility.TryParseHtmlString("#00FFB8", out pushedButtonColor);
        displayedkey.color = pushedButtonColor;
            yield return new WaitForSeconds(0.2f);
        displayedkey.color = originalColorDisplayed;

    }


}
