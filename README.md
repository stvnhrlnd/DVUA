# Damn Vulnerable Umbraco Application 💔

A Completely Heartbroken™ web application built on Umbraco CMS (named after
[DVWA](https://github.com/digininja/DVWA), but in no way affiliated).

The goal of DVUA is to help web developers get familiar with some of the most
common web security vulnerabilities. It was built for the
[Web Hacking 101](https://github.com/stvnhrlnd/web-hacking-101)
workshop at Codegarden 2025.

## ⚠️ Warning/Disclaimer

This application is **damn vulnerable**! Do not deploy it to any Internet-facing
servers or they could be easily compromised.

I take no responsibility for any damages incurred by running this application.

## Requirements

To run DVUA you will first need to install the
[.NET 9.0](https://dotnet.microsoft.com/en-us/download/dotnet/9.0) SDK.

## Usage

Run the application as follows (preferably in a virtual machine or container):

```shell
dotnet run --project src/DVUA.Site/
```

If anyone wants to containerise the application for me I'd be super grateful!
