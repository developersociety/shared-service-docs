# Open Referrals UI
This repo contains the source code for the Open Referrals UI project.

The Open Referrals tool is designed to support social prescribing.  It does this by providing functionality that: allows service managers
at voluntary sector organisations to publish information about their organisations and their services; and allows social prescribers from local
authorities and voluntary sector organisations to search for voluntary sector organisations and services in their local area.  The Open Referrals tool
has been built for re-use in differnt local areas, it has also been built using the Open Referrals Standard to drive stadardisation and interoperability.

## Features
 - Create Organsisations/Services
 - Associate with Organisations as a member or admin
 - Service search with Geosearch functionality
 - Hide vulnerable service location data

This Project requires the use of an Azure AD B2C tenant to store user credentials and 
and to do authentication.  

## Prerequisites / Dependencies
 - Azure B2C Tenant
 - A local or deployed instance of the OpenReferralAPI

## Source Code
This project uses BlazorServer and is written in C#

## Setup Azure Resources
- Create an OpenReferralUI app registration
- Create a client secret and copy it down
- Add the _user_impersonation permission from the OpenReferralAPI app registration (See [OpenReferralAPI](https://github.com/siccar/referrals-api))

## GoolgeMapsEmbedApi
 - Setup a google developer account
 - Create an api access key for a new project
 - Find the list of APIs and enable the embed api
 - Copy the api key into the appsettings

## Getting started Locally

### Local machine setup
- Clone the repository
- Open the sln file in VS 2019
- Create a local file named 'appsettings.Development.json'
- Copy the appsettings.json file contents into the file you just created.
- Add all the settings from the azure resources
- Make sure the OpenReferral API is running, see [OpenReferralAPI](https://github.com/siccar/referrals-api)
- Run

### Styling
- The app is styled using a stylesheet called SiccarCast.css.
- The css is built using SASS/SCSS.  Information about sass/scss can be found here (https://sass-lang.com/)
- In the css dircectory there is a file called SiccarCast.scss.  This file contains imports to various other sections that build the CSS file
- There is a partials directory that contains all the scss for the various parts of the site
- The colours, font-sizes, responsive utils reside within the main css folder.
- In the root of the project there is a file called compilerconfig.json.  This file was autogenrated in Visual Studio by a plugin.
- if using Visual Studio the plugin can be installed from here (https://marketplace.visualstudio.com/items?itemName=MadsKristensen.WebCompiler)
- Once installed, when changes to any of the .scss files are saved, then the minified css files are built.
- The SiccarCast.css and the SiccarCast.min.css reside in the wwwroot/css folder

## Deployment
This project can easily deployed using VS2019
 - Firstly follow the same steps in the [OpenReferralAPI](https://github.com/siccar/referrals-api)
 - Get the deployed address of the API from the Azure AppService and paste this into the ORApi:BaseUrl
 - Fill out the appsettings.json file with deployment settings
 - Right click on the project
 - Select Publish
 - Target Azure
 - Use windows app service
 - Target an existing app service instance or create a new one.
 - Skip the api management step
 - Save and publish

 This project also has azure-pipelines.yml setup for Azure DevOps.
 
 - Copy the repository into your own DevOps Repo.
 - Create a new pipeline called OpenReferralUI and target the azure-pipelines.yml
 - Create a release which targets the OpenReferralUi artifact and targets and app service.
 - If you don't want secrets in source code, inject the appsettings configuration at deployment time.
 - Setup triggers or manually create a build and then release to the app service.

## Feedback
User can leave feedback via a [Google Form](https://docs.google.com/forms/d/e/1FAIpQLSfw5D-YCGzu8SDMhkmxqzJSu1KJJx-hYaRuLnrnU_Um7ILyxw/viewform).
This link is also embedded in the app. 

[Issues](https://github.com/siccar/referrals-ui/issues) can also be created against the Github repository 
