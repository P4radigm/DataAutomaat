using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Mail;
using System.Text;
using TMPro;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using System.IO;

public class EmailManager : MonoBehaviour
{
    [System.Serializable]
    public class BezoekerData
    {
        public int bezoekerID;
    }

    private BezoekerData currentBezoekerData;

    public PrintManager printManager;

    [SerializeField] private bool bodyHasPipes;

    public string fromAddress = "your_email_address";
    public string fromDisplayName = "your_display_name";
    private MailAddress fromMailAdress;
    public string toAddress;
    public string subject = "Email subject";
    private string emailHeaderPone =
@"|----------------------------------------|<br>
|-----------DATA-TRANSACTIEBON-----------|<br>
|----------------------------------------|<br>
|                                        |<br>
|           Bibliotheek Utrecht          |<br>
|              Locatie Neude             |<br>
|        Neude 11, 3512AE Utrecht        |<br>
|                                        |<br>
| Bezoeker: ";
    private string emailHeaderPtwo =
@"                       |<br>
|                                        |<br>
| U krijgt                       U geeft |<br>
|----------------------------------------|<br>";
    private string emailFooter =
@"|----------------------------------------|<br>
|                                        |<br>
| Het fijne aan de bibliotheek is dat ze |<br>
| verder niets doen met al deze data,    |<br>
| behalve waar nodig veilig verwerken,   |<br>
| geheel volgens de AVG wet.             |<br>
|                                        |<br>
| Maar sta nu eens stil bij alle andere  |<br>
| diensten die je gebruikt.              |<br>
| Wanneer betaal jij met je data?        |<br>
| Weet je waar die data naartoe gaat?    |<br>
| Wat voor waarde creëer jij?            |<br>
|                                        |<br>
| Let eens op of je zo’n datatransactie  |<br>
| kan herkennen.                         |<br>
| En denk er eens rustig over na.        |<br>
|                                        |<br>
| Moet je het wel accepteren?            |<br>
|                                        |<br>
|----------------------------------------|<br>
|---------------ON-THE-LINE--------------|<br>
|---------BIBLIOTHEEK-NEUDE-&-HKU--------|<br>
|----------------------------------------|<br>";
    private string htmlStart = @"
            <!DOCTYPE HTML>
            <html>
                <head>
                    <meta charset=""UTF-8"">
                    <title>Data Transactiebon Bibliotheek Neude ----- ONtheLINE -----</title>
                    <style>
                        body {
                            font-family: ""consolas"";
                            text-align: center;
                            margin: -15px;
                            padding: -15px;
                            font-size: 11px;
                            background-color: white;
                            width: 227px;
                            height: 1000px;
                        }
                    </style>
                </head>
            <body>
    ";
    private string htmlEnd = @"
            </body>
        </html>
    ";
    [Space(20)]
    private string basisText =
@"|------------// Basispakket //-----------|<br>
|                                        |<br>
|                     Uw beeld op camera |<br>
| Toegang tot het gebouw                 |<br>
| Warmte                                 |<br>
| Elektriciteit                          |<br>
| Zitplek                                |<br>
| Toegang tot materiaal                  |<br>
| Veiligheid                             |<br>
|                                        |<br>";
    private string lenenText =
@"|--// Materiaal Lenen of Terugbrengen //-|<br>
|                                        |<br>
|                          Uw leesgedrag |<br>
|                      Uw volledige naam |<br>
|                               Uw Adres |<br>
|                               Uw Email |<br>
|                      Uw Telefoonnummer |<br>
| Materiaal tijdelijk mee naar huis      |<br>
|                                        |<br>";
    private string wcText =
@"|-------------// WC Bezoek //------------|<br>
|                                        |<br>
|                                      - |<br>
| Toegang tot toilet                     |<br>
| Water                                  |<br>
| Papier                                 |<br>
| Zeep                                   |<br>
|                                        |<br>";
    private string wifiText =
@"|-----------// WiFi Gebruik //-----------|<br>
|                                        |<br>
|                    Uw internet verkeer |<br>
|                 Uw apparaat informatie |<br>
|                  Uw browser informatie |<br>
|                   Uw apparaat IP adres |<br>
| Toegang tot het internet               |<br>
|                                        |<br>";
    private string computerText =
@"|-------// Bieb Computer Gebruik //------|<br>
|                                        |<br>
|                    Uw internet verkeer |<br>
|                    Uw locale bestanden |<br>
|                      Uw volledige naam |<br>
|                               Uw Adres |<br>
|                               Uw Email |<br>
|                      Uw Telefoonnummer |<br>
| Toegang tot bepaalde applicaties       |<br>
| Connectie met onze printers            |<br>
|                                        |<br>";
    private string printenText =
@"|--------------// Printen //-------------|<br>
|                                        |<br>
|              Uw te printen bestand(en) |<br>
|                      Uw volledige naam |<br>
|                               Uw Adres |<br>
|                               Uw Email |<br>
|                      Uw Telefoonnummer |<br>
| Bedrukt papier                         |<br>
|                                        |<br>";
    private string websiteText =
@"|----------// Bezoek Website /-----------|<br>
|                                        |<br>
|                Uw gedrag op de website |<br>
|                   Uw bezochte pagina's |<br>
|                      Uw zoekopdrachten |<br>
|                          Uw klikgedrag |<br>
|                    Uw locatie gegevens |<br>
|                  Uw browser informatie |<br>
|                 Uw apparaat informatie |<br>
|                   Uw apparaat IP adres |<br>
| Toegang tot onze online kennis         |<br>
| Informatie over activiteiten           |<br>
| Mogelijkheid aanmelden activiteiten    |<br>
| Mogelijkheid reserveren materiaal      |<br>
|                                        |<br>";
    private string kluisjeText =
@"|---------// Kluisjes gebruik //---------|<br>
|                                        |<br>
|                   Uw gebruikte pincode |<br>
| Beveiligde opslagruimte                |<br>
|                                        |<br>";
    private string etenText =
@"|-------// Eten of Drinken Halen //------|<br>
|                                        |<br>
|                                Uw IBAN |<br>
|                       Uw rekening naam |<br>
| Eten of Drinken                        |<br>
|                                        |<br>";
    [Space(20)]
    public GameObject parentOne;
    public GameObject parentTwo;
    public GameObject parentThree;
    public TMP_InputField emailField;
    public TextMeshProUGUI emailErrorText;
    public TextMeshProUGUI itemsText;
    public Toggle basis;
    public Toggle lenen;
    public Toggle wc;
    public Toggle wifi;
    public Toggle computer;
    public Toggle printen;
    public Toggle website;
    public Toggle kluisje;
    public Toggle eten;
    [Space(20)]
    public string basisItemText;
    public string lenenItemText;
    public string wcItemText;
    public string wifiItemText;
    public string computerItemText;
    public string printenItemText;
    public string websiteItemText;
    public string kluisjeItemText;
    public string etenItemText;

