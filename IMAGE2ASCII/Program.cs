using System;
using System.Drawing;
using System.IO;

namespace IMAGE2ASCII
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This program creates ascii art from an image.");
            Console.WriteLine("The format should be like this:");
            Console.WriteLine("path row_size column_size");
            Console.WriteLine(@"Example: C:\Users\User\Desktop\teker.jpg 40 25");
            Console.WriteLine("");

            string input = Console.ReadLine().Replace("\n", "");
            String[] inputArr = input.Split(' ');

            if (inputArr.Length != 3)
            {
                Console.WriteLine("Wrong format! You entered wrong amount of input."); 
                return;
            }

            string imagePath = inputArr[0].Replace("\"", "");

            try
            {
                Bitmap image = new Bitmap(imagePath);

                int row, col;

                if (int.TryParse(inputArr[1], out row))
                {
                    if (int.TryParse(inputArr[2], out col))
                    {
                        string st = PrintImageToConsole(image, row, col);

                        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); ;
                        string filePath = Path.Combine(desktopPath, "text.txt");
                        File.WriteAllText(filePath, st);

                        Console.WriteLine("\n" + st);
                    }
                    else
                    {
                        Console.WriteLine("Wrong format! Check your column value.");
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("Wrong format! Check your row value.");
                    return;
                }
            }
            catch (System.ArgumentException ex)
            {
                Console.WriteLine("Wrong format! You entered wrong path name. ");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something unexpected happened.");
            }

            Console.ReadLine();
        }

        static string PrintImageToConsole(Bitmap image, int row, int column)
        {
            int width = image.Width; int height = image.Height;
            row -= 1; column -= 1;

            string writtenLine = "";

            for (int y = 0; y < height; y += height/column)
            {
                for (int x = 0; x < width; x += width/row)
                {
                    Color pixelColor = image.GetPixel(x, y);

                    int brightness = GetBrightness(pixelColor);

                    if (brightness > 230)
                        writtenLine += "@";
                    else if (brightness > 205)
                        writtenLine += "$";
                    else if (brightness > 180)
                        writtenLine += "#";
                    else if (brightness > 155)
                        writtenLine += "*";
                    else if (brightness > 130)
                        writtenLine += "!";
                    else if (brightness > 105)
                        writtenLine += "+";
                    else if (brightness > 80)
                        writtenLine += ":";
                    else if (brightness > 55)
                        writtenLine += "~";
                    else if (brightness > 30)
                        writtenLine += "-";
                    else if (brightness > 5)
                        writtenLine += ".";
                    else if (brightness >= 0)
                        writtenLine += " ";

                }
                writtenLine += "\n";
            }
            return writtenLine;
        }

        static int GetBrightness(Color color)
        {
            int mean = (int)(0.2126 * color.R + 0.7152 * color.G + 0.0722 * color.B);
            return mean;
        }
    }
}
