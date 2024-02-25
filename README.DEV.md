### Certificates
- generate certificates: `./certificates.sh # chmod 777 ./certificates.sh `.

### Css-in-js
- `.vscode/settings.json`
```json
{
  // appulate.filewatcher extension command
  "filewatcher.commands": [
    {
      "event": "onFileChange",
      "match": "\\.css",
      "vscodeTask": ["workbench.action.tasks.runTask", "css-in-js"],
    }
  ]
}
```
- `.vscode/tasks.json`
```json
{
  {
    "label": "css-in-js",
    "command": "deno",
    "type": "shell",
    "args": [
      "run",
      "--allow-read",
      "--allow-write",
      "https://gist.githubusercontent.com/dragos-tudor/1e687fd89fb416d0c0581cd03f9368d6/raw/8182b6efce4e020e91972a36ee86d06556bf2f67/css-in-js.js",
      "${file}"
    ],
    "options": {
      "cwd": "${workspaceFolder}/Security.Sample.www"
    },
    "presentation": {
      "reveal": "silent",
      "revealProblems": "onProblem",
      "close": true
    }
  }
}
```