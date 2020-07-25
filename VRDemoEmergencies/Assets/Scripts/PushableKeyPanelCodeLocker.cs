using TMPro;
using UnityEngine;

public class PushableKeyPanelCodeLocker : MonoBehaviour, Interactable
{
    public CodeSystemKeys keyAssociated;
    private CodeLocker codeSystemAssociated;
    public TextMeshProUGUI displayedkey;

    private void Start()
    {
        codeSystemAssociated = GetComponentInParent<CodeLocker>();
        UpdatestringDisplayed();
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


}
