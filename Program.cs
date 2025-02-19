using System;
using System.IO;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

class Program
{
    static void Main(string[] args)
    {
        // Hacer la variable args como ruta de acceso a la carpeta
        args = new string[] { @"C:\Users\marco\OneDrive\Escritorio\Impresion" };

        // Verificar si la carpeta existe
        if (Directory.Exists(args[0]))
        {
            // Obtener todos los archivos PDF en la carpeta
            string[] pdfFiles = Directory.GetFiles(args[0], "*.pdf");

            // Recorrer todos los archivos PDF
            foreach (var filePath in pdfFiles)
            {
                if (File.Exists(filePath))
                {
                    try
                    {
                        using (PdfDocument document = PdfReader.Open(filePath, PdfDocumentOpenMode.Import))
                        {
                            if (document.PageCount > 0)
                            {
                                PdfDocument singlePageDocument = new PdfDocument();
                                singlePageDocument.AddPage(document.Pages[0]);

                                PrintDocument(singlePageDocument);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error al procesar el archivo {filePath}: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine($"El archivo {filePath} no existe.");
                }
            }
        }
        else
        {
            Console.WriteLine($"La carpeta {args[0]} no existe.");
        }
    }

    static void PrintDocument(PdfDocument document)
    {
        string tempFilePath = Path.Combine(Path.GetTempPath(), "tempDocument.pdf");
        document.Save(tempFilePath);

        // Aquí puedes usar una herramienta externa para imprimir el archivo PDF
        // Por ejemplo, puedes usar el comando de impresión de Windows:
        System.Diagnostics.Process.Start("cmd.exe", $"/c start /min Acrobat.exe /t \"{tempFilePath}\"");
    }
}

