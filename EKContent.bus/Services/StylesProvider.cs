﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using EKContent.bus.Entities;


namespace EKContent.bus.Services
{
    public class StylesProvider
    {


        PageService _pageService = null;
        public string RandomString { get; set; }
        public StylesProvider(PageService service)
        {
            this._pageService = service;
            RandomString = GenerateRandomString();
        }


        private Site Site
        {
            get { return _pageService.GetSite(); }
        }


        private StyleSettings StyleSettings
        {
            get { return _pageService.GetStyleSettings(); }
        }

        private bool HasCustomStyle()
        {
            return !String.IsNullOrEmpty(Site.CustomStyleSheetIdentifier);
        }

        public string GetStyleSheet(string baseStyleSheet)
        {
            return String.Format("{0}{1}{2}.css", HasCustomStyle() ? Site.CustomStyleSheetIdentifier : String.Empty, HasCustomStyle() ? ".custom." : String.Empty, baseStyleSheet);
        }

        public string GetResponsiveStyleSheet(string baseStyleSheet)
        {
            return String.Format("{0}{1}{2}.css", HasCustomStyle() ? Site.CustomStyleSheetIdentifier : String.Empty, HasCustomStyle() ? "." : String.Empty, baseStyleSheet);
        }

        private void AddProperty(List<string> properties, string name, string value)
        {
            if (!String.IsNullOrEmpty(value))
                properties.Add(String.Format("@{0}: {1};", name, value));
        }

        public string GetVariableString()
        {
            var data = new List<string>();
            foreach (var setting in VariablesList())
                if(!setting.isDefault())
                    AddProperty(data, setting.Key, setting.Value);
            return String.Join(Environment.NewLine, data.ToArray());
        }

        public string GenerateRandomString()
        {
            var random = new Random();
            var color = String.Format("#{0:X6}", random.Next(0x1000000));
            return String.Format("_{0}", color.Substring(1));
        }

        public List<FileInfo> CustomFiles()
        {
            var list = new List<FileInfo>();
            list.AddRange(LessDirectory().GetFiles("*.custom.*"));
            list.AddRange(ContentDirectory().GetFiles("*.custom.*"));
            return list;
        }

        public DirectoryInfo LessDirectory()
        {
            return new DirectoryInfo(AbsPath("~/content/less"));
        }

        public DirectoryInfo ContentDirectory()
        {
            return new DirectoryInfo(AbsPath("~/content/css"));
        }

        public string ReplaceContents(string contents, string toReplace, string replaceWith)
        {
            return contents.Replace(toReplace, replaceWith);
        }

        public string ReplaceVariables(string start, string newFile)
        {
            return ReplaceContents(start, "variables.less", String.Format("{0}.less", newFile));
        }

        public string VariableGeneratedFileName
        {
            get { return String.Format("{0}.custom.variables.less", RandomString); }
        }

        public string BootstrapLessGeneratedFile
        {
            get { return String.Format("{0}.custom.bootstrap.less", RandomString); }
        }

        public string ResponsiveLessGeneratedFile
        {
            get { return String.Format("{0}.custom.responsive.less", RandomString); }
        }

        public string RandomBootstrapCssFileName
        {
            get { return String.Format("{0}.custom.bootstrap.min.css", RandomString); }
        }


        public string RandomBootstrapResponsiveCssFileName
        {
            get { return String.Format("{0}.custom.bootstrap-responsive.min.css", RandomString); }
        }

        private string AbsPath(string relativePath)
        {
            return System.Web.HttpContext.Current.Server.MapPath(relativePath);
        }
        public string AbsolutePathToCssFile(string file)
        {
            return AbsPath(String.Format("~/content/css/{0}", file));
        }

        public string AbsolutePathToLessFile(string file)
        {
            return AbsPath(String.Format("~/content/less/{0}", file));
        }

        public string FileGeneratedBootstrapDotCss
        {
            get { return AbsolutePathToCssFile(RandomBootstrapCssFileName); }
        }

        public string FileGeneratedResponsiveDotCss
        {
            get { return AbsolutePathToCssFile(RandomBootstrapResponsiveCssFileName); }
        }

        public string FileGeneratedBootstrapDotLess
        {
            get { return AbsolutePathToLessFile(BootstrapLessGeneratedFile); }
        }

