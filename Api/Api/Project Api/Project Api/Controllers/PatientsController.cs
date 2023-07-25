using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Api.DataContext;
using Project_Api.Models;
using PdfSharpCore.Pdf;
using PdfSharpCore.Drawing;
using PdfSharpCore.Drawing.Layout;
using System.Text;
using DinkToPdf;
using DinkToPdf.Contracts;
using System.IO;

namespace Project_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly Context _Context;

        public object PdfGenerator { get; private set; }

        public PatientsController(Context Context)
        {
            _Context = Context;
        }


        [HttpGet("Getall")]
        public async Task<ActionResult<IEnumerable<Patients>>> GetPatients()
        {
            if (_Context.patients == null)
            {
                return NotFound();
            }
            return await _Context.patients.ToListAsync();
        }

        [HttpGet("GetPatientsbyId/{id}")]
        public async Task<ActionResult<Patients>> GetPatientsbyId(int id)
        {
            if (_Context.patients == null)
            {
                return NotFound();
            }
            var pat = await _Context.patients.FindAsync(id);
            if (pat == null)
            {
                return NotFound();
            }
            return pat;
        }
        //[HttpGet("Generatepdf/{id}")]
        //public async Task<ActionResult> GeneratePDF(string RecordNO)
        //{
        //    var document = new PdfDocument();
        //    string HtmlContent = "<h1>Welcome to mohamed</h1>";
        //    //  PdfGenerator.AddPdfPages(document, HtmlContent, PageSize.A4);

        //    // PdfPage page = document.AddPage();
        //    byte[]? response = null;
        //    using(MemoryStream ms = new MemoryStream())
        //    {
        //        document.Save(ms);
        //        response = ms.ToArray();
        //    }
        //    string Filename = $"Records{RecordNO}.pdf";
        //    return File(response, "application/pdf", Filename);
        //   // var pat = await _Context.patients.FindAsync();

        //}

        //    [HttpGet("generate")]
        //    public async Task<ActionResult> GeneratePdf()
        //    {
        //        PdfDocument document = new PdfDocument();

        //        // Add a page to the document
        //        PdfPage page = document.AddPage();
        //        XGraphics gfx = XGraphics.FromPdfPage(page);
        //        // Draw some content on the page
        //        string htmlContent = $@"
        //    <!DOCTYPE html>
        //    <html>
        //    <head>
        //        <title>Invoice</title>
        //    </head>
        //    <body>
        //        <h1>Invoice</h1>

        //        <h2>Invoice Details</h2>
        //        <hr />

        //        <div>
        //            <p><strong>Invoice ID:</strong> invoiceId</p>
        //            <p><strong>Status:</strong> status</p>
        //            <p><strong>Amount:</strong> amount</p>
        //            <p><strong>Invoice Date:</strong> invoiceDate.ToString</p>

        //            <h4>Patient Information</h4>
        //            <p><strong>Patient ID:</strong> patientId</p>
        //            <p><strong>Patient Name:</strong> patientName</p>
        //            <p><strong>Patient Gender:</strong> patientGender</p>
        //            <!-- Add other patient properties as needed -->

        //            <h4>Employee Information</h4>
        //            <p><strong>Employee ID:</strong> employeeId</p>
        //            <p><strong>Employee Name:</strong> employeeName</p>
        //            <p><strong>Employee Position:</strong> employeePosition</p>
        //            <!-- Add other employee properties as needed -->

        //            <!-- Add more details about the invoice as needed -->
        //        </div>

        //        <div>
        //            <a asp-action=""Index"" class=""btn btn-secondary"">Back to Invoice List</a>
        //        </div>

        //    </body>
        //    </html>
        //";
        //        XFont font = new XFont("Arial", 12);
        //        XTextFormatter textFormatter = new XTextFormatter(gfx);
        //        XRect rect = new XRect(50, 100, page.Width - 100, page.Height - 200);
        //        textFormatter.DrawString(htmlContent, font, XBrushes.Black, rect);

        //        // Save the PDF to a MemoryStream
        //        using (var stream = new System.IO.MemoryStream())
        //        {
        //            document.Save(stream, false);
        //            stream.Position = 0;

        //            // Return the PDF as a FileContentResult
        //            return File(stream.ToArray(), "application/pdf", "generated_pdf.pdf");
        //        }
        //    }

        [HttpGet("generate")]
        public async Task<ActionResult> GeneratePdf(string invoiceId, string status, decimal amount, DateTime invoiceDate,
                                            string patientId, string patientName, string patientGender,
                                            string employeeId, string employeeName, string employeePosition)
        {
            PdfDocument document = new PdfDocument();

            // Add a page to the document
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Draw some content on the page
           var htmlContent = $@"
        <!DOCTYPE html>
        <html>
        <head>
            <title>Invoice</title>
        </head>
        <body>
            <h1>Invoice</h1>

            <h2>Invoice Details</h2>
            <hr />

            <div>
                <p><strong>Invoice ID:</strong> {invoiceId}</p>
                <p><strong>Status:</strong> {status}</p>
                <p><strong>Amount:</strong> {amount}</p>
                <p><strong>Invoice Date:</strong> {invoiceDate.ToString("dd/MM/yyyy")}</p>

                <h4>Patient Information</h4>
                <p><strong>Patient ID:</strong> {patientId}</p>
                <p><strong>Patient Name:</strong> {patientName}</p>
                <p><strong>Patient Gender:</strong> {patientGender}</p>
                <!-- Add other patient properties as needed -->

                <h4>Employee Information</h4>
                <p><strong>Employee ID:</strong> {employeeId}</p>
                <p><strong>Employee Name:</strong> {employeeName}</p>
                <p><strong>Employee Position:</strong> {employeePosition}</p>
                <!-- Add other employee properties as needed -->

                <!-- Add more details about the invoice as needed -->
            </div>

            <div>
                <a asp-action=""Index"" class=""btn btn-secondary"">Back to Invoice List</a>
            </div>

        </body>
        </html>
    ";

            XFont font = new XFont("Arial", 12);
            XTextFormatter textFormatter = new XTextFormatter(gfx);
            XRect rect = new XRect(50, 100, page.Width - 100, page.Height - 200);
            textFormatter.DrawString(htmlContent, font, XBrushes.Black, rect);

            // Save the PDF to a MemoryStream
            using (var stream = new System.IO.MemoryStream())
            {
                document.Save(stream, false);
                stream.Position = 0;

                // Return the PDF as a FileContentResult
                return File(stream.ToArray(), "application/pdf", "generated_pdf.pdf");
            }
        }











        //2
        //[HttpGet("generate/{pageId}")]
        //public IActionResult GeneratePdfForPage(int pageId)
        //{
        //    // Get the content of the page based on the provided pageId
        //    string pageContent = GetPageContent(pageId);

        //    // Create a new instance of PdfDocument
        //    PdfDocument document = new PdfDocument();

        //    // Add a page to the document
        //    PdfPage page = document.AddPage();
        //    XGraphics gfx = XGraphics.FromPdfPage(page);

        //    // Draw the content on the page
        //    XFont font = new XFont("Arial", 12);
        //    XTextFormatter textFormatter = new XTextFormatter(gfx);
        //    XRect rect = new XRect(50, 100, page.Width - 100, page.Height - 200);
        //    textFormatter.DrawString(pageContent, font, XBrushes.Black, rect);

        //    // Save the PDF to a MemoryStream
        //    using (var stream = new System.IO.MemoryStream())
        //    {
        //        document.Save(stream, false);
        //        stream.Position = 0;

        //        // Return the PDF as a FileContentResult
        //        return File(stream.ToArray(), "application/pdf", $"page_{pageId}_pdf.pdf");
        //    }
        //}

        //// This is a placeholder method to get the content of the page based on the provided pageId.
        //private string GetPageContent(int pageId)
        //{
        //    // Replace this with the logic to fetch the content of the page from your application's data.
        //    // For example, retrieve content from a database based on the pageId.
        //    return $"This is the content of Page {pageId}";
        //}


        //rrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrr
        [HttpGet("GetrecordsPatient/{id:int}")]
        public async Task<ActionResult<MedicalRecords>> GetrecordsPatient(int id)
        {
            //var result = new List <MedicalRecords>();
            var record = _Context.records.Where(e => e.Idpatient == id).ToList();
            if (record != null)
            {
                return Ok(record);
            }
            return NotFound();

        }
        [HttpGet("GetPatientsByName/{name:alpha}")]
        public async Task<IActionResult> GetPatientsByName(string name)
        {
            Patients pat = await _Context.patients.FirstOrDefaultAsync(d => d.Name == name);

            if (pat != null)
            {
                return Ok(pat);
            }
            else
            {
                return NotFound("Patient Not Found");
            }
        }

        [HttpPost("PostPatients")]
        public async Task<ActionResult<Patients>> PostPatients(Patients pat)
        {
            _Context.patients.Add(pat);
            await _Context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPatientsbyId), new { id = pat.Id }, pat);
        }

        [HttpPut("PutPatients")]
        public async Task<ActionResult> PutPatients(int id, Patients pat)
        {
            //if (id != pat.Id)
            //{
            //    return BadRequest();
            //}
            _Context.Entry(pat).State = EntityState.Modified;
            try
            {
                await _Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!patientsExsite(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }

        private bool patientsExsite(int id)
        {
            return (_Context.patients?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        //[HttpGet("GetrecordsForPatient/{id:int}")]
        //public async Task<IActionResult> GetrecordsForPatient(int id)
        //{
        //    var record = _Context.records.Where(e => e.Idpatient == id).ToList();
        //    if (record != null && record.Count > 0)
        //    {
        //        // Generate the PDF content
        //        string pdfContent = GeneratePdfContent(record);

        //        // Return the PDF as a downloadable file
        //        var pdfBytes = Encoding.UTF8.GetBytes(pdfContent);
        //        return File(pdfBytes, "application/pdf", $"medical_records_patient_{id}.pdf");
        //    }
        //    return NotFound();
        //}

        //public string    GeneratePdfContent(List<MedicalRecords> records)
        //{
        //    // Create a new instance of PdfDocument
        //    PdfDocument document = new PdfDocument();

        //    // Add a page to the document
        //    PdfPage page = document.AddPage();
        //    XGraphics gfx = XGraphics.FromPdfPage(page);

        //    // Draw the content on the page
        //    XFont font = new XFont("Arial", 12);
        //    XTextFormatter textFormatter = new XTextFormatter(gfx);
        //    XRect rect = new XRect(50, 100, page.Width - 100, page.Height - 200);

        //    // Generate the content for the PDF
        //    StringBuilder pdfContentBuilder = new StringBuilder();
        //    foreach (var record in records)
        //    {
        //        // Customize how the records are formatted in the PDF
        //        pdfContentBuilder.AppendLine($"Record ID: {record.Id}");
        //        pdfContentBuilder.AppendLine($"Patient ID: {record.Idpatient}");
        //        pdfContentBuilder.AppendLine($"Date: {record.VisitDate}");
        //        pdfContentBuilder.AppendLine($"Description: {record.Treatment}");
        //        pdfContentBuilder.AppendLine($"Description: {record.Diagnosis}");
        //        pdfContentBuilder.AppendLine(); // Add an empty line between records
        //    }

        //    // Draw the generated content on the page
        //    textFormatter.DrawString(pdfContentBuilder.ToString(), font, XBrushes.Black, rect);

        //    // Save the PDF to a MemoryStream
        //    using (var stream = new System.IO.MemoryStream())
        //    {
        //        document.Save(stream, false);
        //        stream.Position = 0;

        //        // Return the PDF content as a string
        //        return Encoding.UTF8.GetString(stream.ToArray());
        //    }
        //}



        [HttpGet("GetrecordsForPatient/{id:int}")]
        public async Task<IActionResult> GetrecordsForPatient(int id)
        {
            var record = _Context.records.Where(e => e.Idpatient == id).ToList();
            if (record != null && record.Count > 0)
            {
                // Generate the PDF content
                string pdfContent = GeneratePdfContent(record);

                // Return the PDF as a downloadable file
                var pdfBytes = Encoding.UTF8.GetBytes(pdfContent);
                return File(pdfBytes, "application/pdf", $"medical_records_patient_{id}.pdf");
            }
            return NotFound();
        }

        private string GeneratePdfContent(List<MedicalRecords> records)
        {
            // Create a new instance of PdfDocument
            PdfDocument document = new PdfDocument();

            // Add a page to the document
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Draw the content on the page
            XFont font = new XFont("Arial", 12);
            XTextFormatter textFormatter = new XTextFormatter(gfx);
            XRect rect = new XRect(50, 100, page.Width - 100, page.Height - 200);

            // Generate the content for the PDF
            StringBuilder pdfContentBuilder = new StringBuilder();
            foreach (var record in records)
            {
                // Customize how the records are formatted in the PDF
                pdfContentBuilder.AppendLine($"Record ID: {record.Id}");
                pdfContentBuilder.AppendLine($"Patient ID: {record.Idpatient}");
                pdfContentBuilder.AppendLine($"Date: {record.VisitDate}");
                pdfContentBuilder.AppendLine($"Description: {record.Treatment}");
                pdfContentBuilder.AppendLine($"Description: {record.Diagnosis}");
                pdfContentBuilder.AppendLine(); // Add an empty line between records
            }

            // Draw the generated content on the page
            textFormatter.DrawString(pdfContentBuilder.ToString(), font, XBrushes.Black, rect);

            // Save the PDF to a MemoryStream
            using (var stream = new System.IO.MemoryStream())
            {
                document.Save(stream, false);
                stream.Position = 0;

                // Return the PDF content as a string
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }




        [HttpDelete("DeletePatients/{id:int}")]
        public async Task<ActionResult<Patients>> DeletePatients(int id)
        {
            if (_Context.patients == null)
            {
                return NotFound();
            }
            var pat = await _Context.patients.FindAsync(id);
            if (pat == null)
            {
                return NotFound();
            }
            _Context.patients.Remove(pat);
            await _Context.SaveChangesAsync();
            return Ok();
        }
    }
}
