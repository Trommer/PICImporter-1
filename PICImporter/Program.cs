using System.Drawing;
using System.Drawing.Imaging;

namespace PICImporter
{
    class Program
    {
        
        private static string ExifDate(Image image)
        {
            PropertyItem[] items = image.PropertyItems;
            foreach (PropertyItem pi in items)
            {
                if (pi.Id == 306)
                {
                    string val =
                        System.Text.Encoding.Default.GetString
                            (pi.Value);
                    return val;
                }
            }
            return "n/a";
        }

        static void Main(string[] args)
        {
            string UserFolder = "C:\\Users\\TSE\\Pictures\\";

            foreach (string pic in args)
            {
                string oldName = pic;
                Image newImage = Image.FromFile(oldName);           
                string PicDate = ExifDate(newImage);
                PicDate = PicDate.Replace(":", "-");
                PicDate = PicDate.Replace(" ", "_");

                PicDate = PicDate.Substring(0, PicDate.Length - 1);

                string year = PicDate.Substring(0,4);
                if (!System.IO.Directory.Exists(UserFolder + year))
                {
                    System.IO.Directory.CreateDirectory(UserFolder + year);
                    
                }

                string month = PicDate.Substring(5,2);
                if (!System.IO.Directory.Exists(UserFolder + year + "\\" + month))
                {
                    System.IO.Directory.CreateDirectory(UserFolder + year + "\\" + month);
                }

                string day = PicDate.Substring(8,2);
                if (!System.IO.Directory.Exists(UserFolder + year+"\\"+month+"\\"+day))
                {
                    System.IO.Directory.CreateDirectory(UserFolder + year+"\\"+month+"\\"+day);
                }

                string newName = UserFolder + year + "\\" + month + "\\" + day + "\\" + PicDate + ".jpg";

                try
                {
                    System.IO.File.Copy(oldName, newName);

                }
                catch
                {
                    System.Console.WriteLine("Fehler bei: " + pic);
                }
                
                System.Console.WriteLine(oldName + "->" + newName);
            }

            System.Console.WriteLine("FERTIG");
            System.Console.ReadKey();

        }

    }
}
