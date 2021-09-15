The following steps were tested by one of our charity stakeholders.

Prerequisites?? https://docs.digitalocean.com/droplets/tutorials/recommended-setup/

Step 1: Create Ubuntu 20.04 Droplet 2Gb RAM

Step 2: SSH in to the Droplet https://docs.digitalocean.com/products/droplets/how-to/connect-with-ssh/
(or use the Console in the UI ) 

Step 3. Create a non-root user https://www.digitalocean.com/community/tutorials/initial-server-setup-with-ubuntu-20-04

Step 4. Configure firewall

Step 5. Upgrade PHP https://www.digitalocean.com/community/tutorials/how-to-install-php-7-4-and-set-up-a-local-development-environment-on-ubuntu-20-04

Step 6. Install some things we'll need
sudo apt install -y php7.4-cli  php7.4-mysql php7.4-zip php7.4-gd php7.4-mbstring php7.4-curl php7.4-xml zip unzip 

Step 7. Install LAMP stack https://www.digitalocean.com/community/tutorials/how-to-install-linux-apache-mysql-php-lamp-stack-on-ubuntu-20-04

Install Apache
Install MySQL

Step 7. Create database and database user:

mysql> CREATE DATABASE database;

mysql> CREATE USER ‘user’@’localhost’ IDENTIFIED BY ‘password’;

mysql> GRANT ALL privileges on database.* to ‘user’@’localhost’;


Step 9. Install Composer

https://www.digitalocean.com/community/tutorials/how-to-install-and-use-composer-on-ubuntu-20-04

Step 10. Create Resourcehub project in /var/www

composer create-project --stability dev --ignore-platform-reqs --remove-vcs openresources/resourcehub-project:^1.0 resourcehub;

todo (who should I be)

composer install

Create an symbolic link to the web folder

ln -s resourcehub/web/

Step 6. Create Settings.php (with DB settings) in /var/www/resourcehub/sites/default

cd /var/www/resourcehub/sites/default cp default.settings.php settings.php

Edit the settings.php and add in the database name , username and password from step 7

eg
```
$databases['default']['default'] = [
   'database' => 'example_database',
   'username' => 'example_user',
   'password' => 'password',
   'host' => 'localhost',
   'port' => '3306',
   'driver' => 'mysql',
   'prefix' => '',
   'collation' => 'utf8mb4_general_ci',
 ];
```

Step 7. Change permissions on /var/www/resourcehub to www-data

cd /var/www
sudo chown -R www-data:www-data resourcehub

Step 8. 

sudo systemctl reload apache2
sudo vi /etc/apache2/sites-available/resourcehub.conf

todo virtualhost settings

sudo a2ensite resourcehub


Step 9. Install drupal from webpage – select resourcehub

(May be fixed) Step 10. Finally disable Gin Toolbar ./bin/drush pmu gin_toolbar