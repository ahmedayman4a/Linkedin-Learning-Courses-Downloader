using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LLCD.DownloaderGUI
{
    class Downloader : IDisposable
    {
        private HttpClient _httpClient = new HttpClient();
        public void Dispose()
        {
            _httpClient.Dispose();
        }

        //https://github.com/dotnet/runtime/issues/31479#issuecomment-578436466
        /// <summary>
        /// Downloads a file from the specified Uri into the specified stream.
        /// </summary>
        /// <param name="cancellationToken">An optional CancellationToken that can be used to cancel the in-progress download.</param>
        /// <param name="progressCallback">If not null, will be called as the download progress. The first parameter will be the number of bytes downloaded so far, and the second the total size of the expected file after download.</param>
        /// <returns>A task that is completed once the download is complete.</returns>
        public async Task DownloadFileAsync(Uri uri, Stream toStream, CancellationToken cancellationToken = default, Action<long, long> progressCallback = null)
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));
            if (toStream == null)
                throw new ArgumentNullException(nameof(toStream));

            if (uri.IsFile)
            {
                using (Stream file = File.OpenRead(uri.LocalPath))
                {
                    if (progressCallback != null)
                    {
                        long length = file.Length;
                        byte[] buffer = new byte[4096];
                        int read;
                        int totalRead = 0;
                        while ((read = await file.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false)) > 0)
                        {
                            await toStream.WriteAsync(buffer, 0, read, cancellationToken).ConfigureAwait(false);
                            totalRead += read;
                            progressCallback(totalRead, length);
                        }
                        Debug.Assert(totalRead == length || length == -1);
                    }
                    else
                    {
                        await file.CopyToAsync(toStream, 4096, cancellationToken).ConfigureAwait(false);
                    }
                }


            }
            else
            {
                using (HttpResponseMessage response = await _httpClient.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false))
                {
                    if (progressCallback != null)
                    {
                        long length = response.Content.Headers.ContentLength ?? -1;
                        using (Stream stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                        {
                            byte[] buffer = new byte[16384];
                            int read;
                            int totalRead = 0;
                            while ((read = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false)) > 0)
                            {
                                await toStream.WriteAsync(buffer, 0, read, cancellationToken).ConfigureAwait(false);
                                totalRead += read;
                                progressCallback(totalRead, length);
                            }
                            Debug.Assert(totalRead == length || length == -1);
                        }
                    }
                    else
                    {
                        await response.Content.CopyToAsync(toStream).ConfigureAwait(false);
                    }
                }
            }
        }
    }
}