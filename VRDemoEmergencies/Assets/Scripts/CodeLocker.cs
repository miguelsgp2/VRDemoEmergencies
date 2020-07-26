using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class CodeLocker : EmergencyScenarioQuestObject
{
    public TextMeshProUGUI displayScreenText;
    public Light accessLight; 
    private VerifiableString verifiableString;
    public string passwordCode = "1244";
    private void Start()
    {
        verifiableString = new VerifiableString();
        verifiableString.SetPassword(passwordCode);
        UpdateTextDisplayed();
    }

    public void KeyActivated(PushableKeyPanelCodeLocker pushedKey)
    {
        var codeSystemKeyActivated = pushedKey.GetKeyCodeSystemAssociated();
        ProcessKeyCode(codeSystemKeyActivated);
        UpdateTextDisplayed();

    }

    private void ProcessKeyCode(CodeSystemKeys _keyCodeToProcess)
    {
        switch (_keyCodeToProcess)
        {
            case CodeSystemKeys.RESET:
                verifiableString.EraseCurrentString();
                break;
            case CodeSystemKeys.ENTER:
                if (verifiableString.IsCurrentStringValidPassword())
                {
                    GrantAccess();
                }
                verifiableString.EraseCurrentString();
                break;
            case CodeSystemKeys.ZERO:
                verifiableString.AddStringToTheRight("0");
                break;
            case CodeSystemKeys.ONE:
                verifiableString.AddStringToTheRight("1");
                break;
            case CodeSystemKeys.TWO:
                verifiableString.AddStringToTheRight("2");
                break;
            case CodeSystemKeys.THREE:
                verifiableString.AddStringToTheRight("3");
                break;
            case CodeSystemKeys.FOUR:
                verifiableString.AddStringToTheRight("4");
                break;
            case CodeSystemKeys.FIVE:
                verifiableString.AddStringToTheRight("5");
                break;
            case CodeSystemKeys.SIX:
                verifiableString.AddStringToTheRight("6");
                break;
            case CodeSystemKeys.SEVEN:
                verifiableString.AddStringToTheRight("7");
                break;
            case CodeSystemKeys.EIGHT:
                verifiableString.AddStringToTheRight("8");
                break;
            case CodeSystemKeys.NINE:
                verifiableString.AddStringToTheRight("9");
                break;
            default:
                break;
        }
    }

    private void UpdateTextDisplayed()
    {
        displayScreenText.text = verifiableString.GetCurrentModifiableString();
    }

    private void GrantAccess()
    {
        accessLight.color = Color.green;
        myEmergencyScenario.ScenarioCompleted();
    }

    private class VerifiableString
    {
        private string currentModifiableString;
        private string password;

        public VerifiableString()
        {
            EraseCurrentString();
        }

        public void SetPassword(string newPassword)
        {
            password = newPassword;
        }

        public void AddStringToTheRight(string stringAdded)
        {
            currentModifiableString = currentModifiableString + stringAdded;
        }

        public bool IsCurrentStringValidPassword()
        {
            return string.Equals(currentModifiableString, password);
        }

        public void EraseCurrentString()
        {
            currentModifiableString = "";
        }

        public string GetCurrentModifiableString()
        {
            return currentModifiableString;
        }
    }
}

public enum CodeSystemKeys { RESET, ENTER, ZERO, ONE, TWO, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT, NINE}

