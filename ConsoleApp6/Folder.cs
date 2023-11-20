using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    public class C_Cursor
    {
        public int m_iPos;
        public int m_iMaxPos;
        public C_Cursor(int pos, int max_pos) 
        {
            m_iPos = pos;
            m_iMaxPos = max_pos;
        }
    }

    public class C_Elements
    {
        public C_Elements(string name, string path, bool folder)
        {
            m_strName = name;
            m_strPath = path;
            m_bFolder = folder;
        }

        public string m_strName;
        public string m_strPath;
        public bool m_bFolder;
    }
    public static class C_Files
    {
        public static List<C_Elements> Get(string path)
        {
            List<C_Elements> elements = new List<C_Elements>();

            foreach (string file in Directory.GetFiles(path))
                elements.Add(new C_Elements(file.Split("\\")[^1], file, false));
            foreach (string file in Directory.GetDirectories(path))
                elements.Add(new C_Elements(file.Split("\\")[^1], file, true));

            return elements;
        }
    }
}
