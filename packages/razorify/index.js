#! /usr/bin/env node
const { readFileSync, writeFileSync } = require("fs");
const path = require("path");

const XRegExp = require("xregexp");
const { prepend, capitalize } = require("./helpers");

const yargs = require("yargs/yargs");
const { hideBin } = require("yargs/helpers");
const argv = yargs(hideBin(process.argv)).argv;

const input_filepath = path.resolve(__dirname, argv.file ?? "index.html");
const input_filename = capitalize(input_filepath.replace(".html", ""));

const appname = path.basename(path.resolve());
console.log("appname :>> ", appname);

console.log("input_filepath :>> ", input_filepath);
console.log("input_filename :>> ", input_filename);

const html = readFileSync(input_filepath, { encoding: "utf8", flag: "r" });
const regex = /<body>(?<body>(.|\n)*)<\/body>/gi;
const body = XRegExp.exec(html, regex).groups.body;

const final_body = prepend(
  body.trim(),
  `
@page
@model IndexModel
@{
    // var foo = 'bar';
}

`
);

const razorfilename = `${input_filename}.cshtml`;
const razorcodebehind_filename = `${input_filename}.cshtml.cs`;

console.log("razorfilename :>> ", razorfilename);
console.log("razorcodebehind_filename :>> ", razorcodebehind_filename);


writeFileSync(razorfilename, final_body);

const cs_html = `
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ${appname}.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }        
    }
}

`;

writeFileSync(razorcodebehind_filename, cs_html);

module.exports = {

}