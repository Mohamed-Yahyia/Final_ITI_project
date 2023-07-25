using DinkToPdf;
using DinkToPdf.Contracts;
using System.IO;

namespace Project_Api.Controllers
{
   

        public class PdfService
        {
            private readonly IConverter _pdfConverter;

            public PdfService(IConverter pdfConverter)
            {
                _pdfConverter = pdfConverter;
            }

            public byte[] GenerateInvoicePdf(string htmlContent)
            {
                var globalSettings = new GlobalSettings
                {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait,
                    Margins = new MarginSettings { Top = 10, Bottom = 10, Left = 10, Right = 10 },
                    DPI = 300,
                    ColorMode = ColorMode.Color,
                    UseCompression = true,
                };

                var objectSettings = new ObjectSettings
                {
                    PagesCount = true,
                    HtmlContent = htmlContent,
                    WebSettings = { DefaultEncoding = "utf-8" },
                };

                var document = new HtmlToPdfDocument()
                {
                    GlobalSettings = globalSettings,
                    Objects = { objectSettings },
                };

                return _pdfConverter.Convert(document);
            }
        }
    

}