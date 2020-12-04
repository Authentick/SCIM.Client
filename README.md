# SCIM Client for .NET

This is a SCIM client library written in C#. SCIM is an open-standard for Cross-Domain Identity Management, and supported by [Gatekeeper](https://github.com/GetGatekeeper/server).

## Supported

This library implements not the whole SCIM standard. Instead it focuses on commonly used operations for provisioning users and groups on other systems.

- [Creating Resources using POST](https://tools.ietf.org/html/rfc7644#section-3.3)
- [Retrieving a Known Resource using GET](https://tools.ietf.org/html/rfc7644#section-3.4.1)
- [Modifying Resources using PUT](https://tools.ietf.org/html/rfc7644#section-3.5.1)
- [Deleting Resources using DELETE](https://tools.ietf.org/html/rfc7644#section-3.6)

## Get via NuGet

You can get this library via Nuget: https://www.nuget.org/packages/Gatekeeper.SCIM.Client

## Sample

For usage examples look into `Gatekeeper.SCIM.Client.Tests.Integration/ClientTest.cs` which uses the public API for integration testing against the [Azure AD SCIM Reference Code from Microsoft](https://github.com/AzureAD/SCIMReferenceCode/).
