To deploy the system to a hosting environment, two main approaches are posisble.

This page descibes the monorepo approach.

## 1 Monorepo approach.

For most small charities, we recommend following this 'monorepo' approach. 

This involves installing the code locally using composer to gather all code and dependencies, committing this code all to a git repository and using git to pull all the files to the hosting environment.

## 1.1 Install locally

Install the codebase locally

```
composer create-project --stability dev --ignore-platform-reqs --remove-vcs openresources/resourcehub-project:^1.0 resourcehub;
cd resourcehub;
```

## 1.2 Add code to a git repositry 

Initialise a new github repository

```
git init
```

Assuming you have a private Gitlab repository at git@gitlab.com:organisation/my-resource-hub.git, add your remote with

```
git remote add origin git@gitlab.com:organisation/my-resource-hub.git
```

Remove the .gitignore

```
rm .gitignore
git add .
git commit -am 'Initial commit of resourcehub codebase.'
git push origin master
```

## 1.2 Deploy code to the live server.

SSH into your server.

Clone the codebase to the hosting root.

Make sure the web root is pointing at the web folder. 

## 1.3 Configure the database

@todo - describe settings.php

## Install the resource hub Drupal site.

Run the installation.




