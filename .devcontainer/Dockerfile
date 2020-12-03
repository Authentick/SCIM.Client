FROM mcr.microsoft.com/dotnet/sdk:5.0

LABEL org.opencontainers.image.source https://github.com/GetGatekeeper/SCIM.Client

RUN apt-get update
RUN apt-get install -y zsh
RUN sh -c "$(curl -fsSL https://raw.githubusercontent.com/ohmyzsh/ohmyzsh/master/tools/install.sh)" "" --unattended

RUN cd /root/workspace/ && git clone https://github.com/AzureAD/SCIMReferenceCode.git && cd SCIMReferenceCode/ && git checkout ae181c1d0800a2a22333f0a4081c4012f39028e8
