using System;
using DinkToPdf;
using DinkToPdf.Contracts;
using System.IO;

namespace Project_Api.Controllers
{


    public class Startup
    {
        // ...

        public void ConfigureServices(IServiceCollection services)
        {
            // Other service registrations...

            services.AddScoped<PdfService>(); // Add the PdfService class as a scoped service.
        }

        // ...
    }


}
