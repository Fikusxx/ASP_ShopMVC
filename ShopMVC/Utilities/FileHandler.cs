
namespace ShopMVC_Utility
{
    public static class FileHandler
    {
        private static readonly string imagePath = "images/product/";

        /// <summary>
        /// Extracts a file, creates a unique file name for it, 
        /// writes it to the web root directory and returns it's name
        /// </summary>
        public static async Task<string> ProcessUploadedFile(IFormFile file, IWebHostEnvironment hostingEnvironment)
        {
            string uniqueFileName = null;

            if (file != null)
            {
                // получаем путь до папки с картинками в wwwroot
                var uploadFolder = Path.Combine(hostingEnvironment.WebRootPath, imagePath);

                // получаем имя загружженого файла и соединяем его с guid
                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;

                // получаем конечный путь/название для файла в строку
                var filePath = Path.Combine(uploadFolder, uniqueFileName);

                // создаем(копируем) файл(картинку) в папку wwwroot/images/imagename
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fs);
                }
            }

            return uniqueFileName;
        }
    }
}
