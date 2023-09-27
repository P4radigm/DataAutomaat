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
    public Texture2D leonLogo;
    [SerializeField] private string[] takeHomeMessageLines;

    private Printer printer;

    [SerializeField] private StateManager stateManager;
    [SerializeField] private SinglePageContentManager contentManager;

    public void PrintReceipt()
    {
        //Setup printer
        float printerYPos = printerStartPosition.y;

        printer = new Printer();
        printer.SelectPrinter(printerName);

        printer.StartDocument();
        printer.SetTextFontFamily(fontName);
        printer.SetTextFontSize(fontSize);
        printer.SetTextColor(Color.black);

        printer.SetPrintPosition(printerStartPosition.x, printerYPos);

        //Print Creatief Talent Werkt Logo
        printer.PrintText("                                        ");
        printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        printer.SetPrintPosition(printerStartPosition.x, printerYPos);

        printer.PrintTexture(logoType, 70f, 35f);
        printerYPos += 35f; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        printer.SetPrintPosition(printerStartPosition.x, printerYPos);

        printer.PrintText("                                        ");
        printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        printer.SetPrintPosition(printerStartPosition.x, printerYPos);

        //print adres
        printer.PrintText("----------------------------------------");
        printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        printer.SetPrintPosition(printerStartPosition.x, printerYPos);

        printer.PrintText("                                        ");
        printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        printer.SetPrintPosition(printerStartPosition.x, printerYPos);

        printer.PrintText("          HKU locatie IBB-laan          ");
        printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        printer.SetPrintPosition(printerStartPosition.x, printerYPos);

        printer.PrintText("       Ina Boudier-bakkerlaan 50        ");
        printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }

        printer.SetPrintPosition(printerStartPosition.x, printerYPos);
        printer.PrintText("            3582 VA, Utrecht            ");
        printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }

        printer.PrintText("                                        ");
        printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        printer.SetPrintPosition(printerStartPosition.x, printerYPos);

        printer.PrintText("----------------------------------------");
        printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        printer.SetPrintPosition(printerStartPosition.x, printerYPos);

        //Print bestelling info
        printer.PrintText("                                        ");
        printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        printer.SetPrintPosition(printerStartPosition.x, printerYPos);

        printer.PrintText($"          Bestelling No {stateManager.bezoekerID}          ");
        printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        printer.SetPrintPosition(printerStartPosition.x, printerYPos);

        if (stateManager.isSelectingOrganisation) 
        { printer.PrintText("   Jouw ideale creatief professional    "); }
        else 
        { printer.PrintText("        Jouw ideale organisatie         "); }
        printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        printer.SetPrintPosition(printerStartPosition.x, printerYPos);

        printer.PrintText("                                        ");
        printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        printer.SetPrintPosition(printerStartPosition.x, printerYPos);

        //Print selected items

        if (stateManager.isSelectingOrganisation)
        {
            for (int i = 0; i < contentManager.menuItemsO.Length; i++)
            {
                if (contentManager.menuItemsO[i].selected)
                {
                    for (int b = 0; b < contentManager.menuItemsO[i].bonContent.Length; b++)
                    {
                        printer.PrintText(contentManager.menuItemsO[i].bonContent[b]);
                        printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
                        printer.SetPrintPosition(printerStartPosition.x, printerYPos);
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < contentManager.menuItemsCP.Length; i++)
            {
                if (contentManager.menuItemsCP[i].selected)
                {
                    for (int b = 0; b < contentManager.menuItemsCP[i].bonContent.Length; b++)
                    {
                        printer.PrintText(contentManager.menuItemsCP[i].bonContent[b]);
                        printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
                        printer.SetPrintPosition(printerStartPosition.x, printerYPos);
                    }
                }
            }
        }

        //print take home message

        for (int i = 0; i < takeHomeMessageLines.Length; i++)
        {
            printer.PrintText(takeHomeMessageLines[i]);
            printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
            printer.SetPrintPosition(printerStartPosition.x, printerYPos);
        }

        //Print Date
        printer.PrintText("----------------------------------------");
        printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        printer.SetPrintPosition(printerStartPosition.x, printerYPos);

        printer.PrintText("---------------28/09/2023---------------");
        printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        printer.SetPrintPosition(printerStartPosition.x, printerYPos);

        printer.PrintText("----------------------------------------");
        printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        printer.SetPrintPosition(printerStartPosition.x, printerYPos);

        //Print Leon Logo + info
        printer.PrintText("                                        ");
        printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        printer.SetPrintPosition(printerStartPosition.x, printerYPos);

        printer.PrintTexture(leonLogo, 70f, 17.5f);
        printerYPos += 17.5f; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        printer.SetPrintPosition(printerStartPosition.x, printerYPos);

        printer.PrintText("                                        ");
        printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        printer.SetPrintPosition(printerStartPosition.x, printerYPos);

        printer.PrintText("            @leonVoldenborgh            ");
        printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        printer.SetPrintPosition(printerStartPosition.x, printerYPos);

        printer.PrintText("          leonvanoldenborgh.nl          ");
        printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        printer.SetPrintPosition(printerStartPosition.x, printerYPos);

        printer.PrintText("                                        ");
        printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        printer.SetPrintPosition(printerStartPosition.x, printerYPos);

        printer.PrintText("----------------------------------------");
        printerYPos += lineHeight; if (printerYPos > 297f) { printer.NewPage(); printerYPos = 0; }
        printer.SetPrintPosition(printerStartPosition.x, printerYPos);

        //Marginbottom
        printer.SetPrintPosition(printerStartPosition.x, printerYPos + marginBottom);
        printer.PrintText("______________________________________");

        printer.EndDocument();
    }
}
