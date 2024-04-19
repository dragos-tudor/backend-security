set -e
trap 'kill 0' SIGINT;
trap 'kill 0' SIGCHLD;

SAMPLE_PATH=/workspaces/backend-security/Security.Sample
SAMPLE_API_PATH=$SAMPLE_PATH.Api
SAMPLE_WWW_PATH=$SAMPLE_PATH.www

deno task start $SAMPLE_WWW_PATH &
dotnet run --no-restore --no-build --project $SAMPLE_API_PATH &
wait