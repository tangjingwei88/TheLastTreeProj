﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;
using ProtoBuf;

namespace proto_conv
{
    abstract class ValueConstraint
    {
        protected string m_constraintString;

        protected ValueConstraint(string constraintString)
        {
            m_constraintString = constraintString;
        }

        public abstract bool GetValue(object v, out object vout);

        public override string ToString()
        {
            return m_constraintString;
        }

        public static ValueConstraint CreateConstraint(string typename, string constraintString)
        {
            if (string.IsNullOrEmpty(constraintString))
                return null;

            switch(typename)
            {
                case "int32":
                        return new IntMinMaxConstraint(constraintString);
                case "int64":
                        return new LongMinMaxConstraint(constraintString);
                case "float":
                        return new FloatMinMaxConstraint(constraintString);
                case "double":
                        return new DoubleMinMaxConstraint(constraintString);
                case "enum":
                        return new EnumConstraint(constraintString);
                case "string":
                        return new RegexConstraint(constraintString);
            }
            return null;
        }
    }

    class RegexConstraint : ValueConstraint
    {
        Regex m_regex;
        public RegexConstraint(string constraintString)
            : base(constraintString)
        {
            m_regex = new Regex(m_constraintString, RegexOptions.Multiline);

        }

        public override bool GetValue(object v, out object vout)
        {
            bool ret = false;
            Match m = m_regex.Match(v.ToString());
            if (!m.Success)
            {
                ret = true;
            }
            
            vout = v;
            
            return ret;
        }
    }

    class EnumConstraint : ValueConstraint
    {
        public string m_enumTypename;
        Dictionary<string, int> m_enumValues = new Dictionary<string, int>();

        public EnumConstraint(string constraintString)
            : base(constraintString)
        {
            string[] enumDefStrings = constraintString.Split(',');
            m_enumTypename = enumDefStrings[0].Split('=')[1];
            for (int i = 1; i < enumDefStrings.Length; i++)
            {
                string[] keyvalue = enumDefStrings[i].Split('=');
                m_enumValues.Add(keyvalue[0], int.Parse(keyvalue[1]));
            }

        }

        public override bool GetValue(object v, out object vout)
        {
            bool ret = false;
            int enumValue;
            if (!m_enumValues.TryGetValue(v.ToString(), out enumValue))
            {
                ret = true;
                vout = 0;
            }
            else
            {
                vout = enumValue;
            }
            return ret;
        }
    }

    class LongMinMaxConstraint : ValueConstraint
    {
        public long m_min;
        public long m_max;

        public LongMinMaxConstraint(string constraintString)
            : base(constraintString)
        {
            string[] minmax = constraintString.Split('-');
            m_min = long.Parse(minmax[0]);
            m_max = long.Parse(minmax[1]);
        }

        public override bool GetValue(object v, out object vout)
        {
            bool ret = false;
            long iv = (long)v;
            if(iv > m_max)
            {
                ret = true;
                iv = m_max;
            }
            else if (iv < m_min)
            {
                ret = true;
                iv = m_min;
            }
            vout = iv;
            return ret;
        }
    }

    class IntMinMaxConstraint : ValueConstraint
    {
        public int m_min;
        public int m_max;

        public IntMinMaxConstraint(string constraintString)
            : base(constraintString)
        {
            string[] minmax = constraintString.Split('-');
            m_min = int.Parse(minmax[0]);
            m_max = int.Parse(minmax[1]);
        }

        public override bool GetValue(object v, out object vout)
        {
            bool ret = false;
            int iv = (int)v;
            if (iv > m_max)
            {
                ret = true;
                iv = m_max;
            }
            else if (iv < m_min)
            {
                ret = true;
                iv = m_min;
            }
            vout = iv;
            return ret;
        }
    }

    class FloatMinMaxConstraint : ValueConstraint
    {
        public float m_min;
        public float m_max;


        public FloatMinMaxConstraint(string constraintString)
            : base(constraintString)
        {
            m_constraintString = constraintString.Replace('#', '.');
            string[] minmax = m_constraintString.Split('-');
            m_min = float.Parse(minmax[0]);
            m_max = float.Parse(minmax[1]);
        }

