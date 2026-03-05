using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CukCuk.Backend.Core.Interfaces.File
{
    /// <summary>
    /// interface lưu trữ file
    /// </summary>
    /// Created by Phuong 26/02/2026
    public interface IFileStorageService
    {
        /// <summary>
        /// Lưu file
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="fileName"></param>
        /// <param name="folder"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> SaveFileAsync(
            Stream fileStream,
            string fileName,
            string folder,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Xóa file
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        Task DeleteFileAsync(string filePath);
    }
}
