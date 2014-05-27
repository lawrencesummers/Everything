/*
 * Created by SharpDevelop.
 * User: guanxiang
 * Date: 2013/10/31
 * Time: 14:52
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace LawrenceUtils
{
	/// <summary>
	/// Description of FileStruct.
	/// </summary>
	public class FileStruct
	{
		public FileStruct()
		{
		}
		public string Flags;
        public string Owner;
        public string Group;
        public bool IsDirectory;
        public DateTime CreateTime;
        public string Name;
	}
}
