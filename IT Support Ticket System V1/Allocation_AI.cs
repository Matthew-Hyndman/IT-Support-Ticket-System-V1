
////////////////////////////////////
//Developer & Owner: Matthew Hyndman
////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace IT_Support_Ticket_System_V1
{
    public class Allocation_AI
    {

        SqlConnection conn; //creates a connetion to the DB

        //stores the data in a sudo Database (DataSet)
        DataSet ds = new DataSet();
        DataSet ds2 = new DataSet();
        //is a up-to-date snap shot of the DB
        DataSet dsDeaflt = new DataSet();

        SqlDataAdapter daContractor, daWordCheck, daNewWord, daShouldIgnore,
            daDeafultContractor, daDeafultWordCheck, daDeafultNewWord;

        SqlCommandBuilder cmdBDeafultContractor, cmdBDeafultWordCheck, cmdBDeafultNewWord;

        SqlCommand cmdContractor, cmdWordCheck, cmdNewWord, cmdShouldIgnore;

        DataRow drContractor, drWordCheck, drNewWord, drShouldIgnore;

        //keeps track of key words and how many time they where said
        List<WordPoints> words = new List<WordPoints>();

        //Deafult Queries
        string sqlContractor = @"select * from Contractor";
        string sqlKeyWord = @"select * from Key_Words";
        string sqlNewKeyWord = @"select * from New_Key_Words";

        //Custom Queries
        string sqlGetContractor = @"exec Get_Contractor @word = @w";
        string sqlWordCheck = @"exec Check_Key_Words @word = @w";
        string sqlNewWordCheck = @"exec Check_New_Key_Words @word = @w";
        string sqlShouldIgnore = @"exec shouldIgnore @word = @w";


        //words that may mean the next word is a key word
        string[] indecatorWords =
        {
            "The", "of", "and", "a", "to", "in", "is", "that", "it", "was", "are",
            "this", "have", "or", "one", "had", "but","not", "all", "an", "each",
            "many", "them", "these", "some", "has", "no", "my", "been", "find"
        };

        //words that may mean the next word is not a key word
        string[] notIndecatorWords =
        {
            "you", "he", "for", "on", "as", "with", "his", "they", "i", "at", "from", "be", "by", "words", "what", "were", "we",
            "when", "your", "can", "said", "there", "use", "which", "she", "do", "how", "their", "if", "will", "up", "other", "about",
            "out", "then", "so", "her", "would", "make", "like", "him", "into", "time", "look", "two", "more", "write", "go", "see",
             "number", "way", "could", "people", "than", "first", "water", "called", "who", "oil", "sit", "now", "long", "down", "day",
              "get", "come", "made", "may", "part"
        };
                
        bool indecatorFound = false; //true when next word is likly a key word
        bool keyWordFound = false; //true if key word is found

        string connStr;//containes the name of the DB and server name, used for connecting
                       //and geting an up to date feed of data from the DB

        public string server = "";//server name

        //gets a contractor ID based on the Number of key words and
        //if the contractor is too heard to determent then assing to deafult ID
        public int pick_Contractor(string desc)
        {
            desc.Trim();//to pervent any bugs
            desc += " ";//indecator of the end of the description
            int index = 0;//desc navigator
            char[] ch = desc.ToCharArray();
            string strPossablieKeyWord = "";//current word
            try
            {
                do
                {


                    try
                    {
                        strPossablieKeyWord += ch[index];
                        index++;
                    }
                    catch (IndexOutOfRangeException) { break; }

                    if (char.IsWhiteSpace(ch[index]) || char.IsSymbol(ch[index]) || ch[index] == '.')
                    {
                        /*if (char.IsSymbol(ch[index]) || ch[index] == ' ' && index == desc.Length-1)
                        {
                            strPossablieKeyWord = "";
                            continue;
                        }*/

                        if (!indecatorFound)
                        {

                            indecatorFound = isIndecatorWord(strPossablieKeyWord);
                            strPossablieKeyWord = "";
                        }

                        if (!keyWordFound && indecatorFound)
                        {
                            keyWordFound = (!isNonIndicatorWord(strPossablieKeyWord));
                            strPossablieKeyWord = "";
                        }

                        if (doseWordExsist(strPossablieKeyWord))
                        {

                            if (isInList(strPossablieKeyWord))
                            {
                                int max = indecatorWords.Length,
                                w1 = 0,
                                w2,
                                index2 = 0;

                                bool found = false;

                                if (words.Count % 2 >= 1)
                                {
                                    double d = (words.Count / 2);
                                    w2 = int.Parse(Math.Floor(d).ToString());
                                    //w2 = int.Parse(Math.Ceiling(d).ToString());

                                }
                                else
                                {
                                    w2 = (words.Count / 2);
                                }

                                do
                                {
                                    if (words[w1].Word.ToUpper() == strPossablieKeyWord.ToUpper())
                                    {
                                        index2 = w1;
                                        found = true;
                                        break;
                                    }
                                    else if (words[w2].Word.ToUpper() == strPossablieKeyWord.ToUpper())
                                    {
                                        index2 = w2;
                                        found = true;
                                        break;
                                    }
                                    w1++;
                                    w2++;
                                } while (w2 < max);

                                if (found) words[index2].Increment();
                                strPossablieKeyWord = "";
                            }
                            else
                            {
                                WordPoints wp = new WordPoints(strPossablieKeyWord);
                                words.Add(wp);
                                strPossablieKeyWord = "";
                            }
                        }

                        else if (keyWordFound && !isIndecatorWord(strPossablieKeyWord) && !isNonIndicatorWord(strPossablieKeyWord))
                        {

                            if (doseNewWordExsist(strPossablieKeyWord))
                            {
                                drNewWord = dsDeaflt.Tables["New_Key_Words"].NewRow();

                                drNewWord["Word_ID"] = 100 + dsDeaflt.Tables["New_Key_Words"].Rows.Count;
                                drNewWord["Word"] = strPossablieKeyWord;

                                dsDeaflt.Tables["New_Key_Words"].Rows.Add(drNewWord);

                                daDeafultNewWord.Update(dsDeaflt, "New_Key_Words");

                                daWordCheck.Update(ds, "Key_word");

                                daNewWord.Update(ds, "New_Key_words");

                                WordPoints wp = new WordPoints(strPossablieKeyWord);
                                words.Add(wp);
                                strPossablieKeyWord = "";
                                keyWordFound = false;
                                indecatorFound = false;
                            }
                            else if (doseWordExsist(strPossablieKeyWord))
                            {
                                WordPoints wp = new WordPoints(strPossablieKeyWord);
                                words.Add(wp);
                                strPossablieKeyWord = "";
                                keyWordFound = false;
                                indecatorFound = false;
                            }
                            //indecatorFound = false;
                            //keyWordFound = false;

                        }

                        index++;
                        strPossablieKeyWord = "";
                    }


                    if (index == desc.Length) break;

                } while (true);
            }

            catch (IndexOutOfRangeException) { }

            if (words.Count > 0)
            {
                //sorting the list by word point value (decending)
                WordPoints temp;
                for (int i = 0; i < words.Count - 1; i++)
                {
                    for (int j = 0; j < words.Count; j++)
                    {
                        if (words[i].Points < words[j].Points)
                        {
                            temp = words[i];
                            words[i] = words[j];
                            words[j] = temp;
                        }
                    }
                }

                for (int i = 0; i < words.Count; i++)
                {
                    int con = getCon(words[0].Word, i);
                    if (con != 10000)
                    {
                        return con;
                    }
                }                
            }
            return 10000;
        }


        private int getCon(string word, int index)
        {
            //returns the resulst of the the quiry in a given index
            try
            {
                cmdContractor.Parameters["@w"].Value = word;
                daContractor.Update(ds, "Contractor");
            }
            catch (DBConcurrencyException)
            {
                cmdContractor.Parameters["@w"].Value = word;
                daContractor.Fill(ds, "Contractor");
            }

            try
            {
                drContractor = ds.Tables["Contractor"].Rows[index];
                return int.Parse(drContractor[0].ToString());
            }
            catch (NullReferenceException)
            {
                return 10000;
            }
            catch (IndexOutOfRangeException)
            {
                return 10000;
            }
        }

        //ckecks if the given word existis in the list of WordPoints
        private bool isInList(string str)
        {
            if (words.Count == 0) return false;

            else if (words.Count > 1)
            {

                int max = indecatorWords.Length,
                w1 = 0,
                w2;
                if (words.Count % 2 >= 1)
                {
                    double d = (words.Count / 2);
                    w2 = int.Parse(Math.Floor(d).ToString());

                }
                else
                {
                    w2 = (words.Count / 2);
                }

                if (words.Count > 1)
                {
                    do
                    {
                        try
                        {


                            if (words[w1].Word == str)
                            {
                                return true;
                            }
                            else if (words[w2].Word == str)
                            {
                                return true;
                            }
                            w1++;
                            w2++;
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            return false;
                        }
                    } while (w2 < max);
                }
            }

            else if (words[0].Word == str)
            {
                return true;
            }

            return false;

        }

        //checks if the given word should be ignoired
        private bool should_ignoir(string str)
        {

            try
            {
                cmdShouldIgnore.Parameters["@w"].Value = str;
                daShouldIgnore.Update(ds, "Key_word");
            }
            catch (DBConcurrencyException)
            {
                cmdShouldIgnore.Parameters["@w"].Value = str;
                daShouldIgnore.Fill(ds, "Key_word");
            }

            try
            {
                drShouldIgnore = ds2.Tables["Key_word"].Rows[0];
                return true;
            }
            catch (NullReferenceException)
            {
                return false;
            }
        }

        //checkes if the given word existes in the indecator word array
        private bool isIndecatorWord(string str)
        {
            int max = indecatorWords.Length,
            iw1 = 0,
            iw2 = (indecatorWords.Length / 2);
            do
            {
                if (indecatorWords[iw1].ToUpper() == str.ToUpper())
                {
                    indecatorFound = true;
                    return true;
                }
                else if (indecatorWords[iw2].ToUpper() == str.ToUpper())
                {
                    indecatorFound = true;
                    return true;
                }
                iw1++;
                iw2++;
            } while (iw2 < max);

            indecatorFound = false;
            return false;
        }

        //counts number of words in a given string
        public int word_Count(string str)
        {
            int count = 0;
            bool ignore = true;
            string word = "";

            foreach (char c in str)
            {
                if (char.IsLetter(c))
                {
                    word += c;
                    ignore = false;
                    continue;
                }
                else if (c == ' ' || char.IsSymbol(c) && !ignore)
                {
                    word = "";
                    count++;
                    ignore = true;

                }
            }

            return count;
        }

        //checks if givin word existis in DB
        private bool doseWordExsist(string str)
        {
            try
            {
                cmdWordCheck.Parameters["@w"].Value = str;
                daWordCheck.Update(ds, "Key_word");
            }
            catch (ArgumentNullException)
            {
                cmdWordCheck.Parameters["@w"].Value = str;
                daWordCheck.Fill(ds, "Key_word");
            }

            try
            {
                drWordCheck = ds.Tables["Key_word"].Rows[0];
                return true;
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }

        }
        
        //checks if givin new word existis in DB
        private bool doseNewWordExsist(string str)
        {
            try
            {
                cmdNewWord.Parameters["@w"].Value = str;
                daNewWord.Update(ds, "New_Key_words");
            }
            catch (DBConcurrencyException)
            {
                cmdNewWord.Parameters["@w"].Value = str;
                daNewWord.Fill(ds, "New_Key_words");
            }

            try
            {
                drNewWord = ds.Tables["Key_word"].Rows[0];
                return true;
            }
            catch (NullReferenceException)
            {
                return false;
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }

        }

        //checkes if the given word existes in the non-indecator word array
        private bool isNonIndicatorWord(string str)
        {
            int max = notIndecatorWords.Length,
            w1 = 0,
            w2;

            if (notIndecatorWords.Length % 2 >= 1)
            {
                double d = (notIndecatorWords.Length / 2);
                w2 = int.Parse(Math.Floor(d).ToString());
            }
            else
            {
                w2 = (notIndecatorWords.Length / 2);
            }

            if (notIndecatorWords.Length > 0)
            {
                do
                {
                    if (notIndecatorWords[w1] == str)
                    {
                        return true;
                    }
                    else if (notIndecatorWords[w2] == str)
                    {
                        return true;
                    }
                    w1++;
                    w2++;
                } while (w2 < max);
            }

            return false;

        }

        //find word in array
        private int search(string[] arr, string str)
        {
            int max = arr.Length,
            w1 = 0,
            w2;

            if (arr.Length % 2 >= 1)
            {
                double d = (arr.Length / 2);
                w2 = int.Parse(Math.Floor(d).ToString());
            }
            else
            {
                w2 = (arr.Length / 2);
            }

            if (words.Count > 0)
            {
                do
                {
                    if (arr[w1] == str)
                    {
                        return w1;
                    }
                    else if (arr[w2] == str)
                    {
                        return w2;
                    }
                    w1++;
                    w2++;
                } while (w2 < max);
            }

            return -1;

        }


        public void populate()
        {
            ////////comment this code if you want to unit test/////////
            //string userFile = File.ReadAllText("User.txt");
            //int toggle = 0;
            //for (int i = 0; i < userFile.Length; i++)
            //{
            //    if (userFile[i] == '|')
            //    {
            //        toggle++;
            //        continue;
            //    }

            //    if (toggle == 4) server += userFile[i];
            //}
            ////////////////////////////////////////////////////////////


            //DESKTOP-NSHTTAL (Local Server Name)
            connStr = @"Data Source = " + server + "; Initial Catalog = IT_Support_Tickect_System; Integrated Security = true";

            conn = new SqlConnection(connStr);
            conn.Open();

            daDeafultContractor = new SqlDataAdapter(sqlContractor, conn);
            cmdBDeafultContractor = new SqlCommandBuilder(daDeafultContractor);
            daDeafultContractor.FillSchema(dsDeaflt, SchemaType.Source, "Contractor");
            daDeafultContractor.Fill(dsDeaflt, "Contractor");

            daDeafultWordCheck = new SqlDataAdapter(sqlKeyWord, conn);
            cmdBDeafultWordCheck = new SqlCommandBuilder(daDeafultWordCheck);
            daDeafultWordCheck.FillSchema(dsDeaflt, SchemaType.Source, "Key_Words");
            daDeafultWordCheck.Fill(dsDeaflt, "Key_word");

            daDeafultNewWord = new SqlDataAdapter(sqlNewKeyWord, conn);
            cmdBDeafultNewWord = new SqlCommandBuilder(daDeafultNewWord);
            daDeafultNewWord.FillSchema(dsDeaflt, SchemaType.Source, "New_Key_Words");
            daDeafultNewWord.Fill(dsDeaflt, "New_Key_Words");

            cmdContractor = new SqlCommand(sqlGetContractor, conn);
            cmdContractor.Parameters.Add("@w", SqlDbType.VarChar);
            daContractor = new SqlDataAdapter(cmdContractor);
            daContractor.FillSchema(ds, SchemaType.Source, "Contractor");

            cmdWordCheck = new SqlCommand(sqlWordCheck, conn);
            cmdWordCheck.Parameters.Add("@w", SqlDbType.VarChar);
            daWordCheck = new SqlDataAdapter(cmdWordCheck);
            daWordCheck.FillSchema(ds, SchemaType.Source, "Key_word");

            cmdNewWord = new SqlCommand(sqlNewWordCheck, conn);
            cmdNewWord.Parameters.Add("@w", SqlDbType.VarChar);
            daNewWord = new SqlDataAdapter(cmdNewWord);
            daNewWord.FillSchema(ds, SchemaType.Source, "New_Key_words");

            cmdShouldIgnore = new SqlCommand(sqlShouldIgnore, conn);
            cmdShouldIgnore.Parameters.Add("@w", SqlDbType.VarChar);
            daShouldIgnore = new SqlDataAdapter(cmdShouldIgnore);
            daShouldIgnore.FillSchema(ds2, SchemaType.Source, "Key_Words");
        }
    }
}
