using Course.Core.Helpers;
using System.Data;

namespace NattapongV3.Common
{
    public class FileHelper
    {
      
        private string rootPath;

        public FileHelper(IWebHostEnvironment env)
        {
            rootPath = Path.Combine(env.WebRootPath, "Files");
        }


        public async Task<string> Upload(IFormFile file, string targetFolder)
        {
            string outFileName = null;
            try
            {
                if (file != null && file.Length > 0)
                {
                    string targetPath = Path.Combine(rootPath, targetFolder);
                    bool pathExists = Directory.Exists(targetPath);
                    if (!pathExists)
                    {
                        Directory.CreateDirectory(targetPath);
                    }

                    string saveName = file.FileName.ToUniqueFileName().Replace(" ", "_");
                    string savePath = Path.Combine(targetPath, saveName);

                    using (var stream = File.Create(savePath))
                    {
                        await file.CopyToAsync(stream);
                    }
                    outFileName = saveName;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return outFileName;
        }

        public void Delete(string targetFolder, string deleteFileName)
        {
            try
            {
                string deletePath = Path.Combine(rootPath, targetFolder, deleteFileName);
                if (File.Exists(deletePath))
                {
                    File.Delete(deletePath);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
