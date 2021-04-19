The **Resourcehub Default Theme** is a custom theme, based on classy. 

We looked at a number of options before taking this approach, including govuk_theme, Material, Emulsify, Olivero and Agile Collective’s own base theme. The questions we were asking were
1. Is there something that could get us most of the way there? 
1. Do we want a pattern library or styleguide? 
1. Can we use something that is easy to extend, and relies on minimal tooling?

For the resourcehub_theme we are using the gov-frontend styling as a baseline for all form elements, and extending these with custom styles as per the [design](https://www.figma.com/proto/ZMzccH9ai1WsYvI6NntXCo/Prototype-March-2021?node-id=122%3A0&scaling=min-zoom)

We’re importing the [Govuk Frontend components](https://github.com/alphagov/govuk-frontend/tree/master/package/govuk/components) to cover the basic form styling etc. 

## Styleguide module ##
Our current approach is to use the [Styleguide module](https://www.drupal.org/project/styleguide) as the site styleguide, and build the site in such a way that it will work with either the resourcehub_theme or Olivero, which now ships with Drupal 9. 

All custom components we build will be brought into the styleguide.


## Target browser support

https://www.gov.uk/service-manual/technology/designing-for-different-browsers-and-devices

| Operating system | Browser | Support |
| ------ | ------ |------ |
| Windows | Internet Explorer 11 | compliant |
|  | Edge (latest versions)| compliant |
|  | Google Chrome (latest versions) | compliant |
|  | Mozilla Firefox (latest versions) | compliant |
| macOS | Safari 12 and later | compliant |
|  | Google Chrome (latest versions) | compliant |
|  | Mozilla Firefox (latest versions) | compliant |
| iOS | Safari for iOS 12.1 and later | compliant |
|  | Google Chrome (latest versions) | compliant |
| Android | Google Chrome (latest versions) | compliant |
|  | Samsung Internet (latest versions) | compliant |