    private void Start()
    {
        fromMailAdress = new MailAddress(fromAddress, fromDisplayName);
        currentBezoekerData = LoadBezoekerData();

        basisText = basisText.Replace(" ", "&nbsp;");
        lenenText = lenenText.Replace(" ", "&nbsp;");
        wcText = wcText.Replace(" ", "&nbsp;");
        wifiText = wifiText.Replace(" ", "&nbsp;");
        computerText = computerText.Replace(" ", "&nbsp;");
        printenText = printenText.Replace(" ", "&nbsp;");
        websiteText = websiteText.Replace(" ", "&nbsp;");
        kluisjeText = kluisjeText.Replace(" ", "&nbsp;");
        etenText = etenText.Replace(" ", "&nbsp;");


        emailHeaderPone = emailHeaderPone.Replace(" ", "&nbsp;");
        emailHeaderPtwo = emailHeaderPtwo.Replace(" ", "&nbsp;");
        emailFooter = emailFooter.Replace(" ", "&nbsp;");
    }

    public void SendEmail()
    {
        toAddress = emailField.text;
        if (!IsValidEmail(toAddress)) { emailErrorText.enabled = true; return; }

        MailAddress toMailAdress = new MailAddress(toAddress);

        string body = "";

        body += htmlStart;
        body += $"<pre>{emailHeaderPone}{currentBezoekerData.bezoekerID.ToString("D6")}{emailHeaderPtwo}</pre>";

        if (basis.isOn) { body += $"<pre>{basisText}</pre>"; }
        if (lenen.isOn) { body += $"<pre>{lenenText}</pre>"; }
        if (wc.isOn) { body += $"<pre>{wcText}</pre>"; }
        if (wifi.isOn) { body += $"<pre>{wifiText}</pre>"; }
        if (computer.isOn) { body += $"<pre>{computerText}</pre>"; }
        if (printen.isOn) { body += $"<pre>{printenText}</pre>"; }
        if (website.isOn) { body += $"<pre>{websiteText}</pre>"; }
        if (kluisje.isOn) { body += $"<pre>{kluisjeText}</pre>"; }
        if (eten.isOn) { body += $"<pre>{etenText}</pre>"; }

        body += $"<pre>{emailFooter}</pre>";
        body += htmlEnd;

        string bodyNoPipes = body.Replace("|", "");

        MailMessage message = new MailMessage(fromMailAdress, toMailAdress);
        message.IsBodyHtml = true;
        message.Subject = subject;
        message.Body = bodyNoPipes;
        SmtpClient smtpClient = new SmtpClient("mail.antagonist.nl", 587);
        smtpClient.EnableSsl = true;
        smtpClient.Credentials = new NetworkCredential("ONtheLINE.neude@leonvanoldenborgh.nl", "HKU_ONtheLINE");
        smtpClient.Send(message);

        printManager.WritePdfFromHtml(body, currentBezoekerData.bezoekerID.ToString());

        currentBezoekerData.bezoekerID++;
        SaveBezoekerData(currentBezoekerData);

        parentOne.SetActive(false);
        parentTwo.SetActive(false);
        parentThree.SetActive(true);
    }

