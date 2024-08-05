﻿using Microsoft.AspNetCore.Http;
using Course.Core.Helpers;
using System.Web.Mvc;

namespace MinitestTN.Common
{

    public class FileHelper
    {
        //private readonly string rootPath;
        private readonly IWebHostEnvironment env;

        public FileHelper(IWebHostEnvironment env)
        {
            //rootPath = Path.Combine(env.WebRootPath, "Files");
            this.env = env;
        }

        public async Task<string> Upload(IFormFile file, string targetFolder)
        {
            string outFileName = "";
            try
            {
                if (file != null && file.Length > 0)
                {
                    string targetPath = Path.Combine(env.WebRootPath, targetFolder);
                    bool pathExists = Directory.Exists(targetPath);
                    if (!pathExists)
                    {
                        Directory.CreateDirectory(targetPath);
                    }

                    string nameFime = Path.GetFileNameWithoutExtension(file.FileName);
                    //string saveName = file.FileName.ToUniqueFileName().Replace(" ", "_");
                    //string savePath = Path.Combine(targetPath, saveName);
                    string key = DateTime.Now.ToString("yyMMddHHmmssff");
                    string ext = Path.GetExtension(file.FileName);

                    outFileName = $"{nameFime}_{key}{ext}";

                    string savePath = Path.Combine(targetPath, outFileName);

                    using (var stream = File.Create(savePath))
                    {
                        await file.CopyToAsync(stream);
                    }

                    //return outFileName;
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
                if (deleteFileName != null)
                {
                    string daletePath = Path.Combine(env.WebRootPath, targetFolder, deleteFileName);

                    if (File.Exists(daletePath))
                    {
                        File.Delete(daletePath);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}




































//public class FileHelper
//{
//    private readonly IWebHostEnvironment env;

//    public FileHelper(IWebHostEnvironment env)
//    {
//        this.env = env;
//    }
//    public async Task<string> Upload(IFormFile file, string folder)
//    {
//        string OutFileName = "";

//        string targetPath = Path.Combine(env.WebRootPath, folder);

//        if (!Directory.Exists(targetPath))
//        {
//            Directory.CreateDirectory(targetPath); // สร้าง Folder
//        }

//        string nameFime = Path.GetFileNameWithoutExtension(file.FileName);
//        string key = DateTime.Now.ToString("yyMMddHHmmssff");
//        string ext = Path.GetExtension(file.FileName);


//        //OutFileName = nameFime + "_" + DateTime.Now.ToString("yyMMddHHmmssff")+ ext;

//        OutFileName = $"{nameFime}_{key}{ext}";

//        string savePath = Path.Combine(targetPath, OutFileName);

//        using (var stream = File.Create(savePath))
//        {
//            await file.CopyToAsync(stream);
//        }

//        return OutFileName;
//    }

//    public void Delete(string folder, string fileName)
//    {
//        if (fileName != null)
//        {
//            string daletePath = Path.Combine(env.WebRootPath, folder, fileName);

//            if (File.Exists(daletePath))
//            {
//                File.Delete(daletePath);
//            }
//        }
//    }
//}

