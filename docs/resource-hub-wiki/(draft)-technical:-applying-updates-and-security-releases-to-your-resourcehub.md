Drupal releases updates to Drupal Core and contributed modules and themes on a monthly cycle. The releases are posted here: https://www.drupal.org/security and if you have a drupal.org account you can subscribe to email alerts. 

The ResourceHub distribution can be updated using composer.

The ResourceHub itself releases new features, which you can receive via composer too. 

run `composer update -W`

you should then update the database by running:
`drush updb`

 and clear the cache with:
`drush cr`