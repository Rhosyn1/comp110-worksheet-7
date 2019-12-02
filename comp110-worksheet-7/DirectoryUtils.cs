using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace comp110_worksheet_7
{
	public static class DirectoryUtils
	{
		// Return the size, in bytes, of the given file
		public static long GetFileSize(string filePath)
		{
            return new FileInfo(filePath).Length;
        }

		// Return true if the given path points to a directory, false if it points to a file
		public static bool IsDirectory(string path)
		{
			return File.GetAttributes(path).HasFlag(FileAttributes.Directory);
		}

		// Return the total size, in bytes, of all the files below the given directory
		public static long GetTotalSize(string directory)
		{
            long size = 0;
            var fileList = Directory.GetFiles(directory, "*", SearchOption.AllDirectories);
            foreach (var item in fileList)
            {
                size += GetFileSize(item);
            }
            return size;
		}

		// Return the number of files (not counting directories) below the given directory
		public static int CountFiles(string directory)
		{
            return Directory.GetFiles(directory, "*", SearchOption.AllDirectories).Length;
		}

        // Return the nesting depth of the given directory. A directory containing only files (no subdirectories) has a depth of 0.
        public static int GetDepth(string directory)
        {
            var directoryList = Directory.GetDirectories(directory);
            int maxDepth = 0;
            int depth = 0;
            if (directoryList.Length != 0)
            {
                foreach (var dir in directoryList)
                {
                    depth += GetDepth(dir) + 1;
                    if (depth > maxDepth)
                    {
                        maxDepth = depth;
                    }
                    return maxDepth;
                }
            }
            return maxDepth;

        }	

		// Get the path and size (in bytes) of the smallest file below the given directory
		public static Tuple<string, long> GetSmallestFile(string directory)
		{
            long smallestFileSize;
            string smallestFilePath;
            var fileList = Directory.GetFiles(directory, "*", SearchOption.AllDirectories);

            smallestFilePath = fileList[0];
            smallestFileSize = GetFileSize(smallestFilePath);

            foreach (var small in fileList)
            {
                if (GetFileSize(small) < smallestFileSize)
                {
                    smallestFileSize = GetFileSize(small);
                    smallestFilePath = small;
                }
            }
            return new Tuple<string, long>(smallestFilePath, smallestFileSize);

        }

		// Get the path and size (in bytes) of the largest file below the given directory
		public static Tuple<string, long> GetLargestFile(string directory)
		{
            long largestFileSize;
            string largestFilePath;
            var fileList = Directory.GetFiles(directory, "*", SearchOption.AllDirectories);

            largestFilePath = fileList[0];
            largestFileSize = GetFileSize(largestFilePath);

            foreach (var large in fileList)
            {
                if (GetFileSize(large) > largestFileSize)
                {
                    largestFileSize = GetFileSize(large);
                    largestFilePath = large;
                }
            }
            return new Tuple<string, long>(largestFilePath, largestFileSize);

        }

		// Get all files whose size is equal to the given value (in bytes) below the given directory
		public static IEnumerable<string> GetFilesOfSize(string directory, long size)
		{
            List<string> newList = new List<string>();
            var fileList = Directory.GetFiles(directory, "*", SearchOption.AllDirectories);
            foreach (var i in fileList)
            {
                if (GetFileSize(i) == size)
                {
                    newList.Add(i);

                }
            }
            return newList;


        }
	}
}