    public void PrintBon()
    {
        toAddress = emailField.text;
        if (!IsValidEmail(toAddress)) { emailErrorText.enabled = true; return; }

        string body = "";

        body += htmlStart;
        body += $"{emailHeaderPone}{currentBezoekerData.bezoekerID.ToString("D6")}{emailHeaderPtwo}";

        if (basis.isOn) { body += basisText; }
        if (lenen.isOn) { body += lenenText; }
        if (wc.isOn) { body += wcText; }
        if (wifi.isOn) { body += wifiText; }
        if (computer.isOn) { body += computerText; }
        if (printen.isOn) { body += printenText; }
        if (website.isOn) { body += websiteText; }
        if (kluisje.isOn) { body += kluisjeText; }
        if (eten.isOn) { body += etenText; }

        body += emailFooter;
        body += htmlEnd;

        string bodyNoPipes = body.Replace("|", "");

        printManager.WritePdfFromHtml(bodyHasPipes ? body : bodyNoPipes, currentBezoekerData.bezoekerID.ToString());
        currentBezoekerData.bezoekerID++;
        SaveBezoekerData(currentBezoekerData);

        parentOne.SetActive(false);
        parentTwo.SetActive(false);
        parentThree.SetActive(true);
    }

    public void ResetAll()
    {
        SceneManager.LoadScene(0);
    }

    public void PressNext()
    {
        //Update items text
        string itemText = "Items:" + '\n';

        if (basis.isOn) { itemText += basisItemText + '\n'; }
        if (lenen.isOn) { itemText += lenenItemText + '\n'; }
        if (wc.isOn) { itemText += wcItemText + '\n'; }
        if (wifi.isOn) { itemText += wifiItemText + '\n'; }
        if (computer.isOn) { itemText += computerItemText + '\n'; }
        if (printen.isOn) { itemText += printenItemText + '\n'; }
        if (website.isOn) { itemText += websiteItemText + '\n'; }
        if (kluisje.isOn) { itemText += kluisjeItemText + '\n'; }
        if (eten.isOn) { itemText += etenItemText + '\n'; }

        itemsText.text = itemText;

        //Switch parent transforms
        parentOne.SetActive(false);
        parentTwo.SetActive(true);
    }

    public void PressBack()
    {
        //Switch parent transforms
        parentTwo.SetActive(false);
        parentOne.SetActive(true);
    }

    private bool IsValidEmail(string email)
    {
        string pattern = @"^\s*[a-zA-Z0-9._%+-]+\s*@\s*[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}\s*$";
        //return Regex.IsMatch(email, pattern);
        return true;
    }

    private void Update()
    {
        if (emailErrorText.enabled)
        {
            if (IsValidEmail(emailField.text))
            {
                emailErrorText.enabled = false;
            }
        }
    }

    public BezoekerData LoadBezoekerData()
    {
        string filePath = Application.persistentDataPath + "/bezoekerID.json";

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            return JsonUtility.FromJson<BezoekerData>(jsonData);
        }
        else
        {
            BezoekerData newData = new BezoekerData();
            newData.bezoekerID = 1;
            SaveBezoekerData(newData);
            return newData;
        }
    }

    public void SaveBezoekerData(BezoekerData data)
    {
        string jsonData = JsonUtility.ToJson(data);
        string filePath = Application.persistentDataPath + "/bezoekerID.json";
        File.WriteAllText(filePath, jsonData);
    }
}
