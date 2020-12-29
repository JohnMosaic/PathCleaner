using Microsoft.Win32;
using System;
using System.Collections.Generic;

namespace PathCleaner
{
    class MainEntry
    {
        static void Main(string[] args)
        {
            DoWork doWork = new DoWork();

            foreach (string s in doWork.delRegKeyDict.Keys)
            {
                doWork.DelRegKey(s, doWork.delRegKeyDict[s]);
            }

            foreach (string s in doWork.createRegKeyDict.Keys)
            {
                doWork.CreateRegKey(s, doWork.createRegKeyDict[s]);
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("[*] Clear folder access records done.");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("[+] Press any key to continue...");
            Console.ReadLine();
        }
    }

    class DoWork
    {
        public Dictionary<string, string> delRegKeyDict = new Dictionary<string, string>()
        {
            {@"Software\Microsoft\Windows\Shell\BagMRU", "SMWS_BagMRU"},
            {@"Software\Microsoft\Windows\Shell\Bags", "SMWS_Bags"},
            {@"Software\Classes\Local Settings\Software\Microsoft\Windows\Shell\BagMRU", "SCL_SMWS_BagMRU"},
            {@"Software\Classes\Local Settings\Software\Microsoft\Windows\Shell\Bags", "SCL_SMWS_Bags"}
        };

        public Dictionary<string, string> createRegKeyDict = new Dictionary<string, string>()
        {
            {@"Software\Classes\Local Settings\Software\Microsoft\Windows\Shell\BagMRU", "SCL_SMWS_BagMRU"},
            {@"Software\Classes\Local Settings\Software\Microsoft\Windows\Shell\Bags", "SCL_SMWS_Bags"}
        };

        public void DelRegKey(string subkey, string itemName)
        {
            RegistryKey delRegKey = Registry.CurrentUser;

            try
            {
                delRegKey.DeleteSubKeyTree(subkey);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[+] Delete " + itemName + " done.");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[!] Delete " + itemName + " failed. " + ex.Message);
            }
            finally
            {
                delRegKey.Close();
            }
        }

        public void CreateRegKey(string subKey, string itemName)
        {
            RegistryKey createRegKey = Registry.CurrentUser;

            try
            {
                createRegKey.CreateSubKey(subKey);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[+] Create " + itemName + " done.");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[!] Create " + itemName + " failed. " + ex.Message);
            }
            finally
            {
                createRegKey.Close();
            }
        }
    }
}