        public override bool GetValue(object v, out object vout)
        {
            bool ret = false;
            float iv = (float)v;
            if (iv > m_max)
            {
                ret = true;
                iv = m_max;
            }
            else if (iv < m_min)
            {
                ret = true;
                iv = m_min;
            }
            vout = iv;
            return ret;
        }
    }


    class DoubleMinMaxConstraint : ValueConstraint
    {
        public double m_min;
        public double m_max;

        public DoubleMinMaxConstraint(string constraintString)
            : base(constraintString)
        {
            m_constraintString = constraintString.Replace('#', '.');
            string[] minmax = m_constraintString.Split('-');
            m_min = double.Parse(minmax[0]);
            m_max = double.Parse(minmax[1]);
        }

        public override bool GetValue(object v, out object vout)
        {
            bool ret = false;
            double iv = (double)v;
            if (iv > m_max)
            {
                ret = true;
                iv = m_max;
            }
            else if (iv < m_min)
            {
                ret = true;
                iv = m_min;
            }
            vout = iv;
            return ret;
        }
    }

    class FieldInfo
    {
        public string m_fieldName;
        public string m_columnName;
        public string m_typeName;
        public string m_desc;
        public ValueConstraint m_constraint;
    }

    

    class conv
    {
        static string protoGenPath = AppDomain.CurrentDomain.BaseDirectory + "ProtoGen\\protogen.exe";
        static string protobufDll = AppDomain.CurrentDomain.BaseDirectory + "ProtoGen\\protobuf-net.dll";
        static string protoGenWorkDir = AppDomain.CurrentDomain.BaseDirectory + "temp";


        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine(@"Usage: proto_conv.exe -en <xlsx path>");
                return;
            }
            else
            {
                if (args[0].Equals("-ch") || args[0].Equals("-en") || args[0].Equals("-ct") || args[0].Equals("-ko"))
                {
                }
                else
                {
                    Console.WriteLine("语言参数错误!");
                    return;
                }

                string language = args[0].ToLower().Substring(1);
                Console.WriteLine(@"language:" + language);

                string inputFilename = Path.GetFullPath(args[1]);
                string inputDirName = Path.GetDirectoryName(inputFilename);
                string outputPath = inputDirName;

                string part_name = args[1].ToLower();
                string format = "";
                if (args[1].EndsWith(".xlsx"))
                    format = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0 XML;HDR=YES;IMEX=1;'";
                else if (args[1].EndsWith(".xls"))
                    format = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES;IMEX=1'";
                else
                {
                    Console.WriteLine("File Type Error!");
                    return;
                }
                string cs = string.Format(format, inputFilename);
                OleDbConnection CNN = new OleDbConnection(cs);
                CNN.Open();



                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(protoGenWorkDir);
                if (di.Exists)
                {
                    di.Delete(true);
                }
                di.Create();


                DataTable ttable = CNN.GetSchema("Tables");

                foreach (DataRow row in ttable.Rows)
                {
                    if (row["Table_Type"].ToString() == "TABLE")
                    {
                        string tabName = row["Table_Name"].ToString();

                        //Console.WriteLine(tabName);
                        if (tabName.ToLower().Contains("description") || tabName.Contains("#_FilterDatabase") || !tabName.ToLower().Contains("config"))
                            continue;

                        OleDbDataAdapter adpt = new OleDbDataAdapter("select * from [" + tabName + "]", CNN);
                        DataTable dt = new DataTable();
                        adpt.Fill(dt);

                        if (dt.Rows.Count == 0)
                            continue;
                        tabName = tabName.Replace("$", "");
                        //Console.WriteLine("gen proto..." + tabName);
                        Dictionary<string, FieldInfo> fieldInfo = new Dictionary<string, FieldInfo>();
                        string s = GenProto(dt, tabName, fieldInfo, language);


                        string protoFilename = di.FullName + "\\" + tabName + ".proto";
                        StreamWriter sw = File.CreateText(protoFilename);
                        sw.Write(s);
                        sw.Close();

                        string outFilename = inputDirName + "\\code\\" + tabName + ".cs";
                        string param = string.Format("-i:\"{0}\" -o:\"{1}\" -ns:m", protoFilename, outFilename);
                        ProcessStartInfo psi = new ProcessStartInfo();
                        psi.FileName = protoGenPath;
                        psi.Arguments = param;
                        psi.WorkingDirectory = protoGenWorkDir;
                        psi.UseShellExecute = false;
                        Process proc = System.Diagnostics.Process.Start(psi);
                        proc.WaitForExit();

                        string code = File.ReadAllText(outFilename);
                        Assembly lib = CompileCS(code);
                        string typename = "m." + tabName;
                        Type t = lib.GetType(typename);
                        PropertyInfo[] properties = t.GetProperties();

                        FileStream fs = new FileStream(outputPath + "\\protodata\\" + tabName + ".bytes", FileMode.Create);
                        BinaryWriter bw = new BinaryWriter(fs);
                        //bw.Write(t.FullName);
                        //bw.Write((uint)dt.Rows.Count);
                        List<ProtoBuf.IExtensible> outdata = new List<ProtoBuf.IExtensible>();
                        System.Reflection.ConstructorInfo ci = t.GetConstructor(Type.EmptyTypes);
                        int rowCount = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            ProtoBuf.IExtensible o = ci.Invoke(new object[0]) as ProtoBuf.IExtensible;
                            foreach (PropertyInfo pi in properties)
                            {
                                FieldInfo fi = fieldInfo[pi.Name];
                                string valueString = dr[fi.m_columnName].ToString();
                                try
                                {
                                    object v = GetValue(fi, valueString);
                                    pi.SetValue(o, v, BindingFlags.SetProperty, null, null, null);

                                    //Console.WriteLine(string.Format(" 行: {0}, 列: {1}", rowCount + 2, fi.m_desc));
                                }
                                catch (Exception ex)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine(string.Format("{0}, 行: {1}, 列: {2}", ex.Message, rowCount + 2, fi.m_desc));
                                    Console.ResetColor();
                                }
                            }
                         

                            outdata.Add(o);
                            rowCount++;
                            //ProtoBuf.Serializer.NonGeneric.SerializeWithLengthPrefix(fs, o, ProtoBuf.PrefixStyle.Fixed32, 0);
                        }
                        Serializer.Serialize<List<ProtoBuf.IExtensible>>(fs, outdata);
                        fs.Close();
                    }
                }
                CNN.Close();
            }
            
        }

        static object GetValue(FieldInfo fi, string v)
        {
            object ret = null;
            try
            {
                switch (fi.m_typeName)
                {
                    case "float":
                        {
                            int i = v.IndexOf('%');
                            if (i >= 0)
                                ret = float.Parse(v.Substring(0, i + 1));
                            else
                                ret = float.Parse(v);
                        }
                        break;
                    case "double":
                        {
                            int i = v.IndexOf('%');
                            if (i >= 0)
                                ret = double.Parse(v.Substring(0, i + 1));
                            else
                                ret = double.Parse(v);
                        }
                        break;
                    case "int32":
                        ret = int.Parse(v);
                        break;
                    case "int64":
                        ret = long.Parse(v);
                        break;
                    case "bool":
                        ret = (v.ToLower() == "true" || v == "1");
                        break;
                    case "string":
                        ret = v;
                        break;
                    case "enum":
                        ret = v;
                        break;

                }
            }
            catch (Exception)
            {
                throw new Exception(string.Format("无效的值: '{0}'", v));
            }

            if (fi.m_constraint != null)
            {
                if (fi.m_constraint.GetValue(ret, out ret))
                {
                    throw new Exception(string.Format("警告: 值({0})不在约束范围({1})内!", v, fi.m_constraint.ToString()));
                }
            }
            return ret;
        }

        static Assembly CompileCS(string code)
        {
            CodeDomProvider domProvider = CodeDomProvider.CreateProvider("CSharp");
            CompilerParameters cp = new CompilerParameters();
            cp.GenerateExecutable = false;
            cp.GenerateInMemory = true;
            cp.ReferencedAssemblies.Add(protobufDll);

            CompilerResults cr = domProvider.CompileAssemblyFromSource(cp, code);
            if (cr.Errors.Count > 0)
            {
                Console.WriteLine("compile error!");
                foreach (CompilerError ce in cr.Errors)
                {
                    Console.WriteLine("  {0}", ce.ToString());
                    Console.WriteLine();
                }
            }
            System.Reflection.Assembly assembly = cr.CompiledAssembly;
            return assembly;
        }

        static FieldInfo GetFieldInfo(string columnName)
        {
            Regex regx;
            regx = new Regex(@"\A(?<desc>.+?)\|(?<name>.+?)\((?<type>.+?)(:(?<constraint>.*?))?\)$");

            Match mat = regx.Match(columnName);
            if (mat.Success)
            {
                string name = mat.Groups["name"].Value;
                string type = mat.Groups["type"].Value;
                string desc = mat.Groups["desc"].Value;

                string constraint = mat.Groups["constraint"].Value;
                
                FieldInfo fi = new FieldInfo();
                fi.m_columnName = columnName;
                fi.m_fieldName = name;
                fi.m_typeName = type;
                fi.m_desc = desc;
                fi.m_constraint = ValueConstraint.CreateConstraint(type, constraint);
  
                return fi;
            }
            else
            {
                throw new Exception("标题格式错误: " + columnName);
            }

        }

        static string GenEnumDef(string enumDef)
        {
            string[] enumDefStrings = enumDef.Split(',');
            StringBuilder sb = new StringBuilder();
            string[] keyvalue = enumDefStrings[0].Split('=');
            if(keyvalue[0] != "type")
            {
                throw new Exception("错误：第一个枚举定义必须是type.");
            }
            sb.AppendFormat("enum {0} {{\n", keyvalue[1]);

            for (int i = 1; i < enumDefStrings.Length; i++)
            {
                sb.Append("\t");
                sb.AppendLine(enumDefStrings[i] + ";");
            }

            sb.AppendLine("}");
            return sb.ToString();
        }

        static string GenProto(DataTable t, string tablename, Dictionary<string, FieldInfo> fieldInfo, string language)
        {
            StringBuilder sb = new StringBuilder();
            string m_fieldName_ends;
            int m_index;

            sb.AppendFormat("message {0}\n{{\n", tablename);

            fieldInfo.Clear();
            int c = 1;
            foreach (DataColumn dc in t.Columns)
            {
                if (!dc.ColumnName.Contains('|'))
                    continue;
                FieldInfo fi = GetFieldInfo(dc.ColumnName);

                m_index = fi.m_fieldName.IndexOf("_");
                if (m_index != -1) 
                {
                    m_fieldName_ends = fi.m_fieldName.Substring(m_index+1).ToLower();

                    if (m_fieldName_ends != "ch" && m_fieldName_ends != "en" && m_fieldName_ends != "ct" && m_fieldName_ends!="ko")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("excel error at column " + dc.ColumnName);
                        Console.ResetColor();
                    }

                    if (m_fieldName_ends != language)
                    {
                        //Console.WriteLine("other language: " + m_fieldName_ends);
                        continue;
                    }
                    else
                    {
                        fi.m_fieldName = fi.m_fieldName.Substring(0,m_index);
                        Console.WriteLine(" new field name: " + fi.m_fieldName);
                    }

                }

                string typename = fi.m_typeName;
                if (typename == "enum")
                {
                    sb.AppendLine(GenEnumDef(fi.m_constraint.ToString()));
                    typename = (fi.m_constraint as EnumConstraint).m_enumTypename;
                }

                fieldInfo.Add(fi.m_fieldName, fi);
                sb.AppendFormat("\trequired {0} {1} = {2};\n", typename, fi.m_fieldName, c++);               
            }

            sb.Append("}");


            return sb.ToString();
        }
    }
}
