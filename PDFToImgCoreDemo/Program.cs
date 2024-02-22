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
            String fileName = "ImagePDF.pdf";
            ConvertPdfToImage(fileName, ImageFileType.Jpg);
        }
        private static void ConvertPdfToImage(string fileName, ImageFileType  outputFileType)
        {
            // 获取当前应用程序目录
            //String currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            // 源文件路径
            //String sourcePath = Path.Combine(currentDirectory, fileName);
            // 在当前应用程序目录下创建 convertedimgs 文件夹
            //String targetPath = Path.Combine(currentDirectory, "convertedimgs");

            // 上三级目录
            String parentDirectory = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).FullName).FullName).FullName).FullName;
            // 输入文件的路径
            String sourcePath = Path.Combine(parentDirectory, "sourcepdf", fileName);
            // 输出文件夹的路径
            String targetPath = Path.Combine(parentDirectory, "convertedimgs");

            // 检查目录是否存在，如果不存在则创建
            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }

            // 输出文件模板
            string outputFileTemplate = Path.Combine(targetPath, "converted-page-{0}.jpg");
            SavePageStream getPageStream = page => new FileStream(string.Format(outputFileTemplate, page), FileMode.Create);

            try
            {
                // 使用 GroupDocs.Conversion 进行转换
                using (Converter converter = new Converter(sourcePath))
                {
                    ImageConvertOptions options = new ImageConvertOptions
                    {
                        Format = outputFileType
                    };

                    converter.Convert(getPageStream, options);

                    Console.WriteLine("转换成功！生成的图像文件位于: " + targetPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("转换过程中发生异常: " + ex.Message);
            }
        }
    }
}
