# Open Referrals UI
This repo contains the source code for the Open Referrals UI project.

TODO: Add in project description

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
- Add the _user_impersonation permission from the OpenReferralAPI app registration (See OpenReferralAPI project)

## Getting started Locally

- Clone the repository
- Create a local file named 'appsettings.Development.json'
- Copy the appsettings.json file contents into the file you just created.
- Add all the settings
- Open the sln file in VS 2019
- Run

## Deployment
This project can easily deployed using VS2019

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
 - Setup triggers or manually create a build and then release to the app service.

## Feedback
User can leave feedback via a [Google Form](https://docs.google.com/forms/d/e/1FAIpQLSfw5D-YCGzu8SDMhkmxqzJSu1KJJx-hYaRuLnrnU_Um7ILyxw/viewform).
This link is also embedded in the app. 

Issues can also be created against the Github repository 
ToDo Add in link to Github repo
