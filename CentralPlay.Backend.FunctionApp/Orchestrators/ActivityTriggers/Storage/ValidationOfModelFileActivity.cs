using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using System.IO;

namespace CentralPlay.Backend.FunctionApp.Orchestrators.ActivityTriggers.Storage
{
    public class ValidationOfModelFileActivity
    {
        /// <summary>
        /// Validates the added blob as a Model for the Central Play
        /// </summary>
        /// <param name="blobPath">The content of the file</param>
        /// <param name="log">Logger</param>
        /// <returns>Indication of the validation</returns>
        [FunctionName(nameof(ValidationOfModelFileActivity))]
        public static bool Run([ActivityTrigger] string blobPath, ILogger log)
        {
            // TODO: Implement validation in a service
                // Checks if it's a LXF file

                 // Validates the content that it's formatted right (XML)

                // Checks if it's a LXMFL file

                // Checks the the file as a zip container
                    // Opens the file and checks for:
                    // The image file (thumbnail)
                    // The LXF file and validates the content that it's formatted right  (XML)

            // return a boolean true for a valid file and false for an unvalid file.
            return true;
        }
    }
}