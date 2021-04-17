[![pipeline status](https://gitlab.com/openresources/resourcehub_distribution/badges/1.x/pipeline.svg)](https://gitlab.com/openresources/resourcehub_distribution/-/commits/1.x)

# ![Resource Hub logo](resourcehub.png) Resource Hub

TODO: insert product description.

## Getting started

### System requirements

Resource Hub has been designed to be installed via [Composer](https://getcomposer.org/) and has baked-in intergration with Lando so the most straight-forward set up, and the one described below, will require the following to be installed on your system:

* [Composer 2](https://getcomposer.org/doc/00-intro.md)
* [Lando 3](https://docs.lando.dev/basics/installation.html)

### Local project set up

1. `composer create-project --stability dev --ignore-platform-reqs --no-install --remove-vcs openresources/resourcehub-project:^1.0 resourcehub`
2. `cd resourcehub`
3. `lando start`
4. `lando drush si resourcehub -y`

### Local project set up (developers)

If you are developing locally, you probably want to keep the .git version
control folders for ease.

1. `composer create-project --stability dev --ignore-platform-reqs --no-install openresources/resourcehub-project:^1.0 resourcehub`
2. `cd resourcehub`
3. `lando start`
4. `lando drush si resourcehub -y`

### Install Drupal locally

#### Install via the UI

1. Navigate to https://resourcehub.lndo.site
1. Choose 'Resource Hub' from the 'Choose profile' step
1. Complete the Drupal configuration step

#### Install via the command line

1. Move into the project directory
1. Run `lando drush si resourcehub`

### Installing demo content

If you're installing Resource Hub to test it out you can choose to have some demo
content automatically created via can be doing via the UI by checking the `Install demo content`
checkbox or by passing an additional argument to Drush:

`lando drush si resourcehub install_configure_form.install_demo=1`

If you've already installed Resource Hub then you can simply enable the `ResourceHub Demo Content`
module.

### Running tests

The following command will run the tests provided by the installation profile.

`lando phpunit`

### Code analysis

This project comes with PHPCS and PHPSTAN intergration which allows developers to check their code for Drupal style and standards compatibility.

`lando sniff` will check the installation profile for Drupal code style compatibility. Using `lando phix` you may be able to fix these automatically via PHPCBF`.

`lando stan` will check your code for Drupal standards compatibility including use of deprecated APIs.

### Frontend tooling

CSS tooling is based on [Laravel Mix](https://laravel-mix.com/docs/6.0/what-is-mix).

Watch for file changes in `themes/resourcehub_theme/scss` and browserSync run `lando npx mix watch`.
