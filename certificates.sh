set -e

mkcert
echo "export ROOT_CERT=$(mkcert -CAROOT)/rootCA.pem" >> $HOME/.bashrc
source $HOME/.bashrc

[ ! -d "./.certificates" ] && mkdir ./.certificates
[ ! -f "./.certificates/localhost.pem" ] && cd ./.certificates && mkcert localhost
