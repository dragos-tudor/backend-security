set -e
trap 'kill 0' SIGINT;
trap 'kill 0' SIGCHLD;

SAMPLE_PATH=/workspaces/backend-security/Security.Sample
SAMPLE_API_PATH=$SAMPLE_PATH/backend-api
SAMPLE_WWW_PATH=$SAMPLE_PATH

deno task start $SAMPLE_WWW_PATH &
cd $SAMPLE_API_PATH && dotnet run --no-build --no-restore &
wait -n
