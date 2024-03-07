### Certificates
- generate certificates:
```sh
  chmod 755 ./certificates.sh
  ./certificates.sh
```

### Certificates
- using `Security.Sample.Api/settings.json`.
- using environment variables.
```sh
export Kestrel__Endpoints__Https__Url=https://*:80443
...
```
- using command line args.
```sh
dotnet run --no-restore --no-build --Kestrel:Endpoints:Https:Url="https://*:80443" ...
```


### Secrets
- using `Security.Sample.Api/secrets.json`.
```json
{
  "Secrets": {
    "google": {
      "clientid": "<google id>",
      "clientsecret": "<google secret>"
    },
    "facebook": {
      "appid": "<facebook id>",
      "appsecret": "<facebook secret>"
    },
    "twitter": {
      "consumerkey": "<twitter id>",
      "consumersecret": "<twitter secret>"
    }
  }
}
```
- using dotnet user-secrets tool.
```sh
cd Security.Sample.Api
dotnet user-secrets init
dotnet user-secrets set "Secrets:google:clientid" "google_id"
dotnet user-secrets set "Secrets:google:clientsecret" "google_secret"
```
- using environment variables.
```sh
export Secrets__google__clientid=google_id
export Secrets__google__clientsecret=google_secret
...
```
- using command line args.
```sh
dotnet run --no-restore --no-build --Secrets:google:clientid=google_id ...
```


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