﻿using System;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace SimpleTemplates
{
    [Cmdlet(VerbsCommon.New, "STFunction" )]
    [OutputType(typeof(string))]
    public class NewSTBasicFunction : PSCmdlet
    {
        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        public string[] FunctionName { get; set; }
        private const string TemplateFileName = "templates/Function.ps1t";

        protected override void BeginProcessing()
        {
            // check if template file exists
            if (!(File.Exists(TemplateFileName))) {
                throw new FileNotFoundException("Template file not found", TemplateFileName);
            }
            WriteVerbose($"Using template file {TemplateFileName}");
        }

        protected override void ProcessRecord()
        {
            foreach (string F1 in FunctionName) {
                Util.CreateFileFromTemplate(this, TemplateFileName, F1);
            }
        }

        protected override void EndProcessing()
        {
            WriteVerbose("End!");
        }
    }
}
