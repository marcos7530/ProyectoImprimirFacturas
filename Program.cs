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

            // Informar cuántos archivos se van a procesar
            Console.WriteLine($"Se encontraron {pdfFiles.Length} archivos PDF en la carpeta {args[0]}.");
            Console.Write("¿Desea continuar con la creación del nuevo archivo PDF? (s/n): ");
            string respuesta = Console.ReadLine();

            if (respuesta.ToLower() != "s")
            {
                Console.WriteLine("Operación cancelada por el usuario.");
                return;
            }

            // Crear un nuevo documento PDF
            PdfDocument outputDocument = new PdfDocument();

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
                                // Agregar la primera página del documento actual al documento de salida
                                outputDocument.AddPage(document.Pages[0]);
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

            // Generar el nombre del archivo de salida basado en la fecha actual
            string outputFileName = $"PrimeraPagina_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
            string outputFilePath = Path.Combine(args[0], outputFileName);

            // Guardar el documento de salida
            outputDocument.Save(outputFilePath);
            Console.WriteLine($"El archivo PDF se ha guardado como {outputFilePath}.");
        }
        else
        {
            Console.WriteLine($"La carpeta {args[0]} no existe.");
        }
    }
}


