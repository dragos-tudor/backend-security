set -e

WORKSPACE_DIR=/workspaces/backend-security/
PROJECTS=(
  "Security.Authentication"
  "Security.Authentication.BearerToken"
  "Security.Authentication.Cookies"
  "Security.Authentication.OAuth"
  "Security.Authentication.Remote"
  "Security.Authentication.Facebook"
  "Security.Authentication.Google"
  "Security.Authentication.Twitter"
  "Security.Authorization"
  "Security.DataProtection"
)

./building.sh Release
for PROJECT in ${PROJECTS[@]}; do
  echo "packing project $PROJECT ..."
  cd $WORKSPACE_DIR/$PROJECT && dotnet msbuild -t:Packing
done