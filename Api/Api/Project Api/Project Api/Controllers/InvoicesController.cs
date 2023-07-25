using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PdfSharpCore.Drawing.Layout;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using Project_Api.DataContext;
using Project_Api.Models;
using Microsoft.AspNetCore.Hosting;
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
    public class InvoicesController : ControllerBase
    {
        private readonly Context _Context;
     

        //  private static PdfService pdfService;
        // private readonly IConverter pdfConverter;
        private readonly PdfService _pdfService;

        public InvoicesController(PdfService pdfService ,Context Context)
        {
            _pdfService = pdfService;
            _Context = Context;


        }
        [HttpGet("generatepdf")]
        public async Task<ActionResult> GeneratePdf(string invoiceId, string status, decimal amount, DateTime invoiceDate,
                                                     string patientId, string patientName, string patientGender,
                                                     string employeeId, string employeeName, string employeePosition)
        {
            PdfDocument document = new PdfDocument();

            // Add a page to the document
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Draw some content on the page
            XFont font = new XFont("Arial", 12);
            XTextFormatter textFormatter = new XTextFormatter(gfx);
            XRect rect = new XRect(50, 100, page.Width - 100, page.Height - 200);

            string htmlContent = $@"
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


        [HttpPost("generate")]
        public IActionResult GenerateInvoicePdf()
        {
            // Example HTML content for the invoice
            string htmlContent = @"
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
    <p><strong>Invoice ID:</strong> @Model.Id</p>
    <p><strong>Status:</strong> @Model.Status</p>
    <p><strong>Amount:</strong> @Model.Amount</p>
    <p><strong>Invoice Date:</strong> @Model.inviocDate.ToString(""dd/MM/yyyy"")</p>

    <h4>Patient Information</h4>
    <p><strong>Patient ID:</strong> @Model.Idpatient</p>
    <p><strong>Patient Name:</strong> @Model.patients.Name</p>
    <p><strong>Patient Gender:</strong> @Model.patients.Gender</p>
    <!-- Add other patient properties as needed -->

    <h4>Employee Information</h4>
    <p><strong>Employee ID:</strong> @Model.IdEmp</p>
    <p><strong>Employee Name:</strong> @Model.employee.Name</p>
    <p><strong>Employee Position:</strong> @Model.employee.Position</p>
    <!-- Add other employee properties as needed -->

    <!-- Add more details about the invoice as needed -->
</div>

<div>
    <a asp-action=""Index"" class=""btn btn-secondary"">Back to Invoice List</a>
</div>

                </body>
                </html>
            ";

            var pdfBytes = _pdfService.GenerateInvoicePdf(htmlContent);
            return File(pdfBytes, "application/pdf", "Invoice.pdf");
        }

        [HttpGet("generatenew")]
        public async Task<ActionResult> GeneratePdfnew()
        {
            PdfDocument document = new PdfDocument();

            // Add a page to the document
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
           
            // Draw some content on the page
            XFont font = new XFont("Arial", 12);
            XTextFormatter textFormatter = new XTextFormatter(gfx);
            XRect rect = new XRect(50, 100, page.Width - 100, page.Height - 200);
            textFormatter.DrawString("Hello, this is a generated PDF using PdfSharpCore!", font, XBrushes.Black, rect);

            // Save the PDF to a MemoryStream
            using (var stream = new System.IO.MemoryStream())
            {
                document.Save(stream, false);
                stream.Position = 0;

                // Return the PDF as a FileContentResult
                return File(stream.ToArray(), "application/pdf", "generated_pdf.pdf");
            }
        }




        [HttpGet("Getall")]
        public async Task<ActionResult<IEnumerable<Invoices>>> Getinvoice()
        {
            if (_Context.invoices == null)
            {
                return NotFound();
            }
            return await _Context.invoices.ToListAsync();
        }
        [HttpGet("GetInvoicesId/{id:int}")]
        public async Task<ActionResult<Invoices>> GetinvoicbyId(int id)
        {
            if (_Context.invoices == null)
            {
                return NotFound();
            }
            var invoic = await _Context.invoices.FindAsync(id);
            if (invoic == null)
            {
                return NotFound();
            }
            return invoic;
        }
        [HttpPost("PostInvoices")]
        public async Task<ActionResult<Invoices>> Postappoint(Invoices invoice)
        {
            _Context.invoices.Add(invoice);
            await _Context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetinvoicbyId), new { id = invoice.Id }, invoice);
        }
        [HttpPut("PutInvoices")]
        public async Task<ActionResult> Putinvoic(Invoices invoice)
        {
            //if (id != invoice.Id)
            //{
            //    return BadRequest();
            //}
            _Context.Entry(invoice).State = EntityState.Modified;
            try
            {
                await _Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!invoiceExsite(invoice.Id))
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

        private bool invoiceExsite(int id)
        {
            return (_Context.invoices?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        [HttpDelete("DeleteInvoices/{id:int}")]
        public async Task<ActionResult<Invoices>> Deleteinvoice(int id)
        {
            if (_Context.invoices == null)
            {
                return NotFound();
            }
            var invoice = await _Context.invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            _Context.invoices.Remove(invoice);
            await _Context.SaveChangesAsync();
            return Ok();
        }
    }
}
