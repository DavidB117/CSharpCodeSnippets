using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security;

namespace CreateJsonFileDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Create Data
            Data data1 = new Data
            {
                Id = "0",
                Message = "Delta Alpha Victor India Delta",
                Date = DateTime.UtcNow.ToString()
            };
            Data data2 = new Data
            {
                Id = "1",
                Message = "Juliet Oscar Hotel November",
                Date = DateTime.UtcNow.ToString()
            };

            EventData eventData1 = new EventData
            {
                TTID = "TTID1",
                TTUID = "TTUID1",
                UTC = DateTime.UtcNow.ToString(),
                E = "Event1",
                O = data1,
                ID = "Data1"
            };
            EventData eventData2 = new EventData
            {
                TTID = "TTID2",
                TTUID = "TTUID2",
                UTC = DateTime.UtcNow.ToString(),
                E = "Event2",
                O = data2,
                ID = "Data2"
            };

            List<EventData> eventDataList = new List<EventData>
            {
                eventData1,
                eventData2
            };
            #endregion

            string eventObject = JsonConvert.SerializeObject(eventDataList, Formatting.Indented);
            Console.WriteLine("eventObject = " + eventObject);

            // Create a file to store Json object.
            string path = string.Empty;
            try
            {
                path = Environment.CurrentDirectory.ToString();
                System.IO.File.WriteAllText(path + @"\events.json", eventObject);
            }
            catch (Exception e)
            {
                PrintException(e);
            }

            // Read in Json data from the events.json file.
            string jsonData = string.Empty;
            try
            {
                jsonData = System.IO.File.ReadAllText(path + @"\events.json");
            }
            catch (Exception e)
            {
                PrintException(e);
            }
            Console.WriteLine("\njsonData = " + jsonData + "\n");

            // Store Json data in a list, then iterate through the list to get each individual piece of data.
            List<EventData> eventData = JsonConvert.DeserializeObject<List<EventData>>(jsonData);

            #region Add More Data
            // Add another event to the list and rewrite the file.
            Data data3 = new Data
            {
                Id = "2",
                Message = "Lima Alpha Romeo Romeo Yankee",
                Date = DateTime.UtcNow.ToString()
            };
            EventData eventData3 = new EventData
            {
                TTID = "TTID3",
                TTUID = "TTUID3",
                UTC = DateTime.UtcNow.ToString(),
                E = "Event3",
                O = data3,
                ID = "Data3"
            };
            eventData.Add(eventData3);
            #endregion

            // Serialize list to Json with new event data
            eventObject = JsonConvert.SerializeObject(eventData, Formatting.Indented);
            Console.WriteLine("\neventObject = " + eventObject);

            System.IO.File.WriteAllText(path + @"\events.json", eventObject);

            // Delete the events.json file
            Console.Write("\nDelete events.json file (y/n) ? ");
            string input = Console.ReadLine();
            if (input[0] == 'y')
            {
                System.IO.File.Delete(path + @"\events.json");
                Console.WriteLine("\nevents.json has been deleted\n");
            }

            // Keep program from exiting while debugging
            Console.WriteLine("\nPress any key to end program . . .");
            Console.ReadKey();
        }

        #region Methods
        // PrintException
        private static void PrintException(Exception e)
        {
            Console.WriteLine("Source: " + e.Source + " - " + e.TargetSite);
            Console.WriteLine("Exception: " + e.Message);
            Console.WriteLine("Call Stack: " + e.StackTrace);
        }

        // PrintList
        private static void PrintList<T>(List<T> list)
        {
            Console.Write("[ ");
            for (int i = 0; i < list.Count - 1; i++)
            {
                Console.Write(list[i] + ", ");
            }
            Console.WriteLine(list[list.Count - 1] + " ]");
        }
        #endregion
    }

    #region ObjectDumper
    public class ObjectDumper
    {
        private TextWriter Writer;
        private int Pos;
        private int Level;
        private readonly int Depth;

        private ObjectDumper(int depth)
        {
            this.Depth = depth;
        }

        public static void Write(object element)
        {
            Write(element, 0);
        }

        public static void Write(object element, int depth)
        {
            Write(element, depth, Console.Out);
        }

        public static void Write(object element, int depth, TextWriter log)
        {
            ObjectDumper dumper = new ObjectDumper(depth)
            {
                Writer = log
            };
            dumper.WriteObject(null, element);
        }

        private void Write(string s)
        {
            if (s != null)
            {
                Writer.Write(s);
                Pos += s.Length;
            }
        }

        private void WriteIndent()
        {
            for (int i = 0; i < Level; i++) Writer.Write("  ");
        }

        private void WriteLine()
        {
            Writer.WriteLine();
            Pos = 0;
        }

        private void WriteTab()
        {
            Write("  ");
            while (Pos % 8 != 0) Write(" ");
        }

        private void WriteObject(string prefix, object element)
        {
            if (element == null || element is ValueType || element is string)
            {
                WriteIndent();
                Write(prefix);
                WriteValue(element);
                WriteLine();
            }
            else
            {
                if (element is IEnumerable enumerableElement)
                {
                    foreach (object item in enumerableElement)
                    {
                        if (item is IEnumerable && !(item is string))
                        {
                            WriteIndent();
                            Write(prefix);
                            Write("...");
                            WriteLine();
                            if (Level < Depth)
                            {
                                Level++;
                                WriteObject(prefix, item);
                                Level--;
                            }
                        }
                        else
                        {
                            WriteObject(prefix, item);
                        }
                    }
                }
                else
                {
                    MemberInfo[] members = element.GetType().GetMembers(BindingFlags.Public | BindingFlags.Instance);
                    WriteIndent();
                    Write(prefix);
                    bool propWritten = false;
                    foreach (MemberInfo m in members)
                    {
                        FieldInfo f = m as FieldInfo;
                        PropertyInfo p = m as PropertyInfo;
                        if (f != null || p != null)
                        {
                            if (propWritten)
                            {
                                WriteTab();
                            }
                            else
                            {
                                propWritten = true;
                            }
                            Write(m.Name);
                            Write("=");
                            Type t = f != null ? f.FieldType : p.PropertyType;
                            if (t.IsValueType || t == typeof(string))
                            {
                                WriteValue(f != null ? f.GetValue(element) : p.GetValue(element, null));
                            }
                            else
                            {
                                if (typeof(IEnumerable).IsAssignableFrom(t))
                                {
                                    Write("...");
                                }
                                else
                                {
                                    Write("{ }");
                                }
                            }
                        }
                    }
                    if (propWritten) WriteLine();
                    if (Level < Depth)
                    {
                        foreach (MemberInfo m in members)
                        {
                            FieldInfo f = m as FieldInfo;
                            PropertyInfo p = m as PropertyInfo;
                            if (f != null || p != null)
                            {
                                Type t = f != null ? f.FieldType : p.PropertyType;
                                if (!(t.IsValueType || t == typeof(string)))
                                {
                                    object value = f != null ? f.GetValue(element) : p.GetValue(element, null);
                                    if (value != null)
                                    {
                                        Level++;
                                        WriteObject(m.Name + ": ", value);
                                        Level--;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void WriteValue(object o)
        {
            if (o == null)
            {
                Write("null");
            }
            else if (o is DateTime)
            {
                Write(((DateTime)o).ToShortDateString());
            }
            else if (o is ValueType || o is string)
            {
                Write(o.ToString());
            }
            else if (o is IEnumerable)
            {
                Write("...");
            }
            else
            {
                Write("{ }");
            }
        }
    }
    #endregion
}
