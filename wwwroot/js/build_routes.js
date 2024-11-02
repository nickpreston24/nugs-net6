'use strict';

const fs = require('fs');
const {readdirSync} = require('fs');
const path = require('path');
// const XRegExp = require('xregexp');

const getFileList = (dirName) => {
    let files = [];
    const items = readdirSync(dirName, {withFileTypes: true});

    for (const item of items) {
        if (item.isDirectory()) {
            files = [
                ...files,
                ...(getFileList(`${dirName}/${item.name}`)),
            ];
        } else {
            files.push(`${dirName}/${item.name}`);
        }
    }

    return files;
};


const cwd = path.join(__dirname, '../../');
// console.log('cwd :>> ', cwd);
var files = getFileList(cwd);


const paths = files
    .map((f, i) => f
        .replaceAll('../', '')
        .replaceAll('Pages/', '')
        .replaceAll(cwd, '')
    )

    .filter(file => !file.includes("Shared/")
        && !file.includes("/_")
        && !file.startsWith("_")
        && file.endsWith(".cshtml")
    );

const names = [...paths]
    .map((nm, i) => path.parse(nm).name
        .trim()
    )


//https://regex101.com/r/YWbgev/1
/**
 *
 * (?<breadcrumb>((?<parent>\w+\/)*?(\/)?)*)(?<pagename>\w+)
 *
 * /Sandbox/Index
 * /Pages/Admin/Login
 * /Search
 * /super/long/paths/suck/blagh
 *
 */


//
// const url = XRegExp(`^(?<scheme> [^:/?]+ ) ://   # aka protocol
//                       (?<host>   [^/?]+  )       # domain name/IP
//                       (?<path>   [^?]*   ) \\??  # optional path
//                       (?<query>  .*      )       # optional query`, 'x');

// const url = XRegExp(`^(?<scheme>[^:/?]+)(?<host>[^/?]+)(?<path>[^?]*\/)(?<query>.*)`, 'x');

//
// // Get the URL parts
// let parts = XRegExp.exec('https://google.com/Pages/Sandbox/Index?q=1', url);
//
// const breadcrumb = parts.groups.path;
// console.log('breadcrumb :>> ', breadcrumb);
// // parts=null;
// let breadcrumb_pattern = XRegExp(`^(
//     (?<breadcrumb>(
//         (?<parent>\w+\/)*?
//         (\/)?)*)
//         (?<pagename>\w+)
// )`, 'x');

const routes = paths.map(function (path, i) {

    return {
        url: path.replace('.cshtml', '')
        , text: names[i]
        , enabled: true
        , external: false
    };
});

const data = JSON.stringify(routes)


// console.log('cwd :>> ', cwd);
fs.writeFile(path.join(cwd, 'wwwroot', 'routes.json'), data, (err) => {
    if (err) throw err;
    console.log('Routes written to file');
});
