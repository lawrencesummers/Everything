namespace WHC.OrderWater.Commons
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class FileDialogHelper
    {
        private const string string_0 = "配置文件(*.cfg)|*.cfg|All File(*.*)|*.*";
        private static string string_1 = "All File(*.*)|*.*";
        private static string string_10 = "XML文件(*.xml)|*.xml|All files (*.*)|*.*";
        private static string string_11 = "Rar(*.rar)|*.rar|All files (*.*)|*.*";
        private static string string_12 = "Sqlite数据库文件(*.db)|*.db|All files (*.*)|*.*";
        private static string string_2 = "Word(*.doc)|*.doc|All File(*.*)|*.*";
        private static string string_3 = "Excel(*.xls)|*.xls|All File(*.*)|*.*";
        private static string string_4 = "PDF(*.pdf)|*.pdf|All File(*.*)|*.*";
        private static string string_5 = "Image Files(*.BMP;*.bmp;*.JPG;*.jpg;*.GIF;*.gif;*.png;*.PNG)|(*.BMP;*.bmp;*.JPG;*.jpg;*.GIF;*.gif;*.png;*.PNG)|All File(*.*)|*.*";
        private static string string_6 = "HTML files (*.html;*.htm)|*.html;*.htm|All files (*.*)|*.*";
        private static string string_7 = "Access(*.mdb)|*.mdb|All File(*.*)|*.*";
        private static string string_8 = "Zip(*.zip)|*.zip|Rar(*.rar)|*.rar|All files (*.*)|*.*";
        private static string string_9 = "(*.txt)|*.txt|All files (*.*)|*.*";

        private FileDialogHelper()
        {
        }

        public static string Open(string title, string filter)
        {
            return Open(title, filter, string.Empty);
        }

        public static string Open(string title, string filter, string filename)
        {
            return Open(title, filter, filename, null);
        }

        public static string Open(string title, string filter, string filename, string initialDirectory)
        {
            OpenFileDialog dialog = new OpenFileDialog {
                Filter = filter,
                Title = title,
                RestoreDirectory = true,
                FileName = filename
            };
            if (!string.IsNullOrEmpty(initialDirectory))
            {
                dialog.InitialDirectory = initialDirectory;
            }
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.FileName;
            }
            return string.Empty;
        }

        public static string OpenAccessDb()
        {
            return Open("数据库还原", string_7);
        }

        public static string OpenBakDb(string file)
        {
            return Open("数据库还原", "Access(*.bak)|*.bak", file);
        }

        public static string OpenConfig()
        {
            return Open("配置文件还原", "配置文件(*.cfg)|*.cfg|All File(*.*)|*.*");
        }

        public static string OpenDir()
        {
            return OpenDir(string.Empty);
        }

        public static string OpenDir(string selectedPath)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog {
                Description = "请选择路径",
                SelectedPath = selectedPath
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.SelectedPath;
            }
            return string.Empty;
        }

        public static string OpenExcel()
        {
            return Open("Excel选择", string_3);
        }

        public static string OpenExcel(bool multiselect)
        {
            return OpenExcel(multiselect, "");
        }

        public static string OpenExcel(bool multiselect, string fileName)
        {
            return OpenExcel(multiselect, fileName, null);
        }

        public static string OpenExcel(bool multiselect, string fileName, string initialDirectory)
        {
            if (multiselect)
            {
                return OpenMultiselect("选择多个Excel文件", string_3, fileName, initialDirectory);
            }
            return Open("Excel选择", string_3, fileName, initialDirectory);
        }

        public static string OpenFile()
        {
            return Open("打开文件", string_1);
        }

        public static string OpenFile(bool multiselect)
        {
            return OpenFile(multiselect, "");
        }

        public static string OpenFile(bool multiselect, string fileName)
        {
            return OpenFile(multiselect, fileName, null);
        }

        public static string OpenFile(bool multiselect, string fileName, string initialDirectory)
        {
            if (multiselect)
            {
                return OpenMultiselect("打开多个文件", string_1, fileName, initialDirectory);
            }
            return Open("打开文件", string_1, fileName, initialDirectory);
        }

        public static string OpenHtml()
        {
            return Open("Html选择", string_6);
        }

        public static string OpenHtml(bool multiselect)
        {
            if (multiselect)
            {
                return OpenMultiselect("选择多个Html文件", string_6, "");
            }
            return Open("Html选择", string_6);
        }

        public static string OpenImage()
        {
            return Open("图片选择", string_5);
        }

        public static string OpenImage(bool multiselect)
        {
            return OpenImage(multiselect, "");
        }

        public static string OpenImage(bool multiselect, string fileName)
        {
            return OpenImage(multiselect, fileName, null);
        }

        public static string OpenImage(bool multiselect, string fileName, string initialDirectory)
        {
            if (multiselect)
            {
                return OpenMultiselect("选择多个图片", string_5, fileName, initialDirectory);
            }
            return Open("图片选择", string_5, fileName, initialDirectory);
        }

        public static string OpenMultiselect(string title, string filter, string filename)
        {
            return OpenMultiselect(title, filter, filename, null);
        }

        public static string OpenMultiselect(string title, string filter, string filename, string initialDirectory)
        {
            OpenFileDialog dialog = new OpenFileDialog {
                Filter = filter,
                Title = title,
                RestoreDirectory = true,
                FileName = filename,
                Multiselect = true
            };
            if (!string.IsNullOrEmpty(initialDirectory))
            {
                dialog.InitialDirectory = initialDirectory;
            }
            string str = "";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string str3 in dialog.FileNames)
                {
                    str = str + string.Format("{0},", str3);
                }
            }
            return str.Trim(new char[] { ',' });
        }

        public static string OpenPdf(bool multiselect)
        {
            return OpenPdf(multiselect, "");
        }

        public static string OpenPdf(bool multiselect, string fileName)
        {
            return OpenPdf(multiselect, fileName, null);
        }

        public static string OpenPdf(bool multiselect, string fileName, string initialDirectory)
        {
            if (multiselect)
            {
                return OpenMultiselect("选择多个Pdf文件", string_4, fileName, initialDirectory);
            }
            return Open("Pdf选择", string_4, fileName, initialDirectory);
        }

        public static string OpenPDF()
        {
            return Open("PDF选择", string_4);
        }

        public static string OpenRar()
        {
            return Open("RAR压缩文件选择", string_11);
        }

        public static string OpenRar(bool multiselect)
        {
            return OpenRar(multiselect, "");
        }

        public static string OpenRar(string filename)
        {
            return Open("RAR压缩文件选择", string_11, filename);
        }

        public static string OpenRar(bool multiselect, string fileName)
        {
            return OpenRar(multiselect, fileName, null);
        }

        public static string OpenRar(bool multiselect, string fileName, string initialDirectory)
        {
            if (multiselect)
            {
                return OpenMultiselect("选择多个压缩文件", string_11, fileName, initialDirectory);
            }
            return Open("压缩文件选择", string_11, fileName, initialDirectory);
        }

        public static string OpenSqlite()
        {
            return Open("Sqlite数据库文件选择", string_12);
        }

        public static string OpenSqlite(bool multiselect)
        {
            return OpenSqlite(multiselect, "");
        }

        public static string OpenSqlite(string filename)
        {
            return Open("Sqlite数据库文件选择", string_12, filename);
        }

        public static string OpenSqlite(bool multiselect, string fileName)
        {
            return OpenSqlite(multiselect, fileName, null);
        }

        public static string OpenSqlite(bool multiselect, string fileName, string initialDirectory)
        {
            if (multiselect)
            {
                return OpenMultiselect("选择多个Sqlite数据库文件", string_12, fileName, initialDirectory);
            }
            return Open("Sqlite数据库文件选择", string_12, fileName, initialDirectory);
        }

        public static string OpenText()
        {
            return Open("选择文本文件选择", string_9);
        }

        public static string OpenText(bool multiselect)
        {
            return OpenText(multiselect, "");
        }

        public static string OpenText(bool multiselect, string fileName)
        {
            return OpenText(multiselect, fileName, null);
        }

        public static string OpenText(bool multiselect, string fileName, string initialDirectory)
        {
            if (multiselect)
            {
                return OpenMultiselect("选择多个文本文件", string_9, fileName);
            }
            return Open("选择文本文件", string_9, fileName);
        }

        public static string OpenWord()
        {
            return Open("Word选择", string_2);
        }

        public static string OpenWord(bool multiselect)
        {
            return OpenWord(multiselect, "");
        }

        public static string OpenWord(bool multiselect, string fileName)
        {
            return OpenWord(multiselect, fileName, null);
        }

        public static string OpenWord(bool multiselect, string fileName, string initialDirectory)
        {
            if (multiselect)
            {
                return OpenMultiselect("选择多个Word文件", string_2, fileName, initialDirectory);
            }
            return Open("Word选择", string_2, fileName, initialDirectory);
        }

        public static string OpenXml()
        {
            return Open("Xml文件选择", string_10);
        }

        public static string OpenXml(bool multiselect)
        {
            return OpenXml(multiselect, "");
        }

        public static string OpenXml(string filename)
        {
            return Open("Xml数据库文件选择", string_10, filename);
        }

        public static string OpenXml(bool multiselect, string fileName)
        {
            return OpenXml(multiselect, fileName, null);
        }

        public static string OpenXml(bool multiselect, string fileName, string initialDirectory)
        {
            if (multiselect)
            {
                return OpenMultiselect("选择多个Xml文件", string_10, fileName, initialDirectory);
            }
            return Open("Xml文件选择", string_10, fileName, initialDirectory);
        }

        public static string OpenZip()
        {
            return Open("压缩文件选择", string_8);
        }

        public static string OpenZip(bool multiselect)
        {
            return OpenZip(multiselect, "");
        }

        public static string OpenZip(string filename)
        {
            return Open("压缩文件选择", string_8, filename);
        }

        public static string OpenZip(bool multiselect, string fileName)
        {
            return OpenZip(multiselect, fileName, null);
        }

        public static string OpenZip(bool multiselect, string fileName, string initialDirectory)
        {
            if (multiselect)
            {
                return OpenMultiselect("选择多个压缩文件", string_8, fileName, initialDirectory);
            }
            return Open("压缩文件选择", string_8, fileName, initialDirectory);
        }

        public static Color PickColor()
        {
            Color control = SystemColors.Control;
            ColorDialog dialog = new ColorDialog();
            if (DialogResult.OK == dialog.ShowDialog())
            {
                control = dialog.Color;
            }
            return control;
        }

        public static Color PickColor(Color color)
        {
            Color control = SystemColors.Control;
            ColorDialog dialog = new ColorDialog {
                Color = color
            };
            if (DialogResult.OK == dialog.ShowDialog())
            {
                control = dialog.Color;
            }
            return control;
        }

        public static string Save(string title, string filter)
        {
            return Save(title, filter, string.Empty);
        }

        public static string Save(string title, string filter, string filename)
        {
            return Save(title, filter, filename, "");
        }

        public static string Save(string title, string filter, string filename, string initialDirectory)
        {
            SaveFileDialog dialog = new SaveFileDialog {
                Filter = filter,
                Title = title,
                FileName = filename,
                RestoreDirectory = true
            };
            if (!string.IsNullOrEmpty(initialDirectory))
            {
                dialog.InitialDirectory = initialDirectory;
            }
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.FileName;
            }
            return string.Empty;
        }

        public static string SaveAccessDb()
        {
            return Save("数据库备份", string_7);
        }

        public static string SaveBakDb()
        {
            return Save("数据库备份", "Access(*.bak)|*.bak");
        }

        public static string SaveConfig()
        {
            return Save("配置文件备份", "配置文件(*.cfg)|*.cfg|All File(*.*)|*.*");
        }

        public static string SaveExcel()
        {
            return SaveExcel(string.Empty);
        }

        public static string SaveExcel(string filename)
        {
            return Save("保存Excel", string_3, filename);
        }

        public static string SaveExcel(string filename, string initialDirectory)
        {
            return Save("保存Excel", string_3, filename, initialDirectory);
        }

        public static string SaveFile()
        {
            return SaveFile(string.Empty);
        }

        public static string SaveFile(string filename)
        {
            return Save("保存文件", string_1, filename);
        }

        public static string SaveFile(string filename, string initialDirectory)
        {
            return Save("保存文件", string_1, filename, initialDirectory);
        }

        public static string SaveHtml()
        {
            return SaveHtml(string.Empty);
        }

        public static string SaveHtml(string filename)
        {
            return Save("保存Html", string_6, filename);
        }

        public static string SaveHtml(string filename, string initialDirectory)
        {
            return Save("保存Html", string_6, filename, initialDirectory);
        }

        public static string SaveImage()
        {
            return SaveImage(string.Empty);
        }

        public static string SaveImage(string filename)
        {
            return Save("保存图片", string_5, filename);
        }

        public static string SaveImage(string filename, string initialDirectory)
        {
            return Save("保存图片", string_5, filename, initialDirectory);
        }

        public static string SavePdf()
        {
            return SavePdf(string.Empty);
        }

        public static string SavePdf(string filename)
        {
            return Save("保存Pdf", string_4, filename);
        }

        public static string SavePdf(string filename, string initialDirectory)
        {
            return Save("保存Pdf", string_4, filename, initialDirectory);
        }

        public static string SaveRar()
        {
            return SaveRar(string.Empty);
        }

        public static string SaveRar(string filename)
        {
            return Save("压缩文件保存", string_11, filename);
        }

        public static string SaveRar(string filename, string initialDirectory)
        {
            return Save("Rar压缩文件保存", string_11, filename, initialDirectory);
        }

        public static string SaveSqlite()
        {
            return SaveSqlite(string.Empty);
        }

        public static string SaveSqlite(string filename)
        {
            return Save("Sqlite数据库文件保存", string_12, filename);
        }

        public static string SaveSqlite(string filename, string initialDirectory)
        {
            return Save("Sqlite数据库文件保存", string_12, filename, initialDirectory);
        }

        public static string SaveText()
        {
            return SaveText(string.Empty);
        }

        public static string SaveText(string filename)
        {
            return Save("保存文本文件", string_9, filename);
        }

        public static string SaveText(string filename, string initialDirectory)
        {
            return Save("保存文本文件", string_9, filename, initialDirectory);
        }

        public static string SaveWord()
        {
            return SaveWord(string.Empty);
        }

        public static string SaveWord(string filename)
        {
            return Save("保存Word", string_2, filename);
        }

        public static string SaveWord(string filename, string initialDirectory)
        {
            return Save("保存Word", string_2, filename, initialDirectory);
        }

        public static string SaveXml()
        {
            return SaveXml(string.Empty);
        }

        public static string SaveXml(string filename)
        {
            return Save("Xml文件保存", string_10, filename);
        }

        public static string SaveXml(string filename, string initialDirectory)
        {
            return Save("Xml文件保存", string_10, filename, initialDirectory);
        }

        public static string SaveZip()
        {
            return SaveZip(string.Empty);
        }

        public static string SaveZip(string filename)
        {
            return Save("压缩文件保存", string_8, filename);
        }

        public static string SaveZip(string filename, string initialDirectory)
        {
            return Save("压缩文件保存", string_8, filename, initialDirectory);
        }
    }
}

