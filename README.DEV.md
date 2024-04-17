### Generate and install host certificates
- install *mkcert* utility: `sudo apt install -y mkcert`
- generate CA certificate and install in the system trust store and browsers: `mkcert --install`.
  - *mkcert* install generated CA certificate into Chromium.
  - *mkcert* don't install generated CA certificate into Firefox. Generated CA certificate [taken from `mkcert --CAROOT`] should by manually installed into Firefox.
- generate localhost certificates [for testing]:
```sh
cd .../backend-security/.certificates
mkcert localhost
```

### Using certificates
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
    "Google": {
      "ClientId": "<google id>",
      "ClientSecret": "<google secret>"
    },
    "Facebook": {
      "AppId": "<facebook id>",
      "AppSecret": "<facebook secret>"
    },
    "Twitter": {
      "ConsumerKey": "<twitter id>",
      "ConsumerSecret": "<twitter secret>"
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