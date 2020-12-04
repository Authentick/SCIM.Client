# Contributing to the SCIM Client LIbrary

We generally appreciate any contributions to the SCIM Client Library. Please note, that whilst this aims to be a general-purpose library, this library was ultimately created to implement a SCIM client in the [Gatekeeper application](https://github.com/GetGatekeeper/Server).

## Setup Dev Environment

The repository includes a dev container that ships all the required dependencies to set this up. Either use [GitHub Codespaces](https://github.com/codespaces) or [Visual Studio Code Remote Containers](https://code.visualstudio.com/docs/remote/containers#_quick-start-open-a-git-repository-or-github-pr-in-an-isolated-container-volume).

## Folder structures

### Code base

The code base is located in `Gatekeeper.SCIM.Client`. To run it run `dotnet run` inside this folder.

### Unit tests

The unit tests are located in `Gatekeeper.SCIM.Client.Tests.Unit`. To run it run `dotnet test` inside this folder.

### Integration tests

The integration tests are located in `Gatekeeper.SCIM.Client.Tests.Integration`. To run it run `dotnet test` inside this folder.
