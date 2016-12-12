using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigmaConsoleApp {
    class SigmaTwo {

        private static string NEW_FEATURESFILE = AppDomain.CurrentDomain.BaseDirectory.ToLower().Replace("\\bin\\debug", string.Empty) + "App_Data\\NewFeatures.txt";
        private static string FEATURESFILE = AppDomain.CurrentDomain.BaseDirectory.ToLower().Replace("\\bin\\debug", string.Empty) + "App_Data\\Features.txt";
        private static string FEATS2DELFILE = AppDomain.CurrentDomain.BaseDirectory.ToLower().Replace("\\bin\\debug", string.Empty) + "App_Data\\FeaturesToDelete.txt";
        public static List<string> UDB_NUMBS2DEL = new List<string>();

        /// <summary>
        /// Populates the list of string UDBNumbs2Del
        /// </summary>
        public static void GetUDBNumbs2Del() {
            try {

                StringBuilder result = new StringBuilder();
                Console.WriteLine(FEATURESFILE);
                Console.WriteLine(FEATS2DELFILE);

                //1st check FeaturesToDelete.txt file exists 
                if (File.Exists(FEATS2DELFILE)) {
                    //open file
                    using (StreamReader sr = File.OpenText(FEATS2DELFILE)) {
                        while (sr.Peek() != -1) {
                            //read line and add it to stringBuilder
                            result.Append(sr.ReadLine() + ",");
                            //print test 
                            Console.WriteLine(result.ToString());
                        }
                        //close and dispose streamReader
                        sr.DiscardBufferedData();
                        sr.Close();

                    }
                    //Add UDBNumbs from stringBuilder to an array
                    string[] UDBNumbArray = result.ToString().Split(',');
                    //add array to static list
                    UDB_NUMBS2DEL.AddRange(UDBNumbArray);
                    //remove empty/last items (added due to inclusion of , on readline())
                    UDB_NUMBS2DEL.RemoveAt(UDB_NUMBS2DEL.Count - 1);
                    //print test                 
                    foreach (string UDBNumb in UDB_NUMBS2DEL)
                        Console.WriteLine("UDBNumb:\t" + UDBNumb);
                }

            }
            catch (Exception ex) {
                // Log the exception and notify tech support team

                //*** NB *** commented out as this class isnt included on this project
                //  ExceptionUtility.LogException(ex, "Sigma.cs, GetUDBNumbs2Del()");
                //  ExceptionUtility.NotifySystemOps(ex);
                Console.WriteLine("EXCEPTION\t" + ex.Message + "\t" + ex.StackTrace);
            }
        }

        /// <summary>
        /// Copy Features into a new file
        /// </summary>
        public static void GetNewFeatures() {
            try {

                StringBuilder lineUDB = new StringBuilder();
                StringBuilder line = new StringBuilder();

                //create new Features file if it doesnt exist already 
                if (!File.Exists(NEW_FEATURESFILE))
                    File.Create(NEW_FEATURESFILE);
                StreamWriter sw = new StreamWriter(NEW_FEATURESFILE, false);

                //open Features file
                using (StreamReader newSr = File.OpenText(FEATURESFILE)) {
                    while (newSr.Peek() != -1) {
                        //read featues files line and add its UDB numb to stringBuilder
                        line.Append(newSr.ReadLine());
                        lineUDB.Append(line.ToString().Split(',')[0]);
                        //print test 
                        Console.WriteLine(lineUDB.ToString());

                        //check whether line UDB numb exists in UDB_NUMBS2DEL list
                        if (!UDB_NUMBS2DEL.Contains(lineUDB.ToString())) 
                            sw.WriteLine(line.ToString());

                        line.Clear();
                        lineUDB.Clear();

                    }
                    //close streams reader and writer
                    newSr.DiscardBufferedData();
                    newSr.Close();

                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception ex) {
                // Log the exception and notify tech support team

                //*** NB *** commented out as this class isnt included on this project
                //  ExceptionUtility.LogException(ex, "Sigma.cs, GetNewFeatures()");
                //  ExceptionUtility.NotifySystemOps(ex);
                Console.WriteLine("EXCEPTION\t" + ex.Message + "\t"+ ex.StackTrace);
            }
        }



    }//end of class
}//end of namespace
