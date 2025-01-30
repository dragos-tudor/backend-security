set -e

CONFIGURATION=${1:-Debug}
WORKSPACE_DIR=.
PROJECTS=(
  "Security.Testing"
  "Security.Authentication"
  "Security.Authorization"
  "Security.Authentication.BearerToken"
  "Security.Authentication.Cookies"
  "Security.Authentication.OAuth.Base"
  "Security.Authentication.OAuth"
  "Security.Authentication.OpenIdConnect"
  "Security.Authentication.Facebook"
  "Security.Authentication.Google"
  "Security.Authentication.Twitter"
  "Security.DataProtection"
)

dotnet restore
for PROJECT in ${PROJECTS[@]}; do
  echo "building project $PROJECT ..."
  cd $WORKSPACE_DIR/$PROJECT && dotnet build --no-restore --no-dependencies -c $CONFIGURATION && cd ..
done
