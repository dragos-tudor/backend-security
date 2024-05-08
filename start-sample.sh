set -e
trap 'kill 0' SIGINT;
trap 'kill 0' SIGCHLD;

SAMPLE_PATH=/workspaces/backend-security/Security.Sample
SAMPLE_API_PATH=$SAMPLE_PATH/backend-api
SAMPLE_APP_PATH=$SAMPLE_PATH/frontend-app

cd $SAMPLE_API_PATH && dotnet run --no-build --no-restore &
cd $SAMPLE_APP_PATH && dotnet run --no-build --no-restore &
wait -n
