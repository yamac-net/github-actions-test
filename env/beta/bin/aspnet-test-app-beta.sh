SERVICE=aspnet-test-app-beta
PROJECT_ID=yamac-net
REPOSITORY=aspnet-test/app
SYSTEMD_ENV_FILE=/home/web/etc/systemd/aspnet-test-app-beta.env
OP=$1
TAG=$2

if [ -z $TAG ]; then
    echo "No tag"
    exit -1
fi

echo "$HOSTNAME: $SERVICE setting tag '$TAG'"
sed -i -E 's|^(APP_IMAGE=).+|\1ghcr.io/'$PROJECT_ID'/'$REPOSITORY':'$TAG'|' $SYSTEMD_ENV_FILE
echo "$HOSTNAME: $SERVICE set tag '$TAG'"

echo "$HOSTNAME: $SERVICE restarting..."
systemctl --user restart $SERVICE
echo "$HOSTNAME: $SERVICE restarted"
