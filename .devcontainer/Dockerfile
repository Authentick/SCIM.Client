FROM mcr.microsoft.com/dotnet/sdk:5.0

LABEL org.opencontainers.image.source https://github.com/GetGatekeeper/SCIM.Client

RUN apt-get update
RUN apt-get install -y zsh lsof
RUN sh -c "$(curl -fsSL https://raw.githubusercontent.com/ohmyzsh/ohmyzsh/master/tools/install.sh)" "" --unattended

RUN cd /tmp/ && wget https://packages.microsoft.com/config/debian/10/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
RUN dpkg -i /tmp/packages-microsoft-prod.deb
RUN apt-get install -y apt-transport-https
RUN apt-get update
RUN apt-get install -y dotnet-sdk-3.1