        public string FileGeneratedResponsiveDotLess
        {
            get { return AbsolutePathToLessFile(ResponsiveLessGeneratedFile); }
        }

        public string FileGeneratedVariablesDotLess
        {
            get { return AbsolutePathToLessFile(VariableGeneratedFileName); }
        }

        public string GetBootstrapContents()
        {
            return ReadFile(FileBootstrapDotLess);
        }

        public string GetResponsiveContents()
        {
            return ReadFile(FileReponsiveDotLess);
        }

        public string FileBootstrapDotLess
        {
            get
            {
                return AbsolutePathToLessFile("bootstrap.less");
            }
        }

        public string FileReponsiveDotLess
        {
            get
            {
                return AbsolutePathToLessFile("responsive.less");
            }
        }

        public string FileVariablesDotLess
        {
            get
            {
                return AbsolutePathToLessFile("variables.less");
            }           
        }

        public string FileExe
        {
            get
            {
                return AbsPath("~/libs/dotless.compiler.exe");
            }
        }

        public string ReadFile(string file)
        {
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(file);
                return sr.ReadToEnd();
            }
            finally
            {
                sr.Close();
                sr.Dispose();
            }
        }

        public void WriteFile(string file, string contents)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(file);
                sw.Write(contents);
                sw.Flush();
            }
            finally
            {
                sw.Close();
                sw.Dispose();
            }
        }

        public List<StyleSetting> VariablesList()
        {
            var input = ReadFile(FileVariablesDotLess);
             var regex = new Regex("@(?<name>[^@]*?):(?<value>[^;]*?);");
             var defaultValues = new List<StyleSetting>();
            //var results = regex.Matches(input);
            var lines = input.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            string category = String.Empty;
            foreach(var line in lines){
                var match = Regex.Match(line, "// (?<category>[A-Za-z ]+)" );

                if(match.Success)
                    category = match.Groups["category"].Value;
                match = regex.Match(line);
                if(match.Success)
                    defaultValues.Add(new StyleSetting{ Key = match.Groups["name"].Value.Trim(), Value = match.Groups["value"].Value.Trim(), DefaultValue = match.Groups["value"].Value.Trim(), Category = category });

            }
            var current = _pageService.GetStyleSettings().Settings;
            /*var defaultValues = (from Match match in results
                        select
                            new StyleSetting { Key = match.Groups["name"].Value.Trim(), Value = match.Groups["value"].Value.Trim(), DefaultValue = match.Groups["value"].Value.Trim() }).ToList();
             * */
            foreach (var item in defaultValues)
            {
                var currentItem = current.SingleOrDefault(cur => cur.Key == item.Key);
                if (currentItem != null)
                    item.Value = currentItem.Value;
            }
            return defaultValues.ToList();
        }

        public void Generate()
        {
            Revert();
            var variableString = GetVariableString();
            var empty = variableString == String.Empty;
            WriteFile(FileGeneratedVariablesDotLess, GetVariableString());
            WriteFile(FileGeneratedBootstrapDotLess,
                ReplaceContents(GetBootstrapContents(), "[custom.less]", empty ? String.Empty : VariableGeneratedFileName));
            WriteFile(FileGeneratedResponsiveDotLess,
                ReplaceContents(GetResponsiveContents(), "[custom.less]",empty ? String.Empty : VariableGeneratedFileName));
            ProcessDotLess(FileGeneratedBootstrapDotLess, FileGeneratedBootstrapDotCss);
            ProcessDotLess(FileGeneratedResponsiveDotLess, FileGeneratedResponsiveDotCss);
            var site = Site;
            site.CustomStyleSheetIdentifier = RandomString;
            _pageService.SetSite(site);
        }

        public void Revert()
        {
            foreach (var file in CustomFiles())
            {
                File.Delete(file.FullName);
            }
            var site = Site;
            site.CustomStyleSheetIdentifier = String.Empty;
            _pageService.SetSite(site);
        }

        public void Clear()
        {
            foreach (var file in CustomFiles())
            {
                File.Delete(file.FullName);
            }
            var site = Site;
            site.CustomStyleSheetIdentifier = String.Empty;
            _pageService.SetSite(site);
            _pageService.Dal.StyleSettingsProvider.Clear();
        }

        public void ProcessDotLess(string input, string output)
        {
            var startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.FileName = this.FileExe;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = String.Format(@"""{0}"" ""{1}""", input, output);

            try
            {
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }
    }
}
