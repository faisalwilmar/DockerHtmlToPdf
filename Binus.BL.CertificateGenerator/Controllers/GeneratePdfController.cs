using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Binus.BL.CertificateGenerator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GeneratePdfController : ControllerBase
    {
        private readonly ILogger<GeneratePdfController> _logger;
        private readonly IConverter converter;

        public GeneratePdfController(ILogger<GeneratePdfController> logger, IConverter converter)
        {
            _logger = logger;
            this.converter = converter;
        }

        [HttpGet(Name = "GeneratePdfCertificate")]
        [Route("GeneratePdfCertificate")]
        public async Task<IActionResult> GeneratePdfCertificate()
        {
            var pdfPaperSize = new PechkinPaperSize("156mm", "220mm");

            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Landscape,
                PaperSize = pdfPaperSize,
                Margins = new MarginSettings(0, 0, 0, 0),
                DocumentTitle = "PDF Report",
                Outline = false
            };

            var objectSettings = new ObjectSettings
            {
                HtmlContent = "<html><head> <link href='https://fonts.googleapis.com/css?family=Open Sans' rel='stylesheet'> <style type='text/css'> body, html{margin: 0; padding: 0;}body{color: black; display: table; font-family: 'Open Sans'; text-align: left;}.container{width: 1119px; height: 791px;}.text-container{position: absolute; left: 85px; top: 192px;}.text-head{font-family: 'Open Sans'; font-style: normal; font-weight: 700; font-size: 46.2907px; line-height: 63px; color: #1797B7;}.text-sub{font-family: 'Open Sans'; font-style: normal; font-weight: 400; font-size: 20.5736px; line-height: 28px; color: #2C2C47;}.text-name{font-family: 'Open Sans'; font-style: normal; max-width: 750px; min-height: 150px; /* border: 2px black solid; */ font-weight: 700; font-size: 61.7209px; line-height: 84px; color: #2C2C47; text-transform: uppercase;}.text-course{font-family: 'Open Sans'; font-style: normal; font-weight: 600; font-size: 30.8604px; line-height: 42px; color: #1797B7;}.text-small{font-family: 'Open Sans'; font-style: normal; font-weight: 400; font-size: 15.4302px; line-height: 21px; color: #2C2C47;}.text-small-bold{font-family: 'Open Sans'; font-style: normal; font-weight: 600; font-size: 15.4302px; line-height: 21px; color: #2C2C47;}.text-container-bottom{position: absolute; left: 481px; top: 677px; display: flex; flex-direction: row;}.text-container-bottom-right{position: absolute; left: 800px; top: 677px; display: flex; flex-direction: row;}.bg{width: 100%; height: 100%; object-fit: contain;}</style></head><body> <div class=\"container\"> <div class=\"text-container\"> <div class=\"text-head\"> VERIFIED CERTIFICATE </div><div class=\"text-sub\"> This is to certify that </div><div class=\"text-container-name\" style=\"margin-top: 10px;\"> <div class=\"text-name\">{{userName}}</div></div><div> <div class=\"text-sub\" style=\"margin: 10px 0px;\"> has successfully completed {{language}} {{courseLevel}}-Level courses and received passing grade for </div><div class=\"text-course\" style=\"margin: 10px 0px;\">{{courseTitle}}</div><div class=\"text-sub\" style=\"margin: 10px 0px;\"> A digital language learning system provided by Binus University </div></div></div><div class=\"text-container-bottom\"> <div> <div class=\"text-small\"> Verified Certificate </div><div class=\"text-small-bold\"> Issued {{issuedDate}}</div></div></div><div class=\"text-container-bottom-right\"> <div> <div class=\"text-small\"> Valid Certificate ID </div><div class=\"text-small-bold\">{{assertionNumber}}</div></div></div><img class=\"bg\" src=\"https://stblresourcesdev.blob.core.windows.net/certification-service/certificate-template-background.png\"/> </div></body></html>",
                WebSettings = { DefaultEncoding = "utf-8" }
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            var file = converter.Convert(pdf);
            string fileName = "Certificate " + Guid.NewGuid().ToString() + ".pdf";
            return File(file, "application/pdf", fileName);
        }
    }
}
