namespace RDIFramework.Utilities
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;

    public class DosHelper
    {
        private const int int_0 = 0;
        private const int int_1 = 5;

        [DllImport("user32.dll")]
        private static extern int FindWindow(string string_0, string string_1);
        [DllImport("winmm.dll", CharSet=CharSet.Auto)]
        private static extern int mciSendString(string string_0, string string_1, int int_2, int int_3);
        public void method_0()
        {
            mciSendString("set CDAudio door open", null, 0x7f, 0);
        }

        public void method_1()
        {
            mciSendString("set CDAudio door closed", null, 0x7f, 0);
        }

        public void method_10()
        {
            Process.Start(@"C:\Program Files\Microsoft Office\OFFICE11\powerpnt.exe");
        }

        public string method_100()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
        }

        public void method_101()
        {
            Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic));
        }

        public string method_102()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        }

        public void method_103()
        {
            Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
        }

        public string method_104()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.StartMenu);
        }

        public void method_105()
        {
            Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu));
        }

        public void method_11()
        {
            Process.Start(@"C:\Program Files\Microsoft Office\OFFICE11\outlook.exe");
        }

        public void method_12()
        {
            Process.Start("notepad.exe");
        }

        public void method_13()
        {
            Process.Start("calc.exe");
        }

        public void method_14()
        {
            Process.Start("cmd.exe");
        }

        public void method_15()
        {
            Process.Start("regedit.exe");
        }

        public void method_16()
        {
            Process.Start("mspaint.exe");
        }

        public void method_17()
        {
            Process.Start("write.exe");
        }

        public void method_18()
        {
            Process.Start("mplayer2.exe");
        }

        public void method_19()
        {
            Process.Start("explorer.exe");
        }

        public void method_2()
        {
            Process.Start(@"c:\");
        }

        public void method_20()
        {
            Process.Start("taskmgr.exe");
        }

        public void method_21()
        {
            Process.Start("eventvwr.exe");
        }

        public void method_22()
        {
            Process.Start("winmsd.exe");
        }

        public void method_23()
        {
            Process.Start("ntbackup.exe");
        }

        public void method_24()
        {
            Process.Start("winver.exe");
        }

        public void method_25()
        {
            Process.Start("rundll32.exe", "shell32.dll,Control_RunDLL");
        }

        public void method_26()
        {
            Process.Start("rundll32.exe", "shell32.dll,Control_RunDLL access.cpl,,1");
        }

        public void method_27()
        {
            Process.Start("rundll32.exe", "shell32.dll,Control_RunDLL access.cpl,,2");
        }

        public void method_28()
        {
            Process.Start("rundll32.exe", "shell32.dll,Control_RunDLL access.cpl,,3");
        }

        public void method_29()
        {
            Process.Start("rundll32.exe", "shell32.dll,Control_RunDLL access.cpl,,4");
        }

        public void method_3()
        {
            Process.Start(@"d:\");
        }

        public void method_30()
        {
            Process.Start("rundll32.exe", "shell32.dll,Control_RunDLL access.cpl,,5");
        }

        public void method_31()
        {
            Process.Start("rundll32.exe", "shell32.dll,Control_RunDLL sysdm.cpl @1");
        }

        public void method_32()
        {
            Process.Start("rundll32.exe", "shell32.dll,SHHelpShortcuts_RunDLL AddPrinter");
        }

        public void method_33()
        {
            Process.Start("rundll32.exe", "shell32.dll,shell32.dll,Control_RunDLL appwiz.cpl,,1");
        }

        public void method_34()
        {
            Process.Start("rundll32.exe", "shell32.dll,shell32.dll,Control_RunDLL appwiz.cpl,,2");
        }

        public void method_35()
        {
            Process.Start("rundll32.exe", "shell32.dll,shell32.dll,Control_RunDLL appwiz.cpl,,3");
        }

        public void method_36()
        {
            Process.Start("rundll32.exe", " appwiz.cpl,NewLinkHere %1");
        }

        public void method_37()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL timedate.cpl,,0");
        }

        public void method_38()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL timedate.cpl,,1");
        }

        public void method_39()
        {
            Process.Start("rundll32.exe", " syncui.dll,Briefcase_Create");
        }

        public void method_4()
        {
            Process.Start(@"e:\");
        }

        public void method_40()
        {
            Process.Start("rundll32.exe", " diskcopy.dll,DiskCopyRunDll");
        }

        public void method_41()
        {
            Process.Start("rundll32.exe", " rnaui.dll,RnaWizard");
        }

        public void method_42()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL desk.cpl,,0");
        }

        public void method_43()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL desk.cpl,,1");
        }

        public void method_44()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL desk.cpl,,2");
        }

        public void method_45()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL desk.cpl,,3");
        }

        public void method_46()
        {
            Process.Start("rundll32.exe", " shell32.dll,SHFormatDrive");
        }

        public void method_47()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL joy.cpl,,0");
        }

        public void method_48()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL joy.cpl,,1");
        }

        public void method_49()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL main.cpl @1");
        }

        public void method_5()
        {
            Process.Start(@"f:\");
        }

        public void method_50()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL main.cpl @1,,1");
        }

        public void method_51()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL main.cpl @2");
        }

        public void method_52()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL main.cpl @3");
        }

        public void method_53()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL main.cpl @4");
        }

        public void method_54()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL modem.cpl,,add");
        }

        public void method_55()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL mmsys.cpl,,0");
        }

        public void method_56()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL mmsys.cpl,,1");
        }

        public void method_57()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL mmsys.cpl,,2");
        }

        public void method_58()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL mmsys.cpl,,3");
        }

        public void method_59()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL mmsys.cpl,,4");
        }

        public void method_6(string hardpath)
        {
            Process.Start(hardpath);
        }

        public void method_60()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL mmsys.cpl @1");
        }

        public void method_61()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL netcpl.cpl");
        }

        public void method_62()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL password.cpl");
        }

        public void method_63()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL powercfg.cpl");
        }

        public void method_64()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL intl.cpl,,0");
        }

        public void method_65()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL intl.cpl,,1");
        }

        public void method_66()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL intl.cpl,,2");
        }

        public void method_67()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL intl.cpl,,3");
        }

        public void method_68()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL intl.cpl,,4");
        }

        public void method_69()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL odbccp32.cpl");
        }

        public void method_7()
        {
            Process.Start(@"C:\Program Files\Microsoft Office\OFFICE11\winword.exe");
        }

        public void method_70()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL sysdm.cpl,,0");
        }

        public void method_71()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL sysdm.cpl,,1");
        }

        public void method_72()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL sysdm.cpl,,2");
        }

        public void method_73()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL sysdm.cpl,,3");
        }

        public void method_74()
        {
            Process.Start("shutdown.exe", "-r");
        }

        public void method_75()
        {
            Process.Start("shutdown.exe", "-s -f");
        }

        public void method_76(string time)
        {
            string arguments = "-s -t " + time;
            Process.Start("shutdown.exe", arguments);
        }

        public void method_77()
        {
            Process.Start("shutdown.exe", "-l");
        }

        public void method_78()
        {
            Process.Start("shutdown.exe", "-a");
        }

        public void method_79()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL themes.cpl");
        }

        public void method_8()
        {
            Process.Start(@"C:\Program Files\Microsoft Office\OFFICE11\excel.exe");
        }

        public void method_80(string address)
        {
            Process.Start(address);
        }

        public void method_81(string name)
        {
            Process.Start(name);
        }

        public void method_82()
        {
            ShowWindow(FindWindow("Shell_TrayWnd", null), 5);
        }

        public void method_83()
        {
            ShowWindow(FindWindow("Shell_TrayWnd", null), 0);
        }

        public void method_84(string address)
        {
            Process.Start("mailto:" + address);
        }

        public void method_85()
        {
            Process.Start("mailto:80368704@qq.com");
        }

        public string method_86()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.System);
        }

        public void method_87()
        {
            Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.System));
        }

        public string method_88()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
        }

        public void method_89()
        {
            Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
        }

        public void method_9()
        {
            Process.Start(@"C:\Program Files\Microsoft Office\OFFICE11\msaccess.exe");
        }

        public string method_90()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }

        public void method_91()
        {
            Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
        }

        public string method_92()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Startup);
        }

        public void method_93()
        {
            Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.Startup));
        }

        public string method_94()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Cookies);
        }

        public void method_95()
        {
            Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.Cookies));
        }

        public string method_96()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.History);
        }

        public void method_97()
        {
            Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.History));
        }

        public string method_98()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
        }

        public void method_99()
        {
            Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.MyComputer));
        }

        [DllImport("user32.dll")]
        private static extern int ShowWindow(int int_2, int int_3);
    }
}

