using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCard.BAL.Services;
using HealthCard.Entity.Models;
using HealthCard.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using HealthCard.Entity.ViewModels;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using HealthCard.Common;

namespace HealthCard.BAL.Repositories
{
    public class UserService : IUserService
    {

        
        private readonly IConfiguration _configuration;

        private IHostingEnvironment _hostingEnv;

        public UserService(IConfiguration configuration, IHostingEnvironment env)
        {
            _configuration = configuration;
            _hostingEnv= env;

         
        }

        public async Task<UserVM> CreateAppUserAsync(User appUser)
        {

            var userHID = await GetRandomHIDUsingGUID(appUser.Guid);

            var ProfileImage = await Processimage(appUser.ProfileImage);

            var ProfileQR = await CreateQRCode(userHID);
            UserVM iModel = new UserVM()
            {
                FirstName = appUser.FirstName,
                MiddleName = appUser.MiddleName,
                LastName = appUser.LastName,
                ProfileImagePath = ProfileImage,
                PhoneNumber = appUser.PhoneNumber,
                HealthID = userHID,
                BirthDate = appUser.BirthDate,
                PHRAddress= appUser.PHRAddress,
                Gender= appUser.Gender,
                QRCode = ProfileQR,



            };
            return iModel;



        }

        public async Task<string> GetRandomHIDUsingGUID(Guid guid)
        {
            // Get the GUID
            string guidResult = guid.ToString();

            guidResult = guidResult.Replace("-", string.Empty);

            string numberOnly = Regex.Replace(guidResult, "[^0-9.]", "");

            // Remove the hyphens
          
            int length = numberOnly.Length;
            // Make sure length is valid
            if (length <= 0 || length > numberOnly.Length)
                throw new ArgumentException("Length must be between 1 and " + numberOnly.Length);

            // Return the first length bytes
            return numberOnly.Substring(0, length);
        }

        public async Task<string> Processimage(IFormFile upload)
        {
            var FileDic = "Files";

            string FilePath = Path.Combine(_hostingEnv.WebRootPath, FileDic);

            if (!Directory.Exists(FilePath))

                Directory.CreateDirectory(FilePath);

            var fileName = upload.FileName;

            var filePath = Path.Combine(FilePath, fileName);

            var Imagepath = Path.Combine("Files", fileName);

            using (FileStream fs = File.Create(filePath))

            {

                upload.CopyTo(fs);

            }
            return Imagepath;
        }


        public async Task<string> CreateQRCode(string HealthcardID)
        {

            QRCodeGenerator QrGenerator = new QRCodeGenerator();
            QRCodeData QrCodeInfo = QrGenerator.CreateQrCode(HealthcardID, QRCodeGenerator.ECCLevel.Q);
            QRCode QrCode = new QRCode(QrCodeInfo);
            Bitmap QrBitmap = QrCode.GetGraphic(60);
            byte[] BitmapArray = QrBitmap.BitmapToByteArray();
            string QrUri = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(BitmapArray));
            return QrUri;

            

        }

       

    }
   
}
