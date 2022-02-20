﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_4
{
    public class Functionality
    {
        public static string textToReplace;
        public static string text;
        public static string rootfolder = @"C:\Temp\Template";
        public static string efType;
        public static string templateOfChoice;
        public static string connectionStringReplace;

        public static List<string> templates = new List<string>();
        public static List<string> props = new List<string>();
        public static List<string> types = new List<string>();
        public static List<string> required = new List<string>();
        public static List<string> propsConcatList = new List<string>();
        public static List<string> cPropsConcatList = new List<string>();
        public static List<string> efPropsConcatList = new List<string>();
        public static List<string> efDataContextPropsConcatList = new List<string>();

        //Seeding List Of Templates
        public static void SeedingListOfTemplates()
        {
            templates.Add("webapidotnet6");
            templates.Add("Webapi dotnet 6 With DTO");
            templates.Add("Webapi dotnet 5 Minimal");
            templates.Add("Webapi dotnet 5 With Mappers");
        }


        public static void ReplaceTags()
        {
            
            string[] files = Directory.GetFiles(rootfolder, "*.*", SearchOption.AllDirectories);

            //Creating Properties Model
            for (int i = 0; i < props.Count; i++)
            {
                string propsConcat = required[i] + "\n        public " + types[i] + " " + (props[i]) + " { get; set; } = string.Empty;";
                propsConcatList.Add(propsConcat);
            }
            var propsJoined = String.Join("\n        ", propsConcatList.ToArray());

            //TAG_{'propertiesModel'}
            foreach (string file in files)
            {
                try
                {
                    string contents = File.ReadAllText(file);
                    contents = contents.Replace(@"TAG_{'propertiesModel'}", propsJoined);
                    // Make files writable
                    File.SetAttributes(file, FileAttributes.Normal);
                    File.WriteAllText(file, contents);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }


            //Creating Controller Model
            for (int i = 0; i < props.Count; i++)
            {
                string propsConcat = "\n        dbtemplate." + props[i] + " = request." + props[i] + ";";
                cPropsConcatList.Add(propsConcat);
            }
            var cPropsJoined = String.Join("\n        ", cPropsConcatList.ToArray());

            //TAG_{'propertiesModel'}
            foreach (string file in files)
            {
                try
                {
                    string contents = File.ReadAllText(file);
                    contents = contents.Replace(@"TAG_{'controllerPutModel'}", cPropsJoined);
                    // Make files writable
                    File.SetAttributes(file, FileAttributes.Normal);
                    File.WriteAllText(file, contents);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }



            //Creating Ef Properties Model

            for (int i = 0; i < props.Count; i++)
            {
                if (types[i] == "string")
                    efType = "nvarchar(max)";
                if (types[i] == "int")
                    efType = "int";
                if (types[i] == "boolean")
                    efType = "bool";

                string propsConcat = "\n                    Name = table.Column<" + types[i] + ">(type: \"" + efType + "\", nullable: false),";
                efPropsConcatList.Add(propsConcat);
            }
            var efPropsJoined = String.Join("\n                    ", efPropsConcatList.ToArray());


            //TAG_{ 'propertiesEf'}
            foreach (string file in files)
            {
                try
                {
                    string contents = File.ReadAllText(file);
                    contents = contents.Replace(@"TAG_{'propertiesEf'}", efPropsJoined);
                    // Make files writable
                    File.SetAttributes(file, FileAttributes.Normal);
                    File.WriteAllText(file, contents);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }



            //Creating Ef Data Context Properties Model

            for (int i = 0; i < props.Count; i++)
            {
                if (types[i] == "string")
                    efType = "nvarchar(max)";
                if (types[i] == "int")
                    efType = "int";
                if (types[i] == "boolean")
                    efType = "bool";

                string propsConcat = "\n                    b.Property<" + types[i] + ">(\"" + props[i] + "\")\n                        .IsRequired()\n                        .HasColumnType(\"" + efType + "\");";
                efDataContextPropsConcatList.Add(propsConcat);
            }
            var efDataContextPropsJoined = String.Join("\n                    ", efDataContextPropsConcatList.ToArray());


            //TAG_{ 'propertiesEfDesigner'}
            foreach (string file in files)
            {
                try
                {
                    string contents = File.ReadAllText(file);
                    contents = contents.Replace(@"TAG_{'propertiesEfDesigner'}", efDataContextPropsJoined);
                    // Make files writable
                    File.SetAttributes(file, FileAttributes.Normal);
                    File.WriteAllText(file, contents);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            //TAG_{ 'ConnectionString'}
            foreach (string file in files)
            {
                try
                {
                    string contents = File.ReadAllText(file);
                    contents = contents.Replace(@"TAG_{'ConnectionString'}", connectionStringReplace);
                    // Make files writable
                    File.SetAttributes(file, FileAttributes.Normal);
                    File.WriteAllText(file, contents);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
