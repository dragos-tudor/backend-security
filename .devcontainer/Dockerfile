ARG BASE_IMAGE=ubuntu:22.04
FROM --platform=linux/amd64 ${BASE_IMAGE}

ARG PACKAGES="ca-certificates curl git libicu70 mkcert nano tar unzip"
RUN apt update && \
    apt -y install --no-install-recommends --no-install-suggests ${PACKAGES} && \
    apt -y autoremove && \
    apt -y clean && \
    rm -rf /var/lib/apt/lists/*

ARG USER=vscode
ARG GROUP=vscode
ARG USER_HOME="/home/${USER}"
RUN groupadd -g 1000 ${GROUP} && \
    useradd -m -d ${USER_HOME} -s /bin/bash -g 1000 -u 1000 ${USER}

USER ${USER}

ARG DOTNET_PATH=${USER_HOME}/.dotnet
ARG DOTNET_SDK=/5226a5fa-8c0b-474f-b79a-8984ad7c5beb/3113ccbf789c9fd29972835f0f334b7a/dotnet-sdk-8.0.100-linux-x64.tar.gz
RUN curl -fsSL https://download.visualstudio.microsoft.com/download/pr${DOTNET_SDK} -o ${USER_HOME}/dotnet.tar.gz && \
		mkdir -p ${DOTNET_PATH} && \
		tar -xzf ${USER_HOME}/dotnet.tar.gz -C ${DOTNET_PATH} && \
    chmod +x ${DOTNET_PATH}/dotnet && \
    rm ${USER_HOME}/dotnet.tar.gz

ARG NUGET_PATH=${USER_HOME}/.nuget
RUN mkdir ${NUGET_PATH}

RUN mkcert

ENV DOTNET_ROOT=${DOTNET_PATH} \
    DOTNET_GENERATE_ASPNET_CERTIFICATE=false \
    DOTNET_USE_POLLING_FILE_WATCHER=true \
    DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false \
    DOTNET_CLI_TELEMETRY_OPTOUT=1 \
    DOTNET_NOLOGO=true \
    NUGET_XMLDOC_MODE=skip

ENV PATH="${PATH}:${DOTNET_ROOT}:${DOTNET_ROOT}/tools" \
    ROOT_CERT="$(mkcert -CAROOT)/rootCA.pem"

