using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using Crestron.SimplSharp.CrestronIO;

namespace LibrarySystem
{
	internal class FileOperations
	{
		public enum ErrorEnum	//enumeration - List of values
		{ 
			SUCCESS,
			ERROR_DirectoryDoesNotExist,
			ERROR_DirectoryEmpty,
			ERROR_NoMatchingFiles,
			ERROR_Unspecified
		}

		bool debug;

		// these do the actual interaction with the file system
		private FileStream myStream;
		private StreamReader myReader;
		private StreamWriter myWriter;

		/// <summary>
		/// Contains the content read from a file or to be writtent to a file.
		/// </summary>
		public string Content;

		public string[] FilenameList;

		public FileOperations(bool dbg)
		{
			debug = dbg;
		}

		public FileOperations()
		{
			debug = false;
		}

		/// <summary>
		/// Creates a list of the filenames in the specified directory
		/// </summary>
		/// <param name="path">The directory</param>
		/// <returns></returns>
		public ErrorEnum GetFileList(string path)
		{
			ErrorEnum returnVal = ErrorEnum.ERROR_Unspecified;

			DirectoryInfo dir;

			if (debug) CrestronConsole.PrintLine("Staring GetFileList");

			try
			{
				dir = new DirectoryInfo(path);

				if (!dir.Exists) return ErrorEnum.ERROR_DirectoryDoesNotExist;

				if (debug) CrestronConsole.PrintLine("Directory exists, here we go");

				FileInfo[] fileList = dir.GetFiles();

				if (fileList == null) return ErrorEnum.ERROR_DirectoryEmpty;

				if (fileList.Count() == 0) return ErrorEnum.ERROR_DirectoryEmpty;

				if (debug) CrestronConsole.PrintLine("Found some files, attempting to build list");

				FilenameList = new string[fileList.Count()];
				
				int i = 0;
				foreach (FileInfo f in fileList)
				{
					FilenameList[i] = f.FullName;
					i++;
				}

				returnVal = ErrorEnum.SUCCESS;
			}
			catch(Exception e)
			{
				ErrorLog.Notice("error accessing filesystem: {0} => {1}", e.Message, e.StackTrace);			
			}

			return returnVal;
		}

		/// <summary>
		/// Sorts the file list alphabetically
		/// </summary>
		/// <returns></returns>
		public ErrorEnum SortFileList()
		{
			ErrorEnum returnVal = ErrorEnum.ERROR_Unspecified;

			if (FilenameList != null)
			{
				if (FilenameList.Count() > 0)
				{
					Array.Sort(FilenameList);
				}
			}

			return returnVal;
		}

		/// <summary>
		/// Gets the content of a file.
		/// </summary>
		/// <param name="filePath">The filepath of the file to read from.</param>
		/// <returns>Returns 1 if successful, 0 if not successful.</returns>
		public ErrorEnum GetContent(string filePath)
		{
			ErrorEnum returnVal = ErrorEnum.ERROR_Unspecified;

			try
			{
				myStream = new FileStream(filePath, FileMode.Open);
			}
			catch (Exception e)
			{
				ErrorLog.Notice("Error in opening file {0} to read, Error: {1}", filePath, e.Message);
				return returnVal;
			}

			try
			{
				myReader = new StreamReader(myStream);
				while (!myReader.EndOfStream)
				{
					Content = myReader.ReadToEnd();
				}
				myReader.Close();
				returnVal = ErrorEnum.SUCCESS;
			}
			catch (Exception e)
			{
				ErrorLog.Notice("Error in reading file {0}, Error: {1}", filePath, e.Message);
				returnVal = ErrorEnum.ERROR_Unspecified;
			}
			finally
			{
				myStream.Close();
			}

			return returnVal;
		}

		/// <summary>
		/// Writes .Content to a file.
		/// </summary>
		/// <param name="filePath">The filepath of the file to write to.</param>
		/// <returns>Returns 1 if successful, 0 if not successful.</returns>
		public ErrorEnum SaveContent(string filePath)
		{
			 ErrorEnum returnVal =  ErrorEnum.ERROR_Unspecified;

			if (Content == null) return 0;

			try
			{
				myStream = new FileStream(filePath, FileMode.Create);
			}
			catch (Exception e)
			{
				ErrorLog.Notice("Error in opening file {0} to write, Error: {1}", filePath, e.Message);
				return returnVal;
			}

			try
			{
				myWriter = new StreamWriter(myStream);
				myWriter.Write(Content);
				myWriter.Close();
				Content = null;
				returnVal = ErrorEnum.SUCCESS;
			}
			catch (Exception e)
			{
				ErrorLog.Notice("Error in writing file {0}, Error: {1}", filePath, e.Message);
				returnVal = ErrorEnum.ERROR_Unspecified;
			}
			finally
			{
				myStream.Close();
			}

			return returnVal;
		}
	}
}