using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using iText;
using iText.Commons;
using iText.Html2pdf;
using iText.Kernel.Pdf;
using iText.IO.Source;
using iText.StyledXmlParser.Css.Media;
using iText.Layout.Font;
using System.Text.RegularExpressions;

public class PrintManager : MonoBehaviour
{
    public string desiredPdfLocation; // Path to save the generated PDF
    public bool storesToTempFolder;
    [Range(0,80)]public float outputWidthMm;
    public string fontPath;
    public float lineHeightPx;
    private int outputWidthPx;

    private string outputFolderPath;

    private void Start()
    {
        outputFolderPath = desiredPdfLocation;
        if (storesToTempFolder) { outputFolderPath = Path.Combine(Path.GetTempPath(), "ONtheLine"); }

        outputWidthPx = Mathf.CeilToInt((outputWidthMm / 25.4f) * 72f);
        Debug.Log(outputWidthPx);
    }

    public void WritePdfFromHtml(string htmlString, string fileName)
    {
        int lineAmnt = Regex.Matches(htmlString, Regex.Escape("<br>")).Count;
        int heightPx = Mathf.CeilToInt(lineAmnt * lineHeightPx);
        heightPx += 64; //padding top and bottom
        //Add height pictures when pictures are added.

        Debug.Log(htmlString);

        string fullName = $"temp-bezoeker-{fileName}.pdf";
        string outputFilePath = Path.Combine(outputFolderPath, fullName);

        using (FileStream outputStream = new FileStream(outputFilePath, FileMode.Create))
        {
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outputStream));
            pdfDocument.SetDefaultPageSize(new iText.Kernel.Geom.PageSize(outputWidthPx, heightPx));

            FontProvider fontProvider = new FontProvider();
            fontProvider.AddFont(fontPath);

            ConverterProperties converterProperties = new ConverterProperties();
            converterProperties.SetFontProvider(fontProvider);

            HtmlConverter.ConvertToPdf(htmlString, pdfDocument, converterProperties);

            Debug.Log($"Created Pdf from html file = {fullName} at location {outputFolderPath}");
        }
    }

    public void PrintPdf(string pdfPath, float heightMm)
    {
        
    }
}
