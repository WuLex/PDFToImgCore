using System;
using System.IO;
using GroupDocs.Conversion;
using GroupDocs.Conversion.Contracts;
using GroupDocs.Conversion.FileTypes;
using GroupDocs.Conversion.Options.Convert;

namespace PDFToImgCoreDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            String fileName = "Convention-de-stage.pdf";
            ConvertPdfToImage(fileName, ImageFileType.Jpg);
        }
        private static void ConvertPdfToImage(string fileName, ImageFileType  outputFileType)
        {
            String sourcePath = @"D:\";
            String targetPath = @"D:\";

            
            string outputFileTemplate = Path.Combine(targetPath, "converted-page-{0}.jpg");
            SavePageStream getPageStream = page => new FileStream(string.Format(outputFileTemplate, page), FileMode.Create);
            using (Converter converter = new Converter(sourcePath + fileName))
            {
                ImageConvertOptions options = new ImageConvertOptions
                {
                    Format = ImageFileType.Jpg
                };

                converter.Convert(getPageStream, options);
            }

        }
    }
}
