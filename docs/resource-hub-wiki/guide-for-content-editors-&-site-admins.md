# Table of contents
Test

1. [Drupal Glossary](https://gitlab.com/openresources/resourcehub_distribution/-/wikis/Guide-for-content-editors-&-site-admins#drupal-glossary)
1. [An overview](#an-overview)
1. [The basics (logging in)](#the-basics)
1. [Managing your content](#managing-your-content)
    1. [Creating a resource](#creating-a-resource)
    1. [Creating a landing page](#creating-a-landing-page)
    1. [Creating a page](#creating-a-page)
1. [Managing your files and media](#managing-your-files-media)
1. [Managing taxonomy](#managing-taxonomy)
1. [Managing menus](#managing-menus)
1. [Managing users](#managing-users-on-your-site)
1. [Configuring and customising the site](#customisation)
    1. [Creating a custom homepage](#front-page)
    1. [Customising the appearance](#color-scheme)

# Drupal Glossary
| Term | Definition |
| ------ | ------ |
| Drupal | The content management system on which the Guidance Repository is based. The terms below are Drupal-specific terminology.|
| Entity | A container to store types of data e.g. user, content type, block.  |
| Content type | A type of entity for creating pages e.g.  page, resource, landing page etc. It defines default settings, such as the fields, metatags, and URL path pattern. |
| Node | Each item of content is called a node, and each node is one instance of a content type. |
| Taxonomy | Taxonomy is a type of entity that  allows you to connect, relate and classify your website’s content. A taxonomy "vocabulary" e.g Topic  contains “terms” e.g. Health, Wellbeing, Digital literacy. You can add taxonomy terms to an individual resource. |
| Field | Fields contain data: text, number, date, media etc. On a node edit form, they appear in a variety of widgets such as text boxes, select lists, autocomplete search, etc. |
| Users and Roles | Anyone who visits your site is a user and falls into one of 2 types of role: Anonymous  or Authenticated. Custom roles define the permissions that dictate what that role can and can’t do. For example, the content editor has the permission to create and delete content on the site, whereas an anonymous user does not. |
| Theme | The theme is the look of your website: colours, fonts, page layouts etc. In other words, it is the implementation of a design on a web site. |

# An overview
The Guide for Content Editors and Site Admins provides all the information you will need to create, edit and manage everything from the site content, to users, to taxonomy. 

# The basics
## How do I log in? 

1. Go to [www.yoursite.com]/user
1. Enter username and password
1. If you have forgotten your password click “Request new password” and enter your username or email address 


## How do I log out? 

When you are on an administration page you should see your username in the top right. If you hover over this you will access the User menu including the *Log out* button.

![screenshot showing submenu, username "sitemanager" is focused with menu items "View profile, Edit profile and Log out" visible](img/logout.png)

## What can I do? 
Each user has a set of permissions based on the role they have been assigned.
We have set up two custom roles: Site admin, Content editors

These instructions cover all areas, but you may not have access to all this functionality. 

## Navigating the administration

Once you have logged in to the site you will be able to see the Admin Menu at the top of the browser window. 

![screenshot showing admin menu at the top of the page. Menu items are Content, Resource Hub, Structure, Configuration, People and Help ](uploads/0dfa94fdc11696824b40086450592bc2/image.png)

 
| Menu item | Description |
| ------ | ------ |
| Content | Managing content of your site. |
| Customisation | Configuring your site information and customisation. |
| Structure | Manage menus and taxonomies. |
| People | Managing existing users on your site and to create new users. |
| Configuration | Configuration of various site settings, usually only for web developers. |

# Managing your content 
## Editing existing content

There are two options
1. Edit from the page
1. Find the page via the content overview

### Edit from the page

![screenshot of the About this site page, showing the top of the page with the tabs View, Edit, Delete and Revisions showing](img/edit-tabs.png)

1. Navigate to the page that you wish to edit.
1. You will see 4 tabs under the heading ‘View’, ‘Edit’, ‘Delete’ and ‘Revisions’.
1. Select the ‘Edit’ tab. If you do not see this tab it is because either you are not logged in or you do not have permission to edit this page.
1. Make the changes you need to make.
1. You can check the changes you have made using the 'Preview' button.
1. Once you are happy with the change, click 'Save'.
1. Check that the page is displaying as you want it to. If you need to make any further changes, edit the page again.

### Find the page by searching 

![screenshot of the Content admin page, showing the top of the page with the tabs Content, Files and Media showing, with Content active. Below search and filters for Title, Content type, Published and Language. Below this a table showing the results - each row shows the item Title, Content type, Author, Status, Updated date, and Edit ](img/content-overview.png)

1. Go to the “Content” overview via the Admin Menu.
1. Use the search or filter options to find the page - click the edit button beside this item.
1. Follow steps 4-7 above.

## Creating new content

![Screenshot of the top of a Create Landing page page, showing the title Create Landing page, two buttons Preview and Save and an empty field labelled Title* ](img/create-page.png)

1. Go to Content >> Add Content >> (select the type of content you would like to create).
1. You will then see the new content form with a number of  fields that you can fill in to create your new content. 
1. Any field marked with an asterisk (*) is mandatory, you must fill this in in order to save the Content.
1. Once you have finished editing the page, click 'Save'.

## Different types of field you will encounter
There are various types of field that we have set up for you, outlined below are some of the more common ones.

| Field type | Description |
| ------ | ------ |
| Title | This is the page heading that appears above the content.This is also used for your Meta-title and in the URL. |
| Media | An image, file or multimedia item. |
| Reference fields | Creates an internal link to another page in your site. They work by referencing the title of the content you wish to link to. Usually displayed as an autocomplete search widget. |
| Rich-text Fields | Contains a text editor (CKEditor) with a number of formatting options (see below). |
| Paragraphs / Content sections | The paragraph module allows you create predefined “bundles” of fields which enables a content editor to create a more flexible page layout with consistent reusable components. Some examples are a video embed, image, audio embed with transcript. |

### Formatting options
![Screenshot of the CKeditor toolbar showing a field labelled Text* and the button icons and text. Format, B, I, link, remove link, unordered list, ordered list, indent left, indent right, blockquote, table, remove format, expand, view source, accessibility checker  ](img/wysiwyg.png)

The CKEditor toolbar buttons are illustrated with meaningful icons. If you are not sure what functions they perform, hover the mouse cursor over the buttons to see a tooltip with the name of the function.

### Adding media
To add media to a node, when creating or editing content, click add media and select either:

1. Add file
2. Search existing files
3. Select an item from the list and click 'insert selected'

![A screenshot to Add or select media displaying a Browse button to select an item from your device and a search field to search existing files](uploads/77726bc2f7fc6e2e23dcc98c1914e8d0/image.png)

#### Adding images
1. Click ‘Browse’ and select an item from your device to upload
1. Name the item
1. Add alternative text - a brief description of what the image is showing
1. Crop the item if needed
1. Once you are finished, click save
1. The image will appear in uploaded media and be automatically selected. 
1. To add the image to the node, click ‘insert selected’. 

![A screenshot displaying an uploaded image with a title and alternative text](uploads/6b0b3af275c66e5a1b01f20ca6519faf/image.png)

#### Cropping images
If an image is too large or you want it to focus on a particular area, you can crop the image when uploading.

1. Select the ‘Crop image’ dropdown.
1. Select ‘main image’ (how it appears on a page or when downloaded) or ‘thumbnail crop’ (how it appears in  search results and lists)
1. The image will appear with a blue box around the visible area
1. Click on the sides and the corners to resize this - anything within the blue box will be visible, while cropped content will be greyed out. 

![A screenshot displaying an a cropped image of a map](uploads/56402c9ac6dcf8c28f46ec4081e3ab23/image.png)

#### Adding audio and video
Adding audio and video content requires the content to be uploaded and available through a provider such as SoundCloud, Spotify, Vimeo or YouTube.

1. Select 'add audio' or 'add video'
1. Add a title, caption and transcript if available
1. Select 'add media'
1. Paste the URL of the media item and click 'add'
1. Rename the item if you wish and then 'Save'
1. The item will be automatically selected in the list below, click 'insert selected'

![A screenshot displaying the Add Remote video via URL and search functions for adding media](uploads/b8fe37d41cb6e59a381532f9af19bdaa/image.png)

### Creating a link to internal links
On the text editor, select the link icon. A popup will appear:

![Screenshot of the popup. the title Add link, with an empty field labelled URL with the help text Start typing to find content. The field has a circle icon inside. A save button is at the bottom of the popup ](img/link-field.png)

#### Internal links and files
![Screenshot of the same popup with the letters acc entered in to the field. A list of content with titles that contain these letters is visable. ](img/link-field-autocomplete.png)

In the URL field you can type the title of the page or file you want to link to. Once you start typing,  the widget will offer suggestions from which you can choose. 

#### External links 
To add an external link (ie a link to an external website), simply enter the URL in the URL field and then click Save.

#### Accessibility checker
Once you have finished adding text to the text field, you can check for some common accessibility issues by clicking the a11y checker button. Find out more about how to write accessible content here. /// todo 

## Page settings

When you are editing content, on the right of the page you will see a variety of configuration options, depending on your permissions. You will usually be able to see the following:

![Screenshot of the page settings, description in the table below](img/page-settings.png)

| Setting type | Description |
| ------ | ------ |
| Authoring information | Shows who created the node and when. |
| Menu Settings | Edit this to put the node in the menu (if available). Toggle “Provide a menu link”. The ‘Parent link’ defines the item above the current page in the menu hierarchy. |
| URL alias  | This is the human readable URL of the page, by default generated by the page title. If you want to customise this you can toggle 'Generate automatic URL alias' and write your own. |
| XML sitemap | todo |
| Metatags | todo |

## Creating a resource
To create or edit a resource:
1. Go to 'Content >> Add Content'
1. Select 'resource' 
1. Add the required content (see the fields in the table below)
1. Preview your changes
1. Save the content

| Field name | Description | 
| ------ | ------ |
| Title | This is the page heading that appears above the content.This is also used for your Meta-title and in the URL. This is a required field. |
| Featured Image | This image will appear below the page title and above the Summary | 
| Summary | A text field intended for short description of the content. This appears on the page as well as in lists and search results. |
| Who is this for? | A  brief description of the intended audience for this content and how it will help them. |
| Primary resource | This is the main resource for this page (can include audio, documents, images, links, text or video). This is a required field. | 
| Additional resource content | Additional resources can be added where necessary. |
| Tags / Taxonomy | Tag your content with terms below to create relationships with other content. Users will also be able to find the content via the facets on the Resources page. |
| Related content | Create direct links to resources on the site or add external links to relevant content. |

![A screenshot displaying the primary resource menu on a resource page with Add audio, Add Document, Add Image, Add External Link, Add Text and Add Video buttons.](uploads/2501a2969d2f5bcd6069c792ba9fc432/image.png)

Tag the content with the relevant terms from within the Categories section in the sidebar.
![image](uploads/d9e8694e46324a422a043c5a21959d18/image.png)

---

## Creating a landing page
To create or edit a landing page:
1. Go to 'Content >> Add Content'
1. Select 'landing page' 
1. Add the required content (see the fields in the table below)
1. Preview your changes
1. Save the content

Within a landing page, you will be able to add the following:

| Field name | Description | 
| ------ | ------ |
| Title | This is the page heading that appears above the content.This is also used for your Meta-title and in the URL. This is a required field. |
| Introduction | A brief description for the page and intended audience or purpose. |
| Main image | This image will appear below the page title and introduction. | 
| Link blocks| Link blocks allows you to add one or more blocks of links grouped by a title. You may have multiple link blocks on a page |
| link group title | This will appear above the links. You may have one title per block |
| links | You have the choice between internal links ans external links. see below for more details. |

![A screenshot of a link block section with a text field for title and Add External Link and Add Internal Link buttons](uploads/85ac0953cfe0dc5a81209b995357e077/image.png)

### Creating a link block
You can curate sets of links, grouped by a title using Link blocks. 

Each link appears with some descriptive text. In the case of internal links, this text is pulled from the summary field. 
In the case of external links you can manually enter this in the *Link summary* field.

Example:

![A screenshot displaying External link field with text input in to each field.](uploads/9f53e03b3ed8bd00b2fa4ae7545c4029/image.png)

The above field displays like this: 

![A screenshot of the frontend of the site displaying the fields above.](uploads/603581dbb9471ec3d089606c79259a00/image.png)


### Tip
You can link directly to a prefiltered search by using the External link field. Just get the URL of the filtered view (eg /resources/?f[0]=audience:5 ) and add that in the URL field. 

![External link field with the example text in the URL field, and the link text "Everything for content designers"](uploads/575cd443ef003e89cb0ad519e415fb17/image.png)


## Creating a page
To create or edit a page:
1. Go to 'Content >> Add Content'
1. Select 'page' 
1. Add the required content (see the fields in the table below)
1. Preview your changes
1. Save the content

| Field name | Description | 
| ------ | ------ |
| Title | This is the page heading that appears above the content.This is also used for your Meta-title and in the URL. This is a required field. |
| Summary | A brief description for the page and intended audience or purpose. This is also used for lists and search results. |
| Text | This will appear below the page title and summary. | 


# Managing your files & media
This lives in Content >> Files or Content >> Media.
All your files, images and videos can be found in one place under Content >> Media in the Admin Menu.
You can edit and delete the files from here (if you have the appropriate permissions).
Drupal will warn you if the file you wish to delete is in use, so read the messages carefully! If it is in use it is best to either:
- remove the image from the content and then remove the image
- remove the content with the image in it and then remove the image itself

# Managing taxonomy
All the vocabularies used on the site are at Structure >> Taxonomy. 
This page will show a table listing all your vocabularies. The name of vocabulary is on the left with the possible operations on the right.
- To see a list of the terms within a vocabulary,  click on “List terms”. 
- To add a new term , click “Add term”.
- To edit an existing term (ie to change its name), click on “List terms” and then “Edit”.
- To see all content tagged with a term, click on “List terms” and then click on the term name. 

## Resource Hub taxonomies 

The resource hub comes with four predefined taxonomy vocabularies, which are used to classify resources. The vocabularies are not designed to be editable, but the terms within them are. 
We expect the terms to be unique to your organisation, and how you categorise your content. However there are some examples below:

| Vocabulary name | Purpose | Example terms |
| ----- | ----- | ----- |
| Audience | Who is the resource aimed at? | Teachers, Social workers, etc |
| Topics | What (broadly) is the resource about? | Health, Wellbeing, etc |
| Resource type | What kind of resource is this? | Guidance, Legal advice, etc |
| Format | What format is this resource ? | Video, Audio, etc |

# Managing menus

When creating a piece of content you have the option to place the content in the menu structure or you can access the menu structure through  Structure >> Menus

| Setting | Description |
| ------ | ------ |
| Menu Link Title | Title which appears in the Menu e.g. “About” |
| Parent item  | The parent item to which the new item belongs.  |
| Weight | Defines the order in which items are displayed at the same level in the hierarchy. An item with a weight of 1 will display above or before one with a weight of 10. |

## How can I reorder menu items?
If you need to re-organise the menu structure, or the position of a node within the menu structure you can do so from Structure >> Menu.
You will see a list of all the available menus. 

- To the right of the menu there is an option to ‘Edit Menu’. Click this.
- You will see the whole menu hierarchy laid out. Items indented to the right are children of the item above and left of it. 

You can reorder menu items using the cross to the left of the link title, however, we advise that you do this with care.

![Screenshot of the menu overview, with the drag and drop handles visible. The second item is indented right](img/menu-drag.png)

If you find the drag and drop handles difficult to use you can click the “show row weights” link and this will allow you to enter numbers determining the order. Lower numbers / weights will come before higher numbers / weights within each level.

![Screenshot of the same menu items, but with the numerical weight showing instead of the drag handles](img/menu-weights.png)

# Managing users on your site

## How can I edit my own Account?
1. Click on your Username in the Admin menu.
1. Click ‘Edit profile’.
1. Here you can update your password or change your username.

## How do I create a user?
You may wish to give someone some responsibility for managing  the content of your site. We will have allocated some roles within your site, and one of these is ‘Content editor’. This role has the permission to create, edit and delete content on the site, along with other associated permissions. 

1. Go to ‘People’ - this will give you a list of all the people registered on the site, and their associated roles.
1. Click Add User.
1. Fill out the Name, Email, Password fields.
1. Set ‘Status’ to active.
1. Assign the desired role to the user.
1. Check the ‘Notify user of new account’ box to send the user an email about their new account on the site.
1. The new user is sent a one-time log-in link and asked to reset their password.

## How do I block / cancel a user’s account?
![Screenshot of the people overview, showing the users listed in a table with one user "siteadmin" selected and the Action list showing with the option "block the selected user" selected](img/people-block.png)

1. On the ‘People’ page, search for the user by using the filter options.
1. Check the box next the user you wish to delete.
1. Above the table of users, click on the Action dropdown and select one of the following options:
- Block the selected user(s)
- Cancel the selected user account(s)

## Resource Hub Roles 

The resource hub comes with two custom roles predefined. These are broadly: 
| Role | Permission | Notes |
| ----- | ----- | ----- |
| Site admin | Can manage users | Can manage site configuration	|
| Give to trusted users only | Content editor | Can create, edit & delete content, taxonomy terms, and menu items |

---

# Customisation

## Settings

A site administrator has access to the Customisation settings page, from the Admin menu. 
### Site details

| Field | Description |
| ------ | ------ |
| Site name | This is the name of the site and is important for a number of reasons, such as SEO, you can manage how it is displayed on the Theme tab |
| Slogan | This field is not configured to appear at present |
| Email address | The From address in automated emails sent during registration and new password requests, and other notifications. (Use an address ending in your site's domain to help prevent this email being flagged as spam.) |


### Front page

If you would like to show a custom page as the homepage, you can define that here. Add the URL of the page you would like to show. 

If the page is a Landing page, some custom styling will be applied, and the search bar will also appear on the page. 

![image](uploads/b269e330c4b43616209b4be918ab3363/image.png)

### Error pages

If you want to create bespoke 403 and 404 pages you can do so here. 

## Blocks

### Legal copy
The legal copy is an optional block of text which will appear in the footer of the site, on each page. We imagined you might want to put your organisations legal details, or similar. 
### Back to site link
The Back to site link was designed to point to another web property, such as the organisation’s main website. If filled in, it will appear in the header of the site. 

## 
### Color scheme
The default colour set is the Resource Hub default. To add an accent colour to the site you can either pick a colour from the colour wheel, or enter a custom colour in HEX format. 

You can pick 2 accent colours to match the site to your brand.

| Colour | Where it appears |
| ------ | ------ |
| Accent | Accent appears as a border on the header and footer and as a background colour on certain elements |
| Accent 2 | Accent 2 appears as an alternate background colour in the list of resources |
| Header/Footer background | The background colour of the header and the footer |
| Header/footer text | The text in the header and footer. Beware of colour contrast with the background colour. |

![image](uploads/bdb05d09d4f1ec7f1983116497a8ce46/image.png)

### Logo
To change the logo on the site, toggle the “Use the logo supplied by the theme” and Upload an image file or enter the path to the image on the server. 
### Favicon
A favicon appears in your browser tab, and lets people more easily spot your website if they have many tabs open. 
Upload a custom favicon here.