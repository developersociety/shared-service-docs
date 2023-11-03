The basic strategy for a Drupal 9 to 10 upgrade is to prepare your Drupal 9 site for the upgrade and then doing the Drupal 10 upgrade.

If you have customised your Resource Hub site by adding extra modules or code changes then you will also need to check the changes are compatible with Drupal 10. The [Upgrade Status](https://www.drupal.org/project/upgrade_status) module will help determine what needs to be done before you can upgrade to Drupal 10.

## 1. Ensure you're using the latest version of Drupal 9 and Resource Hub.

The first step is to update Drupal and any contrib modules to the latest versions.

To this with Composer and Drush by running:

```sh
composer update drupal/* -W
drush cr
drush updb
drush cr
druch cex
```

Commit all changes if the site code and config is managed through git.

### 2. Ensure you're using PHP 8.1

Drupal 10 requires PHP 8.1. Having updated Drupal and contrib modules the site should now support this so upgrade the environment you're using to upgrade the site to PHP 8.1.

## 3. Preparing your Drupal 9 site.

Uninstall Quick Edit as this is no longer required and removed from Drupal 10.

```sh
drush pmu quickedit
```

CKEditor 4 is removed in Drupal 10 in preference for CKEditor 5. Resource Hub sites currently use 2 CKEditor plugins that are not supported by CKEditor 5 and so these have to be uninstalled prior to being removed later on. It's also necessary to enable the ckeditor5 module.

```sh
drush pmu ckeditor_a11ychecker ckeditor_balloonpanel
drush en ckeditor5
drush cr
```

You now need to migrate the 'Filtered text' and 'Simple' text formats to use CKEditor 5. For more information see <https://www.drupal.org/node/3223395#s-how-to-upgrade-fromckeditor-4-to-ckeditor-5>

Once that's done disable the ckeditor module and export config.

```sh
drush pmu ckeditor
drush cr
drush cex
```

## 4. Upgrade to the latest Resource Hub version.

The last step in getting your site ready for the Drupal 10 upgrade is to update all the Resource Hub components.

```sh
composer update openresources/resourcehub* -W
drush cr
```