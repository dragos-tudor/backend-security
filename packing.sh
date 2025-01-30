set -e

WORKSPACE_DIR=.
PROJECTS=(
  "Security.Authentication"
  "Security.Authentication.BearerToken"
  "Security.Authentication.Cookies"
  "Security.Authentication.OAuth.Base"
  "Security.Authentication.OAuth"
  "Security.Authentication.OpenIdConnect"
  "Security.Authentication.Facebook"
  "Security.Authentication.Google"
  "Security.Authentication.Twitter"
  "Security.Authorization"
  "Security.DataProtection"
)

./building.sh Release
for PROJECT in ${PROJECTS[@]}; do
  echo "packing project $PROJECT ..."
  cd $WORKSPACE_DIR/$PROJECT && dotnet msbuild -t:Packing && cd ..
done
