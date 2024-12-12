namespace Application.Utils
{
    public class FileUtils
    {
        private string DirectoryPath { get; set; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Downloads");

        public FileUtils()
        {
            CreateDirectory();
        }

        private void CreateDirectory()
        {
            if(!Directory.Exists(DirectoryPath)) Directory.CreateDirectory(DirectoryPath);
        }

        public async Task CreateFilePng(string fileName, byte[] content)
        {
            await File.WriteAllBytesAsync(Path.Combine(DirectoryPath, $"{fileName}.png"), content);
        } 

        public async Task<byte[]> ReadAllBytesPng(string fileName)
        {
            return await File.ReadAllBytesAsync(Path.Combine(DirectoryPath, $"{fileName}.png"));
        }

        public string GetFilePathPng(string fileName)
        {
            return Path.Combine(DirectoryPath, $"{fileName}.png");
        }

        public bool ExistsFilePng(string fileName)
        {
            return File.Exists(Path.Combine(DirectoryPath, $"{fileName}.png"));
        }
    }
}
