using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace MD5_Dir
{

    class FileWithHash {
        public String Name { get; set; }
        public String Hash { get; set; }
    }

    class Number
    {
        public Number()
        {
            num = 0;
        }
        private long num;
        public void Add(long n)
        {
            num += n;
        }
        public long Get()
        {
            return num;
        }
    }

    class DirectoryMD5
    {
        private Dictionary<String, String> hashFileMap;
        private DirectoryInfo mainDir;

        private int threadNumber;

        private Number allBytes;

        private Number checkedBytes;

        public DirectoryMD5(String dirName, int _threadNumber)
        {
            mainDir = new DirectoryInfo(dirName);
            threadNumber = _threadNumber;
            hashFileMap = new Dictionary<String, String>();

            allBytes = new Number();
            checkedBytes = new Number();

            var files = mainDir.GetFiles("*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                allBytes.Add(file.Length);
            }
        }

        public DirectoryMD5(String dirName)
        {
            mainDir = new DirectoryInfo(dirName);
            threadNumber = 0;
            hashFileMap = new Dictionary<String, String>();
        }

        private static string GetMd5Hash(MD5 md5Hash, Stream fileStream)
        {
            byte[] data = md5Hash.ComputeHash(fileStream);

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        private static string GetMd5Hash(MD5 md5Hash, String source)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(source));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        private void CalculateFilesHashes(FileInfo[] files, int begin, int end)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                for (int i = begin; i < end; i++)
                {
                    using (var fileStream = files[i].OpenRead())
                    {
                        String hash = GetMd5Hash(md5Hash, fileStream);
                        lock (hashFileMap)
                        {
                            try
                            {
                                hashFileMap.Add(files[i].FullName, hash);
                            }
                            catch (ArgumentException e)
                            {
                                Console.WriteLine("Warning: FileName collision");
                            }
                        }
                        lock (checkedBytes)
                        {
                            checkedBytes.Add(files[i].Length);
                        }
                    }
                }
            }
        }

        private void CalculateAllFilesHashes()
        {
            Console.WriteLine("Getting files...");
            var files = mainDir.GetFiles("*", SearchOption.AllDirectories);
            Console.WriteLine("Getting files done");

            if (threadNumber == 0)
            {
                CalculateFilesHashes(files, 0, files.Length);
            }
            else
            {
                Task[] tasks = new Task[threadNumber];
                int step = files.Length / threadNumber;

                for (int i = 0; i < threadNumber; i++)
                {
                    int left = step * i;
                    int right = (i != threadNumber - 1) ? step * (i + 1) : files.Length;
                    tasks[i] = new Task(() => { CalculateFilesHashes(files, left, right); });
                }

                foreach (var task in tasks)
                {
                    task.Start();
                }

                bool isCompleted = false;
                int cursorPos = Console.CursorTop;

                Task consoleWrite = new Task(() =>
                {
                    while (!isCompleted)
                    {
                        Console.SetCursorPosition(0, cursorPos);
                        GetProcessState();
                        Console.WriteLine("Completed: " + GetProcessState().ToString() + "%");
                        Task.Delay(500);
                    }
                });
                consoleWrite.Start();
                Task.WaitAll(tasks);
                isCompleted = true;

                Task.WaitAll(consoleWrite);

            }
        }

        private String GetDirHash(DirectoryInfo dirInfo)
        {
            var dirs = dirInfo.GetDirectories();
            Array.Sort(dirs, (dir1, dir2) => dir1.FullName.CompareTo(dir2.FullName));
            var files = dirInfo.GetFiles();
            Array.Sort(files, (file1, file2) => file1.FullName.CompareTo(file2.FullName));

            String hash = dirInfo.FullName;

            using (var md5Hasher = MD5.Create())
            {

                foreach (var file in files)
                {
                    hash += hashFileMap[file.FullName];
                }

                foreach (var dir in dirs)
                {
                    hash += GetDirHash(dir);
                }

                hash = GetMd5Hash(md5Hasher, hash);
            }

            return hash;
        }

        public String GetDirHash()
        {
            CalculateAllFilesHashes();
            return GetDirHash(mainDir);
        }

        public int GetProcessState()
        {
            if (checkedBytes.Get() == 0)
            {
                return 0;
            }
            int result = (int)Math.Round((double)checkedBytes.Get() / (double)allBytes.Get() * 100.0);
            return result;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("============== Directory hash calculator ================");
            Console.Write("Dir path: ");
            String dirName = Console.ReadLine();

            DirectoryMD5 dirHasher = new DirectoryMD5(dirName, 3);

            String dirHash = dirHasher.GetDirHash();

            Console.WriteLine("Hash: " + dirHash.ToString());

            Console.ReadKey();
        }
    }
}
