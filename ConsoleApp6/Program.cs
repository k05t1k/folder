using System.Runtime.ConstrainedExecution;
using System.Xml.Linq;

namespace ConsoleApp6
{
    internal class Program
    {
        static void CheckIsMaxPos(List<C_Elements> elements, C_Cursor cursor)
        {
            foreach (C_Elements elem in elements)
            {
                cursor.m_iMaxPos++;
                Console.WriteLine("  " + elem.m_strName);
            }
        }
        static void RenderElements(List<C_Elements> elements)
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in allDrives)
            {
                double totalSpace = drive.TotalSize;
                double availableSpace = drive.AvailableFreeSpace;
                string name = $"Диск {drive.Name}. Доступно {availableSpace} из {totalSpace}";
                elements.Add(new C_Elements(name, drive.Name, true));
            }
        }
        static void Main(string[] args)
        {
            string path = "";
            List<C_Elements> elements = new List<C_Elements>();

            while (true)
            {
                Console.WriteLine("Press escape for go to up!");
                if (path == "")
                    RenderElements(elements);
                else
                {
                    elements.Clear();
                    elements = C_Files.Get(path);
                }
                C_Cursor cursor = new C_Cursor(1, 0);
                CheckIsMaxPos(elements, cursor);
                bool cursor_position = true;
                while (cursor_position)
                {
                    Console.SetCursorPosition(0, cursor.m_iPos);
                    Console.Write(">");
                    ConsoleKey key = Console.ReadKey().Key;
                    switch (key)
                    {
                        case ConsoleKey.UpArrow:
                            Console.SetCursorPosition(0, cursor.m_iPos);
                            Console.Write(" ");
                            if (cursor.m_iPos != 1)
                                cursor.m_iPos--;
                            else
                                cursor.m_iPos = cursor.m_iMaxPos;
                            break;
                        case ConsoleKey.DownArrow:
                            Console.SetCursorPosition(0, cursor.m_iPos);
                            Console.Write(" ");
                            if (cursor.m_iPos != cursor.m_iMaxPos)
                                cursor.m_iPos++;
                            else
                                cursor.m_iPos = 1;
                            break;
                        case ConsoleKey.Enter:
                            cursor_position = false;
                            C_Elements elem = elements[cursor.m_iPos - 1];
                            if (elem.m_bFolder)
                            {
                                path = elem.m_strPath;
                                Console.Clear();
                            }
                            break;
                        case ConsoleKey.Escape:
                            while (true)
                            {
                                if (path[^1] != '\\')
                                    path = path.Remove(path.Length - 1);
                                else
                                    break;

                            }
                            Console.Clear();
                            cursor_position = false;
                            elements = C_Files.Get(path);
                            break;
                    }
                }
            }
        }
    }
}