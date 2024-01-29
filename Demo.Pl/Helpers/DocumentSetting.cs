using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Demo.Pl.Helpers
{
    public static class DocumentSetting
    {
        public static string Uploadfile(IFormFile File, string FolderName)
        {
            // string FolderPath = " C:\\Users\\lenovo\\Downloads\\G04 Demo Solution\\Demo.Pl\\wwwroot\\Files\\";
            //  string FolderPath= Directory .GetCurrentDirectory()+"\\wwwroot\\Files\\" + FolderName;
            string FolderPath=Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName);
       string FileName=$"{Guid.NewGuid()}{File.FileName}" ;
            string FilePath=Path.Combine(FolderPath, FileName);
          using  var fs=new FileStream(FilePath,FileMode.Create);

            File.CopyTo(fs);
            return FileName;
}
       



        public static void Delete(string FileName, string FolderName)
        {
            if (FileName is not null && FolderName is not null)
            {

                 string Filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName, FileName);
            if (File.Exists(Filepath))
           
                File.Delete(Filepath);



            }
               
            



        }







    }
}
