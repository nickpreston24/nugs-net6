'use strict';

const fs = require('fs');
const { readdirSync } = require('fs');
const path=require('path');


const getFileList =  (dirName) => {
    let files = [];
    const items =  readdirSync(dirName, { withFileTypes: true });

    for (const item of items) {
        if (item.isDirectory()) {
            files = [
                ...files,
                ...( getFileList(`${dirName}/${item.name}`)),
            ];
        } else {
            files.push(`${dirName}/${item.name}`);
        }
    }

    return files;
};


const cwd = path.join(__dirname,'../../');
// console.log('cwd :>> ', cwd);
var files =  getFileList(cwd);


const paths = files
    .map((f,i)=> f
        .replaceAll('../','')
        .replaceAll('Pages/','')
        .replaceAll(cwd,'')
    )

    .filter(file => !file.includes("Shared/")
        && !file.includes("/_")    
        && !file.startsWith("_")
        && file.endsWith(".cshtml")    
    );

const names = [...paths]
    .map((nm,i)=> path.parse(nm).name
        .trim()
        )

const routes = paths.map(function(path, i) {
    return {path: path.replace('.cshtml',''), title: names[i]};
});

const data = JSON.stringify(routes)

console.log('cwd :>> ', cwd);
fs.writeFile(path.join(cwd, 'wwwroot', 'routes.json'), data, (err) => {
    if (err) throw err;
    console.log('Routes written to file');
});
