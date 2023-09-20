using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrintLib;

public class PrinterManager : MonoBehaviour
{
    public string printerName;
    public Vector2 printerStartPosition;

    public string fontName;
    public float fontSize;
    public float lineHeight;
    public float marginBottom;
    public int maxCharsPerLine;

    public Texture2D logoType;
    public Texture2D biebLogo;
    public Texture2D hkuLogo;

    private Printer printer;

    private StateManager stateManager;

    private float lastModuloYPos;

    private void Start()
    {
        //PrintReceipt("0`1`2`3`4`5TestTest`6Test");
        stateManager = StateManager.instance;
    }

    public void PrintReceipt(List<string> linesToPrint)
    {
     // printer.PrintText("12345678901234567890123456789012345678");

        float printerYPos = printerStartPosition.y;

        printer = new Printer();
        printer.SelectPrinter(printerName);

        printer.StartDocument();
        printer.SetTextFontFamily(fontName);
        printer.SetTextFontSize(fontSize);
        printer.SetTextColor(Color.black);

        printer.SetPrintPosition(printerStartPosition.x, printerYPos);
        //printer.PrintText("--------------------------------------");
        printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        lastModuloYPos = printerYPos % 297f;


        //Print OnTheLine Logo
        printer.PrintTexture(logoType, 66f, 19.2f);
        printer.SetPrintPosition(printerStartPosition.x, printerYPos);
        printerYPos += 19.2f;

        printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        lastModuloYPos = printerYPos % 297f;
        printer.SetPrintPosition(printerStartPosition.x, printerYPos);

        //print customer no
        printer.SetTextFontSize(fontSize*2);
        printer.PrintText($"No: {stateManager.currentBezoekerData.bezoekerID.ToString().PadLeft(6, '0')}");
        printerYPos += lineHeight*2; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        lastModuloYPos = printerYPos % 297f;

        printer.SetPrintPosition(printerStartPosition.x, printerYPos);
        //printer.PrintText("--------------------------------------");
        printerYPos += lineHeight;
        printer.SetPrintPosition(printerStartPosition.x, printerYPos);

        printer.SetTextFontSize(fontSize);

        //print bieb adres + U krijgt U geeft
        printer.PrintText("--------------------------------------");
        printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        lastModuloYPos = printerYPos % 297f;
        printer.SetPrintPosition(printerStartPosition.x, printerYPos);
        printer.PrintText("                                      ");
        printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        lastModuloYPos = printerYPos % 297f;
        printer.SetPrintPosition(printerStartPosition.x, printerYPos);
        printer.PrintText("          Bibliotheek Utrecht         ");
        printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        lastModuloYPos = printerYPos % 297f;
        printer.SetPrintPosition(printerStartPosition.x, printerYPos);
        printer.PrintText("             Locatie Neude            ");
        printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        lastModuloYPos = printerYPos % 297f;
        printer.SetPrintPosition(printerStartPosition.x, printerYPos);
        printer.PrintText("       Neude 11, 3512AE Utrecht       ");
        printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        lastModuloYPos = printerYPos % 297f;
        printer.SetPrintPosition(printerStartPosition.x, printerYPos);
        printer.PrintText("                                      ");
        printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        lastModuloYPos = printerYPos % 297f;
        printer.SetPrintPosition(printerStartPosition.x, printerYPos);
        printer.PrintText("                                      ");
        printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        lastModuloYPos = printerYPos % 297f;
        printer.SetPrintPosition(printerStartPosition.x, printerYPos);
        printer.PrintText(" Je krijgt                   Je geeft ");
        printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        lastModuloYPos = printerYPos % 297f;
        printer.SetPrintPosition(printerStartPosition.x, printerYPos);
        printer.PrintText("--------------------------------------");
        printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        lastModuloYPos = printerYPos % 297f;
        printer.SetPrintPosition(printerStartPosition.x, printerYPos);
        printer.PrintText("                                      ");
        printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        lastModuloYPos = printerYPos % 297f;
        printer.SetPrintPosition(printerStartPosition.x, printerYPos);

        for (int i = 0; i < linesToPrint.Count; i++)
        {
            if(linesToPrint[i].Length > maxCharsPerLine)
            {
                Debug.LogWarning($"Line {i}: {linesToPrint[i]} has exceeded the maximum characters per line. Max = {maxCharsPerLine}, LineLength = {linesToPrint[i].Length}");
            }

            printer.SetPrintPosition(printerStartPosition.x, printerYPos);
            printer.PrintText(linesToPrint[i]);
            printerYPos += lineHeight; if(printerYPos > 297f) { printer.NewPage(); printerYPos = 0; } lastModuloYPos = printerYPos % 297f;
        }

        printer.SetPrintPosition(printerStartPosition.x, printerYPos);
        printer.PrintText("--------------------------------------");
        printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        lastModuloYPos = printerYPos % 297f;
        printer.SetPrintPosition(printerStartPosition.x, printerYPos);

        //print take home message
        printer.PrintText("                                      "); printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        lastModuloYPos = printerYPos % 297f; printer.SetPrintPosition(printerStartPosition.x, printerYPos);
        printer.PrintText(" Het fijne aan de bibliotheek is dat  "); printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        lastModuloYPos = printerYPos % 297f; printer.SetPrintPosition(printerStartPosition.x, printerYPos);
        printer.PrintText(" ze verder niets doen met al deze     "); printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        lastModuloYPos = printerYPos % 297f; printer.SetPrintPosition(printerStartPosition.x, printerYPos);
        printer.PrintText(" data, behalve waar nodig veilig      "); printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        lastModuloYPos = printerYPos % 297f; printer.SetPrintPosition(printerStartPosition.x, printerYPos);
        printer.PrintText(" verwerken, geheel volgens de AVG     "); printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        lastModuloYPos = printerYPos % 297f; printer.SetPrintPosition(printerStartPosition.x, printerYPos);
        printer.PrintText(" wet.                                 "); printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        lastModuloYPos = printerYPos % 297f; printer.SetPrintPosition(printerStartPosition.x, printerYPos);
        printer.PrintText("                                      "); printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        lastModuloYPos = printerYPos % 297f; printer.SetPrintPosition(printerStartPosition.x, printerYPos);
        printer.PrintText(" Maar sta nu eens stil bij alle       "); printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        lastModuloYPos = printerYPos % 297f; printer.SetPrintPosition(printerStartPosition.x, printerYPos);
        printer.PrintText(" andere diensten die je gebruikt.     "); printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        lastModuloYPos = printerYPos % 297f; printer.SetPrintPosition(printerStartPosition.x, printerYPos);
        printer.PrintText(" Wanneer betaal jij met je data ?     "); printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        lastModuloYPos = printerYPos % 297f; printer.SetPrintPosition(printerStartPosition.x, printerYPos);
        printer.PrintText(" Weet jij waar die data naartoe gaat? "); printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        lastModuloYPos = printerYPos % 297f; printer.SetPrintPosition(printerStartPosition.x, printerYPos);
        printer.PrintText(" Wat voor waarde creëer jij?          "); printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        lastModuloYPos = printerYPos % 297f; printer.SetPrintPosition(printerStartPosition.x, printerYPos);
        printer.PrintText("                                      "); printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        lastModuloYPos = printerYPos % 297f; printer.SetPrintPosition(printerStartPosition.x, printerYPos);
        printer.PrintText(" Let eens op of je zo'n               "); printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        lastModuloYPos = printerYPos % 297f; printer.SetPrintPosition(printerStartPosition.x, printerYPos);
        printer.PrintText(" datatransactie kan herkennen.        "); printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        lastModuloYPos = printerYPos % 297f; printer.SetPrintPosition(printerStartPosition.x, printerYPos);
        printer.PrintText(" En denk er eens rustig over na.      "); printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        lastModuloYPos = printerYPos % 297f; printer.SetPrintPosition(printerStartPosition.x, printerYPos);
        printer.PrintText("                                      "); printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        lastModuloYPos = printerYPos % 297f; printer.SetPrintPosition(printerStartPosition.x, printerYPos);
        printer.PrintText(" Moet je het wel accepteren?          "); printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        lastModuloYPos = printerYPos % 297f; printer.SetPrintPosition(printerStartPosition.x, printerYPos);
        printer.PrintText("                                      "); printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        lastModuloYPos = printerYPos % 297f; printer.SetPrintPosition(printerStartPosition.x, printerYPos);
        printer.PrintText("--------------------------------------"); printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        lastModuloYPos = printerYPos % 297f; printer.SetPrintPosition(printerStartPosition.x, printerYPos);

        //Print Bieb Logo
        printer.SetPrintPosition(printerStartPosition.x, printerYPos);
        printer.PrintTexture(biebLogo, 66f, 19.2f);
        printerYPos += 19.2f; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        lastModuloYPos = printerYPos % 297f;

        //Print HKU Logo
        printer.SetPrintPosition(printerStartPosition.x, printerYPos);
        printer.PrintTexture(hkuLogo, 66f, 19.2f);
        printerYPos += 19.2f; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        lastModuloYPos = printerYPos % 297f;

        printer.SetPrintPosition(printerStartPosition.x, printerYPos);
        printer.PrintText("--------------------------------------");
        printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        lastModuloYPos = printerYPos % 297f;

        printer.SetPrintPosition(printerStartPosition.x, printerYPos + marginBottom);
        printer.PrintText("______________________________________");

        printer.EndDocument();
    }
}
