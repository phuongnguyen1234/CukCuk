using CukCuk.Backend.Core.Interfaces.File;

namespace CukCuk.Backend.Infrastructure.File
{
    /// <summary>
    /// Dịch vụ lưu trữ tệp tin cục bộ, sử dụng hệ thống tệp của máy chủ để lưu trữ và quản lý tệp tin.
    /// </summary>
    /// Created by Phuong 26/02/2026
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly string _webRootPath;

        public LocalFileStorageService(string webRootPath)
        {
            _webRootPath = webRootPath;
        }

        /// <summary>
        /// Lưu file vào local storage và trả về đường dẫn truy cập file sau khi lưu thành công.
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="fileName"></param>
        /// <param name="folder"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<string> SaveFileAsync(
            Stream fileStream,
            string fileName,
            string folder,
            CancellationToken cancellationToken = default)
        {
            var uploadsFolder = Path.Combine(_webRootPath, folder);

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using var output = new FileStream(filePath, FileMode.Create);
            await fileStream.CopyToAsync(output, cancellationToken);

            return $"/{folder}/{uniqueFileName}";
        }

        /// <summary>
        /// Xóa file khỏi local storage dựa trên đường dẫn file đã lưu. Nếu file không tồn tại, phương thức sẽ không thực hiện hành động nào.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public Task DeleteFileAsync(string filePath)
        {
            var fullPath = Path.Combine(_webRootPath, filePath.TrimStart('/'));

            if (System.IO.File.Exists(fullPath))
                System.IO.File.Delete(fullPath);

            return Task.CompletedTask;
        }
    }
}
