namespace BackEndFinal.Extensions
{
    public static class Extension
    {
        public static bool CheckContentType(this IFormFile file)
        {
            return file.ContentType.Contains("image/");
        }
        public static bool CheckSize(this IFormFile file, int size)
        {
            return file.Length <= size * 1024;
        }
        public static async Task<string> SaveFile(this IFormFile file)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",  "img", fileName);

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return fileName;
        }
        public static void DeleteFile(this string fileName)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "admin", "images", fileName);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
