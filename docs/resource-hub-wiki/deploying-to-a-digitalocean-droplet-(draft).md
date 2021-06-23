The following steps were tested by one of our charity stakeholders. 

Prerequisites??
https://docs.digitalocean.com/droplets/tutorials/recommended-setup/


**Step 1: Create Ubuntu 20.04 Droplet 2Gb RAM**

**Step 2: SSH in to the Droplet**
https://docs.digitalocean.com/products/droplets/how-to/connect-with-ssh/

Step 3. Create a non-root user
https://www.digitalocean.com/community/tutorials/initial-server-setup-with-ubuntu-20-04

Step 4. Configure firewall

**?? Step 2. Patch**

**Step 3. Install LEMP stack** (<https://www.digitalocean.com/community/tutorials/how-to-install-linux-nginx-mysql-php-lemp-stack-on-ubuntu-20-04>)

1. Install NGINX
3. Install MySQL
4. Run SQL security install
5. Create database and database user:

`mysql> CREATE DATABASE database;`

`mysql> CREATE USER ‘user’@’localhost’ IDENTIFIED BY ‘password’;`

`mysql> GRANT ALL privileges on database.* to ‘user’@’localhost’;`

1. Install PHP 7.4
https://www.digitalocean.com/community/tutorials/how-to-install-php-7-4-and-set-up-a-local-development-environment-on-ubuntu-18-04
2. Install PHP-CLI, PHP-GD, PHP-CURL, PHP-XML, PHP-MBSTRING

\
**Step 4. Install Composer**

<https://www.digitalocean.com/community/tutorials/how-to-install-and-use-composer-on-ubuntu-20-04>

**Step 5. Create Resourcehub project in /var/www**

`composer create-project --stability dev --ignore-platform-reqs --remove-vcs openresources/resourcehub-project:^1.0 resourcehub;`

Edit: I think we need to run the site install here? 

**Step 6. Create Settings.php** (with DB settings) in /var/www/resourcehub/sites/default

**Step 7. Change permissions** on /var/www/resourcehub to www-data

**Step 8. Change sites enabled to resourcehub/web**

**Step 9. Install drupal from webpage – select resourcehub**

(May be fixed) Step 10. Finally disable Gin Toolbar ./bin/drush pmu gin_toolbar