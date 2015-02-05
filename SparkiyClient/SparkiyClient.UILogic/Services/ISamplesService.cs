using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography.Core;
using Windows.Storage;
using MetroLog;
using SparkiyClient.Common.Helpers;

namespace SparkiyClient.UILogic.Services
{
    public interface ISamplesService
    {
        Task GetSamplesAsync();
    }

    public class SamplesService : ISamplesService
    {
        private static readonly ILogger Log = LogManagerFactory.DefaultLogManager.GetLogger<SamplesService>();

        private const string SamplesSourceUrl = "https://github.com/Sparkiy/sparkiy-projects/archive/master.zip";
        private const string SamplesPath = @"sparkiy-projects-master/Samples";

        private readonly IStorageService storageService;


        public SamplesService(IStorageService storageService)
        {
            this.storageService = storageService;
        }


        public async Task GetSamplesAsync()
        {
            HttpClient client = new HttpClient();

            // Request the data 
            HttpResponseMessage responseMessage = await client.GetAsync(
                new Uri(SamplesSourceUrl, UriKind.Absolute),
                HttpCompletionOption.ResponseHeadersRead);

            // Get the size of the content
            long? contentLength = responseMessage.Content.Headers.ContentLength;

            // Create stream to store data
            using (var mstream = new MemoryStream())
            {
                // Read the content into the stream
                int totalNumberOfBytesRead = 0;
                using (var responseStream = await responseMessage.Content.ReadAsStreamAsync())
                {
                    int numberOfReadBytes;
                    do
                    {
                        // Read a data block into the buffer
                        const int bufferSize = 1048576; // 1MB
                        byte[] responseBuffer = new byte[bufferSize];
                        numberOfReadBytes = await responseStream.ReadAsync(
                            responseBuffer, 0, responseBuffer.Length);
                        totalNumberOfBytesRead += numberOfReadBytes;

                        // Write the data block into the file stream
                        mstream.Write(responseBuffer, 0, numberOfReadBytes);

                        // Calculate the progress
                        if (contentLength.HasValue)
                        {
                            // Calculate the progress
                            double progressPercent = (totalNumberOfBytesRead/(double) contentLength)*100;

                            // Display the progress
                            Log.Debug("\t{0}% done", progressPercent);
                        }
                        else
                        {
                            // Just display the read bytes   
                            Log.Debug("\t{0} bytes done", totalNumberOfBytesRead);
                        }
                    } while (numberOfReadBytes != 0);
                }

                // Unzip package
                using (var archive = new ZipArchive(mstream, ZipArchiveMode.Read))
                {
                    // Go through all items in package
                    foreach (var entry in archive.Entries)
                    {
                        // Only process files that are in /Sample/ folder path
                        if (entry.FullName.StartsWith(SamplesPath) && Path.HasExtension(entry.FullName))
                        {
                            // Get new file path and ensure folder structure is ready
                            var newFilePath = Path.Combine(this.storageService.WorkspaceFolder.Path, entry.FullName.Replace(SamplesPath, ""));
                            var folder = await this.EnsureFolderExists(newFilePath);

                            // Open zipped item 
                            using (var fstream = entry.Open())
                            {
                                // Save zipped item to the file
                                await this.storageService.SaveFileSafeAsync(
                                    folder, 
                                    Path.GetFileName(newFilePath),
                                    async file =>
                                    {
                                        // Open destination file as a stream
                                        using (var deststream = await file.OpenStreamForWriteAsync())
                                        {
                                            // Copy content to destination stream and flush
                                            await fstream.CopyToAsync(deststream);
                                            await deststream.FlushAsync();
                                            Log.Debug("Saved entry \"{0}\"", entry.FullName);
                                        }
                                    });
                            }
                        }
                    }
                }
            }
        }

        private async Task<StorageFolder> EnsureFolderExists(string path)
        {
            var folderPath = Path.GetDirectoryName(path);
            var currentFolder = this.storageService.WorkspaceFolder;
            foreach (var innerFolder in folderPath.Replace(this.storageService.WorkspaceFolder.Path, "").Split(new[] { '\\' }, StringSplitOptions.RemoveEmptyEntries))
            {
                await currentFolder.EnsureFolderExistsAsync(innerFolder);
                currentFolder = await currentFolder.GetFolderAsync(innerFolder);
            }

            return currentFolder;
        }
    }
}
