set -e

WORKSPACE_DIR=.
PROJECTS=(
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

./building.sh Debug
for PROJECT in ${PROJECTS[@]}; do
  echo "testing project $PROJECT ..."
  cd $WORKSPACE_DIR/$PROJECT && dotnet run --no-build --no-restore -- --settings ../.runsettings && cd ..
done
