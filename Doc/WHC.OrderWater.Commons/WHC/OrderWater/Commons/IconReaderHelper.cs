namespace WHC.OrderWater.Commons
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class IconReaderHelper
    {
        public static Icon ExtractIconForExtension(string extension, bool large)
        {
            if (string.IsNullOrEmpty(extension))
            {
                throw new ArgumentException("Invalid file or extension.", "fileOrExtension");
            }
            if (!extension.Trim().StartsWith("."))
            {
                extension = "." + extension.Trim();
            }
            return GetAssociatedIcon("0" + extension, large);
        }

        public static Icon GetAssociatedIcon(string stubPath, bool large)
        {
            uint num2;
            Class1.Struct3 structure = new Class1.Struct3();
            int num = Marshal.SizeOf(structure);
            if (large)
            {
                num2 = 0x110;
            }
            else
            {
                num2 = 0x111;
            }
            Class1.SHGetFileInfo(stubPath, 0x100, ref structure, (uint) num, num2);
            return Icon.FromHandle(structure.intptr_0);
        }

        public static string GetDisplayName(string name, bool isDirectory)
        {
            uint num = 0x410;
            Class1.Struct3 struct2 = new Class1.Struct3();
            uint num2 = isDirectory ? 0x10 : 0x80;
            Class1.SHGetFileInfo(name, num2, ref struct2, (uint) Marshal.SizeOf(struct2), num);
            return struct2.string_1;
        }

        public static Icon GetFileIcon(string name, IconSize size, bool linkOverlay)
        {
            uint num = 0x110;
            if (linkOverlay)
            {
                num += 0x8000;
            }
            if (IconSize.Small == size)
            {
                num += 1;
            }
            else
            {
                num = num;
            }
            Class1.Struct3 struct2 = new Class1.Struct3();
            Class1.SHGetFileInfo(name, 0x80, ref struct2, (uint) Marshal.SizeOf(struct2), num);
            Icon icon = (Icon) Icon.FromHandle(struct2.intptr_0).Clone();
            Class2.DestroyIcon(struct2.intptr_0);
            return icon;
        }

        public static Icon GetFolderIcon(IconSize size, FolderType folderType)
        {
            uint num = 0x100;
            if (FolderType.Open == folderType)
            {
                num += 2;
            }
            if (IconSize.Small == size)
            {
                num += 1;
            }
            else
            {
                num = num;
            }
            Class1.Struct3 struct2 = new Class1.Struct3();
            Class1.SHGetFileInfo(null, 0x10, ref struct2, (uint) Marshal.SizeOf(struct2), num);
            Icon.FromHandle(struct2.intptr_0);
            Icon icon = (Icon) Icon.FromHandle(struct2.intptr_0).Clone();
            Class2.DestroyIcon(struct2.intptr_0);
            return icon;
        }

        public static int GetIcon(ImageList images, string extension)
        {
            return GetIcon(images, extension, false);
        }

        public static int GetIcon(ImageList images, string extension, bool largeIcon)
        {
            for (int i = 0; i < images.Images.Count; i++)
            {
                if (images.Images.Keys[i] == extension)
                {
                    return i;
                }
            }
            images.Images.Add(extension, ExtractIconForExtension(extension, largeIcon));
            return (images.Images.Count - 1);
        }

        public enum FolderType
        {
            Open,
            Closed
        }

        public enum IconSize
        {
            Large,
            Small
        }
    }
}